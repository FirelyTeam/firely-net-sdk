# shellcheck shell=bash
# Shared functions for the AI review loop. Source it; it sets no shell options.
# Reads GITHUB_REPOSITORY (owner/name). gh reads GH_TOKEN itself; nothing here prints it.
# AI_REVIEW_DRY_RUN=1 prints write calls (gh_write) instead of running them; reads still use gh.

# A native jq.exe in Git Bash writes CRLF; -b keeps LF so the same code works locally and on ubuntu.
case "$(uname -s)" in MINGW* | MSYS* | CYGWIN*) jq() { command jq -b "$@"; } ;; esac

# jq twins of normalize_login and sanitize, for use inside jq programs.
AI_REVIEW_JQ_DEFS='
def norm: sub("^app/"; "") | sub("\\[bot\\]$"; "");
def trusted: (.author.login // "" | norm) == "github-actions"
  or (.authorAssociation | IN("OWNER", "MEMBER", "COLLABORATOR"));
# Odd segments are code spans; with an odd backtick count the last one is not (an unmatched ` is literal).
def san: split("`") | (length % 2 == 0) as $unbalanced | (length - 1) as $last | to_entries
  | map(if .key % 2 == 0 or ($unbalanced and .key == $last) then .value | gsub("@(?<c>[A-Za-z0-9-])"; "@​\(.c)") else .value end)
  | join("`");
'
AI_REVIEW_MARKER_RE='<!-- ai-review: round=([0-9]+)/([0-9]+) status=(running|done|stopped|failed) -->'

normalize_login() {
  local l=${1#app/}
  printf '%s\n' "${l%"[bot]"}"
}

warn() { echo "warning: $*" >&2; }

# Runs a gh write call, or prints it under AI_REVIEW_DRY_RUN=1 (and drains any --input - payload).
gh_write() {
  if [[ ${AI_REVIEW_DRY_RUN:-} == 1 ]]; then
    printf 'gh %s\n' "$*"
    if [[ " $* " == *" --input - "* ]]; then cat >/dev/null; fi
    return 0
  fi
  gh "$@" >/dev/null
}

thread_root_id() {
  [[ $2 =~ ^[0-9]+$ ]] || return 0
  jq -r --argjson id "$2" 'map(select(any(.comments[]; .id == $id))) | .[0].comments[0].id // empty' <"$1"
}

# Author (normalized) of the first comment of the thread whose root id is $2.
thread_root_author() {
  jq -r --argjson id "$2" "$AI_REVIEW_JQ_DEFS"'map(select(.comments[0].id == $id)) | .[0].comments[0].author // "" | norm' <"$1"
}

thread_node_id() {
  jq -r --argjson id "$2" 'map(select(.comments[0].id == $id)) | .[0].thread_id // empty' <"$1"
}

marker_read() {
  if [[ $1 =~ $AI_REVIEW_MARKER_RE ]]; then
    echo "${BASH_REMATCH[1]} ${BASH_REMATCH[2]} ${BASH_REMATCH[3]}"
  fi
  return 0
}

marker_write() {
  local body=$1 m="<!-- ai-review: round=$2/$3 status=$4 -->"
  if [[ -z $body || $body == null ]]; then
    printf '%s\n' "$m"
  elif [[ $body =~ $AI_REVIEW_MARKER_RE ]]; then
    printf '%s\n' "${body/"${BASH_REMATCH[0]}"/"$m"}"
  else
    printf '%s\n\n%s\n' "$body" "$m"
  fi
}

# pr_marker_update <pr> <R|-> <T|-> <S|->; returns 3 on a refused restart, 5 on a missing marker with a `-`.
pr_marker_update() {
  local pr=$1 body cur r t s
  body=$(gh api "repos/$GITHUB_REPOSITORY/pulls/$pr" | jq -r '.body // ""') || return 1
  cur=$(marker_read "$body")
  if [[ -z $cur ]]; then
    if [[ $2 == - || $3 == - || $4 == - ]]; then
      warn "PR #$pr has no ai-review marker; not updated"
      return 5
    fi
  else
    read -r r t s <<<"$cur"
  fi
  if [[ ${s:-} == stopped && $4 == running && ${AI_REVIEW_ALLOW_RESTART:-} != 1 ]]; then
    warn "PR #$pr: ai-review status is stopped; not setting running"
    return 3
  fi
  if [[ $2 != - ]]; then r=$2; fi
  if [[ $3 != - ]]; then t=$3; fi
  if [[ $4 != - ]]; then s=$4; fi
  gh_write api -X PATCH "repos/$GITHUB_REPOSITORY/pulls/$pr" -f body="$(marker_write "$body" "$r" "$t" "$s")"
}

# pr_marker_update, but a missing marker (rc 5) only warns: for best-effort `-` updates (R2).
pr_marker_update_soft() {
  local rc=0
  pr_marker_update "$@" || rc=$?
  ((rc == 0 || rc == 5)) || return "$rc"
}

# skill_loaded <execution_file> <skill>: exit 0 only if the session called the Skill tool for <skill>
# (bare or plugin-prefixed name) and that call's tool_result is not an error.
skill_loaded() {
  jq -e --arg s "$2" '
    [.. | objects | select(.type? == "tool_use" and .name? == "Skill"
      and ((.input.skill // .input.command // "") | tostring | ltrimstr("/") | test("(^|:)\($s)$")))
      | .id] as $ids
    | any(.. | objects; .type? == "tool_result" and (.tool_use_id as $t | $ids | index($t) != null)
      and .is_error != true)' <"$1" >/dev/null 2>&1
}

# has_api_key <file>...: exit 0 if a file holds an Anthropic key pattern, raw (binary too) or as a
# JSON-escaped string or key, or a JWT (the federation's GitHub OIDC identity token; JWT characters
# never need JSON escaping). Files go to grep/jq directly: `jq | grep -q` can lose a match to SIGPIPE.
has_api_key() {
  local f
  for f; do
    [[ -e $f ]] || continue
    grep -qaF -- sk-ant- "$f" && return 0
    grep -qaE -- 'eyJ[A-Za-z0-9_-]{10,}\.eyJ[A-Za-z0-9_-]{10,}\.' "$f" && return 0
    jq -e '[.. | (strings, (objects | keys[]))] | any(contains("sk-ant-"))' <"$f" >/dev/null 2>&1 && return 0
  done
  return 1
}

# commits_have_api_key <base> <tip>: has_api_key over every commit in base..tip: patches (binary as
# text, merges against their first parent), messages and identities. A git error counts as a hit.
commits_have_api_key() {
  local f rc=0
  f=$(mktemp)
  git log -p --text --diff-merges=first-parent --format='%an <%ae> %cn <%ce>%n%B' "$1..$2" >"$f" || rc=2
  ((rc == 2)) || has_api_key "$f" || rc=1
  rm -f "$f"
  ((rc != 1))
}

# touches_protected <base> <tip>: exit 0 if the tree diff or any commit in base..tip (merges against
# their first parent) touches .github/, .claude/, .mcp.json, build/ (Azure Pipelines) or a nuget.config
# at any depth. Case-insensitive, so it errs toward refusing. -z: git C-quotes odd paths otherwise.
# A git error counts as a hit.
touches_protected() {
  local f rc=0
  f=$(mktemp)
  { git diff -z --no-renames --name-only "$1" "$2" \
    && git log -z --no-renames --diff-merges=first-parent --name-only --format= "$1..$2"; } >"$f" || rc=2
  ((rc == 2)) || grep -qziE '^(\.github/|\.claude/|\.mcp\.json$|build/)|(^|/)nuget\.config$' "$f" || rc=1
  rm -f "$f"
  ((rc != 1))
}

AI_REVIEW_MAX_ROUNDS=4

more_total() {
  if [[ $3 == running ]]; then echo $(($2 + 1)); else echo $(($1 + 1)); fi
}

# round_cap <T>: the marker's total, at most AI_REVIEW_MAX_ROUNDS (the PR body is editable); empty is 0.
round_cap() {
  local t=${1:-0}
  echo $((t > AI_REVIEW_MAX_ROUNDS ? AI_REVIEW_MAX_ROUNDS : t))
}

# pr_active <REST pull JSON>: exit 0 only for an open, non-draft PR (closed covers merged).
pr_active() {
  jq -e '.state == "open" and .draft != true' <<<"$1" >/dev/null
}

diff_lines() {
  awk '
    /^\+\+\+ / { file = ($2 == "/dev/null") ? "" : substr($0, 7); next }
    /^@@ / { split($3, a, ","); n = substr(a[1], 2) + 0; next }
    /^[+ ]/ { if (file != "") print file "\t" n; n++ }
  ' "$1"
}

sanitize() {
  printf '%s' "$1" | jq -Rsj "$AI_REVIEW_JQ_DEFS"'san'
}

# Exit 2 unless review.json is an object with a string summary and well-formed comments/thread_replies.
review_valid() {
  jq -e '
    type == "object" and (.summary | type == "string")
    and ((.comments // []) | type == "array" and all(.[]; type == "object" and (.body | type == "string")))
    and ((.thread_replies // []) | type == "array"
      and all(.[]; (.comment_id | type == "number") and (.body | type == "string") and (.resolve | type == "boolean")))
  ' <"$1" >/dev/null 2>&1 || return 2
}

build_review_payload() {
  review_valid "$1" || return 2
  # stdin: review.json, then each diff line as a JSON string (keeps big input off argv)
  { cat "$1"; diff_lines "$2" | jq -R .; } | jq -nc --arg sha "$3" --arg round "$4" --arg total "$5" "$AI_REVIEW_JQ_DEFS"'
    input as $r
    | (reduce inputs as $l ({}; .[$l] = true)) as $ok
    | def at($line): $ok["\(.path)\t\($line)"] == true;
    (($r.comments // []) | map(select((.line | type == "number") and at(.line)
        and ((.start_line == null) or ((.start_line | type == "number")
          and ([range(.start_line; .line + 1) as $n | at($n)] | all)))))) as $in
    | (($r.comments // []) - $in) as $out
    | {commit_id: $sha, event: "COMMENT",
       # each piece is sanitized on its own, so a stray backtick in one cannot shift code spans in the next
       body: ("**AI review — round \($round) of \($total)**\n\n" + ($r.summary | san)
         + (if $out == [] then "" else "\n\n### Outside the diff\n\n"
             + ($out | map("- `"
                 + ("\(.path):\(if .start_line != null then "\(.start_line)-" else "" end)\(.line)" | gsub("`"; "") | san)
                 + "`: " + (.body | san)) | join("\n")) end)),
       comments: ($in | map({path, line, side: "RIGHT"}
         + (if .start_line != null and .start_line < .line then {start_line, start_side: "RIGHT"} else {} end)
         + {body: (.body | san)}))}'
}

# $1 holds one or more GraphQL result pages (gh api --paginate output).
trusted_threads() {
  jq -s "$AI_REVIEW_JQ_DEFS"'
    [.[].data.repository.pullRequest.reviewThreads.nodes[]
     | select(.isResolved | not)
     | select(.comments.nodes[0] | trusted)
     | {thread_id: .id, is_resolved: .isResolved, path, line,
        # untrusted replies never reach a model; the root is trusted, so reply targets are unchanged
        comments: [.comments.nodes[] | select(trusted) | {id: .databaseId, author: (.author.login // "ghost" | norm), body}]}]' <"$1"
}

# Drops threads the implementer already declined (`Not changed:`) unless a non-github-actions comment
# came later, and threads the reviewer verified (last comment a github-actions `Verified:`) but could
# not resolve.
implementer_threads() {
  jq "$AI_REVIEW_JQ_DEFS"'
    map(select(.comments as $c
      | ([range($c | length) | select(($c[.].author | norm) == "github-actions"
          and ($c[.].body | startswith("Not changed:")))] | last) as $i
      | ($i == null or any($c[$i + 1:][]; (.author | norm) != "github-actions"))
        and ($c[-1] | (.author | norm) == "github-actions" and (.body | startswith("Verified:")) | not)))' <"$1"
}

command_gate() {
  jq -r '
    select(.issue.pull_request != null
      and (.comment.author_association | IN("OWNER", "MEMBER", "COLLABORATOR")))
    | .comment.body | split("\n")[0] | rtrimstr("\r")
    | if . == "/ai-review stop" then "stop" elif . == "/ai-review more" then "more" else empty end' <"$1"
}
