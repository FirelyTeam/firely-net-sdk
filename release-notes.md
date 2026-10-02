## Intro:

This release makes the JSON parser reject a primitive whose value is supplied twice, makes the
`SnapshotGenerator` report differential paths with an empty segment, and fixes FHIRPath evaluation
over invalid primitive values. There are no breaking API changes, but two changes alter behaviour
or output that callers may depend on: the new fatal parser issue `JSON135`, and the text of
`OperationOutcome.IssueComponent.ToString()`. See the behavioural notes below.

**Serialization**
- The JSON parser now reports a fatal issue, `JSON135` (`PRIMITIVE_VALUE_SUPPLIED_TWICE`), when a
  primitive gets its value from both `name` and `_name`, e.g. `{"name":"John","_name":"Johnny"}`.
  The second value used to be dropped silently, with only the non-fatal `JSON134`, so the
  `NoOverflow`, `Recoverable` and `BackwardsCompatible` modes accepted the data loss. This applies in
  either property order and to elements of primitive arrays. `JSON134` stays a non-fatal error for
  a primitive under `_name` when no value was set yet. See issue [#3533](https://github.com/FirelyTeam/firely-net-sdk/issues/3533).

  > **Behavioural note:** JSON that parsed before, with a non-fatal `JSON134`, now fails to parse in
  > every mode except `Ostrich` when both `name` and `_name` carry a value.

**FHIRPath**
- An unparseable `instant`, `integer64`, `integer`, `positiveInt` or `unsignedInt` value no longer
  makes almost every FHIRPath expression that reaches it throw. Its value is now the unparsed
  string, as it already was for `date`, `dateTime` and `time`. Before, even `$this is Reference`
  threw an `InvalidOperationException` for such a node, so a sweep like
  `descendants().where($this is Reference)` failed on the whole resource. The `Value` getter of the
  POCO still throws.

**Snapshot generation**
- The `SnapshotGenerator` now reports a specific issue (`PROFILE_ELEMENTDEF_INVALID_PATH`, code
  10021) for a differential element whose path has an empty segment, such as `Observation...unit`,
  and skips that element. Such an element used to produce a phantom element `Observation.` in the
  snapshot, and any constraint on it, like a `fixedString`, was silently lost. The rest of the
  differential is still processed. See issue [#3591](https://github.com/FirelyTeam/firely-net-sdk/issues/3591).

**Other**
- `PocoNode.Resolve("#id")` no longer throws a `NullReferenceException` when the resource has a
  contained resource without an `id` before the one it refers to. A contained resource without an
  `id` does not match, and resolution continues. See issue [#3613](https://github.com/FirelyTeam/firely-net-sdk/issues/3613).
- `OperationOutcome.IssueComponent.ToString()` now writes a space before `(further diagnostics:`,
  as it already did before `(at`. See issue [#3621](https://github.com/FirelyTeam/firely-net-sdk/issues/3621).

  > **Behavioural note:** this changes the rendered text of an issue. Tests that compare against
  > `ToString()` output need new expected strings.
