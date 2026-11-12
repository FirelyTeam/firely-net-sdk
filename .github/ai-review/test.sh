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
# The mode is a separate read: marker_read stays three fields, so `read -r r t s` keeps working.
authm='x <!-- ai-review: round=1/2 status=waiting mode=author-fixes --> y'
assert_eq marker-read-mode "1 2 waiting|author-fixes" "$(marker_read "$authm")|$(marker_mode "$authm")"
assert_eq marker-mode-none "" "$(marker_mode '<!-- ai-review: round=1/2 status=running -->')"
assert_eq marker-write-mode 'x <!-- ai-review: round=2/2 status=done mode=author-fixes --> y' \
  "$(marker_write "$authm" 2 2 done author-fixes)"
assert_eq marker-write-clear-mode 'x <!-- ai-review: round=1/3 status=running --> y' "$(marker_write "$authm" 1 3 running)"

# Workflow shells have no pipefail: a failed GET must not become an empty body that gets PATCHed.
out=$(set +o pipefail; STUB_BODY=$tmp/missing.json pr_marker_update 1 0 2 running 2>/dev/null); assert_eq marker-update-get-fails "1:" "$?:$out"

# --- diff and payload ---
assert_eq diff-lines $'src/A.cs\t10\nsrc/A.cs\t11\nsrc/A.cs\t12\nsrc/A.cs\t13\nsrc/B.cs\t1\nsrc/B.cs\t2\nsrc/B.cs\t3' \
  "$(diff_lines "$fx/diff.patch")"
# An added `++ counter;` reads `+++ counter;`: a line of the hunk, not a file header.
printf 'diff --git a/src/D.cs b/src/D.cs\n--- a/src/D.cs\n+++ b/src/D.cs\n@@ -1,1 +1,3 @@\n a\n+++ counter;\n+b\ndiff --git a/src/E.cs b/src/E.cs\n--- a/src/E.cs\n+++ b/src/E.cs\n@@ -5 +5 @@\n-x\n+y\n' >"$tmp/plusplus.patch"
assert_eq diff-lines-plusplus $'src/D.cs\t1\nsrc/D.cs\t2\nsrc/D.cs\t3\nsrc/E.cs\t5' "$(diff_lines "$tmp/plusplus.patch")"
# Git C-quotes non-ASCII paths.
printf 'diff --git "a/src/\\303\\251.cs" "b/src/\\303\\251.cs"\n--- "a/src/\\303\\251.cs"\n+++ "b/src/\\303\\251.cs"\n@@ -1,0 +1,1 @@\n+z\n' >"$tmp/quoted.patch"
assert_eq diff-lines-quoted $'src/\xc3\xa9.cs\t1' "$(diff_lines "$tmp/quoted.patch")"
# A path with a space ends its +++ line with a tab, quoted or not.
printf 'diff --git a/My File.cs b/My File.cs\n--- a/My File.cs\t\n+++ b/My File.cs\t\n@@ -1,0 +1,1 @@\n+z\ndiff --git "a/\\303\\251 x.cs" "b/\\303\\251 x.cs"\n--- "a/\\303\\251 x.cs"\t\n+++ "b/\\303\\251 x.cs"\t\n@@ -1,0 +1,1 @@\n+z\n' >"$tmp/space.patch"
assert_eq diff-lines-space $'My File.cs\t1\n\xc3\xa9 x.cs\t1' "$(diff_lines "$tmp/space.patch")"
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
assert_eq sanitize "ping @${zw}FirelyTeam/devs and \`@${zw}param\`" "$(sanitize 'ping @FirelyTeam/devs and `@param`')"
assert_eq sanitize-payload true "$(jq -r --arg zw "$zw" '.body | contains("@" + $zw + "FirelyTeam")' <<<"$payload")"
# An unmatched backtick does not start code on GitHub, so the mention after it still pings.
assert_eq sanitize-unbalanced "see \`foo and @${zw}alexzautke here" "$(sanitize 'see `foo and @alexzautke here')"
# Mismatched runs (` then ``) form no code span either.
assert_eq sanitize-mismatched-runs "see \`foo @${zw}alice\`\`" "$(sanitize 'see `foo @alice``')"
# Fenced blocks stay byte-exact (suggestions), and the text after the closing fence is neutralized.
assert_eq sanitize-fence $'```suggestion\n[Obsolete("@x")]\n```\ncc @'"${zw}"'bob' \
  "$(sanitize $'```suggestion\n[Obsolete("@x")]\n```\ncc @bob')"
# A backtick fence's info string cannot hold a backtick, so this line is not a fence.
assert_eq sanitize-not-a-fence "\`\`\`a\`b @${zw}carol" "$(sanitize '```a`b @carol')"
echo '{"summary":"a stray ` here","comments":[{"path":"src/A.cs","line":40,"body":"cc @alexzautke"},{"path":"src/A.cs","line":41,"body":"b"}]}' >"$tmp/stray.json"
assert_eq build-review-payload-stray-backtick true \
  "$(build_review_payload "$tmp/stray.json" "$fx/diff.patch" 2222222 1 2 | jq -r --arg zw "$zw" '.body | contains("@" + $zw + "alexzautke")')"
assert_eq trusted-threads "PRRT_bot PRRT_member" "$(trusted_threads "$fx/threads-mixed.json" | jq -r '[.[].thread_id] | join(" ")')"
assert_eq trusted-threads-shape '{"thread_id":"PRRT_bot","is_resolved":false,"path":"src/A.cs","line":12,"comments":[{"id":101,"author":"github-actions","body":"Bot finding."},{"id":102,"author":"alexzautke","body":"Human reply."}]}' \
  "$(trusted_threads "$fx/threads-mixed.json" | jq -c '.[0]')"
assert_eq trusted-threads-drop-outsider-reply "201" "$(trusted_threads "$fx/threads-mixed.json" | jq -r '[.[1].comments[].id] | join(" ")')"

# --- command gate ---
ev=$fx/event-issue-comment.json
jq '.comment.body = "/ai-review stop\r\nplease"' "$ev" >"$tmp/stop.json"
assert_eq command-gate-member-stop stop "$(command_gate "$tmp/stop.json")"
jq '.comment.body = "/ai-review more"' "$ev" >"$tmp/more.json"
assert_eq command-gate-member-more more "$(command_gate "$tmp/more.json")"
jq '.comment.body = "/ai-review fix"' "$ev" >"$tmp/fix.json"
assert_eq command-gate-member-fix fix "$(command_gate "$tmp/fix.json")"
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
out=$(pr_marker_update_soft 1 - - stopped 2>/dev/null); assert_eq marker-soft-missing "0:" "$?:$out"
printf '{"body":"<!-- ai-review: round=1/2 status=stopped -->"}' >"$STUB_BODY"
out=$(pr_marker_update_soft 1 - - running 2>/dev/null); assert_eq marker-soft-keeps-other-errors "3:" "$?:$out"
printf '{"body":null}' >"$STUB_BODY"
out=$(pr_marker_update 1 0 0 stopped); assert_eq marker-null-body \
  "gh api -X PATCH repos/FirelyTeam/firely-net-sdk/pulls/1 -f body=<!-- ai-review: round=0/0 status=stopped -->" "$out"
patched() { printf 'gh api -X PATCH repos/FirelyTeam/firely-net-sdk/pulls/1 -f body=<!-- ai-review: %s -->' "$1"; }
# Author fixes: the switch counts the round and waits for the next push, or ends at the total.
printf '{"body":"<!-- ai-review: round=0/2 status=running -->"}' >"$STUB_BODY"
assert_eq marker-switch-waiting "$(patched 'round=1/2 status=waiting mode=author-fixes')" "$(pr_marker_update 1 1 - next author-fixes)"
assert_eq marker-switch-done "$(patched 'round=2/2 status=done mode=author-fixes')" "$(pr_marker_update 1 2 - next author-fixes)"
# Every other update keeps the mode; none clears it.
printf '{"body":"<!-- ai-review: round=1/2 status=waiting mode=author-fixes -->"}' >"$STUB_BODY"
assert_eq marker-keeps-mode "$(patched 'round=1/2 status=stopped mode=author-fixes')" "$(pr_marker_update 1 - - stopped)"
assert_eq marker-clears-mode "$(patched 'round=1/3 status=running')" "$(pr_marker_update 1 - 3 running none)"
# A stop that came in during a review-only round stays a stop.
printf '{"body":"<!-- ai-review: round=1/3 status=stopped mode=author-fixes -->"}' >"$STUB_BODY"
assert_eq marker-next-keeps-stop "$(patched 'round=2/3 status=stopped mode=author-fixes')" "$(pr_marker_update 1 2 - next)"
out=$(pr_marker_update 1 - - waiting 2>/dev/null); assert_eq marker-no-restart-waiting "3:" "$?:$out"
printf '{"body":"no marker"}' >"$STUB_BODY"
out=$(pr_marker_update 1 1 - next 2>/dev/null); assert_eq marker-next-missing "5:" "$?:$out"

# --- skill_loaded, against the base-action execution_file shape (a JSON array of SDK messages) ---
exec_log() { # <skill input> <is_error>
  printf '[{"type":"system","subtype":"init"},
    {"type":"assistant","message":{"content":[{"type":"tool_use","id":"t1","name":"Skill","input":%s}]}},
    {"type":"user","message":{"content":[{"type":"tool_result","tool_use_id":"t1","content":"x","is_error":%s}]}},
    {"type":"result","num_turns":3,"total_cost_usd":0.5}]' "$1" "$2" >"$tmp/exec.json"
}
exec_log '{"skill":"pr-review-reviewer"}' false; skill_loaded "$tmp/exec.json" pr-review-reviewer; assert_eq skill-loaded 0 "$?"
exec_log '{"skill":"firely-skills-internal:pr-review-reviewer"}' false; skill_loaded "$tmp/exec.json" pr-review-reviewer
assert_eq skill-loaded-plugin-prefix 0 "$?"
skill_loaded "$tmp/exec.json" pr-review-implementer; assert_eq skill-loaded-wrong-skill 1 "$?"
exec_log '{"skill":"pr-review-reviewer"}' true; skill_loaded "$tmp/exec.json" pr-review-reviewer; assert_eq skill-loaded-error-result 1 "$?"
jq 'del(.[2])' "$tmp/exec.json" >"$tmp/exec-noresult.json"; skill_loaded "$tmp/exec-noresult.json" pr-review-reviewer
assert_eq skill-loaded-no-result 1 "$?"

# --- key and path refusals ---
echo '{"replies":[{"comment_id":1,"body":"fine"}]}' >"$tmp/clean.json"
has_api_key "$tmp/clean.json"; assert_eq api-key-clean 1 "$?"
echo 'x sk-ant-api03-abc' >"$tmp/raw.txt"; has_api_key "$tmp/clean.json" "$tmp/raw.txt"; assert_eq api-key-raw 0 "$?"
# JSON-escaped `s` (backslash u0073): no literal key in the bytes, so only the jq decode can catch it.
# The backslash is built with printf so no editor or tool can decode the escape in this source.
bs=$(printf '\x5c')
printf '{"replies":[{"comment_id":1,"body":"%su0073k-ant-x"}]}\n' "$bs" >"$tmp/escaped.json"
printf '{"replies":[],"%su0073k-ant-x":1}\n' "$bs" >"$tmp/escaped-key.json"
assert_eq api-key-fixtures-have-no-literal-key 0 "$(cat "$tmp/escaped.json" "$tmp/escaped-key.json" | grep -c sk-ant-)"
has_api_key "$tmp/escaped.json"; assert_eq api-key-json-escaped 0 "$?"
has_api_key "$tmp/escaped-key.json"; assert_eq api-key-json-escaped-key 0 "$?"
printf 'bin\0ary sk-ant-api03-abc\0' >"$tmp/bin.dat"; has_api_key "$tmp/bin.dat"; assert_eq api-key-binary 0 "$?"
echo '{"body":"t eyJhbGciOiJSUzI1NiJ9.eyJzdWIiOiJyZXBvOngifQ.c2ln"}' >"$tmp/jwt.json"
has_api_key "$tmp/jwt.json"; assert_eq api-key-jwt 0 "$?"
G() { git -C "$tmp/repo" -c user.name=t -c user.email=t@t -c core.autocrlf=false "$@"; }
git init -q "$tmp/repo" && echo a >"$tmp/repo/a" && G add a && G commit -qm A
base=$(G rev-parse HEAD)
echo b >"$tmp/repo/b" && G add b && G commit -qm "plain change"
(cd "$tmp/repo" && touches_protected "$base" HEAD); assert_eq protected-clean 1 "$?"
(cd "$tmp/repo" && commits_have_api_key "$base" HEAD); assert_eq commits-key-clean 1 "$?"
G commit -q --allow-empty -m "note sk-ant-api03-abc"
(cd "$tmp/repo" && commits_have_api_key "$base" HEAD); assert_eq commits-key-message 0 "$?"
G reset -q --hard HEAD~1
printf 'bin\0sk-ant-api03-abc' >"$tmp/repo/k.bin" && G add k.bin && G commit -qm bin && G rm -q k.bin && G commit -qm unbin
(cd "$tmp/repo" && commits_have_api_key "$base" HEAD); assert_eq commits-key-binary-removed 0 "$?"
mkdir -p "$tmp/repo/.claude/skills/é" && echo x >"$tmp/repo/.claude/skills/é/SKILL.md" && G add .claude && G commit -qm odd
(cd "$tmp/repo" && touches_protected "$base" HEAD); assert_eq protected-quoted-path 0 "$?"
(cd "$tmp/repo" && touches_protected "$base" nosuchrev 2>/dev/null); assert_eq protected-git-error 0 "$?"
# Azure Pipelines runs build/ and restores with nuget.config (any case) on every bot push.
b2=$(G rev-parse HEAD)
mkdir -p "$tmp/repo/build" && echo x >"$tmp/repo/build/azure-pipelines.yml" && G add build && G commit -qm pipe
(cd "$tmp/repo" && touches_protected "$b2" HEAD); assert_eq protected-build 0 "$?"
G reset -q --hard "$b2"
echo x >"$tmp/repo/NuGet.Config" && G add NuGet.Config && G commit -qm nuget
(cd "$tmp/repo" && touches_protected "$b2" HEAD); assert_eq protected-nuget-config 0 "$?"
G reset -q --hard "$b2"
mkdir -p "$tmp/repo/src/build" && echo x >"$tmp/repo/src/build/x.cs" && G add src && G commit -qm src
(cd "$tmp/repo" && touches_protected "$b2" HEAD); assert_eq protected-nested-build-ok 1 "$?"

# --- more_total ---
assert_eq more-total-running 3 "$(more_total 1 2 running)"
assert_eq more-total-stopped 2 "$(more_total 1 2 stopped)"
assert_eq more-total-done 3 "$(more_total 2 2 done)"
assert_eq more-total-waiting 3 "$(more_total 1 2 waiting)"

# --- round_cap: a hand-edited marker total cannot pass the hard cap ---
assert_eq round-cap "4 2 4 0" "$(round_cap 40) $(round_cap 2) $(round_cap 4) $(round_cap '')"

# --- pr_active: closed, merged and draft PRs stop the loop ---
assert_eq pr-active "0 1 1" "$(
  pr_active '{"state":"open","draft":false}'; echo -n "$? "
  pr_active '{"state":"closed","draft":false,"merged":true}'; echo -n "$? "
  pr_active '{"state":"open","draft":true}'; echo -n "$?")"

# --- identity and reply targets, against the captured real fixtures ---
assert_eq normalize-login "dependabot github-actions github-actions" \
  "$(normalize_login "$(jq -r .author.login "$fx/pr-dependabot.json")") $(normalize_login 'github-actions[bot]') $(normalize_login github-actions)"
trusted_threads "$fx/threads-raw.json" >"$tmp/threads.json"
assert_eq trusted-threads-real "PRRT_handmade_ga_open PRRT_handmade_ga_declined PRRT_handmade_ga_followup" \
  "$(jq -r '[.[].thread_id] | join(" ")' "$tmp/threads.json")"
assert_eq trusted-threads-outdated-kept null "$(jq -r '.[1].line' "$tmp/threads.json")"
cat "$fx/threads-raw.json" "$fx/threads-raw.json" >"$tmp/threads-2pages.json"
assert_eq trusted-threads-all-pages 6 "$(trusted_threads "$tmp/threads-2pages.json" | jq length)"
assert_eq reply-to-reply-normalized 3000000001 "$(thread_root_id "$tmp/threads.json" 3000000002)"
out=$(thread_root_id "$tmp/threads.json" 42); assert_eq thread-root-unknown "0:" "$?:$out"
assert_eq implementer-threads-skip-declined "PRRT_handmade_ga_open PRRT_handmade_ga_followup" \
  "$(implementer_threads "$tmp/threads.json" | jq -r '[.[].thread_id] | join(" ")')"
assert_eq implementer-threads-outsider-no-reopen "3000000013 false" \
  "$(jq -r '[.. | .databaseId? | select(. == 3000000013)][0]' "$fx/threads-raw.json") $(implementer_threads "$tmp/threads.json" | jq -r 'any(.[]; .thread_id == "PRRT_handmade_ga_declined")')"
assert_eq implementer-threads-human-followup true \
  "$(implementer_threads "$tmp/threads.json" | jq -r 'any(.[]; .thread_id == "PRRT_handmade_ga_followup")')"
# A thread the reviewer verified but could not resolve needs no more work, until a human replies.
printf '[{"thread_id":"V","comments":[{"id":1,"author":"github-actions","body":"Bug."},{"id":2,"author":"github-actions","body":"Fixed in abc1234: x"},{"id":3,"author":"github-actions","body":"Verified: x"}]},
  {"thread_id":"VH","comments":[{"id":4,"author":"github-actions","body":"Bug."},{"id":5,"author":"github-actions","body":"Verified: x"},{"id":6,"author":"alexzautke","body":"Not quite."}]}]' >"$tmp/verified.json"
assert_eq implementer-threads-skip-verified VH "$(implementer_threads "$tmp/verified.json" | jq -r '[.[].thread_id] | join(" ")')"

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
bash "$dir/fetch-context.sh" 1 "$tmp/ctx" d14a13e0e5c86e9a32cf134f165c70e0b8757382; assert_eq fetch-context-exit 0 "$?"
bash "$dir/fetch-context.sh" 1 "$tmp/ctx-moved" 1111111 2>/dev/null; assert_eq fetch-context-head-moved 3 "$?"
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
