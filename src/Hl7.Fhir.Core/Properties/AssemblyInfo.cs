using System;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Hl7.Fhir.DSTU2.Core")]
[assembly: AssemblyDescription("Core .NET support for working with HL7 FHIR. Supports FHIR DSTU2 (1.0)")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyProduct("FHIR")]
[assembly: AssemblyCopyright("Copyright Ewout Kramer and collaborators 2015")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: NeutralResourcesLanguage("en")]
[assembly: ComVisible(false)]

#if DEBUG
[assembly: InternalsVisibleTo("Hl7.Fhir.Core.Tests")]
#endif

#if RELEASE
[assembly:AssemblyKeyFileAttribute("..\\FhirNetApi.snk")]
#endif

[assembly: CLSCompliant(true)]

[assembly: AssemblyVersion("0.90.4.*")]
