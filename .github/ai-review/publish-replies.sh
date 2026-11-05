#!/usr/bin/env bash
# publish-replies.sh <pr> <replies.json> <threads.json>
# Posts the implementer's replies, each to the first comment of its thread.
# Exits 2 on invalid replies.json, before anything is posted.
set -euo pipefail
# shellcheck source=lib.sh
. "$(dirname "${BASH_SOURCE[0]}")/lib.sh"

pr=$1 replies=$2 threads=$3
repo=$GITHUB_REPOSITORY

jq -e '.replies | type == "array" and all(.[]; (.comment_id | type == "number") and (.body | type == "string"))' \
  <"$replies" >/dev/null 2>&1 || { echo "error: invalid replies JSON" >&2; exit 2; }

jq -c '.replies[]' <"$replies" | while read -r reply; do
  id=$(jq -r .comment_id <<<"$reply")
  root=$(thread_root_id "$threads" "$id")
  if [[ -z $root ]]; then
    warn "dropping reply to comment $id: not in a known thread"
    continue
  fi
  jq "$AI_REVIEW_JQ_DEFS"'{body: (.body | san)}' <<<"$reply" \
    | gh_write api -X POST "repos/$repo/pulls/$pr/comments/$root/replies" --input -
done
