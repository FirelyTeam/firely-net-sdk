/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Serialization
{
    internal static class JsonTreeRewriter
    {
        public const string PRIMITIVE_PROP_NAME = "value";
        //public const string SPEC_CHILD_ID = "id";
        //public const string SPEC_CHILD_URL = "url";
        //private const string SPEC_PARENT_EXTENSION = "extension";
        //private const string SPEC_PARENT_MODIFIEREXTENSION = "modifierExtension";

        // Expand the children of a property given in the parameter. The property may only contain either a primitive JValue or a
        // complex JObject, and will list its children, which are only JValues and JObjects as well (JArrays get expanded to a list
        // of JProperties with the same name). Note that a primitive has no children (so in that case the function returns the empty
        // set), only a JObject has.
        //
        // The children of a JObject are expanded as follows:
        // * name: p                         ->             name : { (value): p }
        //
        // * name: { X }                        ->          name : { X }
        //
        // * name : p  (/need not be present/)  ->          name : { (value) : p, extension: {}, ... }
        //   _name: { extension: {},... }
        //
        // * name : [ { X }, { Y } ]            ->          1) name : {X}  2) name : {Y}
        //
        // * name: ["de", null, "Vries"]        ->          1) name: { (value): "de", extension: {X} }
        //   _name: [ { extension: {X}, ... },              2) name: { extension: {Y}, ... }
        //            { extension: {Y}, ... },              3) name: { (value): "Vries" }
        //            null ]
        //
        // Expansion reuses the existing JTokens from the original document as much as possible to avoid wasting memory
        public static IEnumerable<JProperty> ExpandComplexObject(JObject parent)
        {
            string parentName = null;

            if (parent.Parent as JProperty != null)
                parentName = ((JProperty)(parent.Parent)).Name;

            var step1 = expandArrayChildren(parent.Properties()).ToList();
            var step2 = expandPrimitiveChildren(step1, parentName).ToList();
            var step3 = combineChildren(step2).ToList();

            return step3;
        }

        private static IEnumerable<JProperty> expandArrayChildren(IEnumerable<JProperty> children)
        {
            foreach (var child in children)
            {
                if (child.Value is JArray)
                {
                    var arrayValue = (JArray)child.Value;
                    var index = 0;
                    foreach (var item in arrayValue)
                    {
                        var name = index == 0 ? child.Name : child.Name + "[" + index + "]";
                        index++;
                        yield return new JProperty(name, item);
                    }
                }
                else
                    yield return child;
            }
        }


        private static IEnumerable<JProperty> expandPrimitiveChildren(IEnumerable<JProperty> children, string parentName)
        {
            bool inResource = children.Any(prop => prop.Name == JsonDomFhirReader.RESOURCETYPE_MEMBER_NAME);

            foreach (var child in children)
            {
                if (child.Value.Type == JTokenType.Null) 
                    yield return child;
              //  else if (child.Value is JValue && !isTruePrimitive(child,parentName,inResource))
                else if (child.Value is JValue)
                    yield return new JProperty(child.Name, new JObject(new JProperty(PRIMITIVE_PROP_NAME, child.Value)));
                else
                    yield return child;
            }
        }

        // parentName and inResource provide just enough context to guess which json properties are actually
        // attributes (so always primitive) in the Xml representation
        //private static bool isTruePrimitive(JProperty property, string parentName,bool inResource)
        //{
        //    return property.Value is JValue && 
        //        property.Name == PRIMITIVE_PROP_NAME ||
        //        property.Name == SPEC_CHILD_ID && !inResource ||               // id attr that all types can have
        //        (property.Name == "div" && parentName == "text") ||
        //        (property.Name == SPEC_CHILD_URL && parentName != null && parentName.StartsWith(SPEC_PARENT_EXTENSION)) ||     // url attr of extension
        //        (property.Name == SPEC_CHILD_URL && parentName != null && parentName.StartsWith(SPEC_PARENT_MODIFIEREXTENSION));    // contentType attr of Binary resource
        //}

        private static IEnumerable<JProperty> combineChildren(IEnumerable<JProperty> children)
        {
            foreach(var child in children)
            {
                var name = baseChildName(child.Name);
                
                // If this is an "appendix" child, check wether it has a corresponding normal child.
                // Since we base our processing on the normal childs, we can skip the appendix to avoid doing work twice and duplicate the child.
                // This will also skip work on appendix childs of complex properties (which are not allowed anyway. but you won't get an error either.)
                if (child.Name[0] == '_' && children.Any(p => p.Name == name)) continue;

                if (child.Name == name) // This is the base property
                {
                    // lookup its appendix (if any)
                    var appendixProp = children.SingleOrDefault(p => p.Name == "_" + name);

                    //// it is allowed for the appendix not to exist, unless this is an array property
                    //if (appendixProp == null && isArrayProperty(child))
                    //    throw new InvalidOperationException("The two properties representing repeating element " + name + " cannot be matched 1-on-1");

                    yield return mergeAppendix(child, appendixProp);
                }
                else   // This is a sole appendix property (so, only the "_" member exists). 
                {
                    //if (isArrayProperty(child))
                    //    throw new InvalidOperationException("The two properties representing repeating element " + name + " cannot be matched 1-on-1");

                    yield return mergeAppendix(null, child);
                }
            }
        }

        //private static bool isArrayProperty(JProperty appendixProp)
        //{
        //    return appendixProp.Name.EndsWith("]");
        //}


        // Merge a JObject property (e.g. name: { (value) : p } ) with the corresponding "appendix"
        // (e.g. _name: { extension: {A} }). The result is a property with both combined (e.g. name: { (value) : p, extension: {A} }).
        // The main property may be null, in which case the resulting merge has no (value):p property with the primitive value.
        private static JProperty mergeAppendix(JProperty prop, JProperty appendix)
        {
            if (prop != null && prop.Value.Type == JTokenType.Null) prop = null;
            if (appendix != null && appendix.Value.Type == JTokenType.Null) appendix = null;

            if (appendix == null && prop == null) throw new FormatException("property value and appendix (_property) cannot both be null");

            // If there is no corresponding appendix property, return the base contents
            if (appendix == null) return prop;

            // If there is no corresponding base property, just return the appendix contents
            if (prop == null) return new JProperty(baseChildName(appendix.Name), appendix.Value);

            var to = prop.Value as JObject;
            var from = appendix.Value as JObject;

            if (to == null || from == null)
                throw new InvalidOperationException("Internal logic error: trying to merge base/appendix properties which are not JObjects");

            to.Add(from.Properties());

            return prop;
        }
       

        private static string baseChildName(string p)
        {
            return p[0] == '_' ? p.Substring(1) : p;
        }

    }
}
