# InterfaceApplier
This is a tool meant to be ran after FHIR models have been generated. It will let classes, respecting the interface definition (properties and methods), extend this interface.

## Configuration
The built application can be configured using command line parameters. The parameter consists of `--{property-name}={property-value}`.

|Parameter|IsRequired|Description|Default|Example|
|-|:-:|-|-|-|
|SourcesDirectory|x|The absolute or relative path to the `src` directory containing all sources|-|`--SourcesDirectory=../../src`|
|FhirVersions|x|1-n versions, separated by &#124;, to apply the interfaces to. When writing this document following versions are availabel: R4, R4B, R5, STU5|-|<code>--FhirVersions=R4&#124;R4B&#124;R5&#124;STU3</code>|
|LogLevel| |The minimal log level messages to log. Available [log levels](https://github.com/serilog/serilog/wiki/Configuration-Basics#minimum-level)|`Information`|`--LogToFile=Debug`|
|LogToFile| |If `true`, a log file will be written in `{executing-directory}/logs`|`false`|`--LogToFile=true`| 

## How it works
Each version of the FHIR models, specified by the `FhirVersions` command line parameter, are being processed independantly, sequentially.

Steps:
1. Publish the project in debug mode into directory `bin/InterfaceApplier`
2. Load all published assemblies
3. Discover all interfaces marked with attribute `[ApplyInterfaceToGeneratedClasses]`
4. Discover all classes the discovered interfaces can be applied to
5. Let the classes extend the interface
6. Save all modified source code files to disk

## Development
To develop/debug the InterfaceApplier it is recommended to open the code from the solution file contained in the project directory. As running the application modifies the source code of the generated FHIR models during runtime, debugging won't be possible and VS will abort debugging.