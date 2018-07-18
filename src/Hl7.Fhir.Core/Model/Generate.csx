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
using System.Xml;

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
        if (code.StartsWith("_"))
            code = code.Substring(1);
        if (code == "=")
            return "Equal";
        if (code == "<")
            return "LessThan";
        if (code == "<=")
            return "LessOrEqual";
        if (code == ">=")
            return "GreaterOrEqual";
        if (code == ">")
            return "GreaterThan";
        var bits = code.Split(new char[] { ' ', '-' });
        var result = string.Empty;
        foreach (var bit in bits)
        {
            result += bit.Substring(0, 1).ToUpper();
            result += bit.Substring(1);
        }
        if (int.TryParse(result, out var integerValue))
            result = "N" + result;
        return result;
    }

    /// <summary>
    /// Remove the namespace part of fully-qualified code types - i.e. from Hl7.Fhir.Mode.Code&lt;XXX&gt; to Code&lt;XXX&gt; 
    /// All other types are unchanged
    /// </summary>
    public static string RemoveCodeNamespace(string type)
    {
        return type.Replace("Hl7.Fhir.Model.Code<", "Code<");
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
        const string prefix = "Hl7.Fhir.Model.";
        if (type.StartsWith(prefix) && resourcesByName.ContainsKey(type.Substring(prefix.Length)))
        {
            return prefix + version + "." + type.Substring(prefix.Length);
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
    /// <returns>Lines of C# code</returns>
    public static IEnumerable<string> RenderProperties(int nPropNum, IEnumerable<PropertyDetails> properties)
    {
        foreach (var pd in properties)
        {
            nPropNum += 10;
            yield return string.Empty;
            foreach (var line in pd.Render(nPropNum)) yield return line;
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

    private static IEnumerable<string> RenderDeepCopy(string type)
    {
        yield return $"public override IDeepCopyable DeepCopy()";
        yield return $"{{";
        yield return $"     return CopyTo(new { type }());";
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
    /// <returns>Newly created value sets by FHIR version and by name, i.e. a dictionary indexed by FHIR version, 
    /// with an empty string key representing value sets common to all version and 'DSTU2', 'STU3' etc representing the specific version; 
    /// each value is a dictionary indexed by the enumeration name with the value sets as values</returns>
    public static Dictionary<string, Dictionary<string, ValueSet>> LoadAll(IEnumerable<LoadedVersion> loadedVersions)
    {
        var valueSetsByEnumNameByVersion = new Dictionary<string, Dictionary<string, ValueSet>>();
        foreach (var loadedVersion in loadedVersions)
        {
            var valueSetsByEnumName = new Dictionary<string, ValueSet>();
            valueSetsByEnumNameByVersion.Add(loadedVersion.Version, valueSetsByEnumName);
            var nodesResources = loadedVersion.Resources.DocumentElement.SelectNodes(
                "/fhir:Bundle/fhir:entry/fhir:resource/fhir:StructureDefinition[fhir:differential/fhir:element[fhir:type/fhir:code/@value = 'code' and fhir:binding/fhir:strength/@value='required']]", loadedVersion.NSR);
            var nodesTypesRoot = loadedVersion.Types.DocumentElement.SelectNodes(
                "/fhir:Bundle/fhir:entry/fhir:resource/fhir:StructureDefinition[fhir:differential/fhir:element[fhir:type/fhir:code/@value = 'code' and fhir:binding/fhir:strength/@value='required']]", loadedVersion.NST);
            var allElements = nodesResources.OfType<XmlElement>().Concat(nodesTypesRoot.OfType<XmlElement>());
            foreach (var element in allElements)
            {
                foreach (var eProp in element.SelectNodes("fhir:differential/fhir:element[fhir:type/fhir:code/@value = 'code' and fhir:binding]", loadedVersion.NSR).OfType<XmlElement>())
                {
                    string valuesetUrl = null;
                    var n3 = eProp.SelectSingleNode("fhir:binding/fhir:valueSetUri/@value", loadedVersion.NSR);
                    if (n3 != null)
                    {
                        valuesetUrl = n3.Value;
                    }
                    var n4 = eProp.SelectSingleNode("fhir:binding/fhir:valueSetReference/fhir:reference/@value", loadedVersion.NSR);
                    if (n4 != null)
                    {
                        valuesetUrl = n4.Value;
                    }
                    var valuesetElement = loadedVersion.Expansions.SelectSingleNode("/fhir:Bundle/fhir:entry/fhir:resource/fhir:ValueSet[fhir:url/@value = '" + valuesetUrl + "']", loadedVersion.NSE) as XmlElement;
                    if (valuesetElement != null)
                    {
                        var enumName = valuesetElement.SelectSingleNode("fhir:name/@value", loadedVersion.NSE).InnerText;
                        // reformat the name so that it is a valid .NET enumeration name
                        enumName = enumName.Replace(" ", "").Replace("-", "_");
                        if (!valueSetsByEnumName.ContainsKey(enumName))
                        {
                            var valueSet = new ValueSet
                            {
                                EnumName = enumName,
                                Url = valuesetUrl,
                                Description = valuesetElement.SelectSingleNode("fhir:description/@value", loadedVersion.NSE).InnerText,
                                Values = new List<ValueSetValue>()
                            };
                            valueSetsByEnumName.Add(enumName, valueSet);
                            var codedValues = new HashSet<string>();
                            foreach (var eval in valuesetElement.SelectNodes("fhir:expansion/fhir:contains", loadedVersion.NSE).OfType<XmlElement>())
                            {
                                var code = eval.SelectSingleNode("fhir:code/@value", loadedVersion.NSE).Value;
                                var enumValue = StringUtils.ConvertEnumValue(code);
                                if (!codedValues.Contains(enumValue))
                                {
                                    codedValues.Add(enumValue);
                                    var valueSetValue = new ValueSetValue { Code = code };
                                    var system = eval.SelectSingleNode("fhir:system/@value", loadedVersion.NSE).Value;
                                    valueSetValue.System = system;
                                    valueSetValue.Display = eval.SelectSingleNode("fhir:display/@value", loadedVersion.NSE).Value;
                                    string definition = null;
                                    var definitionNode = valuesetElement.SelectSingleNode("fhir:codeSystem[fhir:system/@value = '" + system + "']/fhir:concept[fhir:code/@value = '" + code + "']/fhir:definition/@value", loadedVersion.NSE);
                                    if (definitionNode != null)
                                    {
                                        definition = definitionNode.Value;
                                    }
                                    else
                                    {
                                        definitionNode = valuesetElement.SelectSingleNode("fhir:codeSystem[fhir:system/@value = '" + system + "']/fhir:concept/fhir:concept[fhir:code/@value = '" + code + "']/fhir:definition/@value", loadedVersion.NSE);
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
                                    valueSet.Values.Add(valueSetValue);
                                }
                            }
                        }
                    }
                }
            }
        }
        ExtractShared(valueSetsByEnumNameByVersion);
        return valueSetsByEnumNameByVersion;
    }

    private static void ExtractShared(Dictionary<string, Dictionary<string, ValueSet>> valueSetsByEnumNameByVersion)
    {
        var sharedValueSetsByEnumName = new Dictionary<string, ValueSet>();
        var allEnumNames = valueSetsByEnumNameByVersion.Values
            .SelectMany(valueSetsByEnumName => valueSetsByEnumName.Keys)
            .Distinct()
            .ToList();
        foreach (var enumName in allEnumNames)
        {
            var valueSetsWithSameEnumName = valueSetsByEnumNameByVersion.Values
                .Where(valueSetsByEnumName => valueSetsByEnumName.ContainsKey(enumName))
                .Select(valueSetsByEnumName => valueSetsByEnumName[enumName])
                .ToList();
            ValueSet sharedValueSet = null;
            if (enumName == "ResourceType")
            {
                sharedValueSet = new ValueSet
                {
                    EnumName = "ResourceType",
                    Url = valueSetsWithSameEnumName[0].Url,
                    Description = valueSetsWithSameEnumName[0].Description,
                    Values = valueSetsWithSameEnumName
                        .SelectMany(valueSet => valueSet.Values)
                        .Distinct(new ValueSetValueComparer())
                        .ToList()
                };
            }
            else if (valueSetsWithSameEnumName.Count > 1 && valueSetsWithSameEnumName.Skip(1).All(vs => valueSetsWithSameEnumName[0].IsSame(vs)))
            {
                sharedValueSet = valueSetsWithSameEnumName[0];
            }
            if (sharedValueSet != null)
            {
                sharedValueSetsByEnumName.Add(enumName, sharedValueSet);
                foreach (var valueSetsByEnumName in valueSetsByEnumNameByVersion.Values)
                {
                    valueSetsByEnumName.Remove(enumName);
                }
            }
        }
        valueSetsByEnumNameByVersion.Add(string.Empty, sharedValueSetsByEnumName);
    }

    private class ValueSetValueComparer : IEqualityComparer<ValueSetValue>
    {
        public bool Equals(ValueSetValue x, ValueSetValue y)
        {
            return x.Code == x.Code;
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
        yield return $"{ enumValue },";
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
    /// if later on the resource is determined to be common to multiple version it will contain all the versions it appears in (see the MergeSame() method)
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
    /// Validation contraints of the FHIR resourc or data type - e.g. the 'contact.all(name or telecom or address or organization)' validation expression for Patient
    /// </summary>
    public List<ConstraintDetails> Constraints;

    /// <summary>
    /// Human-readable definition of sub-types - e.g. 'There SHALL be a code if there is a value and it SHALL be an expression of time...' for Age (detived from Quantity)
    /// </summary>
    public string Definition;

    /// <summary>
    /// True if this is a resource, false if it is a data type
    /// </summary>
    public bool IsResource()
    {
        return BaseType.EndsWith(".DomainResource") || BaseType.EndsWith(".Resource") || Name == "DomainResource" || Name == "Resource";
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
            string.IsNullOrEmpty(Definition) ? string.Empty : " definition: " + Definition
        );
        foreach (var prop in Properties)
        {
            var allowedTypes = prop.AllowedTypesByVersion.Count == 0 ?
                string.Empty :
                prop.AllowedTypesByVersion.ContainsKey(string.Empty) ?
                    string.Join(",", prop.AllowedTypesByVersion[string.Empty]) :
                    string.Join(", ", prop.AllowedTypesByVersion.Select(pair => $"{pair.Key}({ string.Join(",", pair.Value) })"));
            writer.WriteLine(
                "    {0}: {1}{2} {3} {4}{5}{6}",
                prop.Name,
                prop.PropType,
                prop.InSummaryVersions.Count > 0 ? " summary(" + string.Join(",", prop.InSummaryVersions) + ")" : string.Empty,
                prop.CardMin,
                prop.CardMax,
                prop.ReferenceTargets.Count == 0 ? string.Empty : " targets: " + string.Join(",", prop.ReferenceTargets),
                string.IsNullOrEmpty(allowedTypes) ? string.Empty : " allowed types: " + allowedTypes
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
                writer.WriteLine("        {0} - {1}: {2}, XPath: {3}, Expression: {4}", constraint.Key, constraint.Severity, constraint.Human, constraint.XPath, constraint.Expression);
            }
        }
    }

    /// <summary>
    /// Checks if this resource is the same as another one - where 'the same' = representable by the same C# class
    /// </summary>
    public bool IsSame(ResourceDetails other)
    {
        return other != null &&
            Name == other.Name &&
            AbstractType == other.AbstractType &&
            BaseType == other.BaseType &&
            IsPrimitive == other.IsPrimitive &&
            PrimitiveTypeName == other.PrimitiveTypeName &&
            Pattern == other.Pattern &&
            Properties.Count == other.Properties.Count &&
            Properties.OrderBy(p => p.Name).Zip(other.Properties.OrderBy(p => p.Name), (p1, p2) => p1.IsSame(p2)).All(same => same) &&
            Components.Count == other.Components.Count &&
            Components.OrderBy(c => c.Name).Zip(other.Components.OrderBy(c => c.Name), (c1, c2) => c1.IsSame(c2)).All(same => same);
    }

    /// <summary>
    /// Merge resource and data type descriptions that are the same
    /// </summary>
    /// <param name="resources">Resources / data types to merge</param>
    /// <param name="resourcesByNameByVersion">All FHIR resources and data types by FHIR version and name - before any merging of resource that are the same across versions</param>
    /// <returns>Newly created merged resource description</returns>
    public static ResourceDetails MergeSame(
        IEnumerable<ResourceDetails> resources,
        Dictionary<string, Dictionary<string, ResourceDetails>> resourcesByNameByVersion
    )
    {
        var firstResource = resources.First();
        return new ResourceDetails
        {
            Versions = resources
                .Select( resource => resource.Versions.Single() )
                .ToList(),
            Name = firstResource.Name,
            Description = firstResource.Description,
            FhirName = firstResource.FhirName,
            AbstractType = firstResource.AbstractType,
            BaseType = firstResource.BaseType,
            IsPrimitive = firstResource.IsPrimitive,
            PrimitiveTypeName = firstResource.PrimitiveTypeName,
            Pattern = firstResource.Pattern,
            Properties = firstResource.Properties
                .Select( 
                    prop => PropertyDetails.MergeSame( 
                        resources.Select( resource => new KeyValuePair<string, PropertyDetails>(resource.Versions.Single().Version, resource.Properties.Single(p => p.Name == prop.Name))),
                        resourcesByNameByVersion
                    )
                )
                .ToList(),
            Components = firstResource.Components
                .Select(
                    comp => ComponentDetails.MergeSame(
                        resources.Select(resource => new KeyValuePair<string, ComponentDetails>(resource.Versions.Single().Version, resource.Components.Single(c => c.Name == comp.Name))),
                        resourcesByNameByVersion
                    )
                )
                .ToList(),
            Constraints = firstResource.Constraints
                .Select(
                    constraint => ConstraintDetails.MergeSame(
                        resources.Select(resource => new KeyValuePair<string, ConstraintDetails>(resource.Versions.Single().Version, resource.Constraints.Single(c => c.Key == constraint.Key)))
                    )
                )
                .ToList(),
            Definition = firstResource.Definition
        };
    }

    /// <summary>
    /// Create a C# file containing the class corresponding to this FHIR resource or data type
    /// </summary>
    /// <param name="filePath">Path of the target file, that is overwritten if it alreadt exist</param>
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
        if (BaseType.EndsWith(".Quantity"))
        {
            return RenderQuantity();
        }
        return RenderAny();
    }

    private IEnumerable<string> RenderAny()
    {
        var version = Versions.Count == 1 ?
            Versions[0].Version :
            "All";
        version = "Hl7.Fhir.Model.Version." + version;
        var isElement = BaseType == "Hl7.Fhir.Model.Element" || IsPrimitive;
        foreach (var line in StringUtils.RenderSummary(Description)) yield return line;
        if (!AbstractType)
        {
            var isResource = !isElement ?
                ", IsResource=true" :
                string.Empty;
            yield return $"[FhirType({version}, \"{ FhirName }\"{ isResource })]";
        }
        yield return $"[DataContract]";

        var isAbstract = AbstractType ?
            " abstract" :
            string.Empty;
        var baseType = !string.IsNullOrEmpty(BaseType) ?
            $"{ BaseType }, " :
            string.Empty;
        yield return $"public{ isAbstract } partial class { Name } : { baseType }System.ComponentModel.INotifyPropertyChanged";

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
            yield return $"    [FhirElement(\"value\", IsPrimitiveValue=true, XmlSerialization=XmlSerializationHint.Attribute, InSummary=new[]{{Hl7.Fhir.Model.Version.All}}, Order=30)]";
            yield return $"    [CLSCompliant(false)]";
            if (!string.IsNullOrEmpty(Pattern) && Name != "FhirDecimal" && Name != "Time" && Name != "Integer" &&
                Name != "UnsignedInt" && Name != "PositiveInt" && Name != "Instant"
                || Name == "FhirUri")
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
            foreach (var line in component.Render(version)) yield return "    " + line;
        }

        if (!IsPrimitive)
        {
            var nPropNum = BaseType == "Hl7.Fhir.Model.Resource" ?
                40 :
                BaseType == "Hl7.Fhir.Model.Element" ? 20 : 80;

            yield return string.Empty;
            foreach (var line in StringUtils.RenderProperties(nPropNum, Properties)) yield return "    " + line;
        }

        yield return string.Empty;
        if (Constraints.Count > 0)
        {
            foreach (var constraint in Constraints)
            {
                yield return string.Empty;
                foreach (var line in constraint.Render(Name)) yield return "    " + line;
            }
            yield return string.Empty;
            yield return $"    public override void AddDefaultConstraints()";
            yield return $"    {{";
            yield return $"        base.AddDefaultConstraints();";
            yield return string.Empty;
            foreach (var constraint in Constraints)
            {
                yield return $"        InvariantConstraints.Add({ constraint.GetName(Name) });";
            }
            yield return $"    }}";
        }

        if (!IsPrimitive)
        {
            yield return string.Empty;
            foreach (var line in StringUtils.RenderCopyAndComparisonMethods(Name, AbstractType, Properties)) yield return "    " + line;

            yield return string.Empty;
            foreach (var line in StringUtils.RenderChildrenMethods(Properties)) yield return "    " + line;
        }

        yield return string.Empty;
        yield return $"}}";
    }

    private IEnumerable<string> RenderQuantity()
    {
        var version = Versions.Count == 1 ?
            Versions[0].Version :
            "All";
        version = "Hl7.Fhir.Model.Version." + version;
        var fhirTypeName = Name == "SimpleQuantity" ?
            "Quantity" :
            Name;
        foreach (var line in StringUtils.RenderSummary(Description)) yield return line;
        yield return $"[FhirType({version}, \"{fhirTypeName}\")]";
        yield return $"public partial class { Name } : Quantity";
        yield return $"{{";
        yield return $"    [NotMapped]";
        yield return $"    public override string TypeName {{ get {{ return \"{ FhirName }\"; }} }}";
        yield return string.Empty;
        yield return $"    public override IDeepCopyable DeepCopy()";
        yield return $"    {{";
        yield return $"        return CopyTo(new { Name }());";
        yield return $"    }}";
        yield return string.Empty;
        yield return $"    // TODO: Add code to enforce these constraints:";
        if (!string.IsNullOrEmpty(Definition))
        {
            yield return $"    // * { Definition }";
        }
        yield return $"}}";
    }

    private IEnumerable<string> GetReferencedFhirTypes()
    {
        const string prefix = "Hl7.Fhir.Model.";
        return GetAllProperties()
            .Select(p => p.PropType)
            .Concat(new[] {  BaseType })
            .Where(pt => pt.StartsWith(prefix))
            .Select(pt => pt.Substring(prefix.Length));
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
    /// <returns>Newly created resources and data types by FHIR version and by name, i.e. a dictionary indexed by FHIR version, 
    /// with an empty string key representing resources and data types common to all version and 'DSTU2', 'STU3' etc representing the specific version; 
    /// each value is a dictionary indexed by the class name with the ResourceDetails as values</returns>
    public static Dictionary<string, Dictionary<string, ResourceDetails>> LoadAll(
        IEnumerable<LoadedVersion> loadedVersions,
        Dictionary<string, Dictionary<string, ValueSet>> valueSetsByEnumNameByVersion
    )
    {
        var enumTypesByValueSetUrlByVersion = loadedVersions.ToDictionary(
                loadedVersion => loadedVersion.Version,
                loadedVersion => valueSetsByEnumNameByVersion[string.Empty]
                    .Select(pair => new KeyValuePair<string, string>(pair.Value.Url, "Hl7.Fhir.Model." + pair.Key))
                    .Concat(valueSetsByEnumNameByVersion[loadedVersion.Version].Select(pair => new KeyValuePair<string, string>(pair.Value.Url, "Hl7.Fhir.Model." + loadedVersion.Version + "." + pair.Key)))
                    .Where(pair => !string.IsNullOrEmpty(pair.Key))
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
        ExtractShared(resourcesByNameByVersion);
        return resourcesByNameByVersion;
    }

    private static void Patch(Dictionary<string, Dictionary<string, ResourceDetails>> resourcesByNameByVersion)
    {
        // Make the STU3 Code patterns the same as DSTU2 (the STU3 one seems just plain wrong)
        var stu3Code = resourcesByNameByVersion["STU3"]["Code"];
        stu3Code.Pattern = @"[^\s]+([\s][^\s]+)*";

        // Make the DSTU2 Element.Id a string as in STU3 - less restrictive
        var dstu2Element = resourcesByNameByVersion["DSTU2"]["Element"];
        var dstu2ElementIdElement = dstu2Element.Properties.Single(p => p.Name == "IdElement");
        dstu2ElementIdElement.PropType = "Hl7.Fhir.Model.FhirString";
        dstu2ElementIdElement.NativeType = "string";

        // Make DSTU2 Parameters constraints the same as in STU3 - the DSTU2 ones are wrong
        var dstu2Parameters = resourcesByNameByVersion["DSTU2"]["Parameters"];
        dstu2Parameters.Constraints = new List<ConstraintDetails>
        {
            new ConstraintDetails
            {
                Expression = "parameter.all((part.exists() and value.empty() and resource.empty()) or (part.empty() and (value.exists() xor resource.exists())))",
                Key = "inv-1",
                Severity = "Warning",
                Human = "A parameter must have only one of (value, resource, part)",
                XPath = "exists(f:value) or exists(f:resource) and not(exists(f:value) and exists(f:resource))"
            }
        };
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

            resource.Constraints = new List<ConstraintDetails>();
            foreach (var node in e.SelectNodes("fhir:differential/fhir:element/fhir:constraint", loadedVersion.NSR).OfType<XmlElement>())
            {
                var expression = node.SelectSingleNode("fhir:extension[@url='http://hl7.org/fhir/StructureDefinition/structuredefinition-expression']/fhir:valueString/@value|fhir:expression/@value", loadedVersion.NSR)?.Value;
                if (expression != null)
                {
                    var parentPath = node.ParentNode.SelectSingleNode("fhir:path/@value", loadedVersion.NSR).Value;
                    if (parentPath.Contains("."))
                    {
                        // This expression applied to a backbone element, so need to give it scope
                        expression = parentPath.Replace("[x]", "").Replace(resourceName + ".", "") + ".all(" + expression + ")";
                    }
                }
                var constraint = new ConstraintDetails
                {
                    Key = node.SelectSingleNode("fhir:key/@value", loadedVersion.NSR).Value,
                    Severity = node.SelectSingleNode("fhir:severity/@value", loadedVersion.NSR).Value,
                    Human = node.SelectSingleNode("fhir:human/@value", loadedVersion.NSR).Value,
                    XPath = node.SelectSingleNode("fhir:xpath/@value", loadedVersion.NSR).Value,
                    Expression = expression
                };
                resource.Constraints.Add(constraint);
            }
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
            if (resourceName == "string")
            {
                resourceName = "FhirString";
                primitiveTypeName = "string";
            }
            else if (resourceName == "decimal")
            {
                resourceName = "FhirDecimal";
                primitiveTypeName = "decimal?";
            }
            else if (resourceName == "uri")
            {
                resourceName = "FhirUri";
                primitiveTypeName = "string";
            }
            else if (resourceName == "narrative")
            {
                resourceName = "Narrative";
                primitiveTypeName = "string";
            }
            else if (resourceName == "boolean")
            {
                resourceName = "FhirBoolean";
                primitiveTypeName = "bool?";
            }
            else if (resourceName == "dateTime")
            {
                resourceName = "FhirDateTime";
                primitiveTypeName = "string";
            }
            else if (resourceName == "date")
            {
                resourceName = "Date";
                primitiveTypeName = "string";
            }
            else if (resourceName == "time")
            {
                resourceName = "Time";
                primitiveTypeName = "string";
            }
            else if (resourceName == "instant")
            {
                resourceName = "Instant";
                primitiveTypeName = "DateTimeOffset?";
            }
            else if (resourceName == "integer")
            {
                resourceName = "Integer";
                primitiveTypeName = "int?";
            }
            else if (resourceName == "positiveInt")
            {
                resourceName = "PositiveInt";
                primitiveTypeName = "int?";
            }
            else if (resourceName == "unsignedInt")
            {
                resourceName = "UnsignedInt";
                primitiveTypeName = "int?";
            }
            else if (resourceName == "code")
            {
                resourceName = "Code";
                primitiveTypeName = "string";
            }
            else if (resourceName == "base64Binary")
            {
                resourceName = "Base64Binary";
                primitiveTypeName = "byte[]";
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

            var patternNode = e.SelectSingleNode("fhir:differential/fhir:element/fhir:type/fhir:extension[@url='http://hl7.org/fhir/StructureDefinition/structuredefinition-regex']/fhir:valueString/@value", loadedVersion.NST);
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

            resource.Constraints = new List<ConstraintDetails>();

            var definitionNode = e.SelectSingleNode("fhir:differential/fhir:element/fhir:definition/@value", loadedVersion.NST);
            if (definitionNode != null)
            {
                resource.Definition = definitionNode.Value;
            }
        }
        return result;
    }

    private static XmlNode GetBaseTypeNode(XmlElement structureDefinitionElement, XmlNamespaceManager ns)
    {
        var result = structureDefinitionElement.SelectSingleNode("fhir:base/@value", ns); // DSTU2
        if (result == null)
        {
            result = structureDefinitionElement.SelectSingleNode("fhir:baseDefinition/@value", ns); // STU3
        }
        return result;
    }

    private static List<PropertyDetails> GetProperties(string className, string resourceName, XmlElement e, XmlNamespaceManager ns, Dictionary<string, string> enumTypesByValueSetUrl)
    {
        var result = new List<PropertyDetails>();
        foreach (var snapshotElement in e.SelectNodes("fhir:differential/fhir:element", ns).OfType<XmlElement>())
        {
            var pd = PropertyDetails.Parse(className, resourceName, snapshotElement, ns, enumTypesByValueSetUrl);
            if (pd != null)
            {
                result.Add(pd);
            }
        }
        return result;
    }

    private static void ExtractShared(Dictionary<string, Dictionary<string, ResourceDetails>> resourcesByNameByVersion)
    {
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
            var firstResourceWithSameName = resourcesWithSameName[0];
            var firstVersion = firstResourceWithSameName.Versions.Single().Version;
            // Extension and Element defintions are circular, and they shlould not be version-specific, so we exclude them from the referenced types testing
            if (resourcesWithSameName.Count > 1 && 
                resourcesWithSameName.Skip(1).All(resource => firstResourceWithSameName.IsSame(resource)) && 
                firstResourceWithSameName.GetReferencedFhirTypes().All(type => type == "Extension" || type == "Element" || !resourcesByNameByVersion[firstVersion].ContainsKey(type)))
            {
                sharedResourcesByName.Add(name, ResourceDetails.MergeSame(resourcesWithSameName, resourcesByNameByVersion));
                foreach (var resourcesByName in resourcesByNameByVersion.Values)
                {
                    resourcesByName.Remove(name);
                }
            }
        }
        FixFhirTypes(resourcesByNameByVersion, sharedResourcesByName);
        resourcesByNameByVersion.Add(string.Empty, sharedResourcesByName);
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
    /// Component properties - corresponding to the C# sub-class properties
    /// </summary>
    public List<PropertyDetails> Properties;

    /// <summary>
    /// Checks if this component is the same - i.e. correspond to an identical C# sub-class - as another one
    /// </summary>
    public bool IsSame(ComponentDetails other)
    {
        return other != null &&
            Name == other.Name &&
            BaseType == other.BaseType &&
            Properties.Count == other.Properties.Count &&
            Properties.OrderBy(p => p.Name).Zip(other.Properties.OrderBy(p => p.Name), (p1, p2) => p1.IsSame(p2)).All(same => same);
    }

    /// <summary>
    /// Merge components that are the same
    /// </summary>
    /// <param name="versionAndComponents">Version and component pairs to merge</param>
    /// <param name="resourcesByNameByVersion">All FHIR resources and data types by FHIR version and name - before any merging of resource that are the same across versions</param>
    /// <returns>Newly created merged component description</returns>
    public static ComponentDetails MergeSame(
        IEnumerable<KeyValuePair<string, ComponentDetails>> versionAndComponents,
        Dictionary<string, Dictionary<string, ResourceDetails>> resourcesByNameByVersion
    )
    {
        var firstComponent = versionAndComponents.First().Value;
        return new ComponentDetails
        {
            Name = firstComponent.Name,
            BaseType = firstComponent.BaseType,
            Properties = firstComponent.Properties
                .Select(
                    prop => PropertyDetails.MergeSame(
                        versionAndComponents.Select(pair => new KeyValuePair<string, PropertyDetails>(pair.Key, pair.Value.Properties.Single(p => p.Name == prop.Name))),
                        resourcesByNameByVersion
                    )
                )
                .ToList(),
        };
    }

    /// <summary>
    /// Renders the C# code for this component (sub-)class
    /// </summary>
    /// <param name="version">Target FHIR version: 'DSTU2' 'STU3' etc. or 'All' if common</param>
    /// <returns>C# code lines</returns>
    public IEnumerable<string> Render(string version)
    {
        yield return $"[FhirType({version}, \"{ Name }\")]";
        yield return $"[DataContract]";
        yield return $"public partial class { Name } : { BaseType }, System.ComponentModel.INotifyPropertyChanged";
        yield return $"{{";
        yield return $"    [NotMapped]";
        yield return $"    public override string TypeName {{ get {{ return \"{ Name }\"; }} }}";

        foreach (var line in StringUtils.RenderProperties(30, Properties)) yield return "    " + line;

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

    /// <summary>
    /// Checks if this constraint is the same as another one
    /// </summary>
    public bool IsSame(ConstraintDetails other)
    {
        return other != null &&
            Key == other.Key &&
            Severity == other.Severity &&
            Human == other.Human &&
            XPath == other.XPath &&
            Expression == other.Expression;
    }

    /// <summary>
    /// Merge constraints that are the same
    /// </summary>
    /// <param name="versionAndConstraints">FHIR version - constraint pairs to merge</param>
    /// <param name="resourcesByNameByVersion">All FHIR resources and data types by FHIR version and name - before any merging of resource that are the same across versions</param>
    /// <returns>Newly created merged resource description</returns>
    public static ConstraintDetails MergeSame(IEnumerable<KeyValuePair<string, ConstraintDetails>> versionAndConstraints)
    {
        var firstConstraint = versionAndConstraints.First().Value;
        return new ConstraintDetails
        {
            Key = firstConstraint.Key,
            Severity = firstConstraint.Severity,
            Human = firstConstraint.Human,
            XPath = firstConstraint.XPath,
            Expression = firstConstraint.Expression
        };
    }

    /// <summary>
    /// Renders the C# code of the constraint
    /// </summary>
    /// <param name="type">Type (class) containing the constrant</param>
    /// <returns>C# code lines</returns>
    public IEnumerable<string> Render(string type)
    {
        yield return $"public static ElementDefinitionConstraint { GetName(type) } = new ElementDefinitionConstraint";
        yield return $"{{";
        if (!string.IsNullOrEmpty(Expression))
        {
            yield return $"    Expression = { StringUtils.Quote(Expression) },";
        }
        yield return $"    Key = { StringUtils.Quote(Key) },";
        var severity = Severity == "Error" ?
            "ConstraintSeverity.Error" :
            "ConstraintSeverity.Warning";
        yield return $"    Severity = { severity },";
        if (!string.IsNullOrEmpty(Human))
        {
            yield return $"    Human = { StringUtils.Quote(Human) },";
        }
        if (!string.IsNullOrEmpty(XPath))
        {
            yield return $"    Xpath = { StringUtils.Quote(XPath) }";
        }
        yield return $"}};";
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

    public bool IsSame(PropertyDetails other)
    {
        return other != null &&
            PropType == other.PropType &&
            Name == other.Name &&
            CardMin == other.CardMin &&
            CardMax == other.CardMax &&
            ReferenceTargets.Count == other.ReferenceTargets.Count &&
            ReferenceTargets.OrderBy(t => t).Zip(other.ReferenceTargets.OrderBy(t => t), (t1, t2) => t1 == t2).All(same => same);
    }

    /// <summary>
    /// Merge properties that are the same
    /// </summary>
    /// <param name="versionAndProperties">FHIR version - property pairs to merge</param>
    /// <param name="resourcesByNameByVersion">All FHIR resources and data types by FHIR version and name - before any merging of resource that are the same across versions</param>
    /// <returns>Newly created merged resource description</returns>
    public static PropertyDetails MergeSame(
        IEnumerable<KeyValuePair<string, PropertyDetails>> versionAndProperties,
        Dictionary<string, Dictionary<string, ResourceDetails>> resourcesByNameByVersion
    )
    {
        const string prefix = "Hl7.Fhir.Model.";

        var firstProperty = versionAndProperties.First().Value;
        var isSummaryVersions = versionAndProperties
            .Where(pair => pair.Value.InSummaryVersions.Count > 0)
            .Select(pair => pair.Key)
            .ToList();
        var allowedTypesAreTheSame = versionAndProperties
            .Skip(1)
            .All(vp => EqualsAnyOrder(firstProperty.AllowedTypes(), vp.Value.AllowedTypes()));
        var allowedTypesAreTheSameAndNotVersionSpecific = allowedTypesAreTheSame &&
            versionAndProperties.All(vp => !vp.Value.AllowedTypes().Any(t => t.StartsWith(prefix) && resourcesByNameByVersion[vp.Key].ContainsKey(t.Substring(prefix.Length))));
        return new PropertyDetails
        {
            Name = firstProperty.Name,
            FhirName = firstProperty.FhirName,
            PropType = firstProperty.PropType,
            InSummaryVersions = isSummaryVersions.Count == 0 ?
                new HashSet<string>() :
                isSummaryVersions.Count == versionAndProperties.Count() ?
                    new HashSet<string>(new[] { string.Empty }) : // All versions
                    new HashSet<string>(isSummaryVersions),
            Summary = firstProperty.Summary,
            CardMin = firstProperty.CardMin,
            CardMax = firstProperty.CardMax,
            ReferenceTargets = new List<string>(firstProperty.ReferenceTargets),
            NativeType = firstProperty.NativeType,
            NativeName = firstProperty.NativeName,
            AllowedTypesByVersion = allowedTypesAreTheSameAndNotVersionSpecific ?
                new Dictionary<string, List<string>> { { string.Empty, new List<string>(firstProperty.AllowedTypes()) } } : // All versions
                versionAndProperties.ToDictionary(vp => vp.Key, vp => vp.Value.AllowedTypes()),
            IsXmlAttribute = firstProperty.IsXmlAttribute
        };
    }

    private List<string> AllowedTypes()
    {
        return AllowedTypesByVersion.Count == 0 ?
            new List<string>() :
            AllowedTypesByVersion.Values.Single();
    }

    private static bool EqualsAnyOrder(List<string> first, List<string> second)
    {
        return Enumerable.SequenceEqual(first.OrderBy(s => s), second.OrderBy(s => s));
    }

    public IEnumerable<string> Render(int nPropNum)
    {
        foreach (var line in StringUtils.RenderSummary(Summary)) yield return line;
        var choice = PropType == "Hl7.Fhir.Model.Element" ?
            ", Choice=ChoiceType.DatatypeChoice" :
            PropType == "Hl7.Fhir.Model.Resource" ?
                ", Choice=ChoiceType.ResourceChoice" :
                string.Empty;
        yield return $"[FhirElement(\"{FhirName}\"{InSummaryAttribute()}, Order={nPropNum}{choice})]";
        if (ReferenceTargets.Count > 0 || AllowedTypesByVersion.Values.Sum(at => at.Count) > 0 || InSummaryVersions.Count > 0)
        {
            yield return "[CLSCompliant(false)]";
        }
        if (ReferenceTargets.Count > 0)
        {
            yield return $"[References({ string.Join(",",ReferenceTargets.Select(rt => "\"" + rt + "\"")) })]";
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

    private string InSummaryAttribute()
    {
        if (InSummaryVersions.Count == 0)
        {
            return string.Empty;
        }
        var versionsString = string.Join(",", InSummaryVersions.Select( v => string.IsNullOrEmpty(v) ? "Hl7.Fhir.Model.Version.All" : "Hl7.Fhir.Model.Version." + v));
        return ", InSummary=new[]{" + versionsString + "}";
    }

    public IEnumerable<string> RenderAsChildWithName()
    {
        // Exclude special properties encoded as Xml attributes (Element.Id) - not derived from Base
        if (IsXmlAttribute) yield break;

        if (IsMultiCard())
        {
            yield return $"foreach (var elem in {Name}) {{ if (elem != null) yield return new ElementValue(\"{FhirName}\", true, elem); }}";
        }
        else
        {
            yield return $"if ({Name} != null) yield return new ElementValue(\"{FhirName}\", false, {Name});";
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
        const string prefix = "Hl7.Fhir.Model.";
        if (PropType.StartsWith(prefix))
        {
            yield return PropType.Substring(prefix.Length);
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
        switch (propType)
        {
            case "id": return "Hl7.Fhir.Model.Id";
            case "Reference": return "Hl7.Fhir.Model.ResourceReference";
            case "code": return "Hl7.Fhir.Model.Code";
            case "oid": return "Hl7.Fhir.Model.Oid";
            case "uri": return "Hl7.Fhir.Model.FhirUri";
            case "boolean": return "Hl7.Fhir.Model.FhirBoolean";
            case "dateTime": return "Hl7.Fhir.Model.FhirDateTime";
            case "date": return "Hl7.Fhir.Model.Date";
            case "time": return "Hl7.Fhir.Model.Time";
            case "base64Binary": return "Hl7.Fhir.Model.Base64Binary";
            case "decimal": return "Hl7.Fhir.Model.FhirDecimal";
            case "markdown": return "Hl7.Fhir.Model.Markdown";
            case "xhtml": return "Hl7.Fhir.Model.Xhtml";
            case "instant": return "Hl7.Fhir.Model.Instant";
            case "integer": return "Hl7.Fhir.Model.Integer";
            case "unsignedInt": return "Hl7.Fhir.Model.UnsignedInt";
            case "positiveInt": return "Hl7.Fhir.Model.PositiveInt";
            case "string": return "Hl7.Fhir.Model.FhirString";
            case "Quantity":
                return GetQuantityType(element, ns);
        }
        return "Hl7.Fhir.Model." + propType;
    }

    public static PropertyDetails Parse(string className, string resourceName, XmlElement element, XmlNamespaceManager ns, Dictionary<string, string> enumTypesByValueSetUrl)
    {
        var result = new PropertyDetails();
        if (element.SelectSingleNode("fhir:isSummary[@value = 'true']", ns) != null)
            result.InSummaryVersions.Add(string.Empty);
        if (element.SelectSingleNode("fhir:representation[@value = 'xmlAttr']", ns) != null)
            result.IsXmlAttribute = true;
        if (element.SelectSingleNode("fhir:short/@value", ns) != null)
            result.Summary = element.SelectSingleNode("fhir:short/@value", ns).Value;
        if (element.SelectSingleNode("fhir:type/fhir:code/@value", ns) != null)
            result.PropType = element.SelectSingleNode("fhir:type/fhir:code/@value", ns).Value;
        else
            result.PropType = "BackboneElement";

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

        string[] NativeTypes = { "decimal", "dateTime", "time", "integer", "oid", "date", "id", "Code", "code", "instant", "unsignedInt", "positiveInt", "string", "boolean", "uri", "base64Binary", "markdown" };
        if (NativeTypes.Contains(result.PropType))
        {
            result.NativeName = result.Name;
            result.Name = result.Name + "Element";
            result.NativeType = result.PropType;
        }

        if (parsedReferencedType == null)
        {
            switch (result.PropType)
            {
                case "id":
                    result.PropType = "Hl7.Fhir.Model.Id";
                    result.NativeType = "string";
                    break;
                case "Reference":
                    result.PropType = "Hl7.Fhir.Model.ResourceReference";
                    break;
                case "Code":
                    result.PropType = "Hl7.Fhir.Model.Code";
                    result.NativeType = "string";
                    // If the binding is required, then we can change to the 
                    // locally defined enumeration type
                    break;
                case "code":
                    result.PropType = "Hl7.Fhir.Model.Code";
                    result.NativeType = "string";
                    // If the binding is required, then we can change to the 
                    // locally defined enumeration type
                    break;
                case "oid":
                    result.PropType = "Hl7.Fhir.Model.Oid";
                    result.NativeType = "string";
                    break;
                case "markdown":
                    result.PropType = "Hl7.Fhir.Model.Markdown";
                    result.NativeType = "string";
                    break;
                case "integer":
                    result.PropType = "Hl7.Fhir.Model.Integer";
                    result.NativeType = "int?";
                    break;
                case "boolean":
                    result.PropType = "Hl7.Fhir.Model.FhirBoolean";
                    result.NativeType = "bool?";
                    break;
                case "uri":
                    result.PropType = "Hl7.Fhir.Model.FhirUri";
                    result.NativeType = "string";
                    break;
                case "base64Binary":
                    result.PropType = "Hl7.Fhir.Model.Base64Binary";
                    result.NativeType = "byte[]";
                    break;
                case "Resource":
                    result.PropType = "Hl7.Fhir.Model.Resource";
                    result.AllowedTypesByVersion = new Dictionary<string, List<string>> { { string.Empty, new List<string> { "Hl7.Fhir.Model.Resource" } } };
                    break;
                case "dateTime":
                    result.PropType = "Hl7.Fhir.Model.FhirDateTime";
                    result.NativeType = "string";
                    break;
                case "date":
                    result.PropType = "Hl7.Fhir.Model.Date";
                    result.NativeType = "string";
                    break;
                case "time":
                    result.PropType = "Hl7.Fhir.Model.Time";
                    result.NativeType = "string";
                    break;
                case "instant":
                    result.PropType = "Hl7.Fhir.Model.Instant";
                    result.NativeType = "DateTimeOffset?";
                    break;
                case "unsignedInt":
                    result.PropType = "Hl7.Fhir.Model.UnsignedInt";
                    result.NativeType = "int?";
                    break;
                case "positiveInt":
                    result.PropType = "Hl7.Fhir.Model.PositiveInt";
                    result.NativeType = "int?";
                    break;
                case "decimal":
                    result.PropType = "Hl7.Fhir.Model.FhirDecimal";
                    result.NativeType = "decimal?";
                    break;
                case "string":
                    result.PropType = "Hl7.Fhir.Model.FhirString";
                    result.NativeType = "string";
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
            var codeRequiredBinding = GetCodeRequiredBinding(element, ns, resourceName, enumTypesByValueSetUrl);
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
            string[] NativeTypes = { "decimal", "dateTime", "integer", "oid", "date", "id", "Code", "code", "instant", "unsignedInt", "positiveInt", "string", "boolean", "uri", "base64Binary", "markdown" };
            if (NativeTypes.Contains(result.PropType))
            {
                result.NativeType = result.PropType;
            }

            switch (result.PropType)
            {
                case "id":
                    result.PropType = "Hl7.Fhir.Model.Id";
                    result.NativeType = "string";
                    break;
                case "Reference":
                    result.PropType = "Hl7.Fhir.Model.ResourceReference";
                    break;
                case "Resource":
                    result.PropType = "Hl7.Fhir.Model.Resource";
                    result.AllowedTypesByVersion = new Dictionary<string, List<string>>() { { string.Empty, new List<string> { "Hl7.Fhir.Model.Resource" } } };
                    break;
                case "Code":
                    result.PropType = "Hl7.Fhir.Model.Code";
                    result.NativeType = "string";
                    // If the binding is required, then we can change to the 
                    // locally defined enumeration type
                    break;
                case "code":
                    result.PropType = "Hl7.Fhir.Model.Code";
                    result.NativeType = "string";
                    // If the binding is required, then we can change to the 
                    // locally defined enumeration type
                    break;
                case "markdown":
                    result.PropType = "Hl7.Fhir.Model.Markdown";
                    result.NativeType = "string";
                    break;
                case "oid":
                    result.PropType = "Hl7.Fhir.Model.Oid";
                    result.NativeType = "string";
                    break;
                case "integer":
                    result.PropType = "Hl7.Fhir.Model.Integer";
                    result.NativeType = "int?";
                    break;
                case "boolean":
                    result.PropType = "Hl7.Fhir.Model.FhirBoolean";
                    result.NativeType = "bool?";
                    break;
                case "uri":
                    result.PropType = "Hl7.Fhir.Model.FhirUri";
                    result.NativeType = "string";
                    break;
                case "base64Binary":
                    result.PropType = "Hl7.Fhir.Model.Base64Binary";
                    result.NativeType = "byte[]";
                    break;
                case "dateTime":
                    result.PropType = "Hl7.Fhir.Model.FhirDateTime";
                    result.NativeType = "string";
                    break;
                case "date":
                    result.PropType = "Hl7.Fhir.Model.Date";
                    result.NativeType = "string";
                    break;
                case "time":
                    result.PropType = "Hl7.Fhir.Model.Time";
                    break;
                case "instant":
                    result.PropType = "Hl7.Fhir.Model.Instant";
                    result.NativeType = "DateTimeOffset?";
                    break;
                case "unsignedInt":
                    result.PropType = "Hl7.Fhir.Model.UnsignedInt";
                    result.NativeType = "int?";
                    break;
                case "positiveInt":
                    result.PropType = "Hl7.Fhir.Model.PositiveInt";
                    result.NativeType = "int?";
                    break;
                case "decimal":
                    result.PropType = "Hl7.Fhir.Model.FhirDecimal";
                    result.NativeType = "decimal?";
                    break;
                case "string":
                    result.PropType = "Hl7.Fhir.Model.FhirString";
                    result.NativeType = "string";
                    break;
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

        if (result.PropType == "Hl7.Fhir.Model.ResourceReference")
        {
            result.ReferenceTargets = GetPossibleReferenceTargets(element, ns);
        }

        if (result.PropType == "Code" || result.PropType == "Hl7.Fhir.Model.Code")
        {
            var codeRequiredBinding = GetCodeRequiredBinding(element, ns, resourceBase, enumTypesByValueSetUrl);
            if (!string.IsNullOrEmpty(codeRequiredBinding))
            {
                result.PropType = result.PropType + "<" + codeRequiredBinding + ">";
                result.NativeType = codeRequiredBinding + "?";
            }
        }

        return result;
    }

    private static string GetCodeRequiredBinding(XmlElement element, XmlNamespaceManager ns, string resourceName, Dictionary<string, string> enumTypesByValueSetUrl)
    {
        // Grab the required binding value set reference (if any) from the element
        var codeRequiredBindingNode = element.SelectSingleNode("fhir:binding[fhir:strength/@value = 'required']/fhir:valueSetReference/fhir:reference/@value", ns);
        if (codeRequiredBindingNode == null)
        {
            return null;
        }

        var codeRequiredBinding = codeRequiredBindingNode.Value;
        if (string.IsNullOrEmpty(codeRequiredBinding) || codeRequiredBinding == "http://hl7.org/fhir/ValueSet/operation-parameter-type")
        {
            return null;
        }

        if (enumTypesByValueSetUrl.ContainsKey(codeRequiredBinding))
        {
            return enumTypesByValueSetUrl[codeRequiredBinding];
        }

        codeRequiredBinding = element.SelectSingleNode("fhir:path/@value", ns).Value;
        if (codeRequiredBinding.Contains(resourceName + "."))
            codeRequiredBinding = codeRequiredBinding.Substring(resourceName.Length + 1, 1).ToUpper() + codeRequiredBinding.Substring(resourceName.Length + 2);
        while (codeRequiredBinding.Contains("."))
        {
            var index = codeRequiredBinding.IndexOf(".");
            codeRequiredBinding = codeRequiredBinding.Substring(0, index) + codeRequiredBinding.Substring(index + 1, 1).ToUpper() + codeRequiredBinding.Substring(index + 2);
        }
        return codeRequiredBinding;
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
                    .Where(r => !r.IsResource() && r.Name != "Xhtml")
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
            .Where(r => !r.IsResource() && r.Name != "Xhtml")
            .Select(r => Tuple.Create(r.FhirName, "Hl7.Fhir.Model." + _version.Version + "." + r.Name))
            .Concat(
                sharedResources
                    .Where(r => !r.IsResource() && r.Name != "Xhtml")
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

    public SearchParameter(string resourceName, XmlElement sp, XmlNamespaceManager ns)
    {
        _resourceName = resourceName;
        _name = sp.SelectSingleNode("fhir:name/@value", ns).Value;
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
        yield return $"new SearchParamDefinition() {{ Resource = \"{ _resourceName }\", Name = \"{ _name }\", Description = { StringUtils.Quote(_description) }, Type = SearchParamType.{ _outputType }, Path = new string[] {{ { _path } }}{ target }{ xpath }{ expression } }},";
    }
}

static string GetMyDirectory([System.Runtime.CompilerServices.CallerFilePath] string path = "") => Path.GetDirectoryName(path);

var rootDirectory = GetMyDirectory();
var loadedVersions = LoadedVersion.LoadAll(rootDirectory);
Console.WriteLine("Generating code for versions {0}", string.Join(", ", loadedVersions.Select(lv => lv.Version)));

var valueSetsByEnumNameByVersion = ValueSet.LoadAll(loadedVersions);
valueSetsByEnumNameByVersion[string.Empty].Add(
    "Version",
    new ValueSet
    {
        EnumName = "Version",
        Description = "Supported FHIR versions",
        Values = loadedVersions
            .Select(loadedVersion => new ValueSetValue { Code = loadedVersion.Version })
            .Concat(new[] { new ValueSetValue { Code = "All" } })
            .ToList()
    }
);
var resourcesByNameByVersion = ResourceDetails.LoadAll(loadedVersions, valueSetsByEnumNameByVersion);

var generatedDirectory = Path.Combine(rootDirectory, "Generated");
foreach (var pair in valueSetsByEnumNameByVersion)
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

var sharedResourcesByName = resourcesByNameByVersion[string.Empty];
var toSkip = new[] { "Element", "Extension", "Narrative", "Resource", "Xhtml" };
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
