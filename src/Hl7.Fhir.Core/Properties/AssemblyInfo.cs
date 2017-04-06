using System;
using System.Reflection;
using System.Runtime.CompilerServices;

// This assembly is not fully CLSCompliant, but this triggers compiler warnings to avoid the issue as described here, when mixing C# and VB 
// https://msdn.microsoft.com/en-us/library/ms235408(v=vs.90).aspx 
[assembly: CLSCompliant(true)]

#if DEBUG
[assembly: InternalsVisibleTo("Hl7.Fhir.Core.Tests")]
#endif
