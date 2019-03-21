using Hl7.Fhir.Utility;
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
        public ElementDefinitionConstraint(
            IEnumerable<Version> versions,
            string key,
            ConstraintSeverity? severity = null,
            string human = null,
            string expression = null,
            string xpath = null
        )
        {
            if (versions == null || !versions.Any()) throw Error.ArgumentNullOrEmpty(nameof(versions));
            if (string.IsNullOrEmpty(key)) throw Error.ArgumentNullOrEmpty(nameof(key));

            Versions = new HashSet<Version>(versions);
            Key = key;
            Severity = severity;
            Human = human;
            Expression = expression;
            Xpath = xpath;
        }

        public ElementDefinitionConstraint(DSTU2.ElementDefinition.ConstraintComponent component)
        {
            if (component == null) throw Error.ArgumentNull(nameof(component));

            Versions = new HashSet<Version> { Version.DSTU2 };
            Key = component.Key;
            Severity = component.Severity;
            Human = component.Human;
            Expression = component.GetStringExtension("http://hl7.org/fhir/StructureDefinition/structuredefinition-expression");
            Xpath = component.Xpath;
        }

        public ElementDefinitionConstraint(STU3.ElementDefinition.ConstraintComponent component)
        {
            if (component == null) throw Error.ArgumentNull(nameof(component));

            Versions = new HashSet<Version> { Version.STU3 };
            Key = component.Key;
            Severity = component.Severity;
            Human = component.Human;
            Expression = component.Expression;
            Xpath = component.Xpath;
        }

        public ElementDefinitionConstraint(R4.ElementDefinition.ConstraintComponent component)
        {
            if (component == null) throw Error.ArgumentNull(nameof(component));

            Versions = new HashSet<Version> { Version.R4 };
            Key = component.Key;
            Severity = component.Severity;
            Human = component.Human;
            Expression = component.Expression;
            Xpath = component.Xpath;
        }

        /// <summary>
        /// FHIR versions this constraint applies to
        /// </summary>
        public HashSet<Version> Versions { get; }

        /// <summary>
        /// Target of 'condition' reference above
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// error | warning
        /// </summary>
        public ConstraintSeverity? Severity { get; }

        /// <summary>
        /// Human description of constraint
        /// </summary>
        public string Human { get; }

        /// <summary>
        /// FHIRPath expression of constraint
        /// </summary>
        public string Expression { get; }

        /// <summary>
        /// XPath expression of constraint
        /// </summary>
        public string Xpath { get; }

        public bool AppliesTo(Model.Version version)
        {
            return Versions.Contains(Version.All) || Versions.Contains(version);
        }

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

    public static class ElementDefinitionConstraintsExtensions
    {
        public static DSTU2.ElementDefinition.ConstraintComponent[] ToDstu2(this IEnumerable<ElementDefinitionConstraint> constraints)
        {
            return constraints
                .AppliesTo(Version.DSTU2)
                .Select(constraint => constraint.ToDstu2())
                .ToArray();
        }

        public static STU3.ElementDefinition.ConstraintComponent[] ToStu3(this IEnumerable<ElementDefinitionConstraint> constraints)
        {
            return constraints
                .AppliesTo(Version.STU3)
                .Select(constraint => constraint.ToStu3())
                .ToArray();
        }

        public static R4.ElementDefinition.ConstraintComponent[] ToR4(this IEnumerable<ElementDefinitionConstraint> constraints)
        {
            return constraints
                .AppliesTo(Version.R4)
                .Select(constraint => constraint.ToR4())
                .ToArray();
        }

        public static IEnumerable<ElementDefinitionConstraint> AppliesTo(this IEnumerable<ElementDefinitionConstraint> constraints, Model.Version version)
        {
            if (version == Version.All) throw Error.Argument(nameof(version), "Must be a specific version");

            return constraints?
                .Where(constraint => constraint.AppliesTo(version));
        }
    }
}
