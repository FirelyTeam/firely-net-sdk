using System.Reflection;
using System.Runtime.CompilerServices;

// Version information, see:
// http://blogs.msdn.com/b/jjameson/archive/2009/04/03/best-practices-for-net-assembly-versioning.aspx
[assembly: AssemblyVersion("0.91.0.3")]
[assembly: AssemblyFileVersion("0.91.0.3")]
[assembly: AssemblyInformationalVersion("Hl7.Fhir .Net Library 0.91.0-alpha3")]

#if !SIGNED
[assembly: InternalsVisibleTo("Hl7.Fhir.Core.Tests")]
#else
// Enable execution of unit tests on Release Assembly. See: https://msdn.microsoft.com/en-us/library/0tke9fxk.aspx
// Sign with a confidential key. Make internals visible to unit tests signed with a unit-test key.
[assembly:AssemblyKeyFileAttribute("..\\FhirNetApi.snk")]
[assembly: InternalsVisibleTo("Hl7.Fhir.Core.Tests, PublicKey=00240000048000009400000006020000002400005253413100040000010001001717D77343870ECA52515A2FF9BA7EF2FF314F2F1E651F4A069401E35193D4D5124B33379A6380D510239044F012F720D395064192157EAE8F67B3E4D524B79DAADEBD4E65CE67DB327949B77BF26CA6C0F97C4CA1A578811202A537E4D112FFFB2E42E852AFDD71C3295C694911CDF0535F709B72BA172C40A2B1F2B607FFDC")]
#endif
