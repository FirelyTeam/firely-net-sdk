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
            _myClassMapping = _inspector.FindOrImportClassMapping(root.GetType())!;

            InstanceType = ((IStructureDefinitionSummary)_myClassMapping).TypeName;
            Definition = ElementDefinitionSummary.ForRoot(_myClassMapping, rootName ?? root.TypeName);

            Location = InstanceType!;
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
            _myClassMapping = _inspector.FindOrImportClassMapping(instanceType)!;
            InstanceType = ((IStructureDefinitionSummary)_myClassMapping).TypeName;
            Definition = definition;

            ExceptionHandler = parent.ExceptionHandler;
            Location = location;
            ShortPath = shortPath;
        }

        private Type determineInstanceType(PropertyMapping definition)
        {
            if (!definition.IsPrimitive) return definition.PropertyTypeMapping.NativeType;
            throw new NotSupportedException(
                $"Encountered unexpected primitive type {Name} for PocoElementNode.InstanceType.");
        }

        public IElementDefinitionSummary Definition { get; }

        public string ShortPath { get; }

        /// <summary>
        /// Elements from the IReadOnlyDictionary can be of type <see cref="Base"/>, IEnumerable&lt;Base&gt; or string.
        /// This method uniformly returns a list of (Base, int) tuples, where the int is the index of the element in the IEnumerable.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        private IEnumerable<(Base val, int ix)> returnElements(string name, object element)
        {
            return (element, Current, name) switch
            {
                (Base @base, _, _)  => new[] { (@base, 0) },
                (IEnumerable<Base> bases, _, _)  => bases.Select((e, i) => (e, i)),
                _ => Enumerable.Empty<(Base, int)>(),
            };
        }

        /// <summary>
        /// Get the children from the IReadOnlyDictionary, and turn them into PocoElementNodes.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<ITypedElement> Children(string name)
        {
            if (Current is null) return Enumerable.Empty<PocoElementNode>();

            if (name is null)
            {
                return Current.EnumerateElements().SelectMany(kvp
                    => createChildNodes(kvp.Key, kvp.Value));
            }

            Current.TryGetValue(name, out var dictValue);
            return createChildNodes(name, dictValue);

            IEnumerable<PocoElementNode> createChildNodes(string childName, object value)
            {
                var elts = returnElements(childName, value);
                var elementDef = _myClassMapping.FindMappedElementByName(childName);
                //Create the list of child nodes differently based on:
                //- whether multiple children are allowed or not
                //- whether a parent location is known or not (i.e: root nodes have an empty location) 
                //I assume that Location and ShortPath are either both null or both not null - CK
                var input = (elementDef?.IsCollection ?? true, Location == null) switch
                {
                    (false, true) =>
                        elts.Select(c => new PocoElementNode(
                            location: $"{childName}[{c.ix}]",
                            shortPath: $"{childName}",
                            instance: c.val,
                            definition: elementDef,
                            inspector: _inspector,
                            parent: this
                        )),
                    (false, false) =>
                        elts.Select(c => new PocoElementNode(
                            location: $"{Location}.{childName}[{c.ix}]",
                            shortPath: $"{ShortPath}.{childName}",
                            instance: c.val,
                            definition: elementDef,
                            inspector: _inspector,
                            parent: this
                        )),
                    (true, true) =>
                        elts.Select(c => new PocoElementNode(
                            location: $"{childName}[{c.ix}]",
                            shortPath: $"{childName}[{c.ix}]",
                            instance: c.val,
                            definition: elementDef,
                            inspector: _inspector,
                            parent: this
                        )),
                    (true, false) =>
                        elts.Select(c => new PocoElementNode(
                            location: $"{Location}.{childName}[{c.ix}]",
                            shortPath: $"{ShortPath}.{childName}[{c.ix}]",
                            instance: c.val,
                            definition: elementDef,
                            inspector: _inspector,
                            parent: this
                        )),
                };
                return input;
            }
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
                if (Current is not PrimitiveType { JsonValue: not null } p) return null;

                if (p.JsonValue == _lastCachedValue) return _value;

                _value = ToITypedElementValue();
                _lastCachedValue = p.JsonValue;
                return _value;
            }
        }

        internal object ToITypedElementValue()
        {
            try
            {
                return Current switch
                {
                    Instant { Value: { } ins } => P.DateTime.FromDateTimeOffset(ins),
                    Time { Value: { } time } => P.Time.Parse(time),
                    Date { Value: { } dt } => P.Date.Parse(dt),
                    FhirDateTime { Value: { } fdt } => P.DateTime.Parse(fdt),
                    Integer fint => fint.Value,
                    Integer64 fint64 => fint64.Value,
                    PositiveInt pint => pint.Value,
                    UnsignedInt unsint => unsint.Value,
                    Base64Binary { JsonValue: { } b64 } => b64,
                    PrimitiveType prim => prim.JsonValue,
                    _ => null
                };
            }
            catch (FormatException)
            {
                // If it fails, just return the unparsed contents
                return (Current as PrimitiveType)?.JsonValue;
            }
        }


        public string InstanceType { get; }

        public string Location { get; }

        public string ResourceType => Current is Resource ? InstanceType : null;

        public IEnumerable<object> Annotations(Type type)
        {
            if (type == typeof(PocoElementNode) || type == typeof(ITypedElement) || type == typeof(IShortPathGenerator))
                return [this];
            else if (type == typeof(IFhirValueProvider))
                return [this];
            else if (type == typeof(IResourceTypeSupplier))
                return [this];
            else if (FhirValue is IAnnotated ia)
                return ia.Annotations(type);
            else
                return [];
        }
    }
}