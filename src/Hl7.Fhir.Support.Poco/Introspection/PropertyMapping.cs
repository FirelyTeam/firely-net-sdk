/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading;

#nullable enable


namespace Hl7.Fhir.Introspection
{
    /// <summary>
    /// A container for the metadata of an element of a FHIR datatype as present on a property of a (generated) .NET POCO class.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay(@"\{Name={Name} ElementType={ElementType.Name}}")]
    public class PropertyMapping : IElementDefinitionSummary
    {
        // no public constructors
        private PropertyMapping(
            string name,
            ClassMapping declaringClass,
            PropertyInfo pi,
            Type implementingType,
            ClassMapping propertyTypeMapping,
            Type[] fhirTypes,
            FhirRelease version)
        {
            Name = name;
            NativeProperty = pi;
            Release = version;
            ImplementingType = implementingType;
            FhirType = fhirTypes;
            PropertyTypeMapping = propertyTypeMapping;
            DeclaringClass = declaringClass;
            FiveWs = string.Empty;
#if NET452
            ValidationAttributes = new ValidationAttribute[0];
#else
            ValidationAttributes = Array.Empty<ValidationAttribute>();
#endif
        }

        /// <summary>
        /// The name of the element in the FHIR specification.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// The ClassMapping for the type this property is a member of.
        /// </summary>
        public ClassMapping DeclaringClass { get; internal set; }

        /// <summary>
        /// Whether the element can repeat.
        /// </summary>
        public bool IsCollection { get; internal set; }

        /// <summary>
        /// The element is of an atomic .NET type, not a FHIR generated POCO.
        /// </summary>
        public bool IsPrimitive { get; private set; }

        /// <summary>
        /// The element is a primitive (<seealso cref="IsPrimitive"/>) and 
        /// represents the primitive `value` attribute/property in the FHIR serialization.
        /// </summary>
        public bool RepresentsValueElement { get; private set; }

        /// <summary>
        /// Whether the element appears in _summary 
        /// (see https://www.hl7.org/fhir/search.html#summary)
        /// </summary>
        public bool InSummary { get; private set; }

        /// <summary>
        /// If this modifies the meaning of other elements
        /// (see https://www.hl7.org/fhir/conformance-rules.html#isModifier)
        /// </summary>
        public bool IsModifier { get; private set; }

        /// <summary>
        /// Five W's mappings of the element.
        /// <remarks>it represents the exact element name of one the elements of the 
        /// <c>FiveWs</c> pattern from http://hl7.org/fhir/fivews.html. Choice elements are spelled with the
        /// [x] suffix, like <c>done[x]</c>. </remarks>
        /// </summary>
        public string FiveWs { get; private set; }

        /// <summary>
        /// Whether the element has a cardinality higher than 0.
        /// </summary>
        public bool IsMandatoryElement { get; private set; }

        /// <summary>
        /// The native type of the element.
        /// </summary>
        /// <remarks>If the element is a collection or is nullable, this reflects the
        /// collection item or the type that is made nullable respectively.
        /// </remarks>
        public Type ImplementingType { get; private set; }

        /// <summary>
        /// The native type of the element.
        /// </summary>
        [Obsolete("This element had a different name in R3 and R4. Please use ImplementingType from now on.")]
        public Type ElementType
        {
            get => ImplementingType;
            set => ImplementingType = value;
        }

        /// <summary>
        /// The numeric order of the element (relevant for the XML serialization, which
        /// needs to be in order).
        /// </summary>
        public int Order { get; private set; }

        /// <summary>
        /// How this element is represented in the XML serialization.
        /// </summary>
        public XmlRepresentation SerializationHint { get; private set; }

        /// <summary>
        /// Specifies whether this element contains a choice (either a choice element or a
        /// contained resource).
        /// </summary>
        /// <remarks>In the case of a DataChoice, these elements have names ending in [x] in 
        /// the StructureDefinition and allow a (possibly restricted) set of types to be used. 
        /// These are reflected in the <see cref="FhirType"/> property.</remarks>
        public ChoiceType Choice { get; private set; }

        /// <summary>
        /// This element is a polymorphic Resource, any resource is allowed here.
        /// </summary>
        /// <remarks>These are elements like DomainResource.contained, Parameters.resource etc.</remarks>
        [Obsolete("This property is never initialized and its value will always be false.")]
        public bool IsResourceChoice { get; private set; }

        /// <summary>
        /// The list of possible FHIR types for this element, listed as the representative .NET types. 
        /// For non-choice types this is a single Type, for choices this is either a list of Types or 
        /// just <see cref="Hl7.Fhir.Model.DataType"/>.
        /// </summary>
        /// <remark>These are the defined (choice) types for this element as specified in the
        /// FHIR data definitions. It is derived from the actual property type,
        /// or, if present, via a list of types in the [AllowedTypes] attribute. Finally,
        /// it the property type does not represent FHIR metadata, it is overridden using
        /// the [DeclaredType] attribute.
        /// </remark>
        public Type[] FhirType { get; private set; }

        /// <summary>
        /// The <see cref="ClassMapping" /> that represents the type of this property.
        /// </summary>
        /// <remarks>This is effectively the ClassMapping for the <see cref="ImplementingType" /> unless a
        /// <see cref="DeclaredTypeAttribute" /> specifies otherwise.</remarks>
        public ClassMapping PropertyTypeMapping { get; private set; }

        /// <summary>
        /// The collection of zero or more <see cref="ValidationAttribute"/> (or subclasses) declared
        /// on this property.
        /// </summary>
        public ValidationAttribute[] ValidationAttributes { get; private set; } =
#if NET452
            new ValidationAttribute[0];
#else
            Array.Empty<ValidationAttribute>();
#endif


        /// <summary>
        /// The original <see cref="PropertyInfo"/> the metadata was obtained from.
        /// </summary>
        public readonly PropertyInfo NativeProperty;

        /// <summary>
        /// The release of FHIR for which the metadata was extracted from the property.
        /// </summary>
        public readonly FhirRelease Release;

        [Obsolete("Use TryCreate() instead.")]
        public static PropertyMapping? Create(PropertyInfo prop, ClassMapping declaringClass, FhirRelease version = (FhirRelease)int.MaxValue)
            => TryCreate(prop, out var mapping, declaringClass, version) ? mapping : null;

        /// <summary>
        /// Inspects the given PropertyInfo, extracting metadata from its attributes and creating a new <see cref="PropertyMapping"/>.
        /// </summary>
        /// <remarks>There should generally be no reason to call this method, as you can easily get the required PropertyMapping via
        /// a ClassMapping - which will cache this information as well. This constructor is public for historical reasons only.</remarks>
        public static bool TryCreate(PropertyInfo prop, out PropertyMapping? result, ClassMapping declaringClass, FhirRelease release)
        {
            if (prop == null) throw Error.ArgumentNull(nameof(prop));
            result = default;

            // If there is no [FhirElement] on the property, skip it
            var elementAttr = ClassMapping.GetAttribute<FhirElementAttribute>(prop, release);
            if (elementAttr == null) return false;

            // If there is an explicit [NotMapped] on the property, skip it
            // (in combination with `Since` useful to remove a property from the serialization)
            var notmappedAttr = ClassMapping.GetAttribute<NotMappedAttribute>(prop, release);
            if (notmappedAttr != null) return false;

            // We broadly use .IsArray here - this means arrays in POCOs cannot be used to represent
            // FHIR repeating elements. If we would allow this, we'd also have stuff like `string` and binary
            // data as repeating element, and would need to exclude these exceptions on a case by case basis.
            // This is pretty ugly, so we prefer to not support arrays - you should use lists instead.
            bool isCollection = ReflectionHelper.IsTypedCollection(prop.PropertyType) && !prop.PropertyType.IsArray;

            var cardinalityAttr = ClassMapping.GetAttribute<CardinalityAttribute>(prop, release);

            // Get to the actual (native) type representing this element
            var implementingType = prop.PropertyType;
            if (isCollection) implementingType = ReflectionHelper.GetCollectionItemType(prop.PropertyType);
            if (ReflectionHelper.IsNullableType(implementingType)) implementingType = ReflectionHelper.GetNullableArgument(implementingType);

            // Determine the .NET type that represents the FHIR type for this element.
            // This is normally just the ImplementingType itself, but can be overridden
            // with the [DeclaredType] attribute.
            var declaredType = ClassMapping.GetAttribute<DeclaredTypeAttribute>(prop, release);
            var fhirType = declaredType?.Type ??
                (typeof(Enum).GetTypeInfo().IsAssignableFrom(implementingType) ? typeof(Enum) : implementingType);

            if (!ClassMapping.TryGetMappingForType(fhirType, release, out var propertyTypeMapping))
                throw new InvalidOperationException($"Property {prop.Name} in class {prop.DeclaringType!.Name} is of type " +
                    $"{fhirType}, for which a classmapping cannot be found.");

            // The [AllowedElements] attribute can specify a set of allowed types
            // for this element. Take this list as the declared list of FHIR types.
            // If not present assume this is the implementing FHIR type above
            var allowedTypes = ClassMapping.GetAttribute<AllowedTypesAttribute>(prop, release);

            var fhirTypes = allowedTypes?.Types?.Any() == true ?
                allowedTypes.Types : new[] { fhirType };

            var isPrimitive = isAllowedNativeTypeForDataTypeValue(implementingType);

            result = new PropertyMapping(elementAttr.Name, declaringClass, prop, implementingType, propertyTypeMapping!, fhirTypes, release)
            {
                InSummary = elementAttr.InSummary,
                IsModifier = elementAttr.IsModifier,
                Choice = elementAttr.Choice,
                SerializationHint = elementAttr.XmlSerialization,
                Order = elementAttr.Order,
                IsCollection = isCollection,
                IsMandatoryElement = cardinalityAttr?.Min > 0,
                IsPrimitive = isPrimitive,
                RepresentsValueElement = isPrimitive && isPrimitiveValueElement(elementAttr, prop),
                ValidationAttributes = ClassMapping.GetAttributes<ValidationAttribute>(prop, release).ToArray(),
                FiveWs = elementAttr.FiveWs
            };

            return true;
        }

        private static bool isPrimitiveValueElement(FhirElementAttribute valueElementAttr, PropertyInfo prop)
        {
            var isValueElement = valueElementAttr != null && valueElementAttr.IsPrimitiveValue;

            return !isValueElement || isAllowedNativeTypeForDataTypeValue(prop.PropertyType)
                ? isValueElement
                : throw Error.Argument(nameof(prop), "Property {0} is marked for use as a primitive element value, but its .NET type ({1}) " +
                    "is not supported by the serializer.".FormatWith(buildQualifiedPropName(prop), prop.PropertyType.Name));
        }

        private static string buildQualifiedPropName(PropertyInfo p)
            => $"{p.DeclaringType?.Name ?? throw Error.ArgumentNull(nameof(p.DeclaringType))}.{p.Name}";

        private static bool isAllowedNativeTypeForDataTypeValue(Type type)
        {
            // Special case, allow Nullable<enum>
            if (ReflectionHelper.IsNullableType(type))
                type = ReflectionHelper.GetNullableArgument(type);

            return type.IsEnum() || ClassMapping.SupportedDotNetPrimitiveTypes.Contains(type);
        }

        /// <summary>
        /// Given an instance of the parent class, gets the value for this property.
        /// </summary>
        public object? GetValue(object instance) => LazyInitializer.EnsureInitialized(ref _getter, NativeProperty.GetValueGetter)!(instance);

        private Func<object, object?>? _getter;

        /// <summary>
        /// Given an instance of the parent class, sets the value for this property.
        /// </summary>
        public void SetValue(object instance, object? value) =>
            LazyInitializer.EnsureInitialized(ref _setter, NativeProperty.GetValueSetter)!(instance, value);

        private Action<object, object?>? _setter;

        #region IElementDefinitionSummary members
        string IElementDefinitionSummary.ElementName => this.Name;

        bool IElementDefinitionSummary.IsCollection => this.IsCollection;

        bool IElementDefinitionSummary.IsRequired => this.IsMandatoryElement;

        bool IElementDefinitionSummary.InSummary => this.InSummary;

        /// <inheritdoc/>
        bool IElementDefinitionSummary.IsModifier => this.IsModifier;

        bool IElementDefinitionSummary.IsChoiceElement => this.Choice == ChoiceType.DatatypeChoice;

        bool IElementDefinitionSummary.IsResource => this.Choice == ChoiceType.ResourceChoice;

        string? IElementDefinitionSummary.DefaultTypeName => null;

        ITypeSerializationInfo[] IElementDefinitionSummary.Type
        {
            get
            {
                LazyInitializer.EnsureInitialized(ref _types, buildTypes);
                return _types!;
            }
        }

        private ITypeSerializationInfo[]? _types;

        string? IElementDefinitionSummary.NonDefaultNamespace => null;

        XmlRepresentation IElementDefinitionSummary.Representation =>
            SerializationHint != XmlRepresentation.None ?
            SerializationHint : XmlRepresentation.XmlElement;

        int IElementDefinitionSummary.Order => Order;

        private ITypeSerializationInfo[] buildTypes()
        {
            var elementTypeMapping = PropertyTypeMapping;

            if (elementTypeMapping!.IsNestedType)
            {
                var info = elementTypeMapping;
                return new ITypeSerializationInfo[] { info };
            }
            else if (IsPrimitive)
            {
                // Backwards compat hack: the primitives (since .value is never queried, this
                // means Element.id, Narrative.div and Extension.url) should be returned as FHIR type names, not
                // system (CQL) type names.
                var bwcompatType = Name switch
                {
                    "url" => "uri",
                    "id" => "string",
                    "div" => "xhtml",
                    _ => throw new NotSupportedException($"Encountered unexpected primitive type {Name} in backward compat behaviour for ITypedElement.InstanceType.")
                };

                return new[] { (ITypeSerializationInfo)new PocoTypeReferenceInfo(bwcompatType) };
            }
            else
            {
                var names = FhirType.Select(ft => getFhirTypeName(ft));
                return names.Select(n => (ITypeSerializationInfo)new PocoTypeReferenceInfo(n)).ToArray();
            }

            string getFhirTypeName(Type ft)
            {
                // The special case where the mapping name is a backbone element name can safely
                // be ignored here, since that is handled by the first case in the if statement above.
                return ClassMapping.TryGetMappingForType(ft, Release, out var tm)
                    ? ((IStructureDefinitionSummary)tm!).TypeName
                    : throw new NotSupportedException($"Type '{ft.Name}' is listed as an allowed type for property " +
                        $"'{buildQualifiedPropName(NativeProperty)}', but it does not seem to" +
                        $"be a valid FHIR type POCO.");
            }
        }

        private struct PocoTypeReferenceInfo : IStructureDefinitionReference
        {
            public PocoTypeReferenceInfo(string canonical)
            {
                ReferredType = canonical;
            }

            public string ReferredType { get; private set; }
        }

        #endregion
    }
}

#nullable restore