using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Version-independent element definition constraint<br/>
    /// Used to define constraints in a version-agnostic way in the resource class declarations
    /// </summary>
    public class ElementDefinitionConstraint
    {
        public ElementDefinitionConstraint()
        { }

        public ElementDefinitionConstraint(DSTU2.ElementDefinition.ConstraintComponent component)
        {
            Key = component.Key;
            Severity = component.Severity;
            Human = component.Human;
            Expression = component.GetStringExtension("http://hl7.org/fhir/StructureDefinition/structuredefinition-expression");
            Xpath = component.Xpath;
        }

        public ElementDefinitionConstraint(STU3.ElementDefinition.ConstraintComponent component)
        {
            Key = component.Key;
            Severity = component.Severity;
            Human = component.Human;
            Expression = component.Expression;
            Xpath = component.Xpath;
        }

        public ElementDefinitionConstraint(R4.ElementDefinition.ConstraintComponent component)
        {
            Key = component.Key;
            Severity = component.Severity;
            Human = component.Human;
            Expression = component.Expression;
            Xpath = component.Xpath;
        }

        /// <summary>
        /// Target of 'condition' reference above
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// error | warning
        /// </summary>
        public ConstraintSeverity? Severity { get; set; }

        /// <summary>
        /// Human description of constraint
        /// </summary>
        public string Human { get; set; }

        /// <summary>
        /// FHIRPath expression of constraint
        /// </summary>
        public string Expression { get; set; }

        /// <summary>
        /// XPath expression of constraint
        /// </summary>
        public string Xpath { get; set; }

        public DSTU2.ElementDefinition.ConstraintComponent ToDstu2()
        {
            return new DSTU2.ElementDefinition.ConstraintComponent
            {
                Key = Key,
                Severity = Severity,
                Human = Human,
                Extension = new List<Extension>
                {
                    new Extension("http://hl7.org/fhir/StructureDefinition/structuredefinition-expression", new FhirString(Expression))
                },
                Xpath = Xpath,
            };
        }

        public STU3.ElementDefinition.ConstraintComponent ToStu3()
        {
            return new STU3.ElementDefinition.ConstraintComponent
            {
                Key = Key,
                Severity = Severity,
                Human = Human,
                Expression = Expression,
                Xpath = Xpath,
            };
        }

        public R4.ElementDefinition.ConstraintComponent ToR4()
        {
            return new R4.ElementDefinition.ConstraintComponent
            {
                Key = Key,
                Severity = Severity,
                Human = Human,
                Expression = Expression,
                Xpath = Xpath,
            };
        }
    }
}
