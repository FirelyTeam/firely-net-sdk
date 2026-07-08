## Intro:

This release includes a behavioral change in parsing alongside a minor bug fix. Please read the notes below carefully before upgrading.

**POCO / type system**
- The SDK now detects incorrectly cased element and resource type names. Two new error codes are introduced: `PVAL131` (`WRONG_CASED_ELEMENT`) and `PVAL132` (`WRONG_CASED_RESOURCE_TYPE`).

  > **Breaking change:** Parsers running in strict mode will now report errors when receiving data with incorrectly cased element or resource type names. Data that was previously silently corrected during parsing will now cause parse errors instead. Make sure your data sources use the correct casing as defined by the FHIR specification. All non-strict modes (BackwardsCompatible, Recoverable, NoOverflow, and the default) continue to bind leniently, preserving the behaviour from previous SDK versions. The exact handling of wrong-cased names in lenient modes is subject to change in future major versions of the SDK.

**Other fixes**
- Fixed redundant null-checks and initialization logic in bundle link handling.
