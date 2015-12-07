using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Hl7.Fhir.DSTU2.Specification")]
[assembly: AssemblyDescription(".NET additional support for working with HL7 FHIR. Supports FHIR DSTU2 (1.0).")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyProduct("Hl7.Fhir.Specification")]
[assembly: AssemblyCopyright("Copyright © Ewout Kramer and collaborators 2015")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: NeutralResourcesLanguage("en")]
[assembly: ComVisible(false)]
//[assembly: System.CLSCompliant(true)]
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("0.90.4.*")]

#if DEBUG
[assembly: InternalsVisibleTo("Hl7.Fhir.Specification.Tests")]
#endif

#if RELEASE
[assembly:AssemblyKeyFileAttribute("..\\FhirNetApi.snk")]
#endif