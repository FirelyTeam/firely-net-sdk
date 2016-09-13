/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Validation
{
    internal class ScopeTracker
    {
        private class Scope
        {
            public Scope Parent;
            public string Path;
            public List<ScopedReference> References;
            public IElementNavigator Container;

            public ScopedReference Locate(string uri)
            {
            }

            public void Append(Scope child)
            {
                if (!child.Path.StartsWith(Path))
                    throw Error.Argument("Instance with path '{0}' is not a child scope for path '{1}'".FormatWith(child.Path, Path));

                if (child.Path == Path)
                    throw Error.Argument("There is already a scope for path '{0}'".FormatWith(child.Path));

                // Scope is a true child
                child.Parent = this;
            }
        }

        private Scope _root = null;

        public void Push(IElementNavigator instance)
        {
            var isContainer = ModelInfo.FhirTypeNameToFhirType(instance.TypeName) == FHIRDefinedType.Bundle ||
                                    ModelInfo.IsKnownResource(instance.TypeName);

            var newScope = new Scope { Path = instance.Path };

            if (isContainer)
            {
                var idList = ReferenceHarvester.Harvest(instance);
                newScope.References = idList;
                newScope.Container = instance.Clone();
            }

            if (_root == null)
                _root = newScope;
            else
                _root.Append(newScope);
        }

        public void Pop(IElementNavigator instance)
        {
            var isBundle = ModelInfo.FhirTypeNameToFhirType(instance.TypeName) == FHIRDefinedType.Bundle);
            var isResource = ModelInfo.IsKnownResource(instance.TypeName);

            if (isBundle || isResource)
            {
                var path = instance.Path;
                var scopeToRemove = _scopes.SingleOrDefault(s => s.Path == path);
                if (scopeToRemove == null)
                    throw Error.Argument("There is no known scope for path '{0}'".FormatWith(path));

                _scopes.Remove(scopeToRemove);
            }
        }


        public Scope FindScopeFor(IElementNavigator instance)
        {
                        
        }
    }

    internal struct ScopedReference
    {
        public ReferenceKind Kind;
        public string Reference;
        public IElementNavigator Instance;
    }

    internal class ReferenceHarvester
    {
        private static string getStringValue(IElementNavigator instance)
        {
            if (instance == null) return null;
            if (instance.Value == null) return null;

            return instance.Value as string;
        }


        public static List<ScopedReference> Harvest(IElementNavigator instance)
        {
            var scanner = instance.Clone();

            if (ModelInfo.FhirTypeNameToFhirType(scanner.TypeName) == FHIRDefinedType.Bundle)
            {
                return HarvestBundle(scanner);
            }
            else if (ModelInfo.IsKnownResource(instance.TypeName))
            {
                return HarvestResource(instance);
            }
            else
                return new List<ScopedReference>();
        }

        public static List<ScopedReference> HarvestResource(IElementNavigator instance)
        {
            return instance.GetChildrenByName("contained")
                    .Select(child =>
                        new ScopedReference
                        {
                            Instance = child.Clone(),
                            Reference = getStringValue(child.GetChildrenByName("id").FirstOrDefault()),
                            Kind = ReferenceKind.Contained
                        })
                    .ToList();
        }

        public static List<ScopedReference> HarvestBundle(IElementNavigator scanner)
        {
            return scanner.GetChildrenByName("entry")
                    .SelectMany(entry =>
                        entry.GetChildrenByName("resource").Take(1)
                            .Select(res =>
                                new ScopedReference
                                {
                                    Instance = res.Clone(),
                                    Reference = getStringValue(entry.GetChildrenByName("fullUrl").FirstOrDefault()),
                                    Kind = ReferenceKind.Bundled
                                }))
                    .ToList();
        }
    }
}
