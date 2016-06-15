using System.Reflection;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Hl7.Fhir.Specification.Tests")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Hl7.Fhir.Specification.Tests")]
[assembly: AssemblyCopyright("Copyright © Ewout Kramer and collaborators 2016")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

[assembly: AssemblyVersion("1.0.*")]    // Stick test version to always 1.0, i.e. "pseudo-strong naming"

// Sign this for trusted friendship to tested Assembly. See: https://msdn.microsoft.com/en-us/library/bb385180.aspx
#if SIGNED
[assembly: AssemblyKeyFileAttribute("..\\FhirNetApi.snk")]
#endif
