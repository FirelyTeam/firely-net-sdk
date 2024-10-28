## Breaking changes:

- EvaluationContext.WithResourceOverrides() introduced in 5.10 is refactored to now be an extension method instead of a static construction method. It should now be called on an instance of EvaluationContext, and will mutate and return that instance.
- We changed the datatype of the Attachment.Url from FhirUrl to FhirUri. The type of this element was changed with the introduction of R4. (FhirUrl doesn't exist in STU3). When we moved Attachment to base, we wrongfully put FhirUrl here, which is the more specific datatype of the two. We have corrected this.
