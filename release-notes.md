## Breaking changes:

- EvaluationContext.WithResourceOverrides() introduced in 5.10 is refactored to now be an extension method instead of a static construction method. It should now be called on an instance of EvaluationContext, and will mutate and return that instance.
