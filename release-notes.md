## Intro:
Highlights of this new release:

- Added $process-message operation support  in the FHIR client. 
- Changes to the JsonSerializerOptions.
- Updates dependencies to newest versions
- Some minor fixes to the terminology services and the source node comparator

Note: From now on due to changes on the JsonSerializerOptions, inputted parameters are changed instead of returning a new instance to support service initialization better.
This change results in the fact that you cannot alter JsonSerializer options anymore after serializer or deserialization has occured.
This applies to the ForFhir(), Pretty(), and Compact() functionality.