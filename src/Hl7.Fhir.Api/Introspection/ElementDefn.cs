using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.XPath;

namespace Hl7.Fhir.Api.Introspection
{
    class ElementDefn
    {
        /// <summary>
        /// Nested Elements (when this is a complex type)
        /// </summary>
        public IEnumerable<ElementDefn> Children { get; set; }

        /// <summary>
        /// Formal element name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Concise definition for xml presentation
        /// </summary>
        public string Short { get; set; }

        /// <summary>
        /// Full formal definition in human language
        /// </summary>
        public string Formal { get; set; }

        /// <summary>
        /// Comments about the use of this element
        /// </summary>
        public string Comments { get; set;  }

        /// <summary>
        /// Why is this needed?
        /// </summary>
        public string Requirements { get; set; }

        /// <summary>
        /// Author notes
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Author todo notes
        /// </summary>
        public string Todo { get; set; }

        /// <summary>
        /// Other names
        /// </summary>
        public IEnumerable<string> Synonym { get; set; }


        
        /// <summary>
        /// Minimum Cardinality
        /// </summary>
        public int? Min { get; set; }

        /// <summary>
        /// Maximum Cardinality (a number or *)
        /// </summary>
        public string Max { get; set; }

        /// <summary>
        /// Data type and Profile for this element
        /// </summary>
        public IEnumerable<TypeRef> Types { get; set; }

        /// <summary>
        /// To another element constraint (by element.name)
        /// </summary>
        public string NameReference { get; set; }
        
        /// <summary>
        /// Fixed value: [as defined for a primitive type]
        /// </summary>
        public Hl7.Fhir.Model.Element Value { get; set; }

        /// <summary>
        /// Example value: [as defined for type]
        /// </summary>
        public Hl7.Fhir.Model.Element Example { get; set; }

        /// <summary>
        /// Length for strings
        /// </summary>
        public int? MaxLength { get; set; }

        /// <summary>
        /// Reference to invariant about presence
        /// </summary>
        public IEnumerable<string> Conditions { get; set; }

        /// <summary>
        /// Condition that must evaluate to true
        /// </summary>
        public IEnumerable<Constraint> Constraints { get; set; }

        /// <summary>
        /// If the element must supported
        /// </summary>
        public bool? MustSupport { get; set; }

        /// <summary>
        /// If this modifies the meaning of other elements
        /// </summary>
        public bool? IsModifier { get; set; }

        /// <summary>
        /// ValueSet details if this is coded
        /// </summary>
        public Binding Binding { get; set; }

        /// <summary>
        /// Map element to another set of definitions
        /// </summary>
        public IEnumerable<Mapping> Mappings { get; set; }

        /// <summary>
        /// The name for this element when it is profiled
        /// </summary>
        public string ProfiledName { get; set; }

        /// <summary>
        /// If this is a complex element (ie is has children), the name
        /// of the Component (as shown in UML, generated in C#, Java, etc)
        /// a.k.a. StatedName
        /// </summary>
        public string ComponentName { get; set; }

        /// <summary>
        /// How is this element rendered? (text, 'value' attribute, attribute, xhtml, 'id' attribute)
        /// </summary>
        public XmlRendering XmlRendering { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSummaryItem { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsInherited { get; set; }


        public IEnumerable<KeyValuePair<string, object>> TransientProperties { get; set; }
    }


    public class TypeRef
    {
      
    }
}
