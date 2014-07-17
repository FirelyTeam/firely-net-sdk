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
using System.Text.RegularExpressions;
using Fhir.IO;

namespace Fhir.Profiling
{
  
    public class ResourceValidator
    {

        private Specification Profile = new Specification();
        public event OutcomeLogger LogOutcome = null;

        private ReportBuilder reporter = new ReportBuilder();

        public void Log(Group group, Status status, Vector vector, string msg, params object[] args)
        {
            Outcome outcome = reporter.Log(group, status, vector, msg, args);
            if (LogOutcome != null) LogOutcome(outcome);
        }

        public ResourceValidator(Specification profile)
        {
            this.Profile = profile;
        }

        public void ValidateCode(Vector vector)
        {
            if (vector.Element.Binding == null)
                return;

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

        public void ValidateCardinality(Vector vector)
        {   
            int count = vector.Count(); 
            if (vector.Element.Cardinality.InRange(count))
            {
                Log(Group.Cardinality, Status.Valid, vector, "Cardinality ({1}) of element [{0}] is valid", vector.Element.Name, count);
            }
            else 
            {
                if (count == 0)
                {
                    Log(Group.Cardinality, Status.Failed, vector,
                     "Node [{0}] has missing child node [{1}] ",
                     vector.NodePath(), vector.Element.Name);
                }
                else
                {
                    Log(Group.Cardinality, Status.Failed, vector,
                        "The occurence {0} of node [{1}] under [{2}] is out of range ({3})",
                        count, vector.NodePath(), vector.Element.Name, vector.Element.Cardinality);
                }
            }
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
            foreach(Vector v in vector.ElementStructures)
            {
                ValidateStructure(v);
            }
        }

        public void ValidatePrimitive(Vector vector)
        {
            // fail. validation of primites should be done at the root element
            // this should be the root element of a structure
            if (!vector.Element.IsPrimitive)
                return;

            if (vector.Element.NameSpacePrefix != FhirNamespaceManager.Fhir)
                return;

            try
            {
                string value = vector.GetContent();
                string pattern = vector.Element.PrimitivePattern;
                if (Regex.IsMatch(value, pattern))
                {   
                    Log(Group.Primitive, Status.Valid, vector, "The value format ({0}) of primitive [{1}] is valid. ", vector.Element.Name, vector.Node.Name);
                }
                else
                {
                    Log(Group.Primitive, Status.Failed, vector, "The value format ({0}) of primitive [{1}] not valid: '{2}'", vector.Element.Name, vector.Node.Name, value);
                }
            }
            catch
            {
                Log(Group.Primitive, Status.Failed, vector, "The value of primitive [{0}] was not present.", vector.Node.Name);
            }
        }

        public void ValidateFixedValue(Vector vector)
        {
            string fixvalue = vector.Element.FixedValue;
            XPathNavigator node = vector.Node.SelectSingleNode("@value");
            string value = (node != null) ? node.Value : null;

            if (fixvalue != null)
            {
                bool equal = fixvalue.Equals(value);
                if (equal)
                {
                    Log(Group.Value, Status.Valid, vector, "Fixed value validated correctly");
                }
                else
                {
                    Log(Group.Value, Status.Failed, vector, "Value [{0}] doesn't match fixed value [{1}] of element [{2}]", value, fixvalue, vector.Element.Name);
                }
            }
        }

        public void ValidateElementChildren(Vector vector)
        {
            foreach (Vector v in vector.ElementChildren)
            {
                ValidateCardinality(v);

                foreach(Vector u in v.Matches)
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
            if (vector.Element.HasTypeRef) //element has a reference, so there are no Element children to validate to. 
                return;

            if (vector.Element.NameSpacePrefix != FhirNamespaceManager.Fhir)
            {
                reporter.Log(Group.Element, Status.Info, "Element [{0}] was skipped because it was not in the FHIR namespace.", vector.Element.Name);
                return;
            }

            foreach(Vector v in vector.NodeChildren)
            {
                ValidateNodeChild(v);
            }
        }

        public void ValidateSlices(Vector vector)
        {
             
        }

        public void ValidateElement(Vector vector)
        {
            reporter.Start(Group.Element, vector);
            {
                ValidateCode(vector);
                ValidateConstraints(vector);
                ValidateStructures(vector);
                ValidatePrimitive(vector);
                ValidateFixedValue(vector);
                ValidateForMissingStructures(vector);
                ValidateNodeChildren(vector);
                ValidateElementChildren(vector);
                ValidateElementRef(vector);
                ValidateSlices(vector);
            }
            reporter.End(Group.Element);
        }
        
        public void ValidateStructure(Vector vector)
        {
            if (vector.Structure == null)
            {
                Log(Group.Structure, Status.Unknown, vector, "Node [{0}] does not match a known structure. Further evaluation is impossible.", vector.Node.Name);
            }
            else
            {
                reporter.Start(Group.Structure, vector);
                ValidateElement(vector);
                reporter.End(Group.Structure);
            }
        }

        public Vector GetVector(XPathNavigator root)
        {
            XmlNamespaceManager nsm = FhirNamespaceManager.CreateManager(root);
            Structure structure = Profile.GetStructureByName(root.Name);
            XPathNavigator node = root.CreateNavigator();

            return Vector.Create(structure, node, nsm);
        }

        public Report Validate(XPathNavigator root)
        {
            reporter.Clear();
            Vector vector = GetVector(root);
            ValidateStructure(vector);

            return reporter.Report;
        }

    }
}
