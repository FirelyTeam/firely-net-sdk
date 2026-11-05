#!/usr/bin/env bash
# publish-review.sh <pr> <review.json> <diff.patch> <head_sha> <round> <total> <threads.json>
# Posts the reviewer's review (event COMMENT), then its replies on threads the loop started.
# Exits 2 on invalid review.json, before anything is posted.
set -euo pipefail
# shellcheck source=lib.sh
. "$(dirname "${BASH_SOURCE[0]}")/lib.sh"

pr=$1 review=$2 diff=$3 sha=$4 round=$5 total=$6 threads=$7
repo=$GITHUB_REPOSITORY

payload=$(build_review_payload "$review" "$diff" "$sha" "$round" "$total") || { rc=$?; echo "error: invalid review JSON" >&2; exit "$rc"; }
printf '%s' "$payload" | gh_write api -X POST "repos/$repo/pulls/$pr/reviews" --input -

jq -c '.thread_replies // [] | .[]' <"$review" | while read -r reply; do
  id=$(jq -r .comment_id <<<"$reply")
  root=$(thread_root_id "$threads" "$id")
  if [[ -z $root || $(thread_root_author "$threads" "$root") != github-actions ]]; then
    warn "dropping thread reply to comment $id: not in a thread the loop started"
    continue
  fi
  jq "$AI_REVIEW_JQ_DEFS"'{body: (.body | san)}' <<<"$reply" \
    | gh_write api -X POST "repos/$repo/pulls/$pr/comments/$root/replies" --input -
  if [[ $(jq -r .resolve <<<"$reply") == true ]]; then
    gh_write api graphql -f query='mutation($id: ID!) { resolveReviewThread(input: {threadId: $id}) { thread { isResolved } } }' \
      -f id="$(thread_node_id "$threads" "$root")" || warn "could not resolve the thread of comment $root"
  fi
done
