/*
  Copyright (c) 2011-2012, HL7, Inc
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
  
  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
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

using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Hl7.Fhir.Support;
using Hl7.Fhir.FhirPath;

namespace Hl7.Fhir.Model
{
    [System.Diagnostics.DebuggerDisplay("\\{\"{TypeName,nq}/{Id,nq}\" Identity={ResourceIdentity()}}")]
    [InvokeIValidatableObject]
    public abstract partial class DomainResource : IModifierExtendable
    {
        /// <summary>
        /// Perform the Invariant based validation for this rule
        /// </summary>
        /// <param name="invariantRule"></param>
        /// <param name="tree"></param>
        /// <param name="result">The OperationOutcome that will have the validation results appended</param>
        /// <returns></returns>
        private static bool ValidateInvariant(ElementDefinition.ConstraintComponent invariantRule, FhirPath.IFhirPathElement tree, OperationOutcome result)
        {
            string expression = invariantRule.GetStringExtension("http://hl7.org/fhir/StructureDefinition/structuredefinition-expression");
            try
            {
                // No FluentPath extension
                if (string.IsNullOrEmpty(expression))
                {
                    result.Issue.Add(new OperationOutcome.IssueComponent()
                    {
                        Code = OperationOutcome.IssueType.Invariant,
                        Severity = OperationOutcome.IssueSeverity.Warning,
                        Details = new CodeableConcept(null, invariantRule.Key, "Unable to validate without a fluentpath expression"),
                        Diagnostics = expression
                    });
                    return true;
                }
                if (FhirPath.PathExpression.Predicate(expression, tree))
                    return true;
                result.Issue.Add(new OperationOutcome.IssueComponent()
                {
                    Code = OperationOutcome.IssueType.Invariant,
                    Severity = OperationOutcome.IssueSeverity.Error,
                    Details = new CodeableConcept(null, invariantRule.Key, invariantRule.Human),
                    Diagnostics = expression
                });
                return false;
            }
            catch (Exception ex)
            {
                result.Issue.Add(new OperationOutcome.IssueComponent()
                {
                    Code = OperationOutcome.IssueType.Invariant,
                    Severity = OperationOutcome.IssueSeverity.Fatal,
                    Details = new CodeableConcept(null, invariantRule.Key, "FATAL: Unable to process the invariant rule: " + invariantRule.Key + " " + expression),
                    Diagnostics = String.Format("FluentPath: {0}\r\nError: {1}", expression, ex.Message)
                });
                return false;
            }
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = new List<ValidationResult>(base.Validate(validationContext));

            if (this.Contained != null)
            {
                if (!Contained.OfType<DomainResource>().All(dr => dr.Text == null))
                    result.Add(new ValidationResult("Resource has contained resources with narrative"));

                if (!Contained.OfType<DomainResource>().All(cr => cr.Contained == null || !cr.Contained.Any()))
                    result.Add(new ValidationResult("Resource has contained resources with nested contained resources"));
            }

            // and process all the invariants from the resource
            if (InvariantConstraints != null && InvariantConstraints.Count > 0)
            {
                var sw = System.Diagnostics.Stopwatch.StartNew();
                // Need to serialize to XML until the object model processor exists
                // string tpXml = Fhir.Serialization.FhirSerializer.SerializeResourceToXml(this);
                // FhirPath.IFhirPathElement tree = FhirPath.InstanceTree.TreeConstructor.FromXml(tpXml);
                FhirPath.IFhirPathElement tree = new ModelTree(this);
                OperationOutcome results = new OperationOutcome();
                foreach (var invariantRule in InvariantConstraints)
                {
                    ValidateInvariant(invariantRule, tree, results);
                }
                foreach (var item in results.Issue)
                {
                    if (item.Severity == OperationOutcome.IssueSeverity.Error
                        || item.Severity == OperationOutcome.IssueSeverity.Fatal)
                        result.Add(new ValidationResult(item.Details.Coding[0].Code + ": " + item.Details.Text));
                }

                sw.Stop();
                System.Diagnostics.Trace.WriteLine(String.Format("Validation of {0} execution took {1}", ResourceType.ToString(), sw.Elapsed.TotalSeconds));
            }

            return result;
        }
    }

    public class ModelTree : FhirPath.IFhirPathElement
    {
        public ModelTree(Resource resource)
        {
            // _root = new ModelTreeRoot(resource);
            _root = new ModelElement(null, resource.ResourceType.ToString(), resource.GetType(), resource);
        }

        ModelElement _root;

        public IFhirPathElement Parent { get { throw new NotImplementedException(); } }

        public object Value { get { throw new NotImplementedException(); } }

        public IEnumerable<ChildNode> Children()
        {
            List<ChildNode> children = new List<ChildNode>();
            children.Add(new ChildNode(_root.Name, _root));
            return children;
        }
    }

    [System.Diagnostics.DebuggerDisplay(@"\{Name = {_name} Value = {_value}}")] // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
    public class ModelElement : IFhirPathElement
    {
        public ModelElement(IFhirPathElement parent, string name, Type elementType, object value)
        {
            _name = name;
            _parent = parent;
            _value = value;
            _mapping = Serialization.SerializationConfig.Inspector.FindClassMappingByType(elementType);
        }

        private IFhirPathElement _parent;
        private string _name;
        private object _value;
        private Introspection.ClassMapping _mapping;

        public string Name { get { return _name; } }
        public IFhirPathElement Parent { get { return _parent; } }

        public object Value
        {
            get
            {
                object result = _value;
                // Now do some conversions on these types to remove the FHIRish ness of the types
                if (result is FhirString)
                {
                    if (result != null)
                        return (result as FhirString).Value;
                    return "";
                }
                if (result is FhirDecimal)
                {
                    if (result != null && (result as FhirDecimal).Value.HasValue)
                        return (result as FhirDecimal).Value;
                    return null;
                }
                if (result is Instant)
                {
                    if (result != null && (result as Instant).Value.HasValue)
                    {
                        PartialDateTime dateResult = new PartialDateTime((result as Instant).Value.Value);
                        return dateResult;
                    }
                    return null;
                }
                if (result is FhirDateTime)
                {
                    if (result != null)
                    {
                        PartialDateTime dateResult = PartialDateTime.Parse((result as FhirDateTime).Value);
                        return dateResult;
                    }
                    return null;
                }
                if (result is Date)
                {
                    if (result != null)
                    {
                        PartialDateTime dateResult = PartialDateTime.Parse((result as Date).Value);
                        return dateResult;
                    }
                    return null;
                }
                if (result == null)
                    return null;
                return result;
            }
        }

        private IEnumerable<ChildNode> _children;

        public IEnumerable<ChildNode> Children()
        {
            if (_children != null)
                return _children;
            List<ChildNode> children = new List<ChildNode>();
            _children = children;
            if (_value == null)
                return children;
            foreach (var item in _mapping.PropertyMappings)
            {
                if (item.IsPrimitive && item.Name.ToLower() == "value")
                    continue;
                var itemValue = item.GetValue(_value);
                if (itemValue != null)
                {
                    if (item.IsCollection)
                    {
                        foreach (var colItem in (itemValue as System.Collections.IList))
                        {
                            if (colItem != null)
                                children.Add(new ChildNode(item.Name, new ModelElement(this, item.Name, item.ElementType, colItem)));
                        }
                    }
                    else
                    {
                        children.Add(new ChildNode(item.Name, new ModelElement(this, item.Name, item.ElementType, itemValue)));
                    }
                }
            }
            return children;
        }

        //internal static bool ConvertValue(Introspection.PropertyMapping prop, ref object value)
        //{
        //    return value;
        //}
    }
}
