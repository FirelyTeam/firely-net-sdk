using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Schema;
using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Navigation
{
    public partial class ElementDefinitionNavigator : ISchemaProvider
    {
        public IAssertion GetSchema()
        {
            var result = new ElementSchema(
                id: "#" + Current.Path,
                harvestAssertions(Current)
                );

            return result;
        }

        private IEnumerable<IAssertion> harvestAssertions(ElementDefinition def)
        {
            if (tryHarvestChildren(this, out var children)) yield return children;
            if (tryHarvestElementRegEx(def, out var elemRegExAssertion)) yield return elemRegExAssertion;
            if (tryHarvestTypeRefRegEx(def, out var typeRefRegExAssertion)) yield return typeRefRegExAssertion;
        }


        private bool tryHarvestElementRegEx(ElementDefinition def, out IAssertion assertion)
        {
            assertion = buildPattern(def, "http://hl7.org/fhir/StructureDefinition/regex");
            return assertion != null;
        }

        private bool tryHarvestTypeRefRegEx(ElementDefinition def, out IAssertion assertion)
        {
            assertion = null;

            if (def.IsPrimitiveValueConstraint() && def.Type.Count == 1)
            {
                assertion = buildPattern(def.Type.Single(), "http://hl7.org/fhir/StructureDefinition/structuredefinition-regex");
                return assertion != null;
            }

            return false;
        }

        private Pattern buildPattern(IExtendable element, string uri)
        {
            var pattern = element.GetStringExtension(uri);
            if (pattern == null) return null;

            return new Pattern(pattern);
        }

        private bool tryHarvestChildren(ElementDefinitionNavigator nav, out IAssertion assertion)
        {           
            assertion = new Children();
            if (!nav.HasChildren) return false;

            var childNav = nav.ShallowCopy();   // make sure closure is to a clone, not the original argument
            assertion = new Children(harvest);
            return true;

            IReadOnlyDictionary<string, IAssertion> harvest()
            {
                var children = new Dictionary<string, IAssertion>();

                childNav.MoveToFirstChild();

                do
                {
                    children.Add(childNav.PathName, childNav.GetSchema());
                }
                while (childNav.MoveToNext());

                return children;
            };
        }
    }
}
