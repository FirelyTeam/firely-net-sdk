/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Hl7.Fhir.Validation
{
    internal class Scope
    {
        public string Uri;

        public IElementNavigator Container { get; private set; }
        public string Path { get; private set; }

        public bool IsBundle { get; private set; }

        public Scope(IElementNavigator container, string uri)
        {
            Container = container;
            Path = container.Path;
            IsBundle = ModelInfo.FhirTypeNameToFhirType(container.TypeName) == FHIRDefinedType.Bundle;
        }
    }


    internal class ScopeList : List<Scope>
    {
        public void Append(IElementNavigator container)
        {
            if(this.Any())
            {
                // If this was not the first scope to be added, we should normally find the container already present in the list of scopes, since
                // when adding a parent container, we add the children to the list of scopes as well
                var scope = this.SingleOrDefault(s => s.Path == container.Path);
                if(scope == null)
                    throw Error.Argument("Tried to append an orphan scope with path '{0}'".FormatWith(container.Path));
            }

            AddRange(ReferenceHarvester.Harvest(container));
        }

        public void Remove(string path)
        {
            var scopeToRemove = Find(path);
            if (scopeToRemove == null)
                throw Error.Argument("There is no known scope for path '{0}'".FormatWith(path));

            Remove(scopeToRemove);
        }

        public Scope FindParent(string path)
        {
            return this.Where(s => path.StartsWith(s.Path + ".")).OrderBy(s => s.Path.Length).Reverse().FirstOrDefault();
        }

        public Scope FindParent(Scope child)
        {
            return FindParent(child.Path);
        }

        public Scope Find(string path)
        {
            return this.SingleOrDefault(s => s.Path == path);
        }
        public Scope NearestResource(string path)
        {
            var result = Find(path);

            if (result == null)
                result = FindParent(path);

            return result;
        }

        public Scope NearestBundle(string path)
        {
            var result = NearestResource(path);

            while (result != null && !result.IsBundle)
                result = FindParent(result);

            return result;
        }

        public IdentifiedChildResource Resolve(string uri)
        {
            throw new NotImplementedException();
        }
    }


    internal class ScopeTracker
    {
        private ScopeList _scopes = new ScopeList();

        public void Push(IElementNavigator instance)
        {
            if (isContainer(instance))
            {
                var newScope = new Scope(instance.Clone(), ReferenceHarvester.Harvest(instance));
                _scopes.Append(newScope);
            }
        }

        public void Pop(IElementNavigator instance)
        {
            _scopes.Remove(instance.Path);
        }

        private static bool isContainer(IElementNavigator instance)
        {
            return ModelInfo.FhirTypeNameToFhirType(instance.TypeName) == FHIRDefinedType.Bundle ||
                                    ModelInfo.IsKnownResource(instance.TypeName);
        }

        public IElementNavigator ResourceContext(IElementNavigator instance)
        {
            var container = _scopes.NearestResource(instance.Path);
            return container != null ? container.Container : null;
        }

        public IElementNavigator FindTarget(IElementNavigator me, string uri)
        {
            var parent = Parent()
        }
    }

    internal class ReferenceHarvester
    {
        private static string getStringValue(IElementNavigator instance)
        {
            if (instance == null) return null;
            if (instance.Value == null) return null;

            return instance.Value as string;
        }


        public static IEnumerable<Scope> Harvest(IElementNavigator instance)
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
                return Enumerable.Empty<Scope>();
        }

        public static IEnumerable<Scope> HarvestResource(IElementNavigator instance)
        {
            return instance.GetChildrenByName("contained")
                    .Select(child =>
                        new Scope(child.Clone(),
                            getStringValue(child.GetChildrenByName("id").FirstOrDefault())));                            
        }

        public static IEnumerable<Scope> HarvestBundle(IElementNavigator instance)
        {
            return instance.GetChildrenByName("entry")
                    .SelectMany(entry =>
                        entry.GetChildrenByName("resource").Take(1)
                            .Select(res =>
                                new Scope(res.Clone(),
                                    getStringValue(entry.GetChildrenByName("fullUrl").FirstOrDefault()))));
        }
    }
}
