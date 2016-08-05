/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
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
using System.Xml.XPath;
using Hl7.Fhir.XPath;
using Hl7.Fhir.Specification.Model;

    // todo: profile references "#lipidpanel" worden nog niet geresolved
    // todo: waar zitten de slicing fixed-values

    // todo: ExtensionDefns opnemen in Specification!!
    // todo: fixed value testen (er is nog geen testdata)

    // todo: Specification heeft nu valuesets. Maar uiteindelijk moeten er zowel met ValueSets als CodeSystems gevalideerd kunnen worden 
    // todo: resourceReference can refer to <profile> which is a structure.
    // todo: Element names are profile URI specific (names can overlap with other profiles)
    // todo: Merge Specification, Structure, Element classes (when possible) with Api.Introspection and Api.ModelInfo
    // todo: Slices

    // todo: adding JSON tests
    
    // done: When an element type is a ResourceReference, then the path does not contain [x]. The restriction is content of the refered resource.
    // done: parse meaning of [x].
    // done: name reference (recursion)
    // done: extensions


namespace Hl7.Fhir.Validation
{
  
    public class ResourceValidator
    {

        private SpecificationWorkspace specification = new SpecificationWorkspace();
        public event OutcomeLogger LogOutcome = null;

        private ReportBuilder reporter = new ReportBuilder();

        public void Log(Group group, Status status, Vector vector, string msg, params object[] args)
        {
            Outcome outcome = reporter.Log(group, status, vector, msg, args);
            if (LogOutcome != null) LogOutcome(outcome);
        }

        public void Start(Group group, Vector vector)
        {
            Outcome outcome = reporter.Start(group, vector);
            if (LogOutcome != null) LogOutcome(outcome);
        }

        public void Stop(Group group)
        {
            Outcome outcome = reporter.End(group);
            if (LogOutcome != null) LogOutcome(outcome);
        }

        public ResourceValidator(SpecificationWorkspace profile)
        {
            this.specification = profile;
        }

        public void ValidateCode(Vector vector)
        {
            if (vector.Element.Binding == null)
            {
                if (vector.Element.BindingUri != null)
                    reporter.Log(Group.Coding, Status.Skipped, vector, "Binding [{0}] could not be resolved for use in validation", vector.Element.BindingUri);
                return;
            }

            if (vector.Element.TypeRefs[0].Code == "code")
            {
                string value = vector.GetValue("@value");
                bool exists = vector.Element.Binding.Contains(value);

                if (exists)
                {
                    reporter.Log(Group.Coding, Status.Valid, vector, "Code [{0}] ({1}) is valid [{2}]",
                        vector.Element.Name, vector.Element.Binding.System, value);
                }
                else
                {
                    Log(Group.Coding, Status.Failed, vector, "Code [{0}] ({1}) contains a nonexisting value [{2}]",
                        vector.Element.Name, vector.Element.Binding.System, value);
                }
            }
            else
                Log(Group.Coding, Status.Skipped, vector, "Cannot handle Coding/CodeableConcept yet");
        }

        public void ValidateCardinality(Vector vector)
        {
            if (vector.Element.Sliced)
            {
                Log(Group.Slice, Status.Info, vector, "Validating a slice");
            }
            string slice = (vector.Element.Sliced) ? string.Format("Slice {0}: ", vector.Element.Slice) : string.Empty;
            int count = vector.Count(); 
            if (vector.Element.Cardinality.InRange(count))
            {
                Log(Group.Cardinality, Status.Valid, vector, "{0}Cardinality ({1}) of element [{2}] is valid", slice, count, vector.Element.Name);
            }
            else 
            {
                if (vector.Element.Sliced)
                {
                    Log(Group.Slice, Status.Failed, vector, "Slice {0} is not valid.", vector.Element.Slice);
                }

                if (count == 0)
                {
                    Log(Group.Cardinality, Status.Failed, vector, "{0}Node [{1}] has missing child node [{2}] ",
                            slice, vector.NodePath, vector.Element.Name);
                }
                else
                {
                    Log(Group.Cardinality, Status.Failed, vector,
                        "{0}The occurence {1} of node [{2}] under [{3}] is out of range ({4})",
                        slice, count, vector.Element.Name, vector.NodePath, vector.Element.Cardinality);
                }
            }
        }

        public void ValidateSlice(Vector vector)
        {
            // Because Vector.Count en Vector.MatchingNodes make use of Element.XPath
            // and Element.XPath takes the Slice discriminator into account,
            // Slicing can be validated within ValidateCardinality and ValidateElementChildren

        } 

        public void ValidateConstraint(Vector vector, Constraint constraint)
        {
            if (constraint.IsValid)
            {
                try
                {
                    bool valid = vector.Evaluate(constraint);

                    if (valid)
                        Log(Group.Constraint, Status.Valid, vector, "Node [{0}] conforms to constraint [{1}]", vector.Node.Name, constraint.Name);
                    else
                        Log(Group.Constraint, Status.Failed, vector, "Node [{0}] does not conform to constraint [{1}]: \"{2}\" ", vector.Node.Name, constraint.Name, constraint.HumanReadable);
                }
                catch (XPathException e)
                {
                    Log(Group.Constraint, Status.Failed, vector, "Evaluation of constraint [{0}] evaluation failed: {1}", constraint.Name, e.Message);
                }
            }
        }

        public void ValidateConstraints(Vector vector)
        {
            foreach (Constraint constraint in vector.Element.Constraints)
            {
                ValidateConstraint(vector, constraint);
            }
        }

        public void ValidateForMissingStructures(Vector vector)
        {
            IEnumerable<string> missing = vector.Element.TypeRefs.Where(t => t.Structure == null).Select(t => t.Code);
            foreach (string s in missing)
            {
                Log(Group.Structure, Status.Skipped, vector, "Profile misses structure [{0}]. Evaluation is skipped.", s);
            }
        }
        
        public void ValidateStructures(Vector vector)
        {

            if (vector.Element.Representation == Representation.Attribute) 
                // todo: HACK: the standard contradicts itself here. 
                // Extension.uri is defined as uri (which we modeled as a structure a primitive element containing extensions. 
                // And the root element is defined as Representation=Element
                // but on a higher level, it is already defined as Representation=Attribute.
                return; 


            foreach(Vector v in vector.ElementStructures)
            {
                ValidateStructure(v);
            }
        }

        public void ValidatePrimitive(Vector vector)
        {
            if (vector.Element.NameSpacePrefix != FhirNamespaceManager.Fhir)
                return;

            try
            {
                string value = vector.GetContent();
                //string pattern = vector.Element.PrimitivePattern;
                bool valid = vector.Element.IsValidPrimitive(value);
                if (valid)
                {   
                    Log(Group.Primitive, Status.Valid, vector, "The value format ({0}) of primitive [{1}] is valid. ", vector.Element.Name, vector.Node.Name);
                }
                else
                {
                    Log(Group.Primitive, Status.Failed, vector, "The value format ({0}) of primitive [{1}] is not valid: '{2}'", vector.Element.Name, vector.Node.Name, value);
                }
            }
            catch
            {
                Log(Group.Primitive, Status.Failed, vector, "The value of primitive [{0}] was not present.", vector.Node.Name);
            }
        }

        public void ValidateNoValue(Vector vector)
        {
            bool exists = vector.NodeHasAttribute("value");
            if (exists)
                Log(Group.Attribute, Status.Failed, vector,
                        "A value attribute is not allowd on non primitive element {0}", vector.Element);
        }


        public void ValidateContent(Vector vector)
        {
            // if it's a primitive, validate the value attribute, otherwise assert there is no value attribute
            if (vector.Element.IsPrimitive)
            {
                ValidatePrimitive(vector);
            }
            else
            {
                ValidateNoValue(vector);
            }
        }

        public void ValidateFixedValue(Vector vector)
        {
            // todo: op dit moment gaan we er vanuit dat de fixedvalue een andere betekenis heeft in het geval van een slice.
            if (!vector.Element.Sliced) 
            {
                var fixvalue = vector.Element.FixedValue;
                XPathNavigator node = vector.Node.SelectSingleNode("@value");
                string value = (node != null) ? node.Value : null;

                if (fixvalue != null)
                {
                    //TODO: This will never be equal. We need to call the IDeepCompare functions to compare FHIR primitives/datatypes (complex). For this
                    //to work, we need to parse the whole Node (not @value), and compare POCO objects. Fortunately the FhirParser has methods to do this,
                    //but they are as yet internal.
                    throw new NotImplementedException("Fixed value validation not yet supported in DSTU2");

                    bool equal = fixvalue.Equals(value);
                    if (equal)
                    {
                        Log(Group.Value, Status.Valid, vector, "Fixed value validated correctly");
                    }
                    else
                    {
                        Log(Group.Value, Status.Failed, vector, "Value [{0}] in node [{1}] doesn't match fixed value [{2}] of element [{3}]",
                            value, vector.Node.Name, fixvalue, vector.Element.Name);
                    }
                }
            }
        }

        public void ValidateElementChildren(Vector vector)
        {
            foreach (Vector v in vector.ElementChildren)
            {
                ValidateCardinality(v);

                foreach(Vector u in v.MatchingNodes)
                {
                    ValidateElement(u);
                }
            }
        }

        public void ValidateElementRef(Vector vector)
        {
            if (vector.Element.ElementRef != null)
            {
                Vector clone = vector.Clone();
                clone.Element = vector.Element.ElementRef;
                ValidateElement(clone);
            }
        }

        public void ValidateNodeChild(Vector vector)
        {
            string name = vector.Node.Name;
            if (!vector.Element.HasChild(name))
            {
                Log(Group.Element, Status.Unknown, vector, "Element [{0}] contains undefined element [{1}]", vector.Element.Name, name);
            }
        }

        public void ValidateNodeChildren(Vector vector)
        {
            if (vector.Element.NameSpacePrefix != FhirNamespaceManager.Fhir)
            {
                Log(Group.Element, Status.Info, vector, "Element [{0}] was skipped because it was not in the FHIR namespace.", vector.Element.Name);
                return;
            }

            foreach(Vector v in vector.NodeChildren)
            {
                ValidateNodeChild(v);
            }
        }

        public bool ShouldValidateElementChildren(Vector vector)
        {
            // RULE 1: children should be validated;
            bool should = vector.Element.HasChildren;

            if (should)
            {
                // RULE 2: Unless there is a profile reference. In that case  there should not be any children, so let's ignore them.
                bool profile = vector.Element.TypeRefs.Any(t => t.ProfileUri != null);
                should = !profile;
            }

            return should;
        }

        public void ValidateChildren(Vector vector)
        {
            if (ShouldValidateElementChildren(vector))
            {
                ValidateNodeChildren(vector);
                ValidateElementChildren(vector); // except when slicing
            }
            else
            {
                ValidateStructures(vector);
                ValidateElementRef(vector);
            }
        }

        public void ValidateAttribute(Vector vector)
        {
            if (vector.Node.Name == "value")
                return; // Value attributes cannot be validated on element level.

            bool existing = vector.ElementHasChild(vector.Node.Name, Representation.Attribute);
            if (!existing)
                Log(Group.Attribute, Status.Unknown, vector, "Attribute [{0}] does not exist.", vector.Node.Name);
        }

        public void ValidateAttributes(Vector vector)
        {
            foreach(Vector attribute in vector.NodeAttributes)
            {
                ValidateAttribute(attribute);
            }
        }

        public void ValidateElement(Vector vector)
        {
            Start(Group.Element, vector);
            {
                ValidateAttributes(vector);
                ValidateCode(vector);
                ValidateConstraints(vector);
                ValidateFixedValue(vector); // except when slicing
                ValidateForMissingStructures(vector);
                ValidateChildren(vector);
                ValidateSlice(vector);
            }
            Stop(Group.Element);
        }
        
        public void ValidateStructure(Vector vector)
        {
            if (vector.Structure == null)
            {
                Log(Group.Structure, Status.Unknown, vector, "Node [{0}] does not match a known structure. Further evaluation is impossible.", vector.Node.Name);
            }
            else
            {
                Start(Group.Structure, vector);
                ValidateContent(vector);
                ValidateElement(vector);
                Stop(Group.Structure);
            }
        }

        public Vector GetVector(XPathNavigator root)
        {
            XmlNamespaceManager nsm = FhirNamespaceManager.CreateManager(root);
            Structure structure = specification.GetStructureByName(root.Name);
            XPathNavigator node = root.CreateNavigator();

            return Vector.Create(structure, node, nsm);
        }

        public Report Validate(XPathNavigator root)
        {
            
            Vector vector = GetVector(root);
            ValidateStructure(vector);

            return reporter.Report;
        }

    }
}
