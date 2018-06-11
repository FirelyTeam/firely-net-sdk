/* 
 * Copyright (c) 2018, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
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
    public class StructureDefinitionMetadataProvider : IModelMetadataProvider
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

        public StructureDefinitionMetadataProvider(IResourceResolver resolver, TypeNameMapper mapper = null)
        {
            _resolver = resolver;
            _typeNameMapper = mapper ?? DefaultTypeNameMapper;
        }

        public IComplexTypeSerializationInfo GetSerializationInfoForStructure(string canonical)
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

    internal struct BackboneElementComplexTypeSerializationInfo : IComplexTypeSerializationInfo
    {
        private readonly ElementDefinitionNavigator _nav;

        public BackboneElementComplexTypeSerializationInfo(ElementDefinitionNavigator nav)
        {
            this._nav = nav;
        }

        public string TypeName => "BackboneElement";

        public bool IsAbstract => true;

        public IEnumerable<IElementSerializationInfo> GetChildren() => StructureDefinitionComplexTypeSerializationInfo.getChildren(_nav);
    }

    internal struct StructureDefinitionComplexTypeSerializationInfo : IComplexTypeSerializationInfo
    {
        private readonly ElementDefinitionNavigator _nav;

        public StructureDefinitionComplexTypeSerializationInfo(ElementDefinitionNavigator nav)
        {
            this._nav = nav;
        }

        public string TypeName => _nav.StructureDefinition.Name;

        public bool IsAbstract => _nav.StructureDefinition.Abstract ?? false;

        public IEnumerable<IElementSerializationInfo> GetChildren()
        {
            if (_nav.Current == null && !_nav.MoveToFirstChild())
                return Enumerable.Empty<IElementSerializationInfo>();

            return getChildren(_nav);
        }

        internal static IEnumerable<IElementSerializationInfo> getChildren(ElementDefinitionNavigator nav)
        {
            string lastName = "";

            var bookmark = nav.Bookmark();

            try
            {
                if (!nav.MoveToFirstChild()) yield break;

                do
                {
                    if (nav.PathName == lastName) continue;    // ignore slices
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

    internal struct TypeReferenceInfo : ITypeReference
    {
        private readonly string _referencedType;

        public TypeReferenceInfo(string referencedType)
        {
            _referencedType = referencedType;
        }

        public string TypeName => _referencedType;
    }


    internal struct ElementDefinitionSerializationInfo : IElementSerializationInfo
    {
        private readonly Lazy<ITypeSerializationInfo[]> _types;
        private readonly ElementDefinitionNavigator _nav;
        private readonly ElementDefinition _definition;

        internal ElementDefinitionSerializationInfo(ElementDefinitionNavigator nav)
        {
            if (nav == null || nav.Current == null) throw Error.ArgumentNull(nameof(nav));

            _types = new Lazy<ITypeSerializationInfo[]>(() => buildTypes(nav));
            this._nav = nav;
            ElementName = noChoiceSuffix(_nav.PathName);
            _definition = _nav.Current;

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
            else
                return nav.Current.Type.Select(t => (ITypeSerializationInfo)new TypeReferenceInfo(t.Code.GetLiteral())).ToArray();
        }

        public string ElementName { get; private set; }

        public bool MayRepeat => _definition.IsRepeating();

        public bool IsAtomicValue => _definition.Representation.Contains(ElementDefinition.PropertyRepresentation.XmlAttr);

        public bool IsChoiceElement => _definition.IsChoice();

        public bool IsContainedResource => isContainedResource(_definition);

        // TODO: This is actually not complete: the Type might be any subclass of Resource (including DomainResource), but this will
        // do for all current situations. I will regret doing this at some point in the future.
        private static bool isContainedResource(ElementDefinition defn) => defn.Type.Count == 1 && defn.Type[0].Code == FHIRDefinedType.Resource;

        public ITypeSerializationInfo[] Type => _types.Value;
    }
}
