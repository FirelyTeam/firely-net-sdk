using System.Reflection;
using System.Runtime.CompilerServices;

// Version information, see:
// http://blogs.msdn.com/b/jjameson/archive/2009/04/03/best-practices-for-net-assembly-versioning.aspx
[assembly: AssemblyVersion("0.90.5.0")]
[assembly: AssemblyFileVersion("0.90.5.0")]
[assembly: AssemblyInformationalVersion("Hl7.Fhir .Net Library 0.90.5-alpha2")]

#if !SIGNED
[assembly: InternalsVisibleTo("Hl7.Fhir.Core.Tests")]
#else
// Enable execution of unit tests on Release Assembly. See: https://msdn.microsoft.com/en-us/library/0tke9fxk.aspx
// Sign with a confidential key. Make internals visible to unit tests signed with a unit-test key.
[assembly:AssemblyKeyFileAttribute("..\\FhirNetApi.snk")]
[assembly: InternalsVisibleTo("Hl7.Fhir.Core.Tests, PublicKey=0024000004800000940000000602000000240000525341310004000001000100B9449B4EC19525BF5FF03AAEBE6E2E2CCD20B5CC7D7D16D59B129335AA55EFC0F04F4E3DDAF9BE8283E644D1BCB68C4EAD9B810543F526D43A95E7A9BD8583DD759BC08978302F9C2208C1828F03F4A09C9D49387F0FCC416BCBF638679B3985DC6B082497164A07B951A2CB9BFC8CB6539F97C90D9064CDDD193334B0B8D1BE")]
#endif
