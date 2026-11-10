#!/usr/bin/env bash
# fetch-context.sh <pr> <out_dir> [expected_head_sha]
# Writes pr.json, diff.patch, threads.json (trusted threads, for the reviewer),
# threads-implementer.json (minus threads already declined) and, after an earlier loop review,
# round-diff.patch. Exits 4 when the diff is unavailable (HTTP 406) or over 400 KB, and 3 when
# the PR head is not expected_head_sha before or after the diff fetch (the diff is always live).
set -euo pipefail
# shellcheck source=lib.sh
. "$(dirname "${BASH_SOURCE[0]}")/lib.sh"

pr=$1 out=$2 want=${3:-}
repo=$GITHUB_REPOSITORY
mkdir -p "$out"
tmp=$(mktemp -d)
trap 'rm -rf "$tmp"' EXIT

head_is_expected() {
  [[ -z $want || $1 == "$want" ]] && return 0
  echo "::error::PR #$pr head moved to $1 during the round (expected $want); comment \`/ai-review more\` to run another round." >&2
  exit 3
}

gh pr view "$pr" -R "$repo" --json number,title,body,author,baseRefName,headRefName,headRefOid >"$out/pr.json"
head_is_expected "$(jq -r .headRefOid <"$out/pr.json")"

if ! gh api -H "Accept: application/vnd.github.diff" "repos/$repo/pulls/$pr" >"$out/diff.patch" 2>"$tmp/err"; then
  cat "$tmp/err" >&2
  if grep -q 'HTTP 406' "$tmp/err"; then exit 4; fi
  exit 1
fi
if (($(wc -c <"$out/diff.patch") > 400 * 1024)); then
  echo "PR #$pr diff is over 400 KB" >&2
  exit 4
fi
[[ -z $want ]] || head_is_expected "$(gh pr view "$pr" -R "$repo" --json headRefOid | jq -r .headRefOid)"

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
