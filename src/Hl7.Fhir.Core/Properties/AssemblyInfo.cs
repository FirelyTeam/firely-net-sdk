using System;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
#if PORTABLE45
[assembly: AssemblyTitle("Hl7.Fhir.DSTU2.Core Portable .Net 4.5")]
#elif NET45
[assembly: AssemblyTitle("Hl7.Fhir.DSTU2.Core .Net 4.5")]
[assembly: AllowPartiallyTrustedCallers]
#elif NET40
[assembly: AssemblyTitle("Hl7.Fhir.DSTU2.Core .Net 4.0")]
[assembly: AllowPartiallyTrustedCallers]
#else
#error No valid constant for target framework defined (NET40, NET45, or PORTABLE45)
#endif

[assembly: AssemblyDescription("Core .NET support for working with HL7 FHIR. Supports FHIR DSTU2 (1.0)")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyProduct("FHIR")]
[assembly: AssemblyCopyright("Copyright Ewout Kramer and collaborators 2016")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: NeutralResourcesLanguage("en")]
[assembly: ComVisible(false)]

#if !SIGNED
[assembly: InternalsVisibleTo("Hl7.Fhir.Core.Tests")]
#else
// Enable execution of unit tests on Release Assembly. See: https://msdn.microsoft.com/en-us/library/0tke9fxk.aspx
// Sign with a confidential key. Make internals visible to unit tests signed with a unit-test key.
[assembly:AssemblyKeyFileAttribute("..\\FhirNetApi.snk")]
[assembly: InternalsVisibleTo("Hl7.Fhir.Core.Tests, PublicKey=0024000004800000940000000602000000240000525341310004000001000100e3cb9ef553365575d717b5d5b4d3b3288a1756faa3a3f52871fbb1b0959b5e4876aa7f9cfbb12185c0c02ec5b27e095611dd487de73347cdd2849183ed1dfbe04dbc07719ba3aa38003b5ae5bae5d132e1ce0945e131a4a1fa283c9bea07f2a56c237713138eb34b5e37ee8d242d8e16abefed5250535e8aa22e123ba71d2ad1")]
#endif

// This assembly is not fully CLSCompliant, but this triggers compiler warnings to avoid the issue as described here, when mixing C# and VB
// https://msdn.microsoft.com/en-us/library/ms235408(v=vs.90).aspx
[assembly: CLSCompliant(true)]

// Version information is contained in separate file, see:
// http://blogs.msdn.com/b/jjameson/archive/2009/04/03/best-practices-for-net-assembly-versioning.aspx
