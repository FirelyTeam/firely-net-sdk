#!/usr/bin/env bash
# Self-test for the AI review loop scripts. Run from anywhere: bash .github/ai-review/test.sh
# Never calls the real GitHub API: `gh` is a stub function and writes are dry-run.
# No `set -e`: tests capture non-zero exit codes on purpose.
set -uo pipefail

dir=$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)
fx=$dir/fixtures
export AI_REVIEW_DRY_RUN=1 GITHUB_REPOSITORY=FirelyTeam/firely-net-sdk GH_TOKEN=stub-not-a-token
# shellcheck source=lib.sh
. "$dir/lib.sh" 2>/dev/null || true
tmp=$(mktemp -d)
trap 'rm -rf "$tmp"' EXIT

assert_eq() {
  if [[ "$2" == "$3" ]]; then
    echo "ok $1"
  else
    printf 'FAIL %s\n--- expected\n%s\n--- actual\n%s\n' "$1" "$2" "$3"
    exit 1
  fi
}

# Stub for every read the scripts make. Responses come from STUB_* files.
gh() {
  case "$*" in
    "pr view"*) cat "$STUB_PR" ;;
    "api graphql"*) cat "$STUB_THREADS" ;;
    *application/vnd.github.diff*/compare/*) printf 'diff --git a/round b/round\n' ;;
    *application/vnd.github.diff*)
      if [[ -n ${STUB_DIFF_406:-} ]]; then echo "gh: Sorry, the diff exceeded the maximum number of files (300). (HTTP 406)" >&2; return 1; fi
      cat "$STUB_DIFF" ;;
    *--paginate*/reviews*) cat "$STUB_REVIEWS" ;;
    "api repos/"*/pulls/*) cat "$STUB_BODY" ;;
    *) echo "unexpected stub call: gh $*" >&2; return 1 ;;
  esac
}
export -f gh

# --- markers ---
out=$(marker_read "no marker here"); assert_eq marker-read-none "0:" "$?:$out"
assert_eq marker-read "1 2 running" "$(marker_read $'x\n<!-- ai-review: round=1/2 status=running -->')"
assert_eq marker-write-null "<!-- ai-review: round=0/2 status=running -->" "$(marker_write "" 0 2 running)"
assert_eq marker-write-replace $'above\n<!-- ai-review: round=2/2 status=done -->\nbelow & more' \
  "$(marker_write $'above\n<!-- ai-review: round=1/2 status=running -->\nbelow & more' 2 2 done)"
assert_eq marker-write-append $'text\n\n<!-- ai-review: round=0/2 status=running -->' "$(marker_write text 0 2 running)"

# --- diff and payload ---
assert_eq diff-lines $'src/A.cs\t10\nsrc/A.cs\t11\nsrc/A.cs\t12\nsrc/A.cs\t13\nsrc/B.cs\t1\nsrc/B.cs\t2\nsrc/B.cs\t3' \
  "$(diff_lines "$fx/diff.patch")"
payload=$(build_review_payload "$fx/review.json" "$fx/diff.patch" 2222222 1 2)
assert_eq build-review-payload-count 1 "$(jq -r '.comments | length' <<<"$payload")"
assert_eq build-review-payload-comment '{"path":"src/A.cs","line":12,"side":"RIGHT","start_line":11,"start_side":"RIGHT"}' \
  "$(jq -c '.comments[0] | del(.body)' <<<"$payload")"
assert_eq build-review-payload-head '2222222 COMMENT true' \
  "$(jq -r '"\(.commit_id) \(.event) \(.body | startswith("**AI review — round 1 of 2**"))"' <<<"$payload")"
assert_eq build-review-payload-outside true \
  "$(jq -r '.body | contains("### Outside the diff") and contains("src/A.cs:40") and contains("Outside any hunk.")' <<<"$payload")"
echo 'not json' >"$tmp/bad.json"
build_review_payload "$tmp/bad.json" "$fx/diff.patch" 2222222 1 2 >/dev/null 2>&1; assert_eq invalid-json 2 "$?"
echo '{"comments":[]}' >"$tmp/nosummary.json"
build_review_payload "$tmp/nosummary.json" "$fx/diff.patch" 2222222 1 2 >/dev/null 2>&1; assert_eq missing-summary 2 "$?"
echo '{"summary":"s","comments":[],"thread_replies":[{"comment_id":"101","body":"b","resolve":true}]}' >"$tmp/badreply.json"
build_review_payload "$tmp/badreply.json" "$fx/diff.patch" 2222222 1 2 >/dev/null 2>&1; assert_eq invalid-thread-reply 2 "$?"
echo '{"summary":"s","comments":[]}' >"$tmp/round1.json"
build_review_payload "$tmp/round1.json" "$fx/diff.patch" 2222222 1 2 >/dev/null 2>&1; assert_eq no-thread-replies-ok 0 "$?"
# GitHub rejects the whole review (422) when start_line and line are in different hunks.
printf 'diff --git a/src/C.cs b/src/C.cs\n--- a/src/C.cs\n+++ b/src/C.cs\n@@ -1,1 +1,1 @@\n c1\n@@ -20,1 +20,1 @@\n c20\n' >"$tmp/twohunks.patch"
echo '{"summary":"s","comments":[{"path":"src/C.cs","start_line":1,"line":20,"body":"spans"}]}' >"$tmp/span.json"
assert_eq build-review-payload-cross-hunk "0 true" \
  "$(build_review_payload "$tmp/span.json" "$tmp/twohunks.patch" 2222222 1 2 | jq -r '"\(.comments | length) \(.body | contains("src/C.cs:1-20"))"')"

# --- text and filters ---
zw=$'\xe2\x80\x8b'
assert_eq sanitize "ping @${zw}FirelyTeam/devs and \`@param\`" "$(sanitize 'ping @FirelyTeam/devs and `@param`')"
assert_eq sanitize-payload true "$(jq -r --arg zw "$zw" '.body | contains("@" + $zw + "FirelyTeam")' <<<"$payload")"
assert_eq trusted-threads "PRRT_bot PRRT_member" "$(trusted_threads "$fx/threads-mixed.json" | jq -r '[.[].thread_id] | join(" ")')"
assert_eq trusted-threads-shape '{"thread_id":"PRRT_bot","is_resolved":false,"path":"src/A.cs","line":12,"comments":[{"id":101,"author":"github-actions","body":"Bot finding."},{"id":102,"author":"alexzautke","body":"Human reply."}]}' \
  "$(trusted_threads "$fx/threads-mixed.json" | jq -c '.[0]')"

# --- command gate ---
ev=$fx/event-issue-comment.json
jq '.comment.body = "/ai-review stop\r\nplease"' "$ev" >"$tmp/stop.json"
assert_eq command-gate-member-stop stop "$(command_gate "$tmp/stop.json")"
jq '.comment.body = "/ai-review more"' "$ev" >"$tmp/more.json"
assert_eq command-gate-member-more more "$(command_gate "$tmp/more.json")"
jq '.comment.author_association = "NONE"' "$tmp/stop.json" >"$tmp/outsider.json"
assert_eq command-gate-outsider "" "$(command_gate "$tmp/outsider.json")"
jq 'del(.issue.pull_request)' "$tmp/stop.json" >"$tmp/issue.json"
assert_eq command-gate-issue "" "$(command_gate "$tmp/issue.json")"
jq '.comment.body = "please /ai-review stop"' "$ev" >"$tmp/notfirst.json"
assert_eq command-gate-not-first "" "$(command_gate "$tmp/notfirst.json")"

# --- status guard (stubbed gh, dry-run writes) ---
export STUB_BODY=$tmp/body.json
printf '{"body":"text\\n<!-- ai-review: round=1/2 status=stopped -->"}' >"$STUB_BODY"
out=$(pr_marker_update 1 - - running 2>/dev/null); assert_eq marker-no-restart "3:" "$?:$out"
out=$(AI_REVIEW_ALLOW_RESTART=1 pr_marker_update 1 - 3 running 2>/dev/null)
assert_eq marker-restart-allowed 'gh api -X PATCH repos/FirelyTeam/firely-net-sdk/pulls/1 -f body=text
<!-- ai-review: round=1/3 status=running -->' "$out"
printf '{"body":"no marker"}' >"$STUB_BODY"
out=$(pr_marker_update 1 1 - - 2>"$tmp/err"); assert_eq marker-missing "5:" "$?:$out"
assert_eq marker-missing-warns true "$(grep -q -i warning "$tmp/err" && echo true)"
printf '{"body":null}' >"$STUB_BODY"
out=$(pr_marker_update 1 0 0 stopped); assert_eq marker-null-body \
  "gh api -X PATCH repos/FirelyTeam/firely-net-sdk/pulls/1 -f body=<!-- ai-review: round=0/0 status=stopped -->" "$out"

# --- more_total ---
assert_eq more-total-running 3 "$(more_total 1 2 running)"
assert_eq more-total-stopped 2 "$(more_total 1 2 stopped)"
assert_eq more-total-done 3 "$(more_total 2 2 done)"

# --- identity and reply targets, against the captured real fixtures ---
assert_eq normalize-login "dependabot github-actions github-actions" \
  "$(normalize_login "$(jq -r .author.login "$fx/pr-dependabot.json")") $(normalize_login 'github-actions[bot]') $(normalize_login github-actions)"
trusted_threads "$fx/threads-raw.json" >"$tmp/threads.json"
assert_eq trusted-threads-real "PRRT_handmade_ga_open PRRT_handmade_ga_declined PRRT_handmade_ga_followup" \
  "$(jq -r '[.[].thread_id] | join(" ")' "$tmp/threads.json")"
assert_eq trusted-threads-outdated-kept null "$(jq -r '.[1].line' "$tmp/threads.json")"
assert_eq reply-to-reply-normalized 3000000001 "$(thread_root_id "$tmp/threads.json" 3000000002)"
out=$(thread_root_id "$tmp/threads.json" 42); assert_eq thread-root-unknown "0:" "$?:$out"
assert_eq implementer-threads-skip-declined "PRRT_handmade_ga_open PRRT_handmade_ga_followup" \
  "$(implementer_threads "$tmp/threads.json" | jq -r '[.[].thread_id] | join(" ")')"
assert_eq implementer-threads-human-followup true \
  "$(implementer_threads "$tmp/threads.json" | jq -r 'any(.[]; .thread_id == "PRRT_handmade_ga_followup")')"

# --- scripts, dry-run ---
trusted_threads "$fx/threads-mixed.json" >"$tmp/mixed.json"
out=$(bash "$dir/publish-review.sh" 1 "$fx/review.json" "$fx/diff.patch" 2222222 1 2 "$tmp/mixed.json" 2>"$tmp/err")
assert_eq publish-review-dry-exit 0 "$?"
assert_eq publish-review-dry-reviews 1 "$(grep -c '^gh api -X POST repos/FirelyTeam/firely-net-sdk/pulls/1/reviews ' <<<"$out")"
assert_eq publish-review-dry-replies 'gh api -X POST repos/FirelyTeam/firely-net-sdk/pulls/1/comments/101/replies --input -' \
  "$(grep '/replies' <<<"$out")"
assert_eq publish-review-dry-resolve 1 "$(grep -c '^gh api graphql .*resolveReviewThread.* -f id=PRRT_bot$' <<<"$out")"
assert_eq publish-review-dry-human-dropped true "$(grep -q 'comment 201' "$tmp/err" && echo true)"
out=$(bash "$dir/publish-review.sh" 1 "$tmp/badreply.json" "$fx/diff.patch" 2222222 1 2 "$tmp/mixed.json" 2>/dev/null)
assert_eq publish-review-invalid "2:" "$?:$out"

out=$(bash "$dir/publish-replies.sh" 1 "$fx/replies.json" "$tmp/mixed.json" 2>"$tmp/err")
assert_eq publish-replies-dry "0:gh api -X POST repos/FirelyTeam/firely-net-sdk/pulls/1/comments/101/replies --input -" "$?:$out"
assert_eq publish-replies-unknown-dropped true "$(grep -q 'comment 999' "$tmp/err" && echo true)"

# --- fetch-context, stubbed reads ---
export STUB_PR=$fx/pr-human.json STUB_THREADS=$fx/threads-raw.json STUB_DIFF=$fx/diff.patch STUB_REVIEWS=$tmp/reviews.json
printf '[{"user":{"login":"github-actions[bot]"},"commit_id":"aaa"}]\n[{"user":{"login":"alexzautke"},"commit_id":"bbb"}]\n' >"$STUB_REVIEWS"
bash "$dir/fetch-context.sh" 1 "$tmp/ctx"; assert_eq fetch-context-exit 0 "$?"
assert_eq fetch-context-files "diff.patch pr.json round-diff.patch threads-implementer.json threads.json" "$(cd "$tmp/ctx" && echo *)"
assert_eq fetch-context-threads "3 2" "$(jq length "$tmp/ctx/threads.json") $(jq length "$tmp/ctx/threads-implementer.json")"
printf '[{"user":{"login":"alexzautke"},"commit_id":"bbb"}]\n' >"$STUB_REVIEWS"
bash "$dir/fetch-context.sh" 1 "$tmp/ctx1"; assert_eq fetch-context-round1 "0 absent" "$? $([[ -e $tmp/ctx1/round-diff.patch ]] && echo present || echo absent)"
export STUB_DIFF=$tmp/big.patch
head -c 512000 /dev/zero | tr '\0' x >"$STUB_DIFF"
bash "$dir/fetch-context.sh" 1 "$tmp/ctx2" 2>/dev/null; assert_eq fetch-context-too-large 4 "$?"
export STUB_DIFF_406=1
bash "$dir/fetch-context.sh" 1 "$tmp/ctx3" 2>/dev/null; assert_eq fetch-context-406 4 "$?"
unset STUB_DIFF_406

echo "all tests passed"
