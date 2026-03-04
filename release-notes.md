## Intro:

FHIRPath CompiledExpression will now accept 0..* resources as input, skipping the requirement for a dummy resource in case an expression does not use it.  
ModelInspector now will protect from cross version contamination of the ClassMappings, which will prevent type resolution errors that could occur when using multiple versions of the library in the same process.  
Improved terminology stack by providing more complete base class that simplifies the implementation and standardizes error handling.  
Improved error handling for Poco validation & serialization, setting an MemberName property for easier debugging.
Minor nullability tweaks
