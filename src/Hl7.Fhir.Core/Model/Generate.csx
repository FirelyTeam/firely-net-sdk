/*
    Generates the model classes from the XML structure definition files

    The XML files are in Source-XXXX sub-directories, where XXX is the version (DSTU2, STU3 etc.) 
    This script automatically generate classes for all the versions it can find. 
    
    Classes that are the same across all versions are generated in the Hl7.Fhir.Model namespace, with files in the Generated sub-directory

    Classes that are specific for a version are generated in the Hl7.Fhir.Model.XXX namespace (e.g. Hl7.Fhir.Model.DSTU2), with files in the Generated\XXX sub-directory

    To execute this script either call

        csi Generate.csx

    from the command line or execute

        #load "Generate.csx"

    from the Visual Studio C# Interactive window
*/

#r "System.Xml"

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

public static class Globals
{
    public static Dictionary<string, string> FhirDataTypeByCsType;
    public static List<string> AllVersions;
}

public static class StringUtils
{

    /// <summary>
    /// Quotes a string for output in C# source code -e.g. a\b becomes "a\\b"
    /// </summary>
    public static string Quote(string str)
    {
        return "\"" + str.Replace("\\", "\\\\").Replace("\r", "\\r").Replace("\n", "\\n").Replace("\"", "\\\"") + "\"";
    }

    /// <summary>
    /// Converts a FHIR code to a valid Pascal case C# enumeration value - e.g. entered-in-error becomes EnteredInError
    /// </summary>
    public static string ConvertEnumValue(string code)
    {
        if (code == "=")
            return "Equal";
        if (code == "!=")
            return "NotEqual";
        if (code == "<")
            return "LessThan";
        if (code == "<=")
            return "LessOrEqual";
        if (code == ">=")
            return "GreaterOrEqual";
        if (code == ">")
            return "GreaterThan";
        if (code.StartsWith("_"))
            code = code.Substring(1);
        code = code.Replace('.', '_');
        var bits = code.Split(new char[] { ' ', '-' });
        var result = string.Empty;
        foreach (var bit in bits)
        {
            result += bit.Substring(0, 1).ToUpper();
            result += bit.Substring(1);
        }
        if (char.IsDigit(result[0]))
            result = "N" + result;
        return result;
    }

    public static string FirstToUpper(string str)
    {
        if (string.IsNullOrEmpty(str)) return str;
        return str.Substring(0, 1).ToUpper() + str.Substring(1);
    }

    /// <summary>
    /// Remove the namespace part of fully-qualified code types - i.e. from Hl7.Fhir.Mode.Code&lt;XXX&gt; to Code&lt;XXX&gt; 
    /// All other types are unchanged
    /// </summary>
    public static string RemoveCodeNamespace(string type)
    {
        return type.Replace("Hl7.Fhir.Model.Code<", "Code<");
    }

    public static string ToInterfaceName(string className)
    {
        return "I" + className;
    }

    public const string ModelNamespacePrefix = "Hl7.Fhir.Model.";

    public static bool TryGetModelClassName(string type, out string className)
    {
        if (type.StartsWith(ModelNamespacePrefix))
        {
            className = type.Substring(ModelNamespacePrefix.Length);
            if (className.StartsWith("Code<"))
            {
                className = "Code";
            }
            return true;
        }
        className = null;
        return false;
    }

    /// <summary>
    /// Fix the FHIR version of a type - e.g. converts Hl7.Fhir.Model.Patient to Hl7.FhirModel.DSTU2.Patient because Patient is version-specific and 
    /// leaves Hl7.Fhir.Model.Resource 'as is' because Resource is common to all versions
    /// </summary>
    /// <param name="type">The type to fix</param>
    /// <param name="version">The target FHIR version</param>
    /// <param name="resourcesByName">All FHIR resources that are specific to the target FHIR version, indexed by their name ('Patient', 'Encounter' etc)</param>
    /// <returns>Fixed FHIR type</returns>
    public static string FixTypeFhirVersion(string type, string version, Dictionary<string, ResourceDetails> resourcesByName)
    {
        var isVersionSpecific = TryGetModelClassName(type, out var className) && resourcesByName.ContainsKey(className);
        if (isVersionSpecific)
        {
            return ModelNamespacePrefix + version + "." + className;
        }
        return type;
    }

    /// <summary>
    /// Render a string as a C# 'summary' comment. New line characters in the string create separate comment lines
    /// </summary>
    /// <returns>Summary comment lines</returns>
    public static IEnumerable<string> RenderSummary(string comment)
    {
        if (!string.IsNullOrEmpty(comment))
        {
            yield return "/// <summary>";
            foreach (var line in ConvertToComment(comment)) yield return line;
            yield return "/// </summary>";
        }
    }

    /// <summary>
    /// Convert a string to a C# comment. New line characters in the string create separate comment lines
    /// </summary>
    /// <returns>Comment lines</returns>
    public static IEnumerable<string> ConvertToComment(string comment)
    {
        if (string.IsNullOrEmpty(comment)) return Enumerable.Empty<string>();
        return comment
            .Replace("\r", "\n")
            .Replace("\n\n", "\n")
            .Replace("&", "&amp;")
            .Replace("<", "&lt;")
            .Replace(">", "&gt;")
            .Replace("\n", "<br/>\n")
            .Split('\n')
            .Select(l => "/// " + l);
    }

    /// <summary>
    /// Renders (i.e. generates) the standard initial part of a generated C# source file: usings, initial comment with copyrights etc., namespace declaration
    /// </summary>
    /// <param name="versions">The loaded data of the FHIR version or versions the C# source file applies to: 
    /// it can be a single element = the file applies to that one specific version and so the namespace is Hl7.Fhir.Model.[specific version],
    /// or more elements = the file applies to all versions (e.g. contains classes that are shared by multiple FHIR versions) and the namespace is Hl7.Fhir.Model</param>
    /// <returns>Initial lines of the C# source file (up to and including the namespace declaration)</returns>
    public static IEnumerable<string> RenderFileHeader(IEnumerable<LoadedVersion> versions)
    {
        var header = @"using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using Hl7.Fhir.Utility;

/*
    Copyright (c) 2011+, HL7, Inc.
    All rights reserved.

    Redistribution and use in source and binary forms, with or without modification, 
    are permitted provided that the following conditions are met:

    * Redistributions of source code must retain the above copyright notice, this 
        list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright notice, 
        this list of conditions and the following disclaimer in the documentation 
        and/or other materials provided with the distribution.
    * Neither the name of HL7 nor the names of its contributors may be used to 
        endorse or promote products derived from this software without specific 
        prior written permission.

    THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS ""AS IS"" AND 
    ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
    WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
    IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
    INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
    NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
    PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
    WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
    ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
    POSSIBILITY OF SUCH DAMAGE.


*/
#pragma warning disable 1591 // suppress XML summary warnings";
        foreach (var line in header.Split('\r')) yield return line.Replace("\n", string.Empty);
        var fhirVersions = string.Join(", ", versions.Select(v => "v" + v.FhirVersion));
        var dotVersion = string.Empty;
        if (versions.Count() == 1)
        {
            dotVersion = "." + versions.First().Version;
        }
        yield return string.Empty;
        yield return $"//";
        yield return $"// Generated for FHIR {fhirVersions}";
        yield return $"//";
        yield return $"namespace Hl7.Fhir.Model{dotVersion}";
        yield return $"{{";
    }

    /// <summary>
    /// Render the C# code of a list of properties
    /// </summary>
    /// <param name="nPropNum">Initial property number - 10, used to set FhirElementAttribute.Order</param>
    /// <param name="properties">The resource, data type or component properties</param>
    /// <returns>Lines of C# code</returns>
    public static IEnumerable<string> RenderProperties(int nPropNum, IEnumerable<PropertyDetails> properties)
    {
        foreach (var property in properties)
        {
            nPropNum += 10;
            yield return string.Empty;
            foreach (var line in property.Render(nPropNum)) yield return line;
        }
    }

    /// <summary>
    /// Renders the properties as explicit interface member when needed
    /// </summary>
    /// <param name="rootInterfaceName">The root interface name - eg IPatient ('root' because it is the same also if the property belongs to a component
    /// and not to the resource itself)</param>
    /// <param name="properties">The properties to render</param>
    /// <param name="components">Componens declared by the resource containing this property</param>
    /// <returns>Code lines</returns>
    public static IEnumerable<string> RenderInterfaceProperties(string rootInterfaceName, List<PropertyDetails> properties, List<ComponentDetails> components)
    {
        foreach (var property in properties)
        {
            foreach (var line in property.RenderAsInterfaceIfNeeded(rootInterfaceName, components)) yield return line;
        }
    }

    public static IEnumerable<string> RenderSerialize(string type, bool abstractType, bool dataType, IEnumerable<PropertyDetails> properties)
    {
        yield return $"internal override void Serialize(Serialization.SerializerSink sink)";
        yield return $"{{";
        if (!abstractType)
        {
            if (dataType)
            {
                yield return $"    sink.BeginDataType(\"{type}\");";
            }
            else
            {
                yield return $"    sink.BeginResource(\"{type}\");";
            }
        }
        yield return $"    base.Serialize(sink);";
        foreach(var property in properties)
        {
            foreach (var line in property.RenderSerialize()) yield return "    " + line;
        }
        if (!abstractType)
        {
            yield return $"    sink.End();";
        }
        yield return $"}}";
    }

    public static IEnumerable<string> RenderSetElementFromJson(IEnumerable<PropertyDetails> properties)
    {
        yield return $"internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)";
        yield return $"{{";
        yield return $"    if (base.SetElementFromJson(jsonPropertyName, ref source))";
        yield return $"    {{";
        yield return $"        return true;";
        yield return $"    }}";
        yield return $"    switch (jsonPropertyName)";
        yield return $"    {{";
        var hasLists = false;
        foreach (var property in properties)
        {
            if (property.IsMultiCard())
            {
                hasLists = true;
            }
            foreach (var line in property.RenderSetElementFromJson()) yield return "        " + line;
        }
        yield return $"    }}";
        yield return $"    return false;";
        yield return $"}}";
        if (hasLists)
        {
            yield return String.Empty;
            yield return $"internal override bool SetListElementFromJson(string jsonPropertyName, int index, ref Serialization.JsonSource source)";
            yield return $"{{";
            yield return $"    if (base.SetListElementFromJson(jsonPropertyName, index, ref source))";
            yield return $"    {{";
            yield return $"        return true;";
            yield return $"    }}";
            yield return $"    switch (jsonPropertyName)";
            yield return $"    {{";
            foreach (var property in properties)
            {
                if (property.IsMultiCard())
                {
                    foreach (var line in property.RenderSetListElementFromJson()) yield return "        " + line;
                }
            }
            yield return $"    }}";
            yield return $"    return false;";
            yield return $"}}";
        }
    }

    /// <summary>
    /// Renders the Children and NamedChildren methods, that return all the properties of a FHIR resource or data type class
    /// as enumeration of Base and ElementValue objects respectively 
    /// </summary>
    /// <param name="properties">The properties of the FHIR resource or data type class, that are going to be returned by the Children and NamedChildren methods</param>
    /// <returns>Lines of C# code of the Children and NamedChildren methods</returns>
    public static IEnumerable<string> RenderChildrenMethods(IEnumerable<PropertyDetails> properties)
    {
        yield return $"[NotMapped]";
        yield return $"public override IEnumerable<Base> Children";
        yield return $"{{";
        yield return $"    get";
        yield return $"    {{";
        yield return $"        foreach (var item in base.Children) yield return item;";
        foreach (var pd in properties)
        {
            foreach (var line in pd.RenderAsChildWithoutName()) yield return "        " + line;
        }
        yield return $"    }}";
        yield return $"}}";
        yield return string.Empty;
        yield return $"[NotMapped]";
        yield return $"internal override IEnumerable<ElementValue> NamedChildren";
        yield return $"{{";
        yield return $"    get";
        yield return $"    {{";
        yield return $"        foreach (var item in base.NamedChildren) yield return item;";
        foreach (var pd in properties)
        {
            foreach (var line in pd.RenderAsChildWithName()) yield return "        " + line;
        }
        yield return $"    }}";
        yield return $"}}";
    }

    /// <summary>
    /// Renders the copy (CopyTo, DeepCopy) and comparison (Matches, IsExactly) methods of a FHIR resource or data type class
    /// </summary>
    /// <param name="type">The FHIR resource or data type class type (e.g. Patient)</param>
    /// <param name="abstractType">True if it is an abstract type (e.g. DomainResource)</param>
    /// <param name="properties">The properties of the FHIR resource or data type class, that are copied or compared by the generate methods</param>
    /// <returns>Lines of C# code of the copy and comparison methods</returns>
    public static IEnumerable<string> RenderCopyAndComparisonMethods(string type, bool abstractType, IEnumerable<PropertyDetails> properties)
    {
        foreach (var line in RenderCopyTo(type, properties)) yield return line;
        if (!abstractType)
        {
            yield return string.Empty;
            foreach (var line in RenderDeepCopy(type)) yield return line;
        }
        yield return string.Empty;
        foreach (var line in RenderMatches(type, properties)) yield return line;
        yield return string.Empty;
        foreach (var line in RenderIsExactly(type, properties)) yield return line;
    }

    public static IEnumerable<string> RenderDeepCopy(string type)
    {
        yield return $"public override IDeepCopyable DeepCopy()";
        yield return $"{{";
        yield return $"     return CopyTo(new { type }());";
        yield return $"}}";
    }

    private static IEnumerable<string> RenderCopyTo(string type, IEnumerable<PropertyDetails> properties)
    {
        yield return $"public override IDeepCopyable CopyTo(IDeepCopyable other)";
        yield return $"{{";
        yield return $"    var dest = other as { type };";
        yield return string.Empty;
        yield return $"    if (dest != null)";
        yield return $"    {{";
        yield return $"        base.CopyTo(dest);";
	    foreach (var pd in properties)
        {
            if (pd.CardMax == "*")
                yield return $"        if({pd.Name} != null) dest.{pd.Name} = new List<{pd.ConvertedPropType()}>({pd.Name}.DeepCopy());";
            else
                yield return $"        if({pd.Name} != null) dest.{pd.Name} = ({pd.ConvertedPropType()}){pd.Name}.DeepCopy();";
        }
        yield return $"        return dest;";
        yield return $"    }}";
        yield return $"    else";
        yield return $"        throw new ArgumentException(\"Can only copy to an object of the same type\", \"other\");";
        yield return $"}}";
    }

    private static IEnumerable<string> RenderMatches(string type, IEnumerable<PropertyDetails> properties)
    {
        yield return $"public override bool Matches(IDeepComparable other)";
        yield return $"{{";
        yield return $"    var otherT = other as { type };";
        yield return $"    if(otherT == null) return false;";
        yield return string.Empty;
        yield return $"    if(!base.Matches(otherT)) return false;";
	    foreach (var pd in properties)
        {
            if (pd.CardMax == "*")
                yield return $"    if( !DeepComparable.Matches({pd.Name}, otherT.{pd.Name})) return false;";
            else
                yield return $"    if( !DeepComparable.Matches({pd.Name}, otherT.{pd.Name})) return false;";
        }
        yield return string.Empty;
        yield return $"    return true;";
        yield return $"}}";
    }

    private static IEnumerable<string> RenderIsExactly(string type, IEnumerable<PropertyDetails> properties)
    {
        yield return $"public override bool IsExactly(IDeepComparable other)";
        yield return $"{{";
        yield return $"    var otherT = other as { type };";
        yield return $"    if(otherT == null) return false;";
        yield return string.Empty;
        yield return $"    if(!base.IsExactly(otherT)) return false;";
        foreach (var pd in properties)
        {
            yield return $"    if( !DeepComparable.IsExactly({pd.Name}, otherT.{pd.Name})) return false;";
        }
        yield return string.Empty;
        yield return $"    return true;";
        yield return $"}}";
    }
}

public class PrimitiveType
{
    public PrimitiveType(string fhirName, string className, string nativeType)
    {
        FhirName = fhirName;
        ClassName = className;
        NativeType = nativeType;
    }

    public string FhirName { get; }
    public string ClassName { get; }
    public string NativeType { get; }

    public static PrimitiveType Get(string fhirName)
    {
        return _primitiveTypes.FirstOrDefault(n => n.FhirName == fhirName);
    }

    private static PrimitiveType[] _primitiveTypes = new PrimitiveType[]
    {
        new PrimitiveType("id", "Id", "string"),
        new PrimitiveType("code", "Code", "string" ),
        new PrimitiveType("oid", "Oid", "string"),
        new PrimitiveType("uuid", "Uuid", "string"),
        new PrimitiveType("uri", "FhirUri", "string"),
        new PrimitiveType("url", "Url", "string"),
        new PrimitiveType("canonical", "Canonical", "string"),
        new PrimitiveType("boolean", "FhirBoolean", "bool?"),
        new PrimitiveType("dateTime", "FhirDateTime", "string"),
        new PrimitiveType("date", "Date", "string"),
        new PrimitiveType("time", "Time", "string"),
        new PrimitiveType("base64Binary", "Base64Binary", "byte[]"),
        new PrimitiveType("decimal", "FhirDecimal", "decimal?"),
        new PrimitiveType("markdown", "Markdown", "string"),
        new PrimitiveType("xhtml", "XHtml", "string"),
        new PrimitiveType("instant", "Instant", "DateTimeOffset?"),
        new PrimitiveType("integer", "Integer", "int?"),
        new PrimitiveType("unsignedInt", "UnsignedInt", "int?"),
        new PrimitiveType("positiveInt", "PositiveInt", "int?"),
        new PrimitiveType("string", "FhirString", "string")
    };
}

/// <summary>
/// The complete XML structure definition data of a specific FHIR version
/// </summary>
public class LoadedVersion
{
    /// <summary>
    /// The FHIR version: 'DSTU2', 'STU3' etc.
    /// </summary>
    public string Version;

    /// <summary>
    /// Content of 'profile-resources.xml': all resources definitions (including the abstract ones like Resource, DomainResource)
    /// </summary>
    public XmlDocument Resources;
    /// <summary>
    /// Namespace manager associated with the Resources XML document
    /// </summary>
    public XmlNamespaceManager NSR;

    /// <summary>
    /// Content of 'expansions.xml':  expansions for all the value sets that are used on an element of type 'code'
    /// </summary>
    public XmlDocument Expansions;
    /// <summary>
    /// Namespace manager associated with the Expansions XML document
    /// </summary>
    public XmlNamespaceManager NSE;

    /// <summary>
    /// Content of 'profile-types.xml': all types definitions - both primitive (e.g. dateTime) and composite (e.g. CodeableConcept)
    /// </summary>
    public XmlDocument Types;
    /// <summary>
    /// Namespace manager associated with the Types XML document
    /// </summary>
    public XmlNamespaceManager NST;

    /// <summary>
    /// Content of 'search-parameters.xml': standard search parameters definitons for all resources
    /// </summary>
    public XmlDocument SearchParameters;
    /// <summary>
    /// Namespace manager associated with the SearchParameters XML document
    /// </summary>
    public XmlNamespaceManager NSSP;

    /// <summary>
    /// The FHIR numeric version: '1.0.2', '3.0.1' etc.
    /// </summary>
    public string FhirVersion;

    /// <summary>
    /// Load the data for all available FHIR versions, scanning the 'Source-XXXX' sub-directories of the specified root directory
    /// </summary>
    /// <returns>List of loaded FHIR versions data</returns>
    public static List<LoadedVersion> LoadAll(string rootDirectory)
    {
        var sourceDirectories = Directory.GetDirectories(rootDirectory)
            .Where(dir => Path.GetFileName(dir).StartsWith(SourceDirectoryPrefix, StringComparison.CurrentCultureIgnoreCase));
        var result = new List<LoadedVersion>();
        foreach (var sourceDirectory in sourceDirectories)
        {
            result.Add(Load(sourceDirectory));
        }
        return result;
    }

    private const string SourceDirectoryPrefix = "Source-";

    private static LoadedVersion Load(string sourceDirectory)
    {
        var result = new LoadedVersion();

        result.Version = Path.GetFileName(sourceDirectory).Substring(SourceDirectoryPrefix.Length);

        result.Resources = new XmlDocument();
        result.Resources.Load(Path.Combine(sourceDirectory, "profiles-resources.xml"));
        result.NSR = new XmlNamespaceManager(result.Resources.NameTable);
        result.NSR.AddNamespace("fhir", "http://hl7.org/fhir");

        result.Expansions = new XmlDocument();
        result.Expansions.Load(Path.Combine(sourceDirectory, "expansions.xml"));
        result.NSE = new XmlNamespaceManager(result.Expansions.NameTable);
        result.NSE.AddNamespace("fhir", "http://hl7.org/fhir");

        result.Types = new XmlDocument();
        result.Types.Load(Path.Combine(sourceDirectory, "profiles-types.xml"));
        result.NST = new XmlNamespaceManager(result.Types.NameTable);
        result.NST.AddNamespace("fhir", "http://hl7.org/fhir");

        result.SearchParameters = new XmlDocument();
        result.SearchParameters.Load(Path.Combine(sourceDirectory, "search-parameters.xml"));
        result.NSSP = new XmlNamespaceManager(result.SearchParameters.NameTable);
        result.NSSP.AddNamespace("fhir", "http://hl7.org/fhir");

        result.FhirVersion = result.Resources.SelectSingleNode("//fhir:fhirVersion/@value", result.NSE).Value;

        return result;
    }
}

/// <summary>
/// Description of a FHIR value set - and of the corresponding C# enumeration
/// </summary>
public class ValueSet
{
    /// <summary>
    /// The C# enumeration name - e.g. AllergyIntoleranceStatus 
    /// </summary>
    public string EnumName;

    /// <summary>
    /// True if the C# enumeration should be marked as [Flags]
    /// </summary>
    public bool IsFlags;

    /// <summary>
    /// URL uniquely identifying the FHIR value set - e.g. http://hl7.org/fhir/ValueSet/allergy-intolerance-status
    /// </summary>
    public string Url;

    /// <summary>
    /// Value set / enumeration description - e.g. 'Assertion about certainty associated with a propensity, or potential risk, of a reaction to the identified Substance'
    /// </summary>
    public string Description;

    /// <summary>
    /// All values contained in the value set, corresponding to the C# enumeration values
    /// </summary>
    public List<ValueSetValue> Values;

    /// <summary>
    /// Checks if this value set is the same - i.e. correspond to an identical C# enumeration - as another one
    /// </summary>
    public bool IsSame(ValueSet other)
    {
        return other != null &&
            EnumName == other.EnumName &&
            GetSortedCodesString() == other.GetSortedCodesString();
    }

    private string GetSortedCodesString()
    {
        if (Values == null)
        {
            return string.Empty;
        }
        return string.Join(", ", Values.Select(v => v.Code).OrderBy(c => c));
    }

    /// <summary>
    /// Renders (generate) the C# code of the value set enumeration
    /// </summary>
    public IEnumerable<string> Render()
    {
        if (!string.IsNullOrEmpty(Description) || !string.IsNullOrEmpty(Url))
        {
            yield return $"/// <summary>";
            foreach (var line in StringUtils.ConvertToComment(Description)) yield return line;
            if (!string.IsNullOrEmpty(Url))
            {
                yield return $"/// (url: { Url })";
            }
        }
        yield return $"/// </summary>";
        yield return $"[FhirEnumeration(\"{ EnumName }\")]";
        if (IsFlags)
        {
            yield return $"[Flags]";
        }
        yield return $"public enum { EnumName }";
        yield return $"{{";
        foreach (var value in Values)
        {
            foreach (var line in value.Render()) yield return "    " + line;
        }
        yield return $"}}";
    }

    /// <summary>
    /// Writes the C# enumerations of a list of value sets to a file
    /// </summary>
    /// <param name="filePath">Path of the destination file, that is overwritten if it already exists</param>
    /// <param name="valueSets">Value sets to write</param>
    /// <param name="versions">The loaded data of the FHIR version or versions the values sets belong to: 
    /// it can be a single element = the value sets belong to that one specific version (and so their namespace is Hl7.Fhir.Model.[specific version]),
    /// or more elements = the value sets belong to all versions (e.g. and so their namespace is Hl7.Fhir.Model</param>
    public static void Write(string filePath, IEnumerable<ValueSet> valueSets, IEnumerable<LoadedVersion> versions)
    {
        using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
        {
            foreach (var line in StringUtils.RenderFileHeader(versions)) writer.WriteLine(line);
            foreach (var valueSet in valueSets)
            {
                writer.WriteLine();
                foreach (var line in valueSet.Render()) writer.WriteLine("    " + line);
            }
            writer.WriteLine();
            writer.WriteLine("}");
        }
    }

    /// <summary>
    /// Load all the value sets from the XML structure definition data of a set of FHIR versions
    /// </summary>
    /// <param name="loadedVersions">The FHIR versions XML structure definition data</param>
    /// <returns>Newly created value sets by FHIR version and by URL, i.e. a dictionary indexed by FHIR version, 
    /// with an empty string key representing value sets common to all version and 'DSTU2', 'STU3' etc representing the specific version; 
    /// each value is a dictionary indexed by the value set URL with the value sets as values</returns>
    public static Dictionary<string, Dictionary<string, ValueSet>> LoadAll(IEnumerable<LoadedVersion> loadedVersions)
    {
        var valueSetsByUrlByVersion = new Dictionary<string, Dictionary<string, ValueSet>>();
        foreach (var loadedVersion in loadedVersions)
        {
            var valueSetsByUrl = new Dictionary<string, ValueSet>();
            valueSetsByUrlByVersion.Add(loadedVersion.Version, valueSetsByUrl);
            var nodesResources = loadedVersion.Resources.DocumentElement.SelectNodes(
                "/fhir:Bundle/fhir:entry/fhir:resource/fhir:StructureDefinition[fhir:differential/fhir:element[fhir:type/fhir:code/@value = 'code' and fhir:binding/fhir:strength/@value='required']]", loadedVersion.NSR);
            var nodesTypesRoot = loadedVersion.Types.DocumentElement.SelectNodes(
                "/fhir:Bundle/fhir:entry/fhir:resource/fhir:StructureDefinition[fhir:differential/fhir:element[fhir:type/fhir:code/@value = 'code' and fhir:binding/fhir:strength/@value='required']]", loadedVersion.NST);
            var allElements = nodesResources.OfType<XmlElement>().Concat(nodesTypesRoot.OfType<XmlElement>());
            foreach (var element in allElements)
            {
                foreach (var eProp in element.SelectNodes("fhir:differential/fhir:element[fhir:type/fhir:code/@value = 'code' and fhir:binding]", loadedVersion.NSR).OfType<XmlElement>())
                {
                    var valueSetUrl = GetRequiredBindingValueSetUrl(eProp, loadedVersion.NSR);
                    if (valueSetUrl != null && !valueSetsByUrl.ContainsKey(valueSetUrl))
                    {
                        var valueSet = TryCreateValueSet(valueSetUrl, loadedVersion.Expansions, loadedVersion.NSE);
                        if (valueSet != null)
                        {
                            valueSetsByUrl.Add(valueSetUrl, valueSet);
                        }
                    }
                }
            }
        }
        Patch(valueSetsByUrlByVersion);
        ExtractShared(valueSetsByUrlByVersion);
        var commonFHIRDefinedType = CreateCommonFHIRDefinedType(valueSetsByUrlByVersion);
        if (commonFHIRDefinedType != null)
        {
            valueSetsByUrlByVersion[string.Empty][commonFHIRDefinedType.EnumName] = commonFHIRDefinedType;
        }
        return valueSetsByUrlByVersion;
    }

    public static string GetRequiredBindingValueSetUrl(XmlElement element, XmlNamespaceManager ns)
    {
        // Grab the required binding value set reference (if any) from the element
        var requiredBindingElement = element.SelectSingleNode("fhir:binding[fhir:strength/@value = 'required']", ns);
        if (requiredBindingElement == null)
        {
            return null;
        }

        var valueSetUrlAttribute = requiredBindingElement.SelectSingleNode("fhir:valueSetReference/fhir:reference/@value", ns);
        if (valueSetUrlAttribute == null)
        {
            valueSetUrlAttribute = requiredBindingElement.SelectSingleNode("fhir:valueSetUri/@value", ns);
        }
        if (valueSetUrlAttribute == null)
        {
            valueSetUrlAttribute = requiredBindingElement.SelectSingleNode("fhir:valueSet/@value", ns);
        }
        if (valueSetUrlAttribute == null)
        {
            return null;
        }

        var valueSetUrl = valueSetUrlAttribute.Value;
        if (string.IsNullOrEmpty(valueSetUrl))
        {
            return null;
        }

        // Remove the version
        var versionSuffixRegex = new Regex(@"\|\d+(\.\d+)?(\.\d+)?$");
        var versionSuffixMatch = versionSuffixRegex.Match(valueSetUrl);
        if (versionSuffixMatch.Success)
        {
            valueSetUrl = valueSetUrl.Substring(0, valueSetUrl.Length - versionSuffixMatch.Length);
        }

        return valueSetUrl;
    }

    private static ValueSet TryCreateValueSet(string valueSetUrl, XmlDocument expansions, XmlNamespaceManager nse)
    {
        var valueSetElement = expansions.SelectSingleNode("/fhir:Bundle/fhir:entry/fhir:resource/fhir:ValueSet[fhir:url/@value = '" + valueSetUrl + "']", nse) as XmlElement;
        if (valueSetElement == null) return null;

        var values = new List<ValueSetValue>();
        var codedValues = new HashSet<string>();
        foreach (var eval in valueSetElement.SelectNodes("fhir:expansion/fhir:contains", nse).OfType<XmlElement>())
        {
            var code = eval.SelectSingleNode("fhir:code/@value", nse).Value;
            var enumValue = StringUtils.ConvertEnumValue(code);
            if (!codedValues.Contains(enumValue))
            {
                codedValues.Add(enumValue);
                var valueSetValue = new ValueSetValue { Code = code };
                var system = eval.SelectSingleNode("fhir:system/@value", nse).Value;
                valueSetValue.System = system;
                valueSetValue.Display = eval.SelectSingleNode("fhir:display/@value", nse).Value;
                string definition = null;
                var definitionNode = valueSetElement.SelectSingleNode("fhir:codeSystem[fhir:system/@value = '" + system + "']/fhir:concept[fhir:code/@value = '" + code + "']/fhir:definition/@value", nse);
                if (definitionNode != null)
                {
                    definition = definitionNode.Value;
                }
                else
                {
                    definitionNode = valueSetElement.SelectSingleNode("fhir:codeSystem[fhir:system/@value = '" + system + "']/fhir:concept/fhir:concept[fhir:code/@value = '" + code + "']/fhir:definition/@value", nse);
                    if (definitionNode != null)
                    {
                        definition = definitionNode.Value;
                    }
                }
                if (string.IsNullOrEmpty(definition))
                {
                    definition = "MISSING DESCRIPTION";
                }
                valueSetValue.Definition = definition;
                values.Add(valueSetValue);
            }
        }

        // Ignore empty ones - there are some
        if (values.Count == 0)
        {
            return null;
        }

        var enumName = valueSetElement.SelectSingleNode("fhir:name/@value", nse).InnerText;
        // reformat the name so that it is a valid .NET enumeration name
        enumName = enumName.Replace(" ", "").Replace("-", "_").Replace(".", "_");

        return new ValueSet
        {
            EnumName = enumName,
            Url = valueSetUrl,
            Description = valueSetElement.SelectSingleNode("fhir:description/@value", nse).InnerText,
            Values = values
        };
    }

    private static void Patch(Dictionary<string, Dictionary<string, ValueSet>> valueSetsByUrlByVersion)
    {
        // Fix duplicate name
        valueSetsByUrlByVersion["R4"]["http://hl7.org/fhir/ValueSet/medication-statement-status"].EnumName = "MedicationStatementStatus";

        // Use R4 HTTP verbs for all version - they are the most comprehensive
        var httpVerbs = valueSetsByUrlByVersion["R4"]["http://hl7.org/fhir/ValueSet/http-verb"];
        valueSetsByUrlByVersion["DSTU2"]["http://hl7.org/fhir/ValueSet/http-verb"] = httpVerbs;
        valueSetsByUrlByVersion["STU3"]["http://hl7.org/fhir/ValueSet/http-verb"] = httpVerbs;

        // Use R4 search parameter types for all version - they are the most comprehensive
        var searchParamTypes = valueSetsByUrlByVersion["R4"]["http://hl7.org/fhir/ValueSet/search-param-type"];
        valueSetsByUrlByVersion["DSTU2"]["http://hl7.org/fhir/ValueSet/search-param-type"] = searchParamTypes;
        valueSetsByUrlByVersion["STU3"]["http://hl7.org/fhir/ValueSet/search-param-type"] = searchParamTypes;
    }

    private static void ExtractShared(Dictionary<string, Dictionary<string, ValueSet>> valueSetsByUrlByVersion)
    {
        var sharedValueSetsByUrl = new Dictionary<string, ValueSet>();
        var allUrls = valueSetsByUrlByVersion.Values
            .SelectMany(valueSetsByUrl => valueSetsByUrl.Keys)
            .Distinct()
            .ToList();
        foreach (var url in allUrls)
        {
            var valueSetsWithSameUrl = valueSetsByUrlByVersion.Values
                .Where(valueSetsByUrl => valueSetsByUrl.ContainsKey(url))
                .Select(valueSetsByUrl => valueSetsByUrl[url])
                .ToList();
            ValueSet sharedValueSet = null;
            if (url == "http://hl7.org/fhir/ValueSet/resource-types")
            {
                sharedValueSet = new ValueSet
                {
                    EnumName = "ResourceType",
                    Url = url,
                    Description = valueSetsWithSameUrl[0].Description,
                    Values = valueSetsWithSameUrl
                        .SelectMany(valueSet => valueSet.Values)
                        .Distinct(new ValueSetValueComparer())
                        .ToList()
                };
            }
            else if (valueSetsWithSameUrl.Count > 1 && valueSetsWithSameUrl.Skip(1).All(vs => valueSetsWithSameUrl[0].IsSame(vs)))
            {
                sharedValueSet = valueSetsWithSameUrl[0];
            }
            if (sharedValueSet != null)
            {
                sharedValueSetsByUrl.Add(url, sharedValueSet);
                foreach (var valueSetsByUrl in valueSetsByUrlByVersion.Values)
                {
                    valueSetsByUrl.Remove(url);
                }
            }
        }
        valueSetsByUrlByVersion.Add(string.Empty, sharedValueSetsByUrl);
    }

    private static ValueSet CreateCommonFHIRDefinedType(Dictionary<string, Dictionary<string, ValueSet>> valueSetsByUrlByVersion)
    {
        const string fhirDefinedTypesUrl = "http://hl7.org/fhir/ValueSet/defined-types";

        Dictionary<string, ValueSetValue> commonValues = null;
        foreach (var valueSetsByUrl in valueSetsByUrlByVersion.Values)
        {
            if (valueSetsByUrl.TryGetValue(fhirDefinedTypesUrl, out var fhirDefinedType))
            {
                if (commonValues == null)
                {
                    commonValues = fhirDefinedType.Values.ToDictionary(value => value.Code);
                }
                else
                {
                    var codes = new HashSet<string>(fhirDefinedType.Values.Select(value => value.Code));
                    var commonCodes = commonValues.Keys.ToList();
                    foreach (var code in commonCodes)
                    {
                        if (!codes.Contains(code))
                        {
                            commonValues.Remove(code);
                        }
                    }
                }
            }
        }
        if (commonValues != null && commonValues.Any())
        {
            return new ValueSet
            {
                EnumName = "FHIRDefinedType",
                Url = fhirDefinedTypesUrl,
                Description = "Either a resource or a data type that is defined in all the supported FHIR versions",
                Values = commonValues.Values.ToList()
            };
        }
        return null;
    }

    private class ValueSetValueComparer : IEqualityComparer<ValueSetValue>
    {
        public bool Equals(ValueSetValue x, ValueSetValue y)
        {
            return x.Code == y.Code;
        }

        public int GetHashCode(ValueSetValue obj)
        {
            return obj.Code.GetHashCode();
        }

    }
}

/// <summary>
/// Description of an individual value set value - corresponding to a C# enumeration value
/// </summary>
public class ValueSetValue
{
    /// <summary>
    /// Value code - e.g. 'active' or 'entered-in-error'
    /// </summary>
    public string Code;

    /// <summary>
    /// Optional numeric value of the generated enumeration
    /// </summary>
    public int? Value;

    /// <summary>
    /// Value system URI - e.g. http://hl7.org/fhir/allergy-intolerance-status
    /// </summary>
    public string System;

    /// <summary>
    /// Value display - e.g. 'Entered In Error'
    /// </summary>
    public string Display;

    /// <summary>
    /// Value definition or description - e.g. 'The statement was entered in error and is not valid'
    /// </summary>
    public string Definition;

    public IEnumerable<string> Render()
    {
        if (!string.IsNullOrEmpty(Definition) || !string.IsNullOrEmpty(System))
        {
            yield return $"/// <summary>";
            foreach (var line in StringUtils.ConvertToComment(Definition)) yield return line;
            if (!string.IsNullOrEmpty(System))
            {
                yield return $"/// (system: { System })";
            }
            yield return $"/// </summary>";
        }
        var enumValue = StringUtils.ConvertEnumValue(Code);
        if (enumValue != Code || !string.IsNullOrEmpty(System) || !string.IsNullOrEmpty(Display))
        {
            yield return $"[EnumLiteral(\"{ Code }\", \"{ System }\"), Description({ StringUtils.Quote(Display) })]";
        }
        if (Value != null)
        {
            yield return $"{ enumValue } = { Value.Value },";
        }
        else
        {
            yield return $"{ enumValue },";
        }
    }
}

/// <summary>
/// Complete description of an auto-generated interface - e.g. IPatient, that access the common properties 
/// of the various version-specific Patient classes
/// </summary>
public class InterfaceDetails
{
    /// <summary>
    /// Versions this interface applies to. 
    /// </summary>
    public List<LoadedVersion> Versions;

    /// <summary>
    /// C# interface name - e.g. 'IPatient'
    /// </summary>
    public string Name;

    /// <summary>
    /// Associated FHIR resource or data type description - e.g. 'Primitive Type boolean'
    /// </summary>
    public string Description;

    /// <summary>
    /// Base interface
    /// </summary>
    public string Base;

    /// <summary>
    /// The C# primitive data type (e.g. bool) - non-null only if this is a primitive type
    /// </summary>
    public string PrimitiveTypeName;

    /// <summary>
    /// Interface properties
    /// </summary>
    public List<InterfacePropertyDetails> Properties;

    /// <summary>
    /// Components interfaces
    /// </summary>
    public List<InterfaceDetails> Components;

    public void FixReferencedFhirTypes(string roorInterfaceName, Dictionary<string, ResourceDetails> sharedResourcesByName)
    {
        foreach (var property in Properties)
        {
            property.FixReferencedFhirTypes(roorInterfaceName, sharedResourcesByName);
        }
        if (Components != null)
        {
            foreach (var component in Components)
            {
                component.FixReferencedFhirTypes(roorInterfaceName, sharedResourcesByName);
            }
        }
    }

    /// <summary>
    /// Create a C# file containing this interface
    /// </summary>
    /// <param name="filePath">Path of the target file, that is overwritten if it already exist</param>
    public void Write(string filePath)
    {
        using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
        {
            foreach (var line in StringUtils.RenderFileHeader(Versions)) writer.WriteLine(line);
            foreach (var line in Render()) writer.WriteLine("    " + line);
            writer.WriteLine();
            writer.WriteLine("}");
        }
    }

    private IEnumerable<string> Render()
    {
        foreach (var line in RenderPrimitive()) yield return line;
        if (Components != null)
        {
            foreach (var component in Components)
            {
                yield return string.Empty;
                foreach (var line in component.RenderPrimitive()) yield return line;
            }
        }
    }

    private IEnumerable<string> RenderPrimitive()
    {
        foreach (var line in StringUtils.RenderSummary(Description)) yield return line;
        yield return $"public partial interface { Name } : { StringUtils.ModelNamespacePrefix }{ Base }";

        yield return $"{{";

        if (!string.IsNullOrEmpty(PrimitiveTypeName))
        {
            yield return string.Empty;
            yield return $"    /// <summary>";
            yield return $"    /// Primitive value of the element";
            yield return $"    /// </summary>";
            yield return $"    { PrimitiveTypeName } Value {{ get; set; }}";
        }
        else
        {
            foreach (var interfaceProperty in Properties)
            {
                yield return string.Empty;
                foreach (var line in interfaceProperty.Render()) yield return "    " + line;
            }
        }

        yield return string.Empty;
        yield return $"}}";
    }

}

public class InterfacePropertyDetails
{
    /// <summary>
    /// The name of the C# property - e.g. BirthDateElement
    /// </summary>
    public string Name;

    /// <summary>
    /// C# property data type - e.g. Hl7.Fhir.Model.Date. 
    /// </summary>
    public string PropType;

    /// <summary>
    /// Native C# data type of the property - e.g. string
    /// Not empty only for property types that can be represented by a native C# type: FhirString as string, FhirDateTime as DateTimeOffset etc
    /// </summary>
    public string NativeType;

    /// <summary>
    /// Native property name - e.g. BirthDate
    /// </summary>
    public string NativeName;

    /// <summary>
    /// True if max cardinality is unlimited = the property is a list
    /// </summary>
    public bool IsMultiCard;

    /// <summary>
    /// True if this is a read-only property
    /// </summary>
    public bool ReadOnly;

    /// <summary>
    /// Summary description of the property - e.g. 'The date of birth for the individual'
    /// </summary>
    public string Summary = string.Empty;

    public void FixReferencedFhirTypes(string interfaceName, Dictionary<string, ResourceDetails> sharedResourcesByName)
    {
        if (StringUtils.TryGetModelClassName(PropType, out var className))
        {
            var isVersionSpecific = !sharedResourcesByName.ContainsKey(className);
            if (isVersionSpecific)
            {
                PropType = StringUtils.ModelNamespacePrefix + StringUtils.ToInterfaceName(className);
                ReadOnly = true;
            }
        }
        else
        {
            // Uses a component...
            PropType = StringUtils.ModelNamespacePrefix + interfaceName + PropType;
            ReadOnly = true;
        }
    }

    public IEnumerable<string> Render()
    {
        foreach (var line in StringUtils.RenderSummary(Summary)) yield return line;

        var accessors = ReadOnly ?
            "get;" :
            "get; set;";

        var propTypeWithCard = !IsMultiCard ?
            PropType :
            ReadOnly ?
                "IEnumerable<" + PropType + ">" :
                "List<" + PropType + ">";

        yield return $"{ StringUtils.RemoveCodeNamespace(propTypeWithCard) } { Name } {{ { accessors } }}";

        if (!string.IsNullOrEmpty(NativeName))
        {
            yield return string.Empty;
            foreach (var line in StringUtils.RenderSummary(Summary)) yield return line;
            yield return "/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>";
            var nativeType = IsMultiCard ?
                $"IEnumerable<{ NativeType }>" :
                NativeType;
            yield return $"{ nativeType  } { NativeName } {{ { accessors } }}";
        }
    }
}

/// <summary>
/// Complete description of a resource (e.g. Patient) or data type (e.g. Identifier), corresponding to a C# class
/// </summary>
public class ResourceDetails
{
    /// <summary>
    /// Versions this resource or data type belongs to. 
    /// Initially it contains only the version this resource has been loaded from (e.g. DSTU2), 
    /// if later on the resource is determined to be common to multiple version it will contain all the versions it appears in
    /// </summary>
    public List<LoadedVersion> Versions;

    /// <summary>
    /// C# class name - e.g. 'Patient' or 'FhirBoolean'
    /// </summary>
    public string Name;

    /// <summary>
    /// FHIR resource or data type description - e.g. 'Primitive Type boolean'
    /// </summary>
    public string Description;

    /// <summary>
    /// Original FHIR resource or data type name - e.g. 'Patient' or 'boolean'
    /// </summary>
    public string FhirName;

    /// <summary>
    /// True if the resource or data type us used only as base for other resources or data type - corresponding to an abstract C# class 
    /// </summary>
    public bool AbstractType;

    /// <summary>
    /// Base type (class) name - e.g. DomainResource
    /// </summary>
    public string BaseType;

    /// <summary>
    /// True if a primitive data type (e.g. boolean) as opposed to a resource or a composite data type like Attachment
    /// </summary>
    public bool IsPrimitive;

    /// <summary>
    /// The C# primitive data type (e.g. bool) - meaningful only if this is a primitive type
    /// </summary>
    public string PrimitiveTypeName;

    /// <summary>
    /// Validation regular expression- used for string-based primitive data types likes Uri
    /// </summary>
    public string Pattern;

    /// <summary>
    /// Properties of the FHIR resource or data type (e.g. Id, Identifier, Active, Name etc for Patient) - corresponding to C# class properties
    /// </summary>
    public List<PropertyDetails> Properties;

    /// <summary>
    /// Components of the FHIR resource or data type (e.g. ContactComponent, AnimalComponent, CommunicationComponent, LinkComponent for Patient) - corresponding to C# sub-classes
    /// </summary>
    public List<ComponentDetails> Components;

    /// <summary>
    /// Validation contraints of the FHIR resource or data type - e.g. the 'contact.all(name or telecom or address or organization)' validation expression for Patient
    /// </summary>
    public List<ConstraintDetails> Constraints;

    /// <summary>
    /// True if this is just a constraint of the base class and not a stand-alone FHIR resource or data type (e.g. SimpleQuantity is just a constraint of Quantity)
    /// </summary>
    public bool IsConstraint;

    /// <summary>
    /// Shared interface implemented by the C# class
    /// </summary>
    public string Interface;

    /// <summary>
    /// True if this is a resource, false if it is a data type
    /// </summary>
    public bool IsResource()
    {
        return BaseType.EndsWith(".DomainResource") || BaseType.EndsWith(".Resource") || Name == "DomainResource" || Name == "Resource";
    }

    public PropertyDetails GetProperty(string name)
    {
        return Properties.FirstOrDefault(p => p.Name == name);
    }

    public ComponentDetails GetComponent(string name)
    {
        return Components.FirstOrDefault(c => c.Name == name);
    }

    public string GetPrimitiveTypeName()
    {
        if (!IsPrimitive) return null;
        return PrimitiveTypeName;
    }

    /// <summary>
    /// Dumps the resource details to the specified text writer - used for debugging
    /// </summary>
    public void Dump(TextWriter writer)
    {
        writer.WriteLine(
            "{0}: {1}{2}{3}{4}{5}{6}",
            Name,
            BaseType,
            AbstractType ? " abstract" : string.Empty,
            IsPrimitive ? " primitive" : string.Empty,
            string.IsNullOrEmpty(PrimitiveTypeName) ? string.Empty : " primitiveType: " + PrimitiveTypeName,
            string.IsNullOrEmpty(Pattern) ? string.Empty : " pattern: " + Pattern,
            IsConstraint ? " constraint" : string.Empty
        );
        foreach (var prop in Properties)
        {
            var allowedTypes = prop.AllowedTypesByVersion.Count == 0 ?
                string.Empty :
                prop.AllowedTypesByVersion.ContainsKey(string.Empty) ?
                    string.Join(",", prop.AllowedTypesByVersion[string.Empty]) :
                    string.Join(", ", prop.AllowedTypesByVersion.Select(pair => $"{pair.Key}({ string.Join(",", pair.Value) })"));
            writer.WriteLine(
                "    {0}: {1}{2}{3} {4} {5}{6}{7}{8}",
                prop.Name,
                prop.PropType,
                prop.Versions.Count > 1 || prop.Versions.Count == 1 && string.IsNullOrEmpty(prop.Versions.Single()) ?
                    "versions(" + string.Join(",", prop.Versions) + ")" : string.Empty,
                prop.InSummaryVersions.Count > 0 ? " summary(" + string.Join(",", prop.InSummaryVersions) + ")" : string.Empty,
                prop.CardMin,
                prop.CardMax,
                prop.ReferenceTargets.Count == 0 ? string.Empty : " targets: " + string.Join(",", prop.ReferenceTargets),
                string.IsNullOrEmpty(allowedTypes) ? string.Empty : " allowed types: " + allowedTypes,
                string.IsNullOrEmpty(Interface) ? string.Empty : " interface: " + Interface
             );
        }
        foreach (var comp in Components)
        {
            writer.WriteLine("    ---- Component {0}", comp.Name);
            foreach (var comprop in comp.Properties)
            {
                writer.WriteLine("        {0}: {1}", comprop.Name, comprop.PropType);
            }
        }
        if (Constraints.Count > 0)
        {
            writer.WriteLine("    ---- Constraints");
            foreach (var constraint in Constraints)
            { 
                writer.WriteLine("        {0} - {1}: {2}, XPath: {3}, Expression: {4}, Versions: {5}", constraint.Key, constraint.Severity, constraint.Human, constraint.XPath, constraint.Expression, string.Join(",", Versions));
            }
        }
    }

    public ResourceDetails Clone()
    {
        if (Versions.Count != 1) throw new ArgumentException("The resource must belong to a single FHIR version", "this");
        var version = Versions[0].Version;

        return new ResourceDetails
        {
            Versions = new List<LoadedVersion>( Versions ),
            Name = Name,
            Description = Description,
            FhirName = FhirName,
            AbstractType = AbstractType,
            BaseType = BaseType,
            IsConstraint = IsConstraint,
            IsPrimitive = IsPrimitive,
            PrimitiveTypeName = PrimitiveTypeName,
            Pattern = Pattern,
            Interface = Interface,
            Properties = Properties
                .Select(prop => prop.Clone(version))
                .ToList(),
            Components = Components
                .Select(comp => comp.Clone(version))
                .ToList(),
            Constraints = Constraints
                .Select(constraint => constraint.Clone())
                .ToList()
        };
    }

    public string TryMerge(ResourceDetails other)
    {
        if (other.Versions.Count != 1) throw new ArgumentException("The resource must belong to a single FHIR version", nameof(other));
        var version = other.Versions[0].Version;

        if (Name != other.Name) return "Name";
        if (AbstractType != other.AbstractType) return "AbstractType";
        if (BaseType != other.BaseType) return "BaseType";
        if (IsPrimitive != other.IsPrimitive) return "IsPrimitive";
        if (IsConstraint != other.IsConstraint) return "IsContraint";
        if (PrimitiveTypeName != other.PrimitiveTypeName) return "PrimitiveTypeName";
        if (Pattern != other.Pattern) return "Pattern";

        Versions.AddRange(other.Versions);

        var tryMergeResult = PropertyDetails.TryMerge(Properties, version, other.Properties);
        if (tryMergeResult != null) return tryMergeResult;

        tryMergeResult = ComponentDetails.TryMerge(Components, version, other.Components);
        if (tryMergeResult != null) return tryMergeResult;

        tryMergeResult = ConstraintDetails.TryMerge(Constraints, other.Constraints);
        if (tryMergeResult != null) return tryMergeResult;

        return null;
    }

    public void Simplify( Dictionary<string, Dictionary<string, ResourceDetails>> resourcesByNameByVersion )
    {
        PropertyDetails.Simplify(Properties, resourcesByNameByVersion);
        ComponentDetails.Simplify(Components, resourcesByNameByVersion);
        ConstraintDetails.Simplify(Constraints, resourcesByNameByVersion);
    }

    /// <summary>
    /// Create a C# file containing the class corresponding to this FHIR resource or data type
    /// </summary>
    /// <param name="filePath">Path of the target file, that is overwritten if it already exist</param>
    public void Write(string filePath)
    {
        using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
        {
            foreach (var line in StringUtils.RenderFileHeader(Versions)) writer.WriteLine(line);
            foreach (var line in Render()) writer.WriteLine("    " + line);
            writer.WriteLine();
            writer.WriteLine("}");
        }
    }

    private IEnumerable<string> Render()
    {
        var primitiveTypesWithPatternAttribute = new[] { "Code", "Date", "FhirDateTime", "Id", "NarrativeXhtml", "Oid", "FhirUri", "Uuid" };

        var version = Versions.Count == 1 ?
            Versions[0].Version :
            "All";
        version = "Hl7.Fhir.Model.Version." + version;
        var isElement = BaseType == "Hl7.Fhir.Model.Element"
            || BaseType == "Hl7.Fhir.Model.BackboneElement"
            || BaseType == "Hl7.Fhir.Model.Quantity"
            || IsPrimitive;
        foreach (var line in StringUtils.RenderSummary(Description)) yield return line;
        if (!AbstractType)
        {
            var isResource = !isElement ?
                ", IsResource=true" :
                string.Empty;
            // If this is just a contraint the FHIR type name used for serialization etc. must be the base type name
            var fhirTypeName = IsConstraint ?
                BaseType.Split( '.' ).Last() :
                FhirName;
            yield return $"[FhirType({version}, \"{ fhirTypeName }\"{ isResource })]";
        }
        yield return $"[DataContract]";

        var isAbstract = AbstractType ?
            " abstract" :
            string.Empty;
        var baseType = !string.IsNullOrEmpty(BaseType) ?
            $"{ BaseType }, " :
            string.Empty;
        var inter = !string.IsNullOrEmpty(Interface) ?
            $"Hl7.Fhir.Model.{ Interface }, " :
            string.Empty;
        yield return $"public{ isAbstract } partial class { Name } : { baseType }{ inter }System.ComponentModel.INotifyPropertyChanged";

        yield return $"{{";

        if (!isElement)
        {
            yield return $"    [NotMapped]";
            yield return $"    public override ResourceType ResourceType {{ get {{ return ResourceType.{ Name }; }} }}";
        }

        yield return $"    [NotMapped]";
        yield return $"    public override string TypeName {{ get {{ return \"{ FhirName }\"; }} }}";

        if (!string.IsNullOrEmpty(Pattern))
        {
            yield return string.Empty;
            yield return $"    // Must conform to the pattern \"{ Pattern }\"";
            yield return $"    public const string PATTERN = @\"{ Pattern }\";";
        }

        if (IsPrimitive)
        {
            yield return string.Empty;
            yield return $"    public { Name }({ PrimitiveTypeName } value)";
            yield return $"    {{";
            yield return $"        Value = value;";
            yield return $"    }}";
            yield return string.Empty;
            yield return $"    public { Name }(): this(({ PrimitiveTypeName })null) {{}}";
            yield return string.Empty;
            yield return $"    /// <summary>";
            yield return $"    /// Primitive value of the element";
            yield return $"    /// </summary>";
            yield return $"    [FhirElement(\"value\", IsPrimitiveValue=true, XmlSerialization=Specification.XmlRepresentation.XmlAttr, InSummary=Hl7.Fhir.Model.Version.All, Order=30)]";
            yield return $"    [CLSCompliant(false)]";
            if (primitiveTypesWithPatternAttribute.Contains(Name))
            {
                yield return $"    [{ Name.Replace("Fhir", "") }Pattern]";
            }
            yield return $"    [DataMember]";
            yield return $"    public { PrimitiveTypeName } Value";
            yield return $"    {{";
            yield return $"        get {{ return ({ PrimitiveTypeName })ObjectValue; }}";
            yield return $"        set {{ ObjectValue = value; OnPropertyChanged(\"Value\"); }}";
            yield return $"    }}";
        }

        foreach (var component in Components)
        {
            yield return string.Empty;
            yield return string.Empty;
            foreach (var line in component.Render(version, Interface, Components)) yield return "    " + line;
        }

        if (!IsPrimitive)
        {
            if (!string.IsNullOrEmpty(Interface))
            {
                foreach (var line in StringUtils.RenderInterfaceProperties(Interface, Properties, Components)) yield return "    " + line;
            }

            var nPropNum = BaseType == "Hl7.Fhir.Model.Resource" ?
                40 :
                BaseType == "Hl7.Fhir.Model.Element" ? 20 : 80;

            yield return string.Empty;
            foreach (var line in StringUtils.RenderProperties(nPropNum, Properties)) yield return "    " + line;
        }

        yield return string.Empty;
        if (Constraints.Any())
        {
            yield return string.Empty;
            yield return $"    public static ElementDefinitionConstraint[] {Name}_Constraints =";
            yield return $"    {{";
            foreach (var constraint in Constraints)
            {
                foreach (var line in constraint.Render()) yield return "        " + line;
            }
            yield return $"    }};";
            yield return string.Empty;
            if (isElement)
            {
                yield return $"    // TODO: Add code to enforce the above constraints";
            }
            else
            {
                yield return $"    public override void AddDefaultConstraints()";
                yield return $"    {{";
                yield return $"        base.AddDefaultConstraints();";
                yield return $"        InvariantConstraints.AddRange({Name}_Constraints);";
                yield return $"    }}";
            }
        }

        if (!IsPrimitive)
        {
            yield return string.Empty;
            if (IsConstraint)
            {
                foreach (var line in StringUtils.RenderDeepCopy(Name)) yield return "    " + line;
            }
            else
            {
                foreach (var line in StringUtils.RenderCopyAndComparisonMethods(Name, AbstractType, Properties)) yield return "    " + line;
            }

            if (Properties.Any())
            {
                yield return string.Empty;
                foreach (var line in StringUtils.RenderSerialize(FhirName, AbstractType, isElement, Properties)) yield return "    " + line;
                yield return string.Empty;
                foreach (var line in StringUtils.RenderSetElementFromJson(Properties)) yield return "    " + line;
                yield return string.Empty;
                foreach (var line in StringUtils.RenderChildrenMethods(Properties)) yield return "    " + line;
            }
        }

        yield return string.Empty;
        yield return $"}}";
    }

    private IEnumerable<string> GetReferencedFhirTypes()
    {
        const string prefix = "Hl7.Fhir.Model.";
        return GetAllProperties()
            .Select(p => p.PropType)
            .Concat(new[] { BaseType })
            .Where(pt => pt.StartsWith(prefix))
            .Select(pt => pt.Substring(prefix.Length))
            .Distinct();
    }

    private void FixReferencedTypesFhirVersion(string version, Dictionary<string,ResourceDetails> resourcesByName)
    {
        BaseType = StringUtils.FixTypeFhirVersion(BaseType, version, resourcesByName);
        foreach (var prop in GetAllProperties())
        {
            prop.FixReferencedFhirTypes(version, resourcesByName);
        }
    }

    private IEnumerable<PropertyDetails> GetAllProperties()
    {
        return Properties.Concat(Components.SelectMany(c => c.Properties));
    }

    /// <summary>
    /// Load all the resources and data types from the XML structure definition data of a set of FHIR versions
    /// </summary>
    /// <param name="loadedVersions">The FHIR versions XML structure definition data</param>
    /// <param name="valueSetsByUrlByVersion">All value sets that have been mapped to enumerations, indexed by their URL and FHIR version</param>
    /// <returns>A tuple with containing newly created resources and data types by FHIR version and by name and created interfaces as a flat list
    /// The first is a dictionary indexed by FHIR version, with an empty string key representing resources and data types common to all version and 
    /// 'DSTU2', 'STU3' etc representing the specific version; each value is a dictionary indexed by the class name with the ResourceDetails as values</returns>
    public static Tuple<Dictionary<string, Dictionary<string, ResourceDetails>>, List<InterfaceDetails>> LoadAll(
        IEnumerable<LoadedVersion> loadedVersions,
        Dictionary<string, Dictionary<string, ValueSet>> valueSetsByUrlByVersion
    )
    {
        var enumTypesByValueSetUrlByVersion = loadedVersions.ToDictionary(
            loadedVersion => loadedVersion.Version,
            loadedVersion => valueSetsByUrlByVersion[string.Empty]
                .Select(pair => new KeyValuePair<string, string>(pair.Key, "Hl7.Fhir.Model." + pair.Value.EnumName))
                .Concat(valueSetsByUrlByVersion[loadedVersion.Version].Select(pair => new KeyValuePair<string, string>(pair.Key, "Hl7.Fhir.Model." + loadedVersion.Version + "." + pair.Value.EnumName)))
                .ToDictionary(pair => pair.Key, pair => pair.Value)
        );

        var resourcesByNameByVersion = new Dictionary<string, Dictionary<string, ResourceDetails>>();
        foreach (var loadedVersion in loadedVersions)
        {
            var enumTypesByValueSetUrl = enumTypesByValueSetUrlByVersion[loadedVersion.Version];
            var resourcesByName = LoadTypes(loadedVersion, enumTypesByValueSetUrl)
                .Concat(LoadResources(loadedVersion, enumTypesByValueSetUrl))
                .ToDictionary( res => res.Name );
            resourcesByNameByVersion.Add(loadedVersion.Version, resourcesByName);
        }
        Patch(resourcesByNameByVersion);
        var interfaces = ExtractShared(loadedVersions, resourcesByNameByVersion);
        return Tuple.Create(resourcesByNameByVersion, interfaces);
    }

    private static void Patch(Dictionary<string, Dictionary<string, ResourceDetails>> resourcesByNameByVersion)
    {
        // Fixed patterns - there were quite a bit of changes in R4 and some of the old ones were plain wrong
        var patternsByType = new Dictionary<string, string>
        {
            { "FhirString", null },
            { "Markdown", null },
            { "FhirUri", null },
            { "FhirBoolean", null },
            { "FhirDecimal", null },
            { "Code", @"[^\s]+(\s[^\s]+)*" },
            { "Oid", @"urn:oid:[0-2](\.(0|[1-9][0-9]*))+" },
            { "Date", @"([0-9]([0-9]([0-9][1-9]|[1-9]0)|[1-9]00)|[1-9]000)(-(0[1-9]|1[0-2])(-(0[1-9]|[1-2][0-9]|3[0-1]))?)?" },
            { "FhirDateTime", @"([0-9]([0-9]([0-9][1-9]|[1-9]0)|[1-9]00)|[1-9]000)(-(0[1-9]|1[0-2])(-(0[1-9]|[1-2][0-9]|3[0-1])(T([01][0-9]|2[0-3]):[0-5][0-9]:([0-5][0-9]|60)(\.[0-9]+)?(Z|(\+|-)((0[0-9]|1[0-3]):[0-5][0-9]|14:00)))?)?)?" },
            { "Instant", @"([0-9]([0-9]([0-9][1-9]|[1-9]0)|[1-9]00)|[1-9]000)-(0[1-9]|1[0-2])-(0[1-9]|[1-2][0-9]|3[0-1])T([01][0-9]|2[0-3]):[0-5][0-9]:([0-5][0-9]|60)(\.[0-9]+)?(Z|(\+|-)((0[0-9]|1[0-3]):[0-5][0-9]|14:00))" },
            { "Time", @"([01][0-9]|2[0-3]):[0-5][0-9]:([0-5][0-9]|60)(\.[0-9]+)?" },
            { "Base64Binary", @"(\s*([0-9a-zA-Z\+\=]){4}\s*)+" }
        };

        foreach (var resourcesByName in resourcesByNameByVersion.Values)
        {
            foreach (var typePattern in patternsByType)
            {
                if (resourcesByName.TryGetValue(typePattern.Key, out var resourceDetails))
                {
                    resourceDetails.Pattern = typePattern.Value;
                }
            }
        }

        var propertyTypes = new string[][]
        {
            // Make the DSTU2 Element.Id a string as in STU3 and R4 - less restrictive
            new[] { "DSTU2", "Element", "IdElement", "Hl7.Fhir.Model.FhirString", "string"},
            // Make the DSTU2 and STU3 Resource.Id a string as in R4 - less restrictive
            new[] { "DSTU2", "Resource", "IdElement", "Hl7.Fhir.Model.FhirString", "string"},
            new[] { "STU3", "Resource", "IdElement", "Hl7.Fhir.Model.FhirString", "string"},
            // Make the DSTU2 and STU3 Meta.Profile a Canonical as in R4 
            new[] {"DSTU2", "Meta", "ProfileElement", "Hl7.Fhir.Model.Canonical", null},
            new[] {"STU3", "Meta", "ProfileElement", "Hl7.Fhir.Model.Canonical", null},
            // Make the DSTU2 and STU3 Attachment.UrlElement a Url as in R4 
            new[] {"DSTU2", "Attachment", "UrlElement", "Hl7.Fhir.Model.Url", null},
            new[] {"STU3", "Attachment", "UrlElement", "Hl7.Fhir.Model.Url", null},
            // Make Extension.UrlElement a URI as in DSTU2 and STU3
            new[] {"R4", "Extension", "UrlElement", "Hl7.Fhir.Model.FhirUri", null},
            // Make the DSTU2 and STU3 Annotation.TextElement Markdown as in R4
            new[] {"DSTU2", "Annotation", "TextElement", "Hl7.Fhir.Model.Markdown", null},
            new[] {"STU3", "Annotation", "TextElement", "Hl7.Fhir.Model.Markdown", null},
            // Make Identifier.UseElement untyped - so that it can stay version-independent (the IdentifierUse value set changed in R4)
            new[] { "DSTU2", "Identifier", "UseElement", "Hl7.Fhir.Model.Code", "string"},
            new[] { "STU3", "Identifier", "UseElement", "Hl7.Fhir.Model.Code", "string"},
            new[] { "R4", "Identifier", "UseElement", "Hl7.Fhir.Model.Code", "string"},
            // Make Address.UseElement untyped - so that it can stay version-independent (the AddressUse value set changed in R4)
            new[] { "DSTU2", "Address", "UseElement", "Hl7.Fhir.Model.Code", "string"},
            new[] { "STU3", "Address", "UseElement", "Hl7.Fhir.Model.Code", "string"},
            new[] { "R4", "Address", "UseElement", "Hl7.Fhir.Model.Code", "string"},
        };

        foreach (var propertyType in propertyTypes)
        {
            var property = resourcesByNameByVersion[propertyType[0]][propertyType[1]].GetProperty(propertyType[2]);
            if (property == null)
            {
                throw new InvalidOperationException($"{propertyType[0]}.{propertyType[1]} does not have a property named '{propertyType[2]}'");
            }
            property.PropType = propertyType[3];
            if (propertyType[4]  != null)
            {
                property.NativeType = propertyType[4];
            }
        }

        // Make DSTU2 Parameters constraints the same as in STU3 - the DSTU2 ones are wrong
        var dstu2Parameters = resourcesByNameByVersion["DSTU2"]["Parameters"];
        dstu2Parameters.Constraints = new List<ConstraintDetails>
        {
            new ConstraintDetails
            {
                Versions = new List<LoadedVersion>( dstu2Parameters.Versions ),
                Expression = "parameter.all((part.exists() and value.empty() and resource.empty()) or (part.empty() and (value.exists() xor resource.exists())))",
                Key = "inv-1",
                Severity = "Warning",
                Human = "A parameter must have only one of (value, resource, part)",
                XPath = "exists(f:value) or exists(f:resource) and not(exists(f:value) and exists(f:resource))"
            }
        };

        // Make OperationOutcome.Isse.CodeElement untyped - so that the changes in the IssueType enumeration do not cause the OperationOutcome to be version-specific
        var dstu2OperationOutcomeIssueCodeElement = resourcesByNameByVersion["DSTU2"]["OperationOutcome"].GetComponent("IssueComponent").GetProperty("CodeElement");
        dstu2OperationOutcomeIssueCodeElement.PropType = "Hl7.Fhir.Model.Code";
        dstu2OperationOutcomeIssueCodeElement.NativeType = "string";
        var stu3OperationOutcomeIssueCodeElement = resourcesByNameByVersion["STU3"]["OperationOutcome"].GetComponent("IssueComponent").GetProperty("CodeElement");
        stu3OperationOutcomeIssueCodeElement.PropType = "Hl7.Fhir.Model.Code";
        stu3OperationOutcomeIssueCodeElement.NativeType = "string";
        var r4OperationOutcomeIssueCodeElement = resourcesByNameByVersion["R4"]["OperationOutcome"].GetComponent("IssueComponent").GetProperty("CodeElement");
        r4OperationOutcomeIssueCodeElement.PropType = "Hl7.Fhir.Model.Code";
        r4OperationOutcomeIssueCodeElement.NativeType = "string";
    }

    private static List<ResourceDetails> LoadResources(LoadedVersion loadedVersion, Dictionary<string, string> enumTypesByValueSetUrl)
    {
        var result = new List<ResourceDetails>();
        var sdNodes = loadedVersion.Resources.DocumentElement.SelectNodes("/fhir:Bundle/fhir:entry/fhir:resource/fhir:StructureDefinition[fhir:kind/@value != 'logical']", loadedVersion.NSR);
        foreach (var e in sdNodes.OfType<XmlElement>())
        {
            var resourceName = e.SelectSingleNode("fhir:name/@value", loadedVersion.NSR).Value;

            var resourceBaseType = string.Empty;
            var resourceBaseTypeNode = GetBaseTypeNode(e, loadedVersion.NSR);
            if (resourceBaseTypeNode != null)
            {
                resourceBaseType = resourceBaseTypeNode.Value;
                if (resourceBaseType == "http://hl7.org/fhir/StructureDefinition/Resource")
                    resourceBaseType = "Hl7.Fhir.Model.Resource";
                else if (resourceBaseType == "http://hl7.org/fhir/StructureDefinition/Element")
                    resourceBaseType = "Hl7.Fhir.Model.Element";
                else if (resourceBaseType == "http://hl7.org/fhir/StructureDefinition/Quantity")
                    resourceBaseType = "Hl7.Fhir.Model.Quantity";
                else
                    resourceBaseType = "Hl7.Fhir.Model.DomainResource";
            }

            var resource = new ResourceDetails { Name = resourceName, FhirName = resourceName, BaseType = resourceBaseType, Versions = new List<LoadedVersion> { loadedVersion } };
            result.Add(resource);

            var resourceDescriptionNode = e.SelectSingleNode("fhir:differential/fhir:element[fhir:path/@value='" + resourceName + "']/fhir:short/@value", loadedVersion.NSR);
            resource.Description = resourceDescriptionNode == null ? string.Empty : resourceDescriptionNode.Value;

            resource.IsPrimitive = false;
            resource.AbstractType = (e.SelectSingleNode("fhir:abstract[@value='true']", loadedVersion.NSR) != null);

            resource.Properties = GetProperties( resourceName, resourceName, e, loadedVersion.NSR, enumTypesByValueSetUrl );

            resource.Components = new List<ComponentDetails>();
            foreach (var e2 in e.SelectNodes("fhir:differential/fhir:element[fhir:type/fhir:code/@value = 'BackboneElement']", loadedVersion.NSR).OfType<XmlElement>())
            {
                var componentElement = e2.SelectSingleNode("fhir:path/@value", loadedVersion.NSR) as XmlAttribute;
                var v = componentElement.Value;
                if (v.Contains("."))
                {
                    var index = v.LastIndexOf(".");
                    v = v.Substring(index + 1, 1).ToUpper() + v.Substring(index + 2);
                }
                var componentName = v + "Component";
                var componentNameElement = (XmlAttribute)e2.SelectSingleNode("fhir:extension[@url = 'http://hl7.org/fhir/StructureDefinition/structuredefinition-explicit-type-name']/fhir:valueString/@value", loadedVersion.NSR);
                if (componentNameElement != null)
                {
                    componentName = componentNameElement.Value + "Component";
                }

                var component = new ComponentDetails
                {
                    Name = componentName,
                    BaseType = "Hl7.Fhir.Model.BackboneElement",
                    Properties = GetProperties(componentName, componentElement.Value, e, loadedVersion.NSR, enumTypesByValueSetUrl)
                };
                resource.Components.Add(component);
            }

            resource.Constraints = GetConstraints(loadedVersion, resourceName, e, loadedVersion.NSR);

            resource.IsConstraint = GetIsConstraintOrPrimitive(e, loadedVersion.NST) && !resource.IsPrimitive;
        }
        return result;
    }

    private static List<ResourceDetails> LoadTypes(LoadedVersion loadedVersion, Dictionary<string, string> enumTypesByValueSetUrl)
    {
        var result = new List<ResourceDetails>();
        var sdNodes = loadedVersion.Types.DocumentElement.SelectNodes("/fhir:Bundle/fhir:entry/fhir:resource/fhir:StructureDefinition", loadedVersion.NST);
        foreach (var e in sdNodes.OfType<XmlElement>())
        {
            var resourceName = e.SelectSingleNode("fhir:name/@value", loadedVersion.NST).Value;
            var rawResourceName = resourceName;

            string primitiveTypeName = null;
            var primitiveType = PrimitiveType.Get(resourceName);
            if (primitiveType != null)
            {
                resourceName = primitiveType.ClassName;
                primitiveTypeName = primitiveType.NativeType;
            }
            else if (resourceName == "Reference")
            {
                resourceName = "ResourceReference";
            }
            else
            {
                resourceName = resourceName.Substring(0, 1).ToUpper() + resourceName.Substring(1);
                primitiveTypeName = "string";
            }

            var resourceBaseType = string.Empty;
            var definedBaseTypeNode = GetBaseTypeNode(e, loadedVersion.NST);
            if (definedBaseTypeNode != null)
            {
                var definedBaseType = definedBaseTypeNode.Value;
                if (definedBaseType == "http://hl7.org/fhir/StructureDefinition/Element")
                {
                    if (rawResourceName.Substring(0, 1).ToLower() == rawResourceName.Substring(0, 1))
                    {
                        resourceBaseType = "Hl7.Fhir.Model.Primitive<" + primitiveTypeName + ">";
                    }
                    else
                        resourceBaseType = "Hl7.Fhir.Model.Element";
                }
                else if (definedBaseType == "http://hl7.org/fhir/StructureDefinition/Quantity")
                    resourceBaseType = "Hl7.Fhir.Model.Quantity";
                else if (definedBaseType == "http://hl7.org/fhir/StructureDefinition/BackboneElement")
                    resourceBaseType = "Hl7.Fhir.Model.BackboneElement";
                else
                    resourceBaseType = "Hl7.Fhir.Model.Primitive<" + primitiveTypeName + ">";
            }

            var resource = new ResourceDetails
            {
                Name = resourceName,
                FhirName = rawResourceName,
                PrimitiveTypeName = primitiveTypeName,
                BaseType = resourceBaseType,
                Versions = new List<LoadedVersion> { loadedVersion }
            };
            result.Add(resource);

            var resourceDescriptionNode = e.SelectSingleNode("fhir:differential/fhir:element[fhir:path/@value='" + rawResourceName + "']/fhir:short/@value", loadedVersion.NST);
            resource.Description = resourceDescriptionNode != null ?
                resourceDescriptionNode.Value :
                string.Empty;

            resource.IsPrimitive = resourceBaseType.Contains("Primitive<");
            resource.AbstractType = (e.SelectSingleNode("fhir:abstract[@value='true']", loadedVersion.NST) != null);

            var patternNode = e.SelectSingleNode("fhir:differential/fhir:element/fhir:type/fhir:extension[@url='http://hl7.org/fhir/StructureDefinition/structuredefinition-regex' or @url='http://hl7.org/fhir/StructureDefinition/regex']/fhir:valueString/@value", loadedVersion.NST);
            resource.Pattern = patternNode != null ? patternNode.Value : null;

            resource.Properties = resource.IsPrimitive ?
                new List<PropertyDetails>() :
                GetProperties(resourceName, rawResourceName, e, loadedVersion.NST, enumTypesByValueSetUrl);

            resource.Components = new List<ComponentDetails>();
            foreach (var e2 in e.SelectNodes("fhir:differential/fhir:element[fhir:type/fhir:code/@value = 'Element']", loadedVersion.NST).OfType<XmlElement>())
            {
                var componentElement = e2.SelectSingleNode("fhir:path/@value", loadedVersion.NST) as XmlAttribute;
                var v = componentElement.Value;
                if (v.Contains("."))
                {
                    var index = v.LastIndexOf(".");
                    v = v.Substring(index + 1, 1).ToUpper() + v.Substring(index + 2);
                    string componentName = v + "Component";
                    var componentNameElement = (XmlAttribute)e2.SelectSingleNode("fhir:extension[@url = 'http://hl7.org/fhir/StructureDefinition/structuredefinition-explicit-type-name']/fhir:valueString/@value", loadedVersion.NST);
                    if (componentNameElement != null)
                    {
                        componentName = componentNameElement.Value + "Component";
                    }

                    var component = new ComponentDetails
                    {
                        Name = componentName,
                        BaseType = "Hl7.Fhir.Model.Element",
                        Properties = GetProperties(componentName, componentElement.Value, e, loadedVersion.NST, enumTypesByValueSetUrl)
                    };
                    resource.Components.Add(component);
                }
            }

            resource.Constraints = GetConstraints(loadedVersion, resourceName, e, loadedVersion.NST);

            resource.IsConstraint = GetIsConstraintOrPrimitive(e, loadedVersion.NST) && !resource.IsPrimitive;
        }
        return result;
    }

    private static List<ConstraintDetails> GetConstraints(LoadedVersion loadedVersion, string resourceName, XmlElement structureDefinitionElement, XmlNamespaceManager ns)
    {
        var result = new List<ConstraintDetails>();
        foreach (var node in structureDefinitionElement.SelectNodes("fhir:differential/fhir:element/fhir:constraint", ns).OfType<XmlElement>())
        {
            var expression = node.SelectSingleNode("fhir:extension[@url='http://hl7.org/fhir/StructureDefinition/structuredefinition-expression']/fhir:valueString/@value|fhir:expression/@value", ns)?.Value;
            if (expression != null)
            {
                var parentPath = node.ParentNode.SelectSingleNode("fhir:path/@value", ns).Value;
                if (parentPath.Contains("."))
                {
                    // This expression applied to a backbone element, so need to give it scope
                    expression = parentPath.Replace("[x]", "").Replace(resourceName + ".", "") + ".all(" + expression + ")";
                }
            }
            var constraint = new ConstraintDetails
            {
                Versions = new List<LoadedVersion> { loadedVersion },
                Key = node.SelectSingleNode("fhir:key/@value", ns).Value,
                Severity = node.SelectSingleNode("fhir:severity/@value", ns).Value,
                Human = node.SelectSingleNode("fhir:human/@value", ns).Value,
                XPath = node.SelectSingleNode("fhir:xpath/@value", ns).Value,
                Expression = expression
            };
            result.Add(constraint);
        }
        return result;
    }

    private static bool GetIsConstraintOrPrimitive(XmlElement structureDefinitionElement, XmlNamespaceManager ns)
    {
        // STU3 and R4 have an explicit 'derivation' element
        var derivationNode = structureDefinitionElement.SelectSingleNode("fhir:derivation/@value", ns);
        if (derivationNode != null)
        {
            return derivationNode.Value == "constraint";
        }

        // DSTU2 has a 'constrainedType' element - that applies also to primitive types though, hence the name of the method...
        return structureDefinitionElement.SelectSingleNode("fhir:constrainedType", ns) != null; 
    }

    private static XmlNode GetBaseTypeNode(XmlElement structureDefinitionElement, XmlNamespaceManager ns)
    {
        var result = structureDefinitionElement.SelectSingleNode("fhir:base/@value", ns); // DSTU2
        if (result == null)
        {
            result = structureDefinitionElement.SelectSingleNode("fhir:baseDefinition/@value", ns); // STU3, R4
        }
        return result;
    }

    private static List<PropertyDetails> GetProperties(string className, string resourceName, XmlElement e, XmlNamespaceManager ns, Dictionary<string, string> enumTypesByValueSetUrl)
    {
        var result = new List<PropertyDetails>();
        foreach (var differentialElement in e.SelectNodes("fhir:differential/fhir:element", ns).OfType<XmlElement>())
        {
            var pd = PropertyDetails.Parse(className, resourceName, differentialElement, ns, enumTypesByValueSetUrl);
            if (pd != null)
            {
                result.Add(pd);
            }
        }
        return result;
    }

    private static List<InterfaceDetails> ExtractShared(IEnumerable<LoadedVersion> loadedVersions, Dictionary<string, Dictionary<string, ResourceDetails>> resourcesByNameByVersion)
    {
        var interfaces = new List<InterfaceDetails>();
        var sharedResourcesByName = new Dictionary<string, ResourceDetails>();
        var allNamesInDependencyOrder = resourcesByNameByVersion.Values
            .SelectMany(resourcesByName => TopologicalSort(resourcesByName))
            .Select(resource => resource.Name)
            .Distinct()
            .ToList();
        foreach (var name in allNamesInDependencyOrder)
        {
            var resourcesWithSameName = resourcesByNameByVersion
                .Values
                .Where(resourcesByName => resourcesByName.ContainsKey(name))
                .Select(resourcesByName => resourcesByName[name])
                .ToList();
            if (resourcesWithSameName.Count == 1)
            {
                if (!resourcesWithSameName[0].IsResource())
                {
                    // Share data types that appear only once 
                    var dataType = resourcesWithSameName[0].Clone();
                    foreach (var loadedVersion in loadedVersions)
                    {
                        if (!dataType.Versions.Contains(loadedVersion))
                        {
                            dataType.Versions.Add(loadedVersion);
                        }
                    }
                    sharedResourcesByName.Add(name, dataType);
                    foreach (var resourcesByName in resourcesByNameByVersion.Values)
                    {
                        resourcesByName.Remove(name);
                    }
                }
            }
            else
            {
                var mergedResource = resourcesWithSameName[0].Clone();
                var firstVersion = mergedResource.Versions.Single().Version;
                var merged = true;
                foreach (var otherResource in resourcesWithSameName.Skip(1))
                {
                    var tryMergeResult = mergedResource.TryMerge(otherResource);
                    if (tryMergeResult != null)
                    {
                        merged = false;
                        Console.WriteLine("{0}: merge version {1} failed: {2}", name, otherResource.Versions.Single().Version, tryMergeResult);
                        break;
                    }
                }
                if (merged)
                {
                    var firstVersionResourcesByName = resourcesByNameByVersion[firstVersion];
                    var versionSpecificReferencedFhirTypes = mergedResource
                        .GetReferencedFhirTypes()
                        // Extension / Element and ResourceReference / Identifier definitions are circular, and they should not be version-specific, so we exclude them from the referenced types testing
                        .Where(type => type != "Extension" && type != "Element" && type != "ResourceReference" && type != "Identifier" && firstVersionResourcesByName.ContainsKey(type))
                        .ToList();
                    if (versionSpecificReferencedFhirTypes.Any())
                    {
                        Console.WriteLine("{0} references version-specific types: {1}", name, string.Join(", ",versionSpecificReferencedFhirTypes));
                        merged = false;
                    }
                    else
                    {
                        mergedResource.Simplify(resourcesByNameByVersion);
                        sharedResourcesByName.Add(name, mergedResource);
                        foreach (var resourcesByName in resourcesByNameByVersion.Values)
                        {
                            resourcesByName.Remove(name);
                        }
                    }
                }
                if (!merged)
                {
                    var inter = TryCreateInterface(resourcesWithSameName);
                    if (inter != null)
                    {
                        interfaces.Add(inter);
                        foreach (var resource in resourcesWithSameName)
                        {
                            resource.Interface = inter.Name;
                        }
                    }
                }
            }
        }
        FixFhirTypes(resourcesByNameByVersion, sharedResourcesByName);
        foreach(var inter in interfaces)
        {
            inter.FixReferencedFhirTypes(inter.Name, sharedResourcesByName);
        }
        resourcesByNameByVersion.Add(string.Empty, sharedResourcesByName);
        return interfaces;
    }

    private static InterfaceDetails TryCreateInterface(List<ResourceDetails> resourcesWithSameName)
    {
        var firstResource = resourcesWithSameName[0];
        var inter = new InterfaceDetails
        {
            Versions = new List<LoadedVersion>(firstResource.Versions),
            Name = StringUtils.ToInterfaceName(firstResource.Name),
            Description = firstResource.Description,
            Base = ComputeInterfaceBase(firstResource.BaseType, firstResource.IsPrimitive),
            PrimitiveTypeName = firstResource.GetPrimitiveTypeName()
        };
        var interfacePropertiesByFhirName = CreateInterfaceProperties(firstResource.Properties);
        var componentInterfacesByName = new Dictionary<string, Tuple<InterfaceDetails, Dictionary<string, InterfacePropertyDetails>>>();
        foreach(var firstComponent in firstResource.Components)
        {
            var componentInterface = new InterfaceDetails
            {
                Versions = new List<LoadedVersion>(firstResource.Versions),
                Name = StringUtils.ToInterfaceName(firstResource.Name + firstComponent.Name),
                Base = ComputeInterfaceBase(firstComponent.BaseType, false),
            };
            var componentInterfacePropertiesByFhirName = CreateInterfaceProperties(firstComponent.Properties);
            componentInterfacesByName.Add(firstComponent.Name, Tuple.Create(componentInterface, componentInterfacePropertiesByFhirName) );
        }
        foreach (var otherResource in resourcesWithSameName.Skip(1))
        {
            var newBase = TryMergeInterfaceBase(inter.Base, otherResource.BaseType, otherResource.IsPrimitive);
            if (newBase == null || inter.PrimitiveTypeName != otherResource.GetPrimitiveTypeName())
            {
                return null;
            }
            inter.Base = newBase;
            inter.Versions.AddRange(otherResource.Versions);
            MergeInterfaceProperties(interfacePropertiesByFhirName, otherResource.Properties);
            var newComponentInterfacesByName = new Dictionary<string, Tuple<InterfaceDetails, Dictionary<string, InterfacePropertyDetails>>>();
            foreach (var otherComponent in otherResource.Components)
            {
                if (componentInterfacesByName.TryGetValue(otherComponent.Name, out var componentInterfaces))
                {
                    var newComponentBase = TryMergeInterfaceBase(componentInterfaces.Item1.Base, otherComponent.BaseType, false);
                    if (newComponentBase != null)
                    {
                        componentInterfaces.Item1.Base = newComponentBase;
                        MergeInterfaceProperties(componentInterfaces.Item2, otherComponent.Properties);
                        componentInterfaces.Item1.Versions.AddRange(otherResource.Versions);
                        newComponentInterfacesByName.Add(otherComponent.Name, componentInterfaces);
                    }
                }
            }
            componentInterfacesByName = newComponentInterfacesByName;
        }
        var componentWithInterfaceNames = new HashSet<string>(componentInterfacesByName.Keys);
        inter.Components = componentInterfacesByName.Values
            .Select(tuple => { var i = tuple.Item1; i.Properties = RemovePropertiesReferencingMissingComponents(tuple.Item2.Values, componentWithInterfaceNames); return i; })
            .ToList();
        inter.Properties = RemovePropertiesReferencingMissingComponents(interfacePropertiesByFhirName.Values, componentWithInterfaceNames);
        foreach (var resource in resourcesWithSameName)
        {
            foreach (var property in resource.Properties)
            {
                if (interfacePropertiesByFhirName.ContainsKey(property.FhirName))
                {
                    property.Interface = inter.Name;
                }
            }
            foreach (var componentInterface in componentInterfacesByName)
            {
                var component = resource.Components.FirstOrDefault(c => c.Name == componentInterface.Key);
                if (component != null)
                {
                    component.Interface = componentInterface.Value.Item1.Name;
                    foreach (var property in component.Properties)
                    {
                        if (componentInterface.Value.Item2.ContainsKey(property.FhirName))
                        {
                            property.Interface = component.Interface;
                        }
                    }
                }
            }
        }
        return inter;
    }

    private static string TryMergeInterfaceBase(string currentBase, string baseType, bool isPrimitive)
    {
        var newBase = ComputeInterfaceBase(baseType, isPrimitive);
        if (currentBase == newBase) return currentBase;
        if (currentBase == "IElement" && (newBase == "IBackboneElement" || newBase == "IQuantity")) return currentBase;
        if (newBase == "IElement" && (currentBase == "IBackboneElement" || currentBase == "IQuantity")) return newBase;
        if (currentBase == "IBackboneElement" && newBase == "IQuantity") return currentBase;
        if (newBase == "IBackboneElement" && currentBase == "IQuantity") return newBase;
        if (currentBase == "IResource" && newBase == "IDomainResource") return currentBase;
        if (newBase == "IResource" && currentBase == "IDomainResource") return newBase;
        return null;
    }

    private static string ComputeInterfaceBase(string baseType, bool isPrimitive)
    {
        if (string.IsNullOrEmpty(baseType)) return null;
        if (isPrimitive) return "IPrimitive";
        switch (baseType)
        {
            case "Hl7.Fhir.Model.Element": return "IElement";
            case "Hl7.Fhir.Model.BackboneElement": return "IBackboneElement";
            case "Hl7.Fhir.Model.Quantity": return "IQuantity";
            case "Hl7.Fhir.Model.Resource": return "IResource";
            case "Hl7.Fhir.Model.DomainResource": return "IDomainResource";
            default: throw new InvalidDataException($"Unknown or not support base type {baseType}");
        }
    }

    private static List<InterfacePropertyDetails> RemovePropertiesReferencingMissingComponents(IEnumerable<InterfacePropertyDetails> properties, HashSet<string> componentNames)
    {
        var result = new List<InterfacePropertyDetails>();
        foreach (var property in properties)
        {
            if (StringUtils.TryGetModelClassName(property.PropType, out var className ) || componentNames.Contains(property.PropType))
            {
                result.Add(property);
            }
        }
        return result;
    }

    private static Dictionary<string, InterfacePropertyDetails> CreateInterfaceProperties(List<PropertyDetails> properties)
    {
        var result = new Dictionary<string, InterfacePropertyDetails>();
        foreach (var property in properties)
        {
            result.Add(property.FhirName, property.ToInterface());
        }
        return result;
    }

    private static void MergeInterfaceProperties(
        Dictionary<string, InterfacePropertyDetails> interfacePropertiesByFhirName,
        List<PropertyDetails> properties
    )
    {
        var newInterfacePropertiesByFhirName = new Dictionary<string, InterfacePropertyDetails>();
        foreach (var property in properties)
        {
            if (interfacePropertiesByFhirName.TryGetValue(property.FhirName, out var interfaceProperty)
                && property.TryMergeInto(interfaceProperty))
            {
                newInterfacePropertiesByFhirName.Add(property.FhirName, interfaceProperty);
            }
        }
        interfacePropertiesByFhirName.Clear();
        foreach(var pair in newInterfacePropertiesByFhirName)
        {
            interfacePropertiesByFhirName.Add(pair.Key, pair.Value);
        }
    }

    private static void FixFhirTypes(Dictionary<string, Dictionary<string, ResourceDetails>> resourcesByNameByVersion, Dictionary<string, ResourceDetails> sharedResourcesByName)
    {
        foreach (var pair in resourcesByNameByVersion)
        {
            foreach (var resource in pair.Value.Values)
            {
                resource.FixReferencedTypesFhirVersion(pair.Key, pair.Value);
            }
            foreach (var sharedResource in sharedResourcesByName.Values)
            {
                sharedResource.FixReferencedTypesFhirVersion(pair.Key, pair.Value);
            }
        }
    }

    private static List<ResourceDetails> TopologicalSort(Dictionary<string, ResourceDetails> resourcesByName)
    {
        var result = new List<ResourceDetails>();
        var visited = new HashSet<string>();
        foreach (var resource in resourcesByName.Values)
        {
            if (!visited.Contains(resource.Name))
            {
                TopologicalSort(resourcesByName, resource, visited, result);
            }
        }
        return result;
    }

    private static void TopologicalSort(Dictionary<string, ResourceDetails> resourcesByName, ResourceDetails current, HashSet<string> visited, List<ResourceDetails> result)
    {
        visited.Add(current.Name);
        foreach (var referencedFhirType in current.GetReferencedFhirTypes())
        {
            ResourceDetails referencedResource;
            if (resourcesByName.TryGetValue(referencedFhirType, out referencedResource) && !visited.Contains(referencedResource.Name))
            {
                TopologicalSort(resourcesByName, referencedResource, visited, result);
            }
        }
        result.Add(current);
    }
}

/// <summary>
/// Complete description of a resource component (e.g. LinkComponent within Patient), corresponding to a C# sub-class
/// </summary>
public class ComponentDetails
{
    /// <summary>
    /// Component name = C# sub-class name (e.g. LinkComponent)
    /// </summary>
    public string Name;

    /// <summary>
    /// Base type for the sub-class (typically BackboneElement)
    /// </summary>
    public string BaseType;

    /// <summary>
    /// Shared interface implemented by the C# class
    /// </summary>
    public string Interface;

    /// <summary>
    /// Component properties - corresponding to the C# sub-class properties
    /// </summary>
    public List<PropertyDetails> Properties;

    public PropertyDetails GetProperty(string name)
    {
        return Properties.FirstOrDefault(p => p.Name == name);
    }

    public ComponentDetails Clone(string version)
    {
        return new ComponentDetails
        {
            Name = Name,
            BaseType = BaseType,
            Properties = Properties
                .Select( prop => prop.Clone(version) )
                .ToList(),
        };
    }

    public string TryMerge(string version, ComponentDetails other)
    {
        if (Name != other.Name) return "Name";
        if (BaseType != other.BaseType) return "BaseType";

        return PropertyDetails.TryMerge(Properties, version, other.Properties);
    }

    public static string TryMerge(List<ComponentDetails> components, string version, List<ComponentDetails> otherComponents)
    {
        var otherComponentsByName = otherComponents.ToDictionary(p => p.Name);
        foreach (var component in components)
        {
            if (otherComponentsByName.TryGetValue(component.Name, out var otherComponent))
            {
                var tryMergeResult = component.TryMerge(version, otherComponent);
                if (tryMergeResult != null) return $"Component {component.Name}: {tryMergeResult}";
                otherComponentsByName.Remove(component.Name);
            }
        }
        foreach (var otherComponent in otherComponentsByName.Values)
        {
            components.Add(otherComponent.Clone(version));
        }
        return null;
    }

    public void Simplify(Dictionary<string, Dictionary<string, ResourceDetails>> resourcesByNameByVersion)
    {
        PropertyDetails.Simplify(Properties, resourcesByNameByVersion);
    }

    public static void Simplify(List<ComponentDetails> components, Dictionary<string, Dictionary<string, ResourceDetails>> resourcesByNameByVersion)
    {
        foreach (var component in components)
        {
            component.Simplify(resourcesByNameByVersion);
        }
    }

    /// <summary>
    /// Renders the C# code for this component (sub-)class
    /// </summary>
    /// <param name="version">Target FHIR version: 'DSTU2' 'STU3' etc. or 'All' if common</param>
    /// <returns>C# code lines</returns>
    public IEnumerable<string> Render(string version, string rootInterfaceName, List<ComponentDetails> components)
    {
        var inter = !string.IsNullOrEmpty(Interface) ?
            $"{StringUtils.ModelNamespacePrefix}{Interface}, " :
            string.Empty;
        yield return $"[FhirType({version}, \"{ Name }\")]";
        yield return $"[DataContract]";
        yield return $"public partial class { Name } : { BaseType }, {inter}System.ComponentModel.INotifyPropertyChanged, IComponent";
        yield return $"{{";
        yield return $"    [NotMapped]";
        yield return $"    public override string TypeName {{ get {{ return \"{ Name }\"; }} }}";

        if (!string.IsNullOrEmpty(Interface))
        {
            foreach (var line in StringUtils.RenderInterfaceProperties(rootInterfaceName, Properties, components)) yield return "    " + line;
        }

        foreach (var line in StringUtils.RenderProperties(30, Properties)) yield return "    " + line;

        yield return string.Empty;
        foreach (var line in StringUtils.RenderSerialize(Name, false, true, Properties)) yield return "    " + line;

        yield return string.Empty;
        foreach (var line in StringUtils.RenderSetElementFromJson(Properties)) yield return "    " + line;

        yield return string.Empty;
        foreach (var line in StringUtils.RenderCopyAndComparisonMethods(Name, false, Properties)) yield return "    " + line;

        yield return string.Empty;
        yield return string.Empty;
        foreach (var line in StringUtils.RenderChildrenMethods(Properties)) yield return "    " + line;

        yield return string.Empty;
        yield return string.Empty;
        yield return $"}}";
    }
}

/// <summary>
/// Description of a FHIR resource or data type constraint
/// </summary>
public class ConstraintDetails
{
    /// <summary>
    /// Versions this constraint refers to. 
    /// </summary>
    public List<LoadedVersion> Versions;

    /// <summary>
    /// Key identifying the constraint - e.g. 'pat-1'
    /// </summary>
    public string Key;

    /// <summary>
    /// Severity of constraint violation: 'Error' = the resource is invalid, 'Warning' = resource valid but violating best practice
    /// </summary>
    public string Severity;

    /// <summary>
    /// Human-readable description of the constraint - e.g. 'SHALL at least contain a contact's details or a reference to an organization'
    /// </summary>
    public string Human;

    /// <summary>
    /// Constraint expressed as an XPath - e.g. 'f:name or f:telecom or f:address or f:organization'
    /// </summary>
    public string XPath;

    /// <summary>
    /// Constraint expressed as a Fluent path  - e.g. 'contact.all(name or telecom or address or organization)'
    /// </summary>
    public string Expression;

    public ConstraintDetails Clone()
    {
        return new ConstraintDetails
        {
            Versions = new List<LoadedVersion>(Versions),
            Key = Key,
            Severity = Severity,
            Human = Human,
            XPath = XPath,
            Expression = Expression
        };
    }

    public string TryMerge(ConstraintDetails other)
    {
        if (Key != other.Key) return "Key";
        if (Severity != other.Severity) return "Severity";
        if (Human != other.Human) return "Human";
        if (XPath != other.XPath) return "XPath";
        if (Expression != other.Expression) return "Expression";

        Versions.AddRange(other.Versions);
        return null;
    }

    public static string TryMerge(List<ConstraintDetails> constraints, List<ConstraintDetails> otherConstraints)
    {
        var otherConstraintByKey = otherConstraints.ToDictionary(c => c.Key);
        foreach (var constraint in constraints)
        {
            if (otherConstraintByKey.TryGetValue(constraint.Key, out var otherConstraint))
            {
                if (constraint.TryMerge(otherConstraint) == null)
                {
                    otherConstraintByKey.Remove(constraint.Key);
                };
            }
        }
        foreach (var otherConstraint in otherConstraintByKey.Values)
        {
            constraints.Add(otherConstraint.Clone());
        }
        return null;
    }

    public void Simplify(Dictionary<string, Dictionary<string, ResourceDetails>> resourcesByNameByVersion)
    {
        // Empty
    }

    public static void Simplify(List<ConstraintDetails> constraints, Dictionary<string, Dictionary<string, ResourceDetails>> resourcesByNameByVersion)
    {
        foreach (var constraint in constraints)
        {
            constraint.Simplify(resourcesByNameByVersion);
        }
    }

    /// <summary>
    /// Renders the C# code of the constraint
    /// </summary>
    /// <returns>C# code lines</returns>
    public IEnumerable<string> Render()
    {
        var versionsString = string.Join(",", Versions.Select(loadedVersion => "Hl7.Fhir.Model.Version." + loadedVersion.Version));
        yield return $"new ElementDefinitionConstraint(";
        yield return $"    versions: new[] {{{ versionsString }}},";
        yield return $"    key: { StringUtils.Quote(Key) },";
        var severity = Severity == "Error" ?
            "ConstraintSeverity.Error" :
            "ConstraintSeverity.Warning";
        yield return $"    severity: { severity },";
        if (!string.IsNullOrEmpty(Expression))
        {
            yield return $"    expression: { StringUtils.Quote(Expression) },";
        }
        if (!string.IsNullOrEmpty(Human))
        {
            yield return $"    human: { StringUtils.Quote(Human) },";
        }
        if (!string.IsNullOrEmpty(XPath))
        {
            yield return $"    xpath: { StringUtils.Quote(XPath) }";
        }
        yield return $"),";
    }

    /// <summary>
    /// Generate a C#-valid name for the constraint - e.g. Patient_PAT_1
    /// </summary>
    /// <param name="type">Type (class) containing the constraint</param>
    public string GetName(string type)
    {
        return type + "_" + Key.Replace("-", "_").ToUpper();
    }
}

/// <summary>
/// Descrption of a FHIR resource or data type property (aka field aka element) - corresponding to a C# property (or a couple of them for primitive types: XXXElement and XXXX)
/// </summary>
public class PropertyDetails
{
    /// <summary>
    /// The name of the C# property - e.g. BirthDateElement
    /// </summary>
    public string Name;

    /// <summary>
    /// The name of the FHIR resource or data type property (element) - e.g. birthDate
    /// </summary>
    public string FhirName;

    /// <summary>
    /// C# property data type - e.g. Hl7.Fhir.Model.Date. 
    /// If the property can have multiple values (see CardMax below) this is the type of each individual value, 
    /// and the actual property type is List&lt;XXXX&gt; where XXXX is the value of PropType
    /// </summary>
    public string PropType;

    /// <summary>
    /// Which versions this property applies to: empty if the property applies to all version,
    /// otherwise contains the property version(s) - e.g. if it contains only 'STU3' it means that the property is 
    /// applies only to the STU3 version (ie it is part of the resource or data type it belongs to only for STU3)
    /// </summary>
    public HashSet<string> Versions = new HashSet<string>();

    /// <summary>
    /// Indicates if the property is part of the FHIR resource or data type summary: empty if the property is NOT part of the summary, 
    /// contains only one empty string if the property is part of the summary for all FHIR versions, otherwise contains the 
    /// version(s) for which it is part of the summary - e.g. if it contains only 'DSTU2' it means that the property is 
    /// part of the summary for FHIR DSTU2 and not part of the summary for all other versions.
    /// </summary>
    public HashSet<string> InSummaryVersions = new HashSet<string>();

    /// <summary>
    /// Summary description of the property - e.g. 'The date of birth for the individual'
    /// </summary>
    public string Summary = string.Empty;

    /// <summary>
    /// Minimum cardinality of the property - either '0' (optional) or '1' (required)
    /// </summary>
    public string CardMin;

    /// <summary>
    /// Maximum cardinality of the property - either '1' (single value) or '*' (multiple values)
    /// </summary>
    public string CardMax;

    /// <summary>
    /// Interface this property belongs to
    /// </summary>
    public string Interface;

    public List<string> ReferenceTargets = new List<string>();

    /// <summary>
    /// Native C# data type of the property - e.g. string
    /// Not empty only for property types that can be represented by a native C# type: FhirString as string, FhirDateTime as DateTimeOffset etc
    /// </summary>
    public string NativeType;

    /// <summary>
    /// Native property name - e.g. BirthDate
    /// </summary>
    public string NativeName;

    /// <summary>
    /// Allowed data types for polymorphic properties - their PropType is Hl7.Fhir.Model.Element and AllowedTypesByVersion contains the list of allowed 
    /// types for each FHIR version, with an empty string key representing all versions
    /// </summary>
    public Dictionary<string, List<string>> AllowedTypesByVersion = new Dictionary<string, List<string>>();

    public bool IsXmlAttribute;

    public PropertyDetails Clone(string version)
    {
        return new PropertyDetails
        {
            Name = Name,
            FhirName = FhirName,
            PropType = PropType,
            Versions = new HashSet<string>(new[] { version }),
            InSummaryVersions = InSummaryVersions.Count == 0 ?
                new HashSet<string>() :
                new HashSet<string>(new[] { version }),
            Summary = Summary,
            CardMin = CardMin,
            CardMax = CardMax,
            ReferenceTargets = new List<string>(ReferenceTargets),
            NativeType = NativeType,
            NativeName = NativeName,
            AllowedTypesByVersion = new Dictionary<string, List<string>> { { version, new List<string>(AllowedTypes()) } },
            IsXmlAttribute = IsXmlAttribute
        };
    }

    public string TryMerge(string version, PropertyDetails other)
    {
        if (PropType != other.PropType) return $"PropType '{PropType}' - '{other.PropType}'";
        if (Name != other.Name) return $"Name '{Name}' - '{other.Name}'";
        if (CardMin != other.CardMin) return $"CardMin '{CardMin}' - '{other.CardMin}'";
        if (CardMax != other.CardMax) return $"CardMax '{CardMax}' - '{other.CardMax}'";
        if (ReferenceTargets.Count != other.ReferenceTargets.Count ||
            ReferenceTargets.OrderBy(t => t).Zip(other.ReferenceTargets.OrderBy(t => t), (t1, t2) => t1 == t2).Any(same => !same))
        {
            return $"ReferenceTargets '{string.Join(",", ReferenceTargets)}' - '{string.Join(",", other.ReferenceTargets)}'";
        }

        Versions.Add(version);
        if (other.InSummaryVersions.Count > 0)
        {
            InSummaryVersions.Add(version);
        }
        AllowedTypesByVersion.Add(version, other.AllowedTypes());

        return null;
    }

    public static string TryMerge(List<PropertyDetails> properties, string version, List<PropertyDetails> otherProperties)
    {
        var otherPropertiesByFhirName = otherProperties.ToDictionary(p => p.FhirName);
        foreach (var property in properties)
        {
            if (!otherPropertiesByFhirName.TryGetValue(property.FhirName, out var otherPropery))
            {
                if (property.CardMin != "0") return $"Extra non-optional this property {property.Name}";
            }
            else
            {
                var tryMergeResult = property.TryMerge(version, otherPropery);
                if (tryMergeResult != null) return $"Property {property.Name}: {tryMergeResult}";
                otherPropertiesByFhirName.Remove(property.FhirName);
            }
        }
        foreach (var otherProperty in otherPropertiesByFhirName.Values)
        {
            if (otherProperty.CardMin != "0") return $"Extra non-optional other property {otherProperty.Name}";

            properties.Add(otherProperty.Clone(version));
        }
        return null;
    }

    public void Simplify(Dictionary<string, Dictionary<string, ResourceDetails>> resourcesByNameByVersion)
    {
        const string prefix = "Hl7.Fhir.Model.";

        if (EqualsAnyOrder(Globals.AllVersions, Versions))
        {
            // All versions
            Versions = new HashSet<string>();
        }
        if (InSummaryVersions.Count > 0 && EqualsAnyOrder(Globals.AllVersions, InSummaryVersions))
        {
            InSummaryVersions = new HashSet<string>(new[] { string.Empty });
        }
        var allowedTypesAllVersions = EqualsAnyOrder(Globals.AllVersions, AllowedTypesByVersion.Keys);
        if (allowedTypesAllVersions)
        {
            var allowedTypesList = AllowedTypesByVersion.Values.ToList();
            var allowedTypesAreTheSame = allowedTypesList
                .Skip(1)
                .All(vp => EqualsAnyOrder(allowedTypesList[0], vp));
            var allowedTypesAreTheSameAndNotVersionSpecific = allowedTypesAreTheSame &&
                AllowedTypesByVersion.All(vp => !vp.Value.Any(t => t.StartsWith(prefix) && resourcesByNameByVersion[vp.Key].ContainsKey(t.Substring(prefix.Length))));
            if (allowedTypesAreTheSameAndNotVersionSpecific)
            {
                AllowedTypesByVersion = new Dictionary<string, List<string>> { { string.Empty, allowedTypesList[0] } };
            }
        }
    }

    public static void Simplify(List<PropertyDetails> properties, Dictionary<string, Dictionary<string, ResourceDetails>> resourcesByNameByVersion)
    {
        foreach (var property in properties)
        {
            property.Simplify(resourcesByNameByVersion);
        }
    }

    private List<string> AllowedTypes()
    {
        return AllowedTypesByVersion.Count == 0 ?
            new List<string>() :
            AllowedTypesByVersion.Values.Single();
    }

    private static bool EqualsAnyOrder(IEnumerable<string> first, IEnumerable<string> second)
    {
        return Enumerable.SequenceEqual(first.OrderBy(s => s), second.OrderBy(s => s));
    }

    public InterfacePropertyDetails ToInterface()
    {
        return new InterfacePropertyDetails
        {
            Name = Name,
            Summary = Summary,
            PropType = PropType,
            NativeType = NativeType,
            NativeName = NativeName,
            IsMultiCard = IsMultiCard(),
            ReadOnly = false
        };
    }

    public bool TryMergeInto(InterfacePropertyDetails interfaceProperty)
    {
        return Name == interfaceProperty.Name
            && PropType == interfaceProperty.PropType
            && NativeType == interfaceProperty.NativeType
            && NativeName == interfaceProperty.NativeName
            && IsMultiCard() == interfaceProperty.IsMultiCard;
    }

    public IEnumerable<string> Render(int nPropNum)
    {
        foreach (var line in StringUtils.RenderSummary(Summary)) yield return line;

        var versionsString = VersionsString(Versions);
        var versionsAttribute = string.IsNullOrEmpty(versionsString) ?
            string.Empty :
            ", Versions=" + versionsString;

        var inSummaryVersionsString = VersionsString(InSummaryVersions);
        var inSummaryAttribute = string.IsNullOrEmpty(inSummaryVersionsString) ?
            string.Empty :
            ", InSummary=" + inSummaryVersionsString;

        var choice = PropType == "Hl7.Fhir.Model.Element" ?
            ", Choice=ChoiceType.DatatypeChoice" :
            PropType == "Hl7.Fhir.Model.Resource" ?
                ", Choice=ChoiceType.ResourceChoice" :
                string.Empty;

        yield return $"[FhirElement(\"{FhirName}\"{versionsAttribute}{inSummaryAttribute}, Order={nPropNum}{choice})]";
        if (!string.IsNullOrEmpty(versionsAttribute) || !string.IsNullOrEmpty(inSummaryAttribute) || ReferenceTargets.Count > 0 || AllowedTypesByVersion.Values.Sum(at => at.Count) > 0)
        {
            yield return "[CLSCompliant(false)]";
        }
        if (ReferenceTargets.Count > 0)
        {
            yield return $"[References({ string.Join(",", ReferenceTargets.Select(rt => "\"" + rt + "\"")) })]";
        }
        foreach (var pair in AllowedTypesByVersion)
        {
            if (pair.Value.Count > 0)
            {
                var types = string.Join(",", pair.Value.Select(at => "typeof(" + at + ")"));
                if (string.IsNullOrEmpty(pair.Key))
                {
                    yield return $"[AllowedTypes({ types })]";
                }
                else
                {
                    yield return $"[AllowedTypes(Version=Version.{pair.Key}, Types=new[]{{{ types }}})]";
                }
            }
        }
        if (CardMax != "1" || CardMin != "0")
        {
            yield return $"[Cardinality(Min={ CardMin },Max={ CardMax.Replace("*", "-1") })]";
        }
        yield return "[DataMember]";
        yield return $"public { ConvertedPropTypeWithCard() } { Name }";
        yield return "{";
        if (IsMultiCard())
        {
            yield return $"    get {{ if(_{ Name }==null) _{ Name } = new { PropTypeWithCard() }(); return _{ Name }; }}";
        }
        else
        {
            yield return $"    get {{ return _{ Name }; }}";
        }
        yield return $"    set {{ _{ Name } = value; OnPropertyChanged(\"{ Name }\"); }}";
        yield return "}";
        yield return string.Empty;
        yield return $"private { ConvertedPropTypeWithCard() } _{ Name };";

        if (!string.IsNullOrEmpty(NativeName))
        {
            yield return string.Empty;
            foreach (var line in StringUtils.RenderSummary(Summary)) yield return line;
            yield return "/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>";
            yield return "[NotMapped]";
            yield return "[IgnoreDataMemberAttribute]";
            var nativeType = IsMultiCard() ?
                $"IEnumerable<{ NativeType }>" :
                NativeType;
            yield return $"public { nativeType  } { NativeName }";
            yield return "{";
            if (IsMultiCard())
            {
                yield return $"    get {{ return { Name } != null ? { Name }.Select(elem => elem.Value) : null; }}";
            }
            else
            {
                yield return $"    get {{ return { Name } != null ? { Name }.Value : null; }}";
            }
            yield return "    set";
            yield return "    {";
            yield return "        if (value == null)";
            yield return $"            { Name } = null;";
            yield return "        else";
            var newValue = IsMultiCard() ?
                "List<" + PropType + ">(value.Select(elem=>new " + PropType + "(elem)))" :
                ConvertedPropType() + "(value)";
            yield return $"            { Name } = new { newValue };";
            yield return $"        OnPropertyChanged(\"{ NativeName }\");";
            yield return "    }";
            yield return "}";
        }
    }

    public IEnumerable<string> RenderSerialize()
    {
        var elementVersions = VersionsString(Versions, "All");
        var summaryVersions = VersionsString(InSummaryVersions, "None");
        var isRequired = CardMin == "1" ?
            "true" :
            "false";
        var elementDescription = $"\"{FhirName}\", {elementVersions}, {summaryVersions}, {isRequired}";
        if (!IsMultiCard())
        {
            var isChoice = AllowedTypesByVersion.Any(pair => pair.Value.Count > 1) ?
                "true" :
                "false";
            yield return $"sink.Element({elementDescription}, {isChoice}); {Name}?.Serialize(sink);";
        }
        else
        {
            yield return $"sink.BeginList({elementDescription});";
            if (!string.IsNullOrEmpty(NativeName))
            {
                yield return $"sink.Serialize({Name});";
            }
            else
            {
                yield return $"foreach(var item in {Name})";
                yield return $"{{";
                yield return $"    item?.Serialize(sink);";
                yield return $"}}";
            }
            yield return $"sink.End();";
        }
    }

    public IEnumerable<string> RenderSetElementFromJson()
    {
        var versionsWhen = VersionsWhen(Versions);
        if (IsMultiCard())
        {
            yield return $"case \"{FhirName}\"{versionsWhen}:";
            if (NativeType != null)
            {
                yield return $"case \"_{FhirName}\"{versionsWhen}:";
            }
            yield return $"    source.SetList(this, jsonPropertyName);";
            yield return $"    return true;";
        }
        else if (PropType == "Hl7.Fhir.Model.Resource")
        {
            yield return $"case \"{FhirName}\"{versionsWhen}:";
            yield return $"    {Name} = source.GetResource();";
            yield return $"    return true;";
        }
        else
        {
            var versionsByAllowedType = ComputeVersionsByAllowedType();
            if (versionsByAllowedType != null && versionsByAllowedType.Any())
            {
                foreach (var pair in versionsByAllowedType)
                {
                    foreach (var line in RenderSetElementXFromJson(pair.Key, pair.Value)) yield return line;
                }
            }
            else if (NativeType == null)
            {
                yield return $"case \"{FhirName}\"{versionsWhen}:";
                yield return $"    {Name} = source.Populate({Name});";
                yield return $"    return true;";
            }
            else
            {
                yield return $"case \"{FhirName}\"{versionsWhen}:";
                yield return $"    {Name} = source.PopulateValue({Name});";
                yield return $"    return true;";
                yield return $"case \"_{FhirName}\"{versionsWhen}:";
                yield return $"    {Name} = source.Populate({Name});";
                yield return $"    return true;";
            }
        }
    }

    private Dictionary<string, HashSet<string>> ComputeVersionsByAllowedType()
    {
        if (AllowedTypesByVersion == null || !AllowedTypesByVersion.Any())
        {
            return null;
        }

        if (AllowedTypesByVersion.TryGetValue(string.Empty, out var allVersionsTypes))
        {
            return allVersionsTypes.ToDictionary(type => type, _ => (HashSet<string>)null);
        }

        var result = new Dictionary<string, HashSet<string>>();
        foreach (var pair in AllowedTypesByVersion)
        {
            var version = pair.Key;
            foreach (var type in pair.Value)
            {
                if (!result.TryGetValue(type, out var versions))
                {
                    versions = new HashSet<string>();
                    result.Add(type, versions);
                }
                versions.Add(version);
            }
        }
        foreach (var versions in result.Values)
        {
            if (EqualsAnyOrder(Globals.AllVersions, versions))
            {
                versions.Clear();
            }
        }
        return result;
    }

    private IEnumerable<string> RenderSetElementXFromJson(string type, HashSet<string> versions)
    {
        var versionsWhen = VersionsWhen(versions);
        var fhirType = Globals.FhirDataTypeByCsType[type];
        var propertyName = FhirName + StringUtils.FirstToUpper(fhirType);
        yield return $"case \"{propertyName}\"{versionsWhen}:";
        if (PrimitiveType.Get(fhirType) == null)
        {
            yield return $"    source.CheckDuplicates<{type}>({Name}, \"{FhirName}\");";
            yield return $"    {Name} = source.Populate({Name} as {type});";
            yield return $"    return true;";
        }
        else
        {
            yield return $"    source.CheckDuplicates<{type}>({Name}, \"{FhirName}\");";
            yield return $"    {Name} = source.PopulateValue({Name} as {type});";
            yield return $"    return true;";
            yield return $"case \"_{propertyName}\"{versionsWhen}:";
            yield return $"    source.CheckDuplicates<{type}>({Name}, \"{FhirName}\");";
            yield return $"    {Name} = source.Populate({Name} as {type});";
            yield return $"    return true;";

        }
    }

    public IEnumerable<string> RenderSetListElementFromJson()
    {
        var versionsWhen = VersionsWhen(Versions);
        yield return $"case \"{FhirName}\"{versionsWhen}:";
        if (NativeType == null)
        {
            yield return $"    source.PopulateListItem({Name}, index);";
            yield return $"    return true;";
        }
        else
        {
            yield return $"    source.PopulatePrimitiveListItemValue({Name}, index);";
            yield return $"    return true;";
            yield return $"case \"_{FhirName}\"{versionsWhen}:";
            yield return $"    source.PopulatePrimitiveListItem({Name}, index);";
            yield return $"    return true;";

        }
    }

    private static string VersionsWhen(HashSet<string> versions)
    {
        var versionsString = VersionsString(versions);
        if (string.IsNullOrEmpty(versionsString)) return null;
        return $" when source.IsVersion({versionsString})";
    }

    private static string VersionsString(HashSet<string> versions, string ifEmpty = "")
    {
        const string prefix = "Hl7.Fhir.Model.Version.";
        if (versions == null || versions.Count == 0)
        {
            if (string.IsNullOrEmpty(ifEmpty)) return ifEmpty;
            return prefix + ifEmpty;
        }
        return string.Join("|", versions.Select(v => prefix + (string.IsNullOrEmpty(v) ? "All" : v)));
    }

    /// <summary>
    /// If needed renders the property as an explicit interface member - ie if the interface defines a member with a different signature than 
    /// the property itself. For example the IPatient interface defines an 'IEnumerable{IHumanName} Name' property that is not implemented by 
    /// the `List{xxx.HumanName> Name` properties of the xxx.Patient classes (xxx=FHIR version), so they all need an explicit 
    /// 'IEnumerable{IHumanName} IPatient.Name' property.
    /// </summary>
    /// <param name="rootInterfaceName">The root interface name - eg IPatient ('root' because it is the same also if the property belongs to a component
    /// and not to the resource itself)</param>
    /// <param name="components">Componens declared by the resource containing this property</param>
    /// <returns>Code lines or empty enumeration if rendering as an explicit interface is not needed</returns>
    public IEnumerable<string> RenderAsInterfaceIfNeeded(string rootInterfaceName, List<ComponentDetails> components)
    {
        string interfacePropertyBaseType = null;
        if (!string.IsNullOrEmpty(Interface))
        {
            // The property has an associated interface
            if (!StringUtils.TryGetModelClassName(PropType, out var className))
            {
                // The property type is a component
                if (!string.IsNullOrEmpty(components?.FirstOrDefault(c => c.Name == PropType)?.Interface))
                {
                    // The component has an interface
                    interfacePropertyBaseType = StringUtils.ModelNamespacePrefix + rootInterfaceName + PropType;
                }
            }
            else if (className.Contains("."))
            {
                // The property type is version-specific (eg HumanName)
                var baseClassName = className.Substring(className.IndexOf('.') + 1);
                interfacePropertyBaseType = StringUtils.ModelNamespacePrefix + StringUtils.ToInterfaceName(baseClassName);
            }
        }
        if (!string.IsNullOrEmpty(interfacePropertyBaseType))
        {
            var interfacePropertyType = IsMultiCard() ?
                $"IEnumerable<{ interfacePropertyBaseType }>" :
                interfacePropertyBaseType;
            yield return string.Empty;
            yield return "[NotMapped]";
            yield return $"{ interfacePropertyType } { StringUtils.ModelNamespacePrefix }{ Interface }.{ Name } {{ get {{ return { Name }; }} }}";
        }
    }

    public IEnumerable<string> RenderAsChildWithName()
    {
        // Exclude special properties encoded as Xml attributes (Element.Id) - not derived from Base
        if (IsXmlAttribute) yield break;

        if (IsMultiCard())
        {
            yield return $"foreach (var elem in {Name}) {{ if (elem != null) yield return new ElementValue(\"{FhirName}\", elem); }}";
        }
        else
        {
            yield return $"if ({Name} != null) yield return new ElementValue(\"{FhirName}\", {Name});";
        }
    }

    public IEnumerable<string> RenderAsChildWithoutName()
    {
        // Exclude special properties encoded as Xml attributes (Element.Id) - not derived from Base
        if (IsXmlAttribute) yield break;

        if (IsMultiCard())
        {
            yield return $"foreach (var elem in {Name}) {{ if (elem != null) yield return elem; }}";
        }
        else
        {
            yield return $"if ({Name} != null) yield return {Name};";
        }
    }

    public string PropTypeWithCard()
    {
        if (CardMax == "*")
            return "List<" + PropType + ">";
        return PropType;
    }

    public string ConvertedPropType()
    {
        return StringUtils.RemoveCodeNamespace(PropType);
    }

    public string ConvertedPropTypeWithCard()
    {
        return StringUtils.RemoveCodeNamespace(PropTypeWithCard());
    }

    public bool IsMultiCard()
    {
        if (CardMax == "*")
            return true;
        return false;
    }

    public IEnumerable<string> GetReferencedFhirTypes()
    {
        // Ignore AllowedTypes because they are version-specific
        if (StringUtils.TryGetModelClassName(PropType, out var className))
        {
            yield return className;
        }
    }

    public void FixReferencedFhirTypes(string version, Dictionary<string, ResourceDetails> resourcesByName)
    {
        PropType = StringUtils.FixTypeFhirVersion(PropType, version, resourcesByName);
        var allowedTypesVersion = AllowedTypesByVersion.ContainsKey(version) ?
            version :
            AllowedTypesByVersion.ContainsKey(string.Empty) ?
                string.Empty :
                null;
        if (allowedTypesVersion != null)
        {
            AllowedTypesByVersion[allowedTypesVersion] = AllowedTypesByVersion[allowedTypesVersion]
                .Select(allowedType => StringUtils.FixTypeFhirVersion(allowedType, version, resourcesByName))
                .ToList();
        }
    }

    public static string ConvertPropertyType(string propType, XmlElement element, XmlNamespaceManager ns)
    {
        var nativeType = PrimitiveType.Get(propType);
        if (nativeType != null )
        {
            return "Hl7.Fhir.Model." + nativeType.ClassName;
        }
        switch (propType)
        {
            case "Reference": return "Hl7.Fhir.Model.ResourceReference";
            case "Quantity":
                return GetQuantityType(element, ns);
        }
        return "Hl7.Fhir.Model." + propType;
    }

    public static PropertyDetails Parse(string className, string resourceName, XmlElement element, XmlNamespaceManager ns, Dictionary<string, string> enumTypesByValueSetUrl)
    {
        var result = new PropertyDetails();

        if (element.SelectSingleNode("fhir:isSummary[@value = 'true']", ns) != null)
        {
            result.InSummaryVersions.Add(string.Empty);
        }

        if (element.SelectSingleNode("fhir:representation[@value = 'xmlAttr']", ns) != null)
        {
            result.IsXmlAttribute = true;
        }

        var shortNode = element.SelectSingleNode("fhir:short/@value", ns);
        if (shortNode != null)
        {
            result.Summary = shortNode.Value;
        }

        var typeCodeNode = element.SelectSingleNode("fhir:type/fhir:code/@value", ns);
        if (typeCodeNode != null)
        {
            const string fhirPathPrefix = "http://hl7.org/fhirpath/System.";

            var propType = typeCodeNode.Value ?? string.Empty;
            if (propType.StartsWith(fhirPathPrefix))
            {
                // In R4 special primitive values have a FHIRPath system type
                propType = propType.Substring(fhirPathPrefix.Length).ToLower();
            }
            result.PropType = propType;
        }
        else
        {
            var typeJsonTypeNode = element.SelectSingleNode("fhir:type/fhir:code/fhir:extension[@url='http://hl7.org/fhir/StructureDefinition/structuredefinition-json-type']/fhir:valueString/@value", ns);
            if (typeJsonTypeNode != null)
            {
                result.PropType = typeJsonTypeNode.Value;
            }
            else
            {
                result.PropType = "BackboneElement";
            }
        }

        // Check for a nameReference to another property
        var parsedReferencedType = ParseReferencedType(element, ns, enumTypesByValueSetUrl);
        if (parsedReferencedType != null)
        {
            result.PropType = parsedReferencedType.PropType;
            result.NativeType = parsedReferencedType.NativeType;
            result.ReferenceTargets = parsedReferencedType.ReferenceTargets;
            result.AllowedTypesByVersion = parsedReferencedType.AllowedTypesByVersion;
        }

        result.FhirName = element.SelectSingleNode("fhir:path/@value", ns).Value;
        if (!result.FhirName.StartsWith(resourceName + "."))
            return null;
        result.FhirName = result.FhirName.Substring(resourceName.Length + 1);

        // Strip out any child component props
        if (result.FhirName.Contains("."))
            return null;

        result.Name = result.FhirName.ToUpper().Substring(0, 1) + result.FhirName.Substring(1); // convert this to the actual property name

        // A property name cannot be the same as the classname, otherwise c# thinks this is a constructor!
        if (result.Name == className)
            result.Name += "_";

        result.CardMin = element.SelectSingleNode("fhir:min/@value", ns).Value;
        result.CardMax = element.SelectSingleNode("fhir:max/@value", ns).Value;

        if (parsedReferencedType == null)
        {
            SetType(result, element, ns);
        }

        if (result.Name.Contains("[x]"))
        {
            result.PropType = "Hl7.Fhir.Model.Element";
            result.Name = result.Name.Substring(0, result.Name.IndexOf("["));
            result.FhirName = result.FhirName.Substring(0, result.FhirName.IndexOf("["));
            var allowedTypes = new List<string>();
            result.AllowedTypesByVersion = new Dictionary<string, List<string>>() { { string.Empty, allowedTypes } };

            foreach (var erp in element.SelectNodes("fhir:type/fhir:code/@value", ns).OfType<XmlAttribute>())
            {
                string allowType = ConvertPropertyType(erp.Value.Substring(erp.Value.LastIndexOf("/") + 1), element, ns);
                if (!allowedTypes.Contains(allowType))
                {
                    allowedTypes.Add(allowType);
                }
            }
        }

        if (result.PropType == "Hl7.Fhir.Model.ResourceReference")
        {
            result.ReferenceTargets = GetPossibleReferenceTargets(element, ns);
        }

        if (result.PropType == "Code" || result.PropType == "Hl7.Fhir.Model.Code")
        {
            var codeRequiredBinding = GetCodeEnumName(element, ns, resourceName, enumTypesByValueSetUrl);
            if (!string.IsNullOrEmpty(codeRequiredBinding))
            {
                result.PropType = result.PropType + "<" + codeRequiredBinding + ">";
                result.NativeType = codeRequiredBinding + "?";
            }
        }

        if (result.NativeName != null && result.NativeName.Contains("[x]"))
        {
            result.NativeName = null;
            result.NativeType = null;
        }

        return result;
    }

    public static PropertyDetails ParseType(XmlElement element, XmlNamespaceManager ns, Dictionary<string, string> enumTypesByValueSetUrl)
    {
        PropertyDetails result = new PropertyDetails();
        string resourceBase = element.SelectSingleNode("fhir:path/@value", ns).Value;
        if (resourceBase.Contains("."))
            resourceBase = resourceBase.Substring(0, resourceBase.IndexOf("."));
        if (element.SelectSingleNode("fhir:type/fhir:code/@value", ns) != null)
            result.PropType = element.SelectSingleNode("fhir:type/fhir:code/@value", ns).Value;
        else
            result.PropType = "BackboneElement";
        result.FhirName = element.SelectSingleNode("fhir:path/@value", ns).Value;
        //	if (result.FhirName.StartsWith(ResourceBase + "."))
        //		result.FhirName = result.FhirName.Substring(result.FhirName.IndexOf(".") + 1);
        result.FhirName = result.FhirName.ToUpper().Substring(0, 1) + result.FhirName.Substring(1); // convert this to the actual property name

        // Check for a nameReference to another property
        var parsedReferencedType = ParseReferencedType(element, ns, enumTypesByValueSetUrl);
        if (parsedReferencedType != null)
        {
            result.PropType = parsedReferencedType.PropType;
            result.NativeType = parsedReferencedType.NativeType;
        }
        else
        {
            SetType(result, element, ns);
        }

        if (result.PropType == "Hl7.Fhir.Model.ResourceReference")
        {
            result.ReferenceTargets = GetPossibleReferenceTargets(element, ns);
        }

        if (result.PropType == "Code" || result.PropType == "Hl7.Fhir.Model.Code")
        {
            var codeRequiredBinding = GetCodeEnumName(element, ns, resourceBase, enumTypesByValueSetUrl);
            if (!string.IsNullOrEmpty(codeRequiredBinding))
            {
                result.PropType = result.PropType + "<" + codeRequiredBinding + ">";
                result.NativeType = codeRequiredBinding + "?";
            }
        }

        return result;
    }

    private static void SetType(PropertyDetails result, XmlElement element, XmlNamespaceManager ns)
    {
        var primitiveType = PrimitiveType.Get(result.PropType);
        if (primitiveType != null)
        {
            result.NativeName = result.Name;
            result.Name = result.Name + "Element";
            result.PropType = "Hl7.Fhir.Model." + primitiveType.ClassName;
            result.NativeType = primitiveType.NativeType;
        }
        else
        {
            switch (result.PropType)
            {
                case "Reference":
                    result.PropType = "Hl7.Fhir.Model.ResourceReference";
                    break;
                case "Resource":
                    result.PropType = "Hl7.Fhir.Model.Resource";
                    result.AllowedTypesByVersion = new Dictionary<string, List<string>>() { { string.Empty, new List<string> { "Hl7.Fhir.Model.Resource" } } };
                    break;
                case "Element":
                case "BackboneElement":
                    result.PropType = ComputeBackboneComponentType(result.FhirName, element, ns);
                    break;
                case "Quantity":
                    result.PropType = GetQuantityType(element, ns);
                    break;
                default:
                    result.PropType = "Hl7.Fhir.Model." + result.PropType;
                    break;
            }
        }
    }

    private static string GetCodeEnumName(XmlElement element, XmlNamespaceManager ns, string resourceName, Dictionary<string, string> enumTypesByValueSetUrl)
    {
        var codeRequiredBinding = ValueSet.GetRequiredBindingValueSetUrl(element, ns);
        if (string.IsNullOrEmpty(codeRequiredBinding) || codeRequiredBinding == "http://hl7.org/fhir/ValueSet/operation-parameter-type")
        {
            return null;
        }

        if (enumTypesByValueSetUrl.TryGetValue(codeRequiredBinding, out var enumType))
        {
            return enumType;
        }

        return null;
    }

    private static PropertyDetails ParseReferencedType(XmlElement element, XmlNamespaceManager ns, Dictionary<string, string> enumTypesByValueSetUrl)
    {
        var referencedTypeElement = GetReferencedTypeElement(element, ns);
        if (referencedTypeElement != null)
        {
            return ParseType(referencedTypeElement, ns, enumTypesByValueSetUrl);
        }
        return null;
    }

    private static XmlElement GetReferencedTypeElement(XmlElement element, XmlNamespaceManager ns)
    {
        // DSTU2
        var attrNameRef = element.SelectSingleNode("fhir:nameReference/@value", ns) as XmlAttribute;
        if (attrNameRef != null)
        {
            var nameRef = attrNameRef.Value;
            return (element.ParentNode as XmlElement).SelectSingleNode("fhir:element[fhir:name/@value = '" + nameRef + "']", ns) as XmlElement;
        }

        // STU3
        var attrContentRef = element.SelectSingleNode("fhir:contentReference/@value", ns) as XmlAttribute;
        if (attrContentRef != null)
        {
            var contentRef = attrContentRef.Value;
            if (!string.IsNullOrEmpty(contentRef) && contentRef.StartsWith("#"))
            {
                return (element.ParentNode as XmlElement).SelectSingleNode("fhir:element[@id = '" + contentRef.Substring(1) + "']", ns) as XmlElement;
            }
        }

        return null;
    }

    private static string ComputeBackboneComponentType(string fhirName, XmlElement element, XmlNamespaceManager ns)
    {
        var componentName = (XmlAttribute)element.SelectSingleNode("fhir:extension[@url = 'http://hl7.org/fhir/StructureDefinition/structuredefinition-explicit-type-name']/fhir:valueString/@value", ns);
        if (componentName != null)
        {
            return componentName.Value + "Component";
        }
        var v = fhirName;
        var index = v.LastIndexOf(".");
        if (index >= 0) v = v.Substring(index + 1);
        v = v.Substring(0, 1).ToUpper() + v.Substring(1);
        return v + "Component";
    }

    private static List<string> GetPossibleReferenceTargets(XmlElement element, XmlNamespaceManager ns)
    {
        var result = new List<string>();
        foreach (var erp in element.SelectNodes("fhir:type[fhir:code/@value = 'Reference']/fhir:profile/@value|fhir:type[fhir:code/@value = 'Reference']/fhir:targetProfile/@value", ns).OfType<XmlAttribute>())
        {
            var resource = erp.Value.Substring(erp.Value.LastIndexOf("/") + 1);
            if (resource != "Resource")
            {
                result.Add(resource);
            }
        }
        return result;
    }

    private static string GetQuantityType(XmlElement element, XmlNamespaceManager ns)
    {
        var typeProfile = (XmlAttribute)element.SelectSingleNode("fhir:type/fhir:profile/@value", ns);
        if (typeProfile == null)
        {
            return "Hl7.Fhir.Model.Quantity";
        }
        if (typeProfile.Value == "http://hl7.org/fhir/StructureDefinition/SimpleQuantity")
        {
            return "Hl7.Fhir.Model.SimpleQuantity";
        }
        if (typeProfile.Value == "http://hl7.org/fhir/StructureDefinition/Money")
        {
            return "Hl7.Fhir.Model.Money";
        }
        if (typeProfile.Value == "http://hl7.org/fhir/StructureDefinition/Age")
        {
            return "Hl7.Fhir.Model.Age";
        }
        if (typeProfile.Value == "http://hl7.org/fhir/StructureDefinition/Duration")
        {
            return "Hl7.Fhir.Model.Duration";
        }
        return "Hl7.Fhir.Model.Quantity";
    }
}

public class ModelInfoBase
{
    protected List<string> _resourceNames;
    protected List<Tuple<string, string>> _typesNameAndType;
    protected List<Tuple<string, string>> _resourcesNameAndType;

    protected IEnumerable<string> RenderSupportedResources()
    {
        yield return $"public static List<string> SupportedResources =";
        yield return $"    new List<string>";
        yield return $"    {{";
        foreach (var resourceName in _resourceNames)
        {
            yield return $"        \"{ resourceName }\",";
        }
        yield return $"    }};";
    }

    protected IEnumerable<string> RenderFhirTypeToCsType()
    {
        yield return $"public static Dictionary<string,Type> FhirTypeToCsType =";
        yield return $"    new Dictionary<string,Type>()";
        yield return $"    {{";
        foreach (var nameAndType in _typesNameAndType)
        {
            yield return $"        {{ \"{ nameAndType.Item1 }\", typeof({ nameAndType.Item2 }) }},";
        }
        yield return string.Empty;
        foreach (var nameAndType in _resourcesNameAndType)
        {
            yield return $"        {{ \"{ nameAndType.Item1 }\", typeof({ nameAndType.Item2 }) }},";
        }
        yield return $"    }};";
    }

    protected IEnumerable<string> RenderFhirCsTypeToString()
    {
        yield return $"public static Dictionary<Type,string> FhirCsTypeToString =";
        yield return $"    new Dictionary<Type,string>()";
        yield return $"    {{";
        foreach (var nameAndType in _typesNameAndType)
        {
            yield return $"        {{ typeof({ nameAndType.Item2 }), \"{ nameAndType.Item1 }\" }},";
        }
        yield return string.Empty;
        foreach (var nameAndType in _resourcesNameAndType)
        {
            yield return $"        {{ typeof({ nameAndType.Item2 }), \"{ nameAndType.Item1 }\" }},";
        }
        yield return $"    }};";
    }

    protected IEnumerable<string> RenderCreateResource()
    {
        yield return $"public static Resource CreateResource(string resourceType)";
        yield return $"{{";
        yield return $"    switch (resourceType)";
        yield return $"    {{";
        foreach (var nameAndType in _resourcesNameAndType)
        {
            if (nameAndType.Item1 != "Resource" && nameAndType.Item1 != "DomainResource")
            {
                yield return $"        case \"{ nameAndType.Item1 }\":";
                yield return $"            return new { nameAndType.Item2 }();";
            }
        }
        yield return $"    }}";
        yield return $"    return null;";
        yield return $"}}";
    }
}

public class AllVersionsModelInfo : ModelInfoBase
{
    private IEnumerable<LoadedVersion> _versions;

    public AllVersionsModelInfo(Dictionary<string, Dictionary<string, ResourceDetails>> resourcesByNameByVersion, IEnumerable<LoadedVersion> versions)
    {
        _resourceNames = resourcesByNameByVersion
            .Values
            .SelectMany(resourcesByName => resourcesByName.Values)
            .Where(r => r.IsResource() && r.Name != "DomainResource" && r.Name != "Resource")
            .Select(r => r.Name)
            .Distinct()
            .OrderBy(name => name)
            .ToList();
        _typesNameAndType = resourcesByNameByVersion
            .SelectMany(
                pair => pair.Value.Values
                    .Where(r => !r.IsResource())
                    .Select(r => Tuple.Create(r.FhirName, "Hl7.Fhir.Model." + (string.IsNullOrEmpty(pair.Key) ? string.Empty : pair.Key + ".") + r.Name))
            )
            .Distinct()
            .OrderBy(nameAndType => nameAndType.Item1)
            .ToList();
        _resourcesNameAndType = resourcesByNameByVersion
            .SelectMany(
                pair => pair.Value.Values
                    .Where(r => r.IsResource())
                    .Select(r => Tuple.Create(r.FhirName, "Hl7.Fhir.Model." + (string.IsNullOrEmpty(pair.Key) ? string.Empty : pair.Key + ".") + r.Name))
            )
            .Distinct()
            .OrderBy(nameAndType => nameAndType.Item1)
            .ToList();
        _versions = versions;
    }

    public void Write(string filePath)
    {
        using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
        {
            foreach (var line in StringUtils.RenderFileHeader(_versions)) writer.WriteLine(line);
            foreach (var line in Render()) writer.WriteLine("    " + line);
            writer.WriteLine();
            writer.WriteLine("}");
        }
    }

    public IEnumerable<string> Render()
    {
        yield return $"public static partial class AllVersionsModelInfo";
        yield return $"{{";
        foreach (var line in RenderSupportedResources()) yield return "    " + line;
        yield return string.Empty;
        foreach (var line in RenderFhirCsTypeToString()) yield return "    " + line;
        yield return $"}}";
    }

    public Dictionary<string, string> GetFhirDataTypeByCsType()
    {
        return _typesNameAndType.ToDictionary( tuple => tuple.Item2, tuple => tuple.Item1 );
    }
}

public class ModelInfo : ModelInfoBase
{
    private LoadedVersion _version;
    private List<SearchParameter> _searchParameters;

    public ModelInfo(IEnumerable<ResourceDetails> versionResources, IEnumerable<ResourceDetails> sharedResources)
    {
        _resourceNames = versionResources
            .Concat(sharedResources)
            .Where(r => r.IsResource() && r.Name != "DomainResource" && r.Name != "Resource")
            .OrderBy(r => r.Name)
            .Select(r => r.Name)
            .ToList();
        _version = versionResources.First().Versions.Single();
        _typesNameAndType = versionResources
            .Where(r => !r.IsResource())
            .Select(r => Tuple.Create(r.FhirName, "Hl7.Fhir.Model." + _version.Version + "." + r.Name))
            .Concat(
                sharedResources
                    .Where(r => !r.IsResource())
                    .Select(r => Tuple.Create(r.FhirName, "Hl7.Fhir.Model." + r.Name))
            )
            .OrderBy(nameAndType => nameAndType.Item1)
            .ToList();
        _resourcesNameAndType = versionResources
            .Where(r => r.IsResource())
            .Select(r => Tuple.Create(r.Name, "Hl7.Fhir.Model." + _version.Version + "." + r.Name))
            .Concat(
                sharedResources
                    .Where(r => r.IsResource())
                    .Select(r => Tuple.Create(r.Name, "Hl7.Fhir.Model." + r.Name))
            )
            .OrderBy(nameAndType => nameAndType.Item1)
            .ToList();
        _searchParameters = new List<SearchParameter>();
        foreach (var resourceName in _resourceNames)
        {
            var spNodes = _version.SearchParameters.DocumentElement.SelectNodes("/fhir:Bundle/fhir:entry/fhir:resource/fhir:SearchParameter[fhir:base/@value = '" + resourceName + "']", _version.NSSP);
            foreach (var sp in spNodes.OfType<XmlElement>())
            {
                _searchParameters.Add(new SearchParameter(resourceName, sp, _version.NSSP));
            }
        }
    }

    public void Write(string filePath)
    {
        using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
        {
            foreach (var line in StringUtils.RenderFileHeader(new[] { _version })) writer.WriteLine(line);
            foreach (var line in Render()) writer.WriteLine("    " + line);
            writer.WriteLine();
            writer.WriteLine("}");
        }
    }

    public IEnumerable<string> Render()
    {
        yield return $"/*";
        yield return $"* A class with methods to retrieve information about the";
        yield return $"* FHIR definitions based on which this assembly was generated.";
        yield return $"*/";
        yield return $"public static partial class ModelInfo";
        yield return $"{{";
        foreach (var line in RenderSupportedResources()) yield return "    " + line;
        yield return string.Empty;
        yield return $"    public static string Version";
        yield return $"    {{";
        yield return $"        get {{ return \"{ _version.FhirVersion }\"; }}";
        yield return $"    }}";
        yield return string.Empty;
        foreach (var line in RenderFhirTypeToCsType()) yield return "    " + line;
        yield return string.Empty;
        foreach (var line in RenderFhirCsTypeToString()) yield return "    " + line;
        yield return string.Empty;
        foreach (var line in RenderCreateResource()) yield return "    " + line;
        yield return string.Empty;
        yield return $"    public static List<SearchParamDefinition> SearchParameters =";
        yield return $"        new List<SearchParamDefinition>";
        yield return $"        {{";
        foreach (var searchParameter in _searchParameters)
        {
            foreach (var line in searchParameter.Render()) yield return "            " + line;
        }
        yield return $"        }};";

        yield return $"}}";
    }
}

public class SearchParameter
{
    private string _resourceName;
    private string _name;
    private string _description;
    private string _outputType;
    private string _path;
    private string _xpath;
    private List<string> _targets;
    private string _expression;
    private string _url;

    public SearchParameter(string resourceName, XmlElement sp, XmlNamespaceManager ns)
    {
        _resourceName = resourceName;
        _name = sp.SelectSingleNode("fhir:name/@value", ns).Value;
        _url = sp.SelectSingleNode("fhir:url/@value", ns).Value;
        var appliesToMultipleResources = sp.SelectNodes("fhir:base", ns).Count > 1;
        var description = sp.SelectSingleNode("fhir:description/@value", ns)?.Value;
        if (!appliesToMultipleResources)
        {
            _description = description;
        }
        else
        {
            // For multi-resources search parameters the description is something like 
            // 'Multiple Resources: &#xD;&#xA;&#xD;&#xA;* [ReferralRequest](referralrequest.html): Who the referral is about&#xD;&#xA;*....'
            var resourceDescription = description
                .Split( new[] { "\r\n" }, StringSplitOptions.None )
                .FirstOrDefault( d => d.Contains( $"[{resourceName}]" ) );
            if (resourceDescription == null)
            {
                resourceDescription = string.Empty;
            }
            else
            {
                var index = resourceDescription.IndexOf("):");
                if (index > 0)
                {
                    resourceDescription = resourceDescription.Substring(index + 2);
                }
                resourceDescription = resourceDescription.Trim(new[] { ' ', '\r', '\n'});
            }
            _description = resourceDescription;
        }
        _outputType = string.Empty;
        var searchType = sp.SelectSingleNode("fhir:type/@value", ns).Value;
        switch (searchType)
        {
            case "number": _outputType = "Number"; break;
            case "date": _outputType = "Date"; break;
            case "string": _outputType = "String"; break;
            case "token": _outputType = "Token"; break;
            case "reference": _outputType = "Reference"; break;
            case "composite": _outputType = "Composite"; break;
            case "quantity": _outputType = "Quantity"; break;
            case "uri": _outputType = "Uri"; break;
            case "special": _outputType = "Special"; break;
            default: throw new InvalidDataException($"Unknown or not supported search type '{searchType}'");
        }
        var xpath = sp.SelectSingleNode("fhir:xpath/@value", ns)?.Value ??
            string.Empty;
        var expression = sp.SelectSingleNode("fhir:expression/@value", ns)?.Value ??
            sp.SelectSingleNode("fhir:extension[@url='http://hl7.org/fhir/StructureDefinition/searchparameter-expression']/fhir:valueString/@value", ns)?.Value ??
            string.Empty;
        var expressions = expression.Split(new char[] { '|', ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var path = string.IsNullOrEmpty(expression) ?
            string.Empty :
            "\"" + string.Join("\", \"", expressions) + "\"";
        if (!appliesToMultipleResources)
        {
            _xpath = xpath;
            _path += path;
            _expression = expression;
        }
        else
        {
            var xpaths = xpath.Split(new char[] { '|', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var resourceXPaths = xpaths.Where(xp => xp.StartsWith($"f:{resourceName}/")).ToArray();
            _xpath = resourceXPaths.Any() ?
                string.Join( " | ", resourceXPaths) :
                xpath;
            var resourceExpressions = expressions.Where(expr => expr.StartsWith($"{resourceName}.")).ToArray();
            if (!resourceExpressions.Any())
            {
                _path = path;
                _expression = expression;
            }
            else
            {
                _path = "\"" + string.Join("\", \"", resourceExpressions) + "\"";
                _expression = string.Join( " | ", resourceExpressions );
            }
        }

        var code = sp.SelectSingleNode("fhir:code/@value", ns).Value;
        if (code == "patient" && searchType == "reference")
        {
            // Hack to fix the 'patient' search parameters that incorrectly target multiple resouurces
            _targets = new List<string> { "ResourceType.Patient" };
        }
        else if (code == "encounter" && searchType == "reference")
        {
            // Hack to fix the 'encounter' search parameters that incorrectly target multiple resouurces
            _targets = new List<string> { "ResourceType.Encounter" };
        }
        else
        {
            _targets = new List<string>();
            foreach (var et in sp.SelectNodes("fhir:target/@value", ns).OfType<XmlAttribute>())
            {
                _targets.Add("ResourceType." + et.Value);
            }
            _targets.Sort();
        }
    }

    public IEnumerable<string> Render()
    {
        var xpath = !string.IsNullOrEmpty(_xpath) ?
            $", XPath = \"{ _xpath }\"" :
            string.Empty;
        var expression = !string.IsNullOrEmpty(_expression) ?
            $", Expression = \"{ _expression }\"" :
            string.Empty;
        var target = _targets.Count > 0 ?
            $", Target = new ResourceType[] {{ { string.Join(", ", _targets) } }}" :
            string.Empty;
        yield return $"new SearchParamDefinition() {{ Resource = \"{ _resourceName }\", Name = \"{ _name }\", Description = { StringUtils.Quote(_description) }, Type = SearchParamType.{ _outputType }, Path = new string[] {{ { _path } }}{ target }{ xpath }{ expression }, Url = \"{ _url }\" }},";
    }
}

static void DeleteSourceFiles(string directoryPath)
{
    foreach (var filePath in Directory.GetFiles(directoryPath, "*.cs"))
    {
        Console.WriteLine("Deleting {0}", filePath);
        var retries = 5;
        while (true)
        {
            try
            {
                File.Delete(filePath);
                break;
            }
            catch
            {
                if (retries <= 0)
                {
                    throw;
                }
                retries--;
                System.Threading.Thread.Sleep(10);
            }
        }
    }
}

static string GetMyDirectory([System.Runtime.CompilerServices.CallerFilePath] string path = "") => Path.GetDirectoryName(path);

var rootDirectory = GetMyDirectory();
var loadedVersions = LoadedVersion.LoadAll(rootDirectory);
Console.WriteLine("Generating code for versions {0}", string.Join(", ", loadedVersions.Select(lv => lv.Version)));

Globals.AllVersions = loadedVersions
    .Select(lv => lv.Version)
    .ToList();

var valueSetsByUrlByVersion = ValueSet.LoadAll(loadedVersions);
valueSetsByUrlByVersion[string.Empty].Add(
    "http://hl7.org/fhir/ValueSet/versions",
    new ValueSet
    {
        EnumName = "Version",
        IsFlags = true,
        Url = "http://hl7.org/fhir/ValueSet/versions",
        Description = "Supported FHIR versions",
        Values = loadedVersions
            .Select( (loadedVersion, index) => new ValueSetValue { Code = loadedVersion.Version, Value = (1 << index) })
            .Concat(new[]{
                new ValueSetValue { Code = "All", Value = (1 << loadedVersions.Count) - 1 }, 
                new ValueSetValue { Code = "None", Value = 0 }
            })
            .ToList()
    }
);
var resourcesByNameByVersionAndInterfaces = ResourceDetails.LoadAll(loadedVersions, valueSetsByUrlByVersion);
var resourcesByNameByVersion = resourcesByNameByVersionAndInterfaces.Item1;
var interfaces = resourcesByNameByVersionAndInterfaces.Item2;

var generatedDirectory = Path.Combine(rootDirectory, "Generated");
DeleteSourceFiles(generatedDirectory);
foreach (var loadedVersion in loadedVersions)
{
    var directoryPath = Path.Combine(generatedDirectory, loadedVersion.Version);
    Directory.CreateDirectory(directoryPath);
    DeleteSourceFiles(directoryPath);
}
foreach (var pair in valueSetsByUrlByVersion)
{
    var version = loadedVersions.FirstOrDefault(lv => lv.Version == pair.Key);
    var versions = version == null ?
        loadedVersions : new List<LoadedVersion> { version };
    var filePath = Path.Combine(generatedDirectory, pair.Key, "_Enumerations.cs");
    Console.WriteLine("Creating {0}", filePath);
    ValueSet.Write(filePath, pair.Value.Values, versions);
}

var allVersionsModelInfo = new AllVersionsModelInfo(resourcesByNameByVersion, loadedVersions);
var allVersionsModelInfoFilePath = Path.Combine(generatedDirectory, "AllVersionsModelInfo.cs");
Console.WriteLine("Creating {0}", allVersionsModelInfoFilePath);
allVersionsModelInfo.Write(allVersionsModelInfoFilePath);

Globals.FhirDataTypeByCsType = allVersionsModelInfo.GetFhirDataTypeByCsType();

var sharedResourcesByName = resourcesByNameByVersion[string.Empty];
var toSkip = new[] { "Element", "Extension", "Narrative", "Resource", "XHtml" };
foreach (var pair in resourcesByNameByVersion)
{
    if (!string.IsNullOrEmpty(pair.Key))
    {
        var modelInfo = new ModelInfo(pair.Value.Values, sharedResourcesByName.Values);
        var filePath = Path.Combine(generatedDirectory, pair.Key, "ModelInfo.cs");
        Console.WriteLine("Creating {0}", filePath);
        modelInfo.Write(filePath);
    }
    foreach (var resource in pair.Value.Values)
    {
        if (!toSkip.Contains(resource.Name))
        {
            var filePath = Path.Combine(generatedDirectory, pair.Key, resource.Name + ".cs");
            Console.WriteLine("Creating {0}", filePath);
            resource.Write(filePath);
        }
    }
}
foreach (var inter in interfaces)
{
    var filePath = Path.Combine(generatedDirectory, inter.Name + ".cs");
    Console.WriteLine("Creating {0}", filePath);
    inter.Write(filePath);
}
