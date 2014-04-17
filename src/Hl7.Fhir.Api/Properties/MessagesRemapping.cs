using System.Resources;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Hl7.Fhir.Api.Properties
{
	/// <summary>
	/// This class is simply here to permit the "sharing"
	/// of the same code between the Portable API and the dotnet 4.0 API.
	/// The PCL library has a different code generator that messes things
	/// up for the regular library, so 2 messages files are required.
	/// Unfortunately we are left with 2 messages resources files that need
	/// to have their content copied between them, not ideal.
	/// But at least this file ensures that there are no "code changes"
	/// required
	/// </summary>
	internal class Messages : Messages45
	{
	}
}
