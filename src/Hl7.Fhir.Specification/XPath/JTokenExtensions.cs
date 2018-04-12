/*
* Copyright (c) 2014, Firely (info@fire.ly) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace Hl7.Fhir.XPath
{
    internal static class JTokenExtensions
    {
        private const string PRIMITIVE_PROP_NAME = "(value)";
        private const string RESOURCE_TYPE_PROP_NAME = "resourceType";

        public static JProperty AsResourceRoot(this JObject root)
        {
            if (root[RESOURCE_TYPE_PROP_NAME] != null)
            {
                var name = root[RESOURCE_TYPE_PROP_NAME] as JValue;

                if (name == null || name.Type != JTokenType.String)
                    throw new FormatException("Found 'resourceType' property, but it is not a primitive string");

                // Since the resourceType has now been used to generate a new complex parent,
                // don't include this prop anymore in its children
                var children = root.Properties().Where(prop => prop.Name != RESOURCE_TYPE_PROP_NAME);

                return new JProperty(name.ToString(), new JObject(children));
            }
            else
                throw new FormatException("Cannot parse this resource, the 'resourceType' property is missing to indicate the type of resource");
        }
   
        public static string ElementText(this JProperty prop)
        {
            // The value for a "primitive" property needs to be converted to Xml syntax
            if (prop.IsValueProperty())
            {
                var primitive = (JValue)prop.Value;

                // We accept four primitive json types, convert them to the correct xml string representations
                switch (primitive.Type)
                {
                    case JTokenType.Integer:
                        return XmlConvert.ToString((Int64)primitive.Value);
                    case JTokenType.Float:
                        return XmlConvert.ToString((Decimal)primitive.Value);
                    case JTokenType.Boolean:
                        return XmlConvert.ToString((bool)primitive.Value);
                    case JTokenType.String:
                        return (string)primitive.Value;
                    default:
                        throw new FormatException("Only integer, float, boolean and string primitives are allowed in FHIR Json");
                }
            }

            // The value for a complex property is simply all the text of its children appended
            else if (prop.Value is JObject)
            {
                var result = new StringBuilder();

                foreach (var child in prop.ElementChildren())
                    result.Append(child.ElementText());

                return result.ToString();
            }
            
            // We only handle primitive & complex, other nodes are expanded by the ElementChildren() function
            else
                throw new InvalidOperationException("Don't know how to get text from a JToken of type " + prop.GetType().Name);
        }


        public static bool IsValueProperty(this JProperty prop)
        {
            return prop.Name == PRIMITIVE_PROP_NAME;
        }

        public static JValue PrimitivePropertyValue(this JProperty token)
        {
            if (token.Value is JObject)
            {
                var obj = (JObject)token.Value;
                var prim = obj.Properties().SingleOrDefault(p => p.Name == PRIMITIVE_PROP_NAME);
                if (prim == null) return null;

                if (prim.IsValueProperty())
                {
                    return (JValue)prim.Value;
                }
            }

            throw new ArgumentException("Property is not a JObject representing a primitive value", "token");
        }


        // Expand the children of a property given in the parameter. The property may only contain either a primitive JValue or a
        // complex JObject, and will list its children, which are only JValues and JObjects as well (JArrays get expanded to a list
        // of JProperties with the same name). Note that a primitive has no children (so in that case the function returns the empty
        // set), only a JObject has.
        //
        // The children of a JObject are expanded as follows:
        // * (value): p                         ->          (value) : p
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
        public static IEnumerable<JProperty> ElementChildren(this JProperty prop)
        {
            // At the leaves of the model, we'll find primitive properties named "(value)". They have no children.
            if (prop.IsValueProperty()) yield break;    

            // Otherwise, property MUST be a complex JObject, since we translate primitives
            // to JObjects with a value property + extensions + id, and expand JArray to repeating JProperties,
            // thus, this function may never be called with anything else
            var parent = prop.Value as JObject;
            if(parent == null) throw new InvalidOperationException("ElementChildren expects a property that's either a JValue named '(value)' or a JObject");

            // Special case: If we have a complex object with a 'resourceType' property, this is represented as a complex JObject with
            // a single member named after the name of the resourceType
            if (parent[RESOURCE_TYPE_PROP_NAME] != null)
            {
                yield return AsResourceRoot(parent);
                yield break;        // that's all, just a single child
            }

            // Before we go to work with the children of the JObject, expand the child list once, 
            // since we need to scan it anyway, and need to rescan it in some cases
            var children = parent.Properties().ToList();

            foreach(var child in children)
            {
                // Note: children can be "normal" properties or "appendix" properties (member with "_" prefix)

                // Normal case A: if this is a primitive value property, don't expand it again, just return it
                if (child.IsValueProperty()) yield return child;

                // Normal case B: a complex non-"appendix" JObject child. it needs no additional processing
                else if (child.Value is JObject && child.Name[0] != '_') yield return child;

                // Normal case C: an array of JObject non-"appendix" children. it needs to be split up into one property per child
                else if (isComplexArray(child.Value) && child.Name[0] != '_')
                    foreach (var elem in child.Value.Children()) yield return new JProperty(child.Name, elem);

                else
                {
                    // What's left are primitive properties (or arrays or primitive properties) which may be absent
                    // and may have an appendix
                    var name = baseChildName(child.Name);

                    // If this is an "appendix" child, check wether it has a corresponding normal child.
                    // Since we base our processing on the normal childs, we can skip the appendix to avoid doing work twice and duplicate the child.
                    // This will also skip work on appendix childs of complex properties (which are not allowed anyway. but you won't get an error either.)
                    if (child.Name[0] == '_' && children.Any(p => p.Name == name)) continue;

                    JProperty primitiveProp = null;     // property contains a JObject or a JArray (of JObjects)

                    if (child.Name == name)     // the base property exists
                    {
                        // lookup its appendix (if any)
                        var appendixProp = children.SingleOrDefault(p => p.Name == "_" + name);

                        // Make sure any (array of) "primitive" property gets turned into a complex one
                        primitiveProp = expandChild(child);

                        if (appendixProp != null) primitiveProp = mergeAppendix(primitiveProp, appendixProp);
                    }
                    else   // This is a sole appendix property (so, only the "_" member exists). 
                    {
                        primitiveProp = mergeAppendix(null, child);
                    }

                    if (primitiveProp.Value is JObject)
                        yield return primitiveProp;
                    else if (primitiveProp.Value is JArray)
                    {
                        foreach (var elem in primitiveProp.Value.Children())
                            yield return new JProperty(name, elem);
                    }
                    else
                        throw new InvalidOperationException(String.Format("Internal logic error. Did not expect property {0} of type {1}", primitiveProp.Name, primitiveProp.Type));
                }
            }
        }

        private static string baseChildName(string p)
        {
            return p[0] == '_' ? p.Substring(1) : p;
        }


        // Merge a JObject property (e.g. name: { (value) : p } ) with the corresponding "appendix"
        // (e.g. _name: { extension: {A} }). The result is a property with both combined (e.g. name: { (value) : p, extension: {A} }).
        // Works both for single properties as well as arrays. The main property may be null, in which case the resulting
        // merge has no (value):p property with the primitive value.
        private static JProperty mergeAppendix(JProperty prop, JProperty appendix)
        {
            // If prop is not null, it's either a JObject or a JArray of JObject (guaranteed by the caller)
            if(appendix == null) throw new ArgumentNullException("appendix", "Need an appendix to be able to expand property");

            bool isArray = (prop != null && prop.Value is JArray) || appendix.Value is JArray;

            if (!isArray)
            {
                var appendixContents = appendix.Value as JObject;
                if(appendixContents == null)
                    throw new FormatException(String.Format("Appendix property {0} is not a complex value.", appendix.Name));

                // There's no corresponding property for the appendix, just return the appendix
                if (prop == null) return new JProperty(baseChildName(appendix.Name), appendix.Value);
                              
                // Combine both the primitive and the appendix into a single property
                var value = (JObject)prop.Value;
                value.Add(appendixContents.Properties());
                return prop;
            }
            else
            {
                assertComplexArray(appendix.Name, appendix.Value);   // appendix props are always complex

                // If there is no corresponding property for the appendix, just return the appendix contents
                if (prop == null) return new JProperty(baseChildName(appendix.Name), appendix.Value);

                // Expand both enumerables, since we browse them multiple times anyway
                var elements = ((JArray)prop.Value).Children().ToList();
                var appendixElements =((JArray)appendix.Value).Children().ToList();

                // property + appendix Arrays should be a 1-to-1 mapping, so the same size
                if (elements.Count != appendixElements.Count)
                    throw new FormatException(String.Format("The appendix array for property {0} does have the same number of elements", prop.Name));

                // Merge the appendices properties into the primitive's
                for (var index = 0; index < elements.Count; index++)
                {
                    var primitiveElem = (JObject)elements[index];
                    
                    if( appendixElements[index] is JObject )
                        primitiveElem.Add( ((JObject)appendixElements[index]).Properties());
                }

                return prop;
            }
        }


        // Expand:
        // * a JValue JProperty (name: p) to a JObject JProperty (name: { (value):p })
        // * a JArray JProperty (name: [a,b,c]) to a JArray JProperty (name: [ { (value):a }, { (value):b }, ... ])
        private static JProperty expandChild(JProperty child)
        {
            var arr = child.Value as JArray;

            if (arr != null)
            {
                var result = new JArray();
                foreach (var element in arr.Children())
                {
                    if (element.Type == JTokenType.Null)
                        result.Add(new JObject());
                    else if (element is JValue)
                        result.Add(new JObject(new JProperty(PRIMITIVE_PROP_NAME, element)));
                    else
                        throw new FormatException(String.Format("Array for property {0} contains an element that is neither primitive nor null", child.Name));
                }

                return new JProperty(child.Name, result);
            }
            else if (child.Value is JValue)
            {
                return new JProperty(child.Name, new JObject(new JProperty(PRIMITIVE_PROP_NAME, child.Value)));
            }
            else
                throw new FormatException(String.Format("Property {0} should be a primitive", child.Name));
        }

        private static void assertPrimitiveArray(string propName, JToken value)
        {
            var arr = value as JArray;

            if (arr == null)
                throw new FormatException(String.Format("Property {0} has to be an array", propName));

            if(!arr.Children().All(c => c is JValue || c.Type == JTokenType.Null))
                throw new FormatException(String.Format("Property {0} contains an element that is neither primitive nor null", propName));
        }

        private static void assertComplexArray(string propName, JToken value)
        {
            var arr = value as JArray;

            if(arr == null)
                throw new FormatException(String.Format("Property {0} has to be an array", propName));

            if(!arr.Children().All(c => c is JObject || c.Type == JTokenType.Null))
                throw new FormatException(String.Format("Property {0} contains an element that is neither complex nor null", propName));
        }


        private static bool isComplexArray(JToken value)
        {
            var arr = value as JArray;

            return arr != null && arr.Children().All(c => c is JObject || c.Type == JTokenType.Null);
        }

    }
}
