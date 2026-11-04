#!/usr/bin/env bash
# fetch-context.sh <pr> <out_dir>
# Writes pr.json, diff.patch, threads.json (trusted threads, for the reviewer),
# threads-implementer.json (minus threads already declined) and, after an earlier loop review,
# round-diff.patch. Exits 4 when the diff is unavailable (HTTP 406) or over 400 KB.
set -euo pipefail
# shellcheck source=lib.sh
. "$(dirname "${BASH_SOURCE[0]}")/lib.sh"

pr=$1 out=$2
repo=$GITHUB_REPOSITORY
mkdir -p "$out"
tmp=$(mktemp -d)
trap 'rm -rf "$tmp"' EXIT

gh pr view "$pr" -R "$repo" --json number,title,body,author,baseRefName,headRefName,headRefOid >"$out/pr.json"

if ! gh api -H "Accept: application/vnd.github.diff" "repos/$repo/pulls/$pr" >"$out/diff.patch" 2>"$tmp/err"; then
  cat "$tmp/err" >&2
  if grep -q 'HTTP 406' "$tmp/err"; then exit 4; fi
  exit 1
fi
if (($(wc -c <"$out/diff.patch") > 400 * 1024)); then
  echo "PR #$pr diff is over 400 KB" >&2
  exit 4
fi

# Writes one JSON page per 100 threads; trusted_threads slurps them.
# shortcut: first 100 comments per thread, no pagination there; enough for the pilot.
gh api graphql --paginate -f owner="${repo%%/*}" -f name="${repo#*/}" -F pr="$pr" -f query='
  query($owner: String!, $name: String!, $pr: Int!, $endCursor: String) {
    repository(owner: $owner, name: $name) { pullRequest(number: $pr) {
      reviewThreads(first: 100, after: $endCursor) { pageInfo { hasNextPage endCursor } nodes {
        id isResolved path line
        comments(first: 100) { nodes { databaseId author { login } authorAssociation body } }
      } } } }
  }' >"$tmp/threads-raw.json"
trusted_threads "$tmp/threads-raw.json" >"$out/threads.json"
implementer_threads "$out/threads.json" >"$out/threads-implementer.json"

gh api --paginate "repos/$repo/pulls/$pr/reviews" >"$tmp/reviews.json"
base=$(jq -rs "$AI_REVIEW_JQ_DEFS"'add // [] | map(select(.user.login // "" | norm == "github-actions")) | last | .commit_id // empty' <"$tmp/reviews.json")
if [[ -n $base ]]; then
  head=$(jq -r .headRefOid <"$out/pr.json")
  # A force-push can remove the reviewed commit; the round diff is optional context, so warn and go on.
  gh api -H "Accept: application/vnd.github.diff" "repos/$repo/compare/$base...$head" >"$tmp/round-diff.patch" \
    && mv "$tmp/round-diff.patch" "$out/round-diff.patch" \
    || warn "could not fetch the diff $base...$head; no round-diff.patch"
fi
