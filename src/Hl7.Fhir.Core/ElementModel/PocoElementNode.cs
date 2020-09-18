/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.ElementModel
{
    internal class PocoElementNode : ITypedElement, IAnnotated, IExceptionSource, IShortPathGenerator, IFhirValueProvider, IResourceTypeSupplier
    {
        public readonly Base Current;
        private readonly ClassMapping _myClassMapping;

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        internal PocoElementNode(Base root, string rootName = null)
        {
            Current = root;
            _myClassMapping = (ClassMapping)PocoStructureDefinitionSummaryProvider.Provide(root.GetType());
            InstanceType = ((IStructureDefinitionSummary)_myClassMapping).TypeName;
            Definition = ElementDefinitionSummary.ForRoot(_myClassMapping, rootName ?? root.TypeName);

            Location = InstanceType;
            ShortPath = InstanceType;
        }

        private PocoElementNode(Base instance, PocoElementNode parent, PropertyMapping definition, string location, string shortPath)
        {
            Current = instance;

            var instanceType = determineInstanceType(instance.GetType(), definition);
            _myClassMapping = (ClassMapping)PocoStructureDefinitionSummaryProvider.Provide(instanceType);
            InstanceType = ((IStructureDefinitionSummary)_myClassMapping).TypeName;
            Definition = definition ?? throw Error.ArgumentNull(nameof(definition));

            ExceptionHandler = parent.ExceptionHandler;
            Location = location;
            ShortPath = shortPath;
        }

        private Type determineInstanceType(Type type, PropertyMapping definition)
        {
            if (definition.Choice != ChoiceType.None)
                return type;
            else
                return definition.FhirType[0];
        }

        public IElementDefinitionSummary Definition { get; private set; }

        public string ShortPath { get; private set; }

        public IEnumerable<ITypedElement> Children(string name)
        {
            if (!(Current is Base parentBase)) yield break;

            var children = parentBase.NamedChildren;
            string oldElementName = null;
            int arrayIndex = 0;

            foreach (var child in children)
            {
                if (name == null || child.ElementName == name)
                {
                    // Poll the actual implementation, which results in a more efficient loopkup when the underlying
                    // implementation of _mySD is ClassMapping.
                    var childElementDef = _myClassMapping.FindMappedElementByName(child.ElementName);

                    if (oldElementName != child.ElementName)
                        arrayIndex = 0;
                    else
                        arrayIndex += 1;

                    var location = Location == null
                        ? child.ElementName
                        : $"{Location}.{child.ElementName}[{arrayIndex}]";
                    var shortPath = ShortPath == null
                        ? child.ElementName
                        : (childElementDef.IsCollection ?
                            $"{ShortPath}.{child.ElementName}[{arrayIndex}]" :
                            $"{ShortPath}.{child.ElementName}");

                    yield return new PocoElementNode(child.Value, this, childElementDef,
                        location, shortPath);
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

        private object _value = null;
        private object _objectValue = null;

        public object Value
        {
            get
            {
                if (Current is PrimitiveType p && p.ObjectValue != null)
                {
                    if (p.ObjectValue != _objectValue)
                    {
                        _value = internalValue;
                        _objectValue = p.ObjectValue;
                    }

                    return _value;
                }
                else
                    return null;
            }
        }


        public object internalValue
        {
            get
            {
                try
                {
                    switch (Current)
                    {
                        case Hl7.Fhir.Model.Instant ins when ins.Value.HasValue:
                            return P.DateTime.FromDateTimeOffset(ins.Value.Value);
                        case Hl7.Fhir.Model.Time time when time.Value is { }:
                            return P.Time.Parse(time.Value);
                        case Hl7.Fhir.Model.Date dt when dt.Value is { }:
                            return P.Date.Parse(dt.Value);
                        case FhirDateTime fdt when fdt.Value is { }:
                            return P.DateTime.Parse(fdt.Value);
                        case Hl7.Fhir.Model.Integer fint:
                            if (!fint.Value.HasValue)
                                return null;
                            return (int)fint.Value;
                        case Hl7.Fhir.Model.PositiveInt pint:
                            if (!pint.Value.HasValue)
                                return null;
                            return (int)pint.Value;
                        case Hl7.Fhir.Model.UnsignedInt unsint:
                            if (!unsint.Value.HasValue)
                                return null;
                            return (int)unsint.Value;
                        case Hl7.Fhir.Model.Base64Binary b64:
                            return b64.Value != null ? PrimitiveTypeConverter.ConvertTo<string>(b64.Value) : null;
                        case PrimitiveType prim:
                            return prim.ObjectValue;
                        default:
                            return null;
                    }
                }
                catch (FormatException)
                {
                    // If it fails, just return the unparsed contents
                    return (Current as PrimitiveType)?.ObjectValue;
                }
            }
        }


        public string InstanceType { get; private set; }

        public string Location { get; private set; }

        public string ResourceType => Current is Resource ? InstanceType : null;

        public IEnumerable<object> Annotations(Type type)
        {
            if (type == typeof(PocoElementNode) || type == typeof(ITypedElement) || type == typeof(IShortPathGenerator))
                return new[] { this };
            else if (type == typeof(IFhirValueProvider))
                return new[] { this };
            else if (type == typeof(IResourceTypeSupplier))
                return new[] { this };
            else if (FhirValue is IAnnotated ia)
                return ia.Annotations(type);

            else
                return Enumerable.Empty<object>();
        }
    }
}