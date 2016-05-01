using System;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Security;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
#if DOTNET
[assembly: AssemblyTitle("Hl7.Fhir.DSTU2.Core Portable dotnet")]
#elif PORTABLE45
[assembly: AssemblyTitle("Hl7.Fhir.DSTU2.Core Portable .Net 4.5")]
#elif NET45
[assembly: AssemblyTitle("Hl7.Fhir.DSTU2.Core .Net 4.5")]
#elif NET40
[assembly: AssemblyTitle("Hl7.Fhir.DSTU2.Core .Net 4.0")]
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


// This assembly is not fully CLSCompliant, but this triggers compiler warnings to avoid the issue as described here, when mixing C# and VB
// https://msdn.microsoft.com/en-us/library/ms235408(v=vs.90).aspx
[assembly: CLSCompliant(true)]

// Version information is contained in separate file, see:
// http://blogs.msdn.com/b/jjameson/archive/2009/04/03/best-practices-for-net-assembly-versioning.aspx
