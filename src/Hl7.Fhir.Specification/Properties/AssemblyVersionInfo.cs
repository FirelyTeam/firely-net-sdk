using System.Reflection;
using System.Runtime.CompilerServices;

// Version information, see:
// http://blogs.msdn.com/b/jjameson/archive/2009/04/03/best-practices-for-net-assembly-versioning.aspx
[assembly: AssemblyVersion("0.90.5.0")]
[assembly: AssemblyFileVersion("0.90.5.0")]
[assembly: AssemblyInformationalVersion("Hl7.Fhir .Net Library 0.90.5-ConnectathonMay2016")]

#if !SIGNED
[assembly: InternalsVisibleTo("Hl7.Fhir.Specification.Tests")]
#else
// Enable execution of unit tests on Release Assembly. See: https://msdn.microsoft.com/en-us/library/0tke9fxk.aspx
[assembly:AssemblyKeyFileAttribute("..\\FhirNetApi.snk")]
[assembly: InternalsVisibleTo("Hl7.Fhir.Specification.Tests, PublicKey=00240000048000009400000006020000002400005253413100040000010001001D2AEF340F7477EC77CC2C7A25283D3AB9A6A22D90EF9A9FC137B30CB022024580E78078971087B0B0DFE21EE72661BEE667A4B6E869CB02F12E0B94EE45DC80F6BEB694E604D69BD612522AA084136C232ACC72E3236D442E7945B935BE7EBE734B0665F4B7A05F0CCEE78D5D493E9233F231F98B13800F16BC533FF758EF8D")]
#endif
