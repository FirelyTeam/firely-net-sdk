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
[assembly: System.CLSCompliant(true)]
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
[assembly: AssemblyVersion("0.90.6.*")]

#if DEBUG
[assembly: InternalsVisibleTo("Hl7.Fhir.Specification.Tests")]
#else
// Enable execution of unit tests on Release Assembly. See: https://msdn.microsoft.com/en-us/library/0tke9fxk.aspx
[assembly:AssemblyKeyFileAttribute("..\\FhirNetApi.snk")]
[assembly: InternalsVisibleTo("Hl7.Fhir.Specification.Tests, PublicKey=0024000004800000940000000602000000240000525341310004000001000100e3cb9ef553365575d717b5d5b4d3b3288a1756faa3a3f52871fbb1b0959b5e4876aa7f9cfbb12185c0c02ec5b27e095611dd487de73347cdd2849183ed1dfbe04dbc07719ba3aa38003b5ae5bae5d132e1ce0945e131a4a1fa283c9bea07f2a56c237713138eb34b5e37ee8d242d8e16abefed5250535e8aa22e123ba71d2ad1")]
#endif