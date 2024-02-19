/*
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.ElementModel
{
    internal class PocoElementNode : ITypedElement, IAnnotated, IExceptionSource, IShortPathGenerator,
        IFhirValueProvider, IResourceTypeSupplier
    {
        public readonly Base Current;
        private readonly ClassMapping _myClassMapping;
        private readonly ModelInspector _inspector;

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        internal PocoElementNode(ModelInspector inspector, Base root, string rootName = null)
        {
            Current = root;
            _inspector = inspector;
            _myClassMapping = _inspector.FindOrImportClassMapping(root.GetType());

            InstanceType = ((IStructureDefinitionSummary)_myClassMapping).TypeName;
            Definition = ElementDefinitionSummary.ForRoot(_myClassMapping, rootName ?? root.TypeName);

            Location = InstanceType;
            ShortPath = InstanceType;
        }

        private PocoElementNode(ModelInspector inspector, Base instance, PocoElementNode parent,
            PropertyMapping definition, string location, string shortPath)
        {
            Current = instance;
            _inspector = inspector;

            var instanceType = definition.Choice != ChoiceType.None
                ? instance.GetType()
                : determineInstanceType(definition);
            _myClassMapping = _inspector.FindOrImportClassMapping(instanceType);
            InstanceType = ((IStructureDefinitionSummary)_myClassMapping).TypeName;
            Definition = definition ?? throw Error.ArgumentNull(nameof(definition));

            ExceptionHandler = parent.ExceptionHandler;
            Location = location;
            ShortPath = shortPath;
        }

        private Type determineInstanceType(PropertyMapping definition)
        {
            if (!definition.IsPrimitive) return definition.PropertyTypeMapping.NativeType;

            // Backwards compat hack: the primitives (since .value is never queried, this
            // means Element.id, Narrative.div and Extension.url) should be returned as FHIR types, not
            // system (CQL) type.
            return definition.Name switch
            {
                "url" => typeof(FhirUri),
                "id" => typeof(FhirString),
                "div" => typeof(XHtml),
                _ => throw new NotSupportedException(
                    $"Encountered unexpected primitive type {Name} in backward compat behaviour for PocoElementNode.InstanceType.")
            };
        }

        public IElementDefinitionSummary Definition { get; private set; }

        public string ShortPath { get; private set; }

        private static (ElementValue val, int ix)[] returnElements(string name, object element)
        {
            return element switch
            {
                Base @base => new[] { (new ElementValue(name, @base), 0) },
                IEnumerable<Base> bases => bases.Select((e, i) => (new ElementValue(name, e), i)).ToArray(),
                _ => Array.Empty<(ElementValue, int)>(),
                // _ => throw new ArgumentException($"element '{location}.{name}' is of type '{element.GetType().FullName}' not a Base or IEnumerable<Base>", nameof(element))
            };
        }

        public IEnumerable<ITypedElement> Children(string name)
        {
            if (Current is null) return Enumerable.Empty<PocoElementNode>();

            IEnumerable<PocoElementNode> getChildrenInternal(string childName, object value)
            {
                var elts = returnElements(childName, value);
                var elementDef = _myClassMapping.FindMappedElementByName(childName);
                //In the switch below I assume that Location and ShortPath are either both null or both not null - CK
                var input = (elementDef?.IsCollection ?? true, Location == null) switch
                {
                    (false, true) =>
                        elts.Select(c => new PocoElementNode(
                            location: $"{childName}[{c.ix}]",
                            shortPath: $"{childName}",
                            instance: c.val.Value,
                            definition: elementDef,
                            inspector: _inspector,
                            parent: this
                        )),
                    (false, false) =>
                        elts.Select(c => new PocoElementNode(
                            location: $"{Location}.{childName}[{c.ix}]",
                            shortPath: $"{ShortPath}.{childName}",
                            instance: c.val.Value,
                            definition: elementDef,
                            inspector: _inspector,
                            parent: this
                        )),
                    (true, true) =>
                        elts.Select(c => new PocoElementNode(
                            location: $"{childName}[{c.ix}]",
                            shortPath: $"{childName}[{c.ix}]",
                            instance: c.val.Value,
                            definition: elementDef,
                            inspector: _inspector,
                            parent: this
                        )),
                    (true, false) =>
                        elts.Select(c => new PocoElementNode(
                            location: $"{Location}.{childName}[{c.ix}]",
                            shortPath: $"{ShortPath}.{childName}[{c.ix}]",
                            instance: c.val.Value,
                            definition: elementDef,
                            inspector: _inspector,
                            parent: this
                        )),
                };
                return input;
            }

            var rod = Current.AsReadOnlyDictionary();


            if (name is null)
            {
                return rod.SelectMany(kvp
                    => getChildrenInternal(kvp.Key, kvp.Value));
            }

            rod.TryGetValue(name, out var value);
            return getChildrenInternal(name, value);
        }

        /// <summary>
        /// This is only needed for search data extraction (and debugging)
        /// to be able to read the values from the selected node (if a coding, so can get the value and system)
        /// </summary>
        /// <remarks>Will return null if on id, value, url (primitive attribute props in xml)</remarks>
        public Base FhirValue => Current;

        public string Name => Definition.ElementName;

        private object _value = null;
        private object _lastCachedValue = null;

        public object Value
        {
            get
            {
                if (Current is PrimitiveType p && p.ObjectValue != null)
                {
                    if (p.ObjectValue != _lastCachedValue)
                    {
                        _value = ToITypedElementValue();
                        _lastCachedValue = p.ObjectValue;
                    }

                    return _value;
                }
                else
                    return null;
            }
        }

        internal object ToITypedElementValue()
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
                    case Hl7.Fhir.Model.Integer64 fint64:
                        if (!fint64.Value.HasValue)
                            return null;
                        return (long)fint64.Value;
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