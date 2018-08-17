/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Specification;

namespace Hl7.Fhir.ElementModel
{
    internal class PocoElementNode : ITypedElement, IAnnotated, IExceptionSource, IShortPathGenerator
    {
        public readonly object Current;
        public readonly PocoStructureDefinitionSummaryProvider Provider;
        public readonly IElementDefinitionSummary DefinitionSummary;
        public readonly int ArrayIndex;

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        internal PocoElementNode(Base parent, PocoStructureDefinitionSummaryProvider provider, string rootName = null)
        {
            Current = parent;
            InstanceType = parent.TypeName;
            var typeInfo = provider.Provide(parent.GetType());
            Definition = Specification.ElementDefinitionSummary.ForRoot(rootName ?? parent.TypeName, typeInfo);
            Location = InstanceType;
            ShortPath = InstanceType;
            ArrayIndex = 0;
            Provider = provider;
        }

        private PocoElementNode(object instance, PocoElementNode parent, string location, string shortPath, int arrayIndex,
            IElementDefinitionSummary summary)
        {
            Current = instance;
            InstanceType = determineInstanceType(instance, summary);
            Provider = parent.Provider;
            ExceptionHandler = parent.ExceptionHandler;
            Definition = summary;
            Location = location;
            ShortPath = shortPath;
            ArrayIndex = arrayIndex;
            Provider = parent.Provider;
        }

        public IElementDefinitionSummary Definition { get; private set; }

        public string ShortPath { get; private set; }

        private IStructureDefinitionSummary down() =>
            // If this is a backbone element, the child type is the nested complex type
            Definition.Type[0] is IStructureDefinitionSummary be ? 
                    be : 
                    Provider.Provide(InstanceType);


        public IEnumerable<ITypedElement> Children(string name)
        {
            if (!(Current is Base parentBase)) yield break;

            var children = parentBase.NamedChildren;

            string oldElementName = null;
            int arrayIndex = 0;
            var childElementDefinitions = down().GetElements();

            foreach (var child in children)
            {
                if (name == null || child.ElementName == name)
                {
                    var mySummary = childElementDefinitions.Single(c => c.ElementName == child.ElementName);

                    if (!mySummary.IsCollection || oldElementName != child.ElementName)
                        arrayIndex = 0;
                    else
                        arrayIndex += 1;

                    var location = Location == null ? child.ElementName :
                                $"{Location}.{child.ElementName}[{arrayIndex}]";
                    var shortPath = ShortPath == null ? child.ElementName :
                        (mySummary.IsCollection ?
                            $"{ShortPath}.{child.ElementName}[{arrayIndex}]" :
                            $"{ShortPath}.{child.ElementName}");

                    yield return new PocoElementNode(child.Value, this, location, shortPath, arrayIndex, mySummary);
                }

                oldElementName = child.ElementName;
            }
        }

        /// <summary>
        /// This is only needed for search data extraction (and debugging)
        /// to be able to read the values from the selected node (if a coding, so can get the value and system)
        /// </summary>
        /// <remarks>Will return null if on id, value, url (primitive attribute props in xml)</remarks>
        public Base FhirValue => Current as Base;

        public string Name => Definition.ElementName;

        public object Value
        {
            get
            {
                try
                {
                    switch (Current)
                    {
                        case string s:
                            return s;
                        case Hl7.Fhir.Model.Instant ins:
                            return ins.ToPartialDateTime();
                        case Hl7.Fhir.Model.Time time:
                            return time.ToTime();
                        case Hl7.Fhir.Model.Date dt:
                            return dt.ToPartialDateTime();
                        case FhirDateTime fdt:
                            return fdt.ToPartialDateTime();
                        case Hl7.Fhir.Model.Integer fint:
                            return (long)fint.Value;
                        case Hl7.Fhir.Model.PositiveInt pint:
                            return (long)pint.Value;
                        case Hl7.Fhir.Model.UnsignedInt unsint:
                            return (long)unsint.Value;
                        case Hl7.Fhir.Model.Base64Binary b64:
                            return b64.Value != null ? PrimitiveTypeConverter.ConvertTo<string>(b64.Value) : null;
                        case Primitive prim:
                            return prim.ObjectValue;
                        default:
                            return null;
                    }
                }
                catch (FormatException)
                {
                    // If it fails, just return the unparsed contents
                    return (Current as Primitive)?.ObjectValue;
                }
            }
        }


        public string InstanceType { get; private set; }

        public static string determineInstanceType(object instance, IElementDefinitionSummary summary)
        {
            var typeName = !summary.IsChoiceElement && !summary.IsResource ?
                        summary.Type.Single().GetTypeName() : ((Base)instance).TypeName;

            return ModelInfo.IsProfiledQuantity(typeName) ? "Quantity" : typeName;
        }

        public string Location { get; private set; }

        public IEnumerable<object> Annotations(Type type)
        {
            if (type == typeof(PocoElementNode) || type == typeof(ITypedElement) || type == typeof(IShortPathGenerator))
                return new[] { this };
            else if (FhirValue is IAnnotated ia)
                return ia.Annotations(type);
            else
                return Enumerable.Empty<object>();
        }
    }
}