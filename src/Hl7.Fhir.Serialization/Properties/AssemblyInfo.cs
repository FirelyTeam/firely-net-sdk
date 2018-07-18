using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// The following GUID is for the ID of the typelib if this project is exposed to COM

[assembly: CLSCompliant(true)]

#if DEBUG
[assembly: InternalsVisibleTo("Hl7.Fhir.Serialization.Tests")]
#endif