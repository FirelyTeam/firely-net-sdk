/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification
{
    public class StructureDefinitionSummaryProvider : IStructureDefinitionSummaryProvider
    {
        public delegate bool TypeNameMapper(string typeName, out string canonical);

        private readonly IResourceResolver _resolver;
        private readonly TypeNameMapper _typeNameMapper;

        public static bool DefaultTypeNameMapper(string name, out string canonical)
        {
            var typeName = ModelInfo.IsProfiledQuantity(name) ? "Quantity" : name;

            canonical = ResourceIdentity.CORE_BASE_URL + typeName;
            return true;
        }

        public StructureDefinitionSummaryProvider(IResourceResolver resolver, TypeNameMapper mapper = null)
        {
            _resolver = resolver;
            _typeNameMapper = mapper ?? DefaultTypeNameMapper;
        }

        public IStructureDefinitionSummary Provide(string canonical)
        {
            var isLocalType = !canonical.Contains("/");
            string mappedCanonical = canonical;

            if (isLocalType)
            {
                var mapSuccess = _typeNameMapper(canonical, out mappedCanonical);
                if (!mapSuccess) return null;
            }

            var sd = _resolver.FindStructureDefinition(mappedCanonical, requireSnapshot: true);
            if (sd == null) return null;

            return new StructureDefinitionComplexTypeSerializationInfo(ElementDefinitionNavigator.ForSnapshot(sd));
        }
    }

    internal struct BackboneElementComplexTypeSerializationInfo : IStructureDefinitionSummary
    {
        private readonly ElementDefinitionNavigator _nav;

        public BackboneElementComplexTypeSerializationInfo(ElementDefinitionNavigator nav)
        {
            this._nav = nav;
        }

        public string TypeName => _nav.Current.Type[0].Code;

        public bool IsAbstract => true;

        public bool IsResource => false;

        public IEnumerable<IElementDefinitionSummary> GetElements() => StructureDefinitionComplexTypeSerializationInfo.getElements(_nav);
    }

    internal struct StructureDefinitionComplexTypeSerializationInfo : IStructureDefinitionSummary
    {
        private readonly ElementDefinitionNavigator _nav;

        public StructureDefinitionComplexTypeSerializationInfo(ElementDefinitionNavigator nav)
        {
            this._nav = nav;
        }

        public string TypeName => _nav.StructureDefinition.Name;

        public bool IsAbstract => _nav.StructureDefinition.Abstract ?? false;

        public bool IsResource => _nav.StructureDefinition.Kind == StructureDefinition.StructureDefinitionKind.Resource;

        public IEnumerable<IElementDefinitionSummary> GetElements()
        {
            if (_nav.Current == null && !_nav.MoveToFirstChild())
                return Enumerable.Empty<IElementDefinitionSummary>();

            return getElements(_nav);
        }

        private static bool isPrimitiveValueConstraint(ElementDefinition ed) => ed.Path.EndsWith(".value") && ed.Type.All(t => t.Code == null);

        internal static IEnumerable<IElementDefinitionSummary> getElements(ElementDefinitionNavigator nav)
        {
            string lastName = "";

            var bookmark = nav.Bookmark();

            try
            {
                if (!nav.MoveToFirstChild()) yield break;

                do
                {
                    if (nav.PathName == lastName) continue;    // ignore slices
                    if (isPrimitiveValueConstraint(nav.Current)) continue;      // ignore value attribute
                            
                    lastName = nav.PathName;
                    yield return new ElementDefinitionSerializationInfo(nav.ShallowCopy());
                }
                while (nav.MoveToNext());
            }
            finally
            {
                nav.ReturnToBookmark(bookmark);
            }
        }
    }

    internal struct TypeReferenceInfo : IStructureDefinitionReference
    {
        private readonly string _referencedType;

        public TypeReferenceInfo(string referencedType)
        {
            _referencedType = referencedType;
        }

        public string ReferredType => _referencedType;
    }


    internal struct ElementDefinitionSerializationInfo : IElementDefinitionSummary
    {
        private readonly Lazy<ITypeSerializationInfo[]> _types;
        private readonly ElementDefinition _definition;

        internal ElementDefinitionSerializationInfo(ElementDefinitionNavigator nav)
        {
            if (nav == null || nav.Current == null) throw Error.ArgumentNull(nameof(nav));

            _types = new Lazy<ITypeSerializationInfo[]>(() => buildTypes(nav));
            ElementName = noChoiceSuffix(nav.PathName);
            _definition = nav.Current;
            Order = nav.OrdinalPosition.Value;     // cannot be null, since nav.Current != null

            string noChoiceSuffix(string n)
            {
                if (n.EndsWith("[x]"))
                    return n.Substring(0, n.Length - 3);
                else
                    return n;
            }
        }

        private static ITypeSerializationInfo[] buildTypes(ElementDefinitionNavigator nav)
        {
            if (nav.Current.IsBackboneElement())
                return new[] { (ITypeSerializationInfo)new BackboneElementComplexTypeSerializationInfo(nav) };
            else if (nav.Current.ContentReference != null)
            {
                var reference = nav.ShallowCopy();
                var name = nav.Current.ContentReference;
                if (!reference.JumpToNameReference(name))
                    throw Error.InvalidOperation($"StructureDefinition '{nav?.StructureDefinition?.Url}' " +
                        $"has a namereference '{name}' on element '{nav.Current.Path}' that cannot be resolved.");

                return new[] { (ITypeSerializationInfo)new BackboneElementComplexTypeSerializationInfo(reference) };
            }
            else
                return nav.Current.Type.Select(t => (ITypeSerializationInfo)new TypeReferenceInfo(t.Code)).Distinct().ToArray();
        }

        public string ElementName { get; private set; }

        public bool IsCollection => _definition.IsRepeating();

        public bool InSummary => _definition.IsSummary ?? false;

        public bool IsRequired => (_definition.Min ?? 0) > 1;

        public XmlRepresentation Representation
        {
            get {
                if (!_definition.Representation.Any()) return XmlRepresentation.XmlElement;

                switch (_definition.Representation.First())
                {
                    case ElementDefinition.PropertyRepresentation.XmlAttr:
                        return XmlRepresentation.XmlAttr;
                    default:
                        return XmlRepresentation.XmlElement;
                }
            }
        }

        public bool IsChoiceElement => _definition.IsChoice();

        public int Order { get; private set; }

        public bool IsResource => isResource(_definition);

        // TODO: This is actually not complete: the Type might be any subclass of Resource (including DomainResource), but this will
        // do for all current situations. I will regret doing this at some point in the future.
        private static bool isResource(ElementDefinition defn) => defn.Type.Count == 1 && 
            (defn.Type[0].Code == "Resource" || defn.Type[0].Code == "DomainResource");

        public ITypeSerializationInfo[] Type => _types.Value;

        public string NonDefaultNamespace =>
            _definition.GetExtensionValue<FhirUri>("http://hl7.org/fhir/StructureDefinition/elementdefinition-namespace")?.Value;

    }
}
