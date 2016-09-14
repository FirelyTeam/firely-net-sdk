/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
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

        private Lazy<List<Scope>> _children;

        public List<Scope> Children { get { return _children.Value; } }

        public Scope(IElementNavigator container, string uri)
        {
            Container = container.Clone();
            Path = container.Path;
            IsBundle = ModelInfo.FhirTypeNameToFhirType(container.TypeName) == FHIRDefinedType.Bundle;
            Uri = uri;

           _children = new Lazy<List<Scope>>(() => new List<Scope>(ReferenceHarvester.Harvest(Container)));
        }

        public Scope FindChild(string uri)
        {
            return Children.SingleOrDefault(s => s.Uri == uri);
        }
    }


    internal class ScopeList : List<Scope>
    {
        public void Append(IElementNavigator container)
        {
            Scope scope = null;

            if (this.Any())
            {
                // If this was not the first scope to be added, we should normally find the container already present in the list of scopes, since
                // when adding a parent container, we add the children to the list of scopes as well
                scope = this.SingleOrDefault(s => s.Path == container.Path);
                if (scope == null)
                    throw Error.Argument("Tried to append an orphan scope with path '{0}'".FormatWith(container.Path));
            }
            else
                scope = new Scope(container, "#");

            // Add the children to the list, but keep pointers to the entries in the parent too
            AddRange(scope.Children);
        }

        public void Remove(IElementNavigator container)
        {
            var scopeToRemove = Find(container.Path);
            if (scopeToRemove == null)
                throw Error.Argument("There is no known scope for path '{0}'".FormatWith(container.Path));

            Remove(scopeToRemove);
        }

        public Scope FindParent(string path)
        {
            return this.Where(s => path.StartsWith(s.Path + "."))
                .OrderBy(s => s.Path.Length).Reverse().FirstOrDefault();
        }

        public Scope Find(string path)
        {
            return this.Where(s => s.Path == path).SingleOrDefault();
        }

        public IEnumerable<Scope> Containers(string path)
        {
            Scope scan = Find(path);
            if (scan == null)
                scan = FindParent(path);

            while(scan != null)
            {
                yield return scan;

                scan = FindParent(scan.Path);
            }
        }

    }


    internal class ScopeTracker
    {
        private ScopeList _scopes = new ScopeList();

        public void Push(IElementNavigator instance)
        {
            if (isContainer(instance))
            {
                _scopes.Append(instance);
            }
        }

        public void Pop(IElementNavigator instance)
        {
            if (isContainer(instance))
            {
                _scopes.Remove(instance);
            }
        }

        private static bool isContainer(IElementNavigator instance)
        {
            return ModelInfo.FhirTypeNameToFhirType(instance.TypeName) == FHIRDefinedType.Bundle ||
                                    ModelInfo.IsKnownResource(instance.TypeName);
        }

        public IElementNavigator ResourceContext(IElementNavigator instance)
        {
            return _scopes.Containers(instance.Path).Take(1).Select(s => s.Container).SingleOrDefault();
        }

        public string ContextFullUrl(IElementNavigator instance)
        {
            foreach (var container in _scopes.Containers(instance.Path))
            {
                if (!container.Uri.StartsWith("#"))
                    return container.Uri;
            }

            return null;
        }

        public IElementNavigator Resolve(IElementNavigator instance, string uri)
        {
            foreach (var container in _scopes.Containers(instance.Path))
            {
                var result = container.FindChild(uri);
                if (result != null) return result.Container;
            }

            return null;
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
                        new Scope(child,
                            "#" + getStringValue(child.GetChildrenByName("id").FirstOrDefault())));                            
        }

        public static IEnumerable<Scope> HarvestBundle(IElementNavigator instance)
        {
            return instance.GetChildrenByName("entry")
                    .SelectMany(entry =>
                        entry.GetChildrenByName("resource").Take(1)
                            .Select(res =>
                                new Scope(res,
                                    getStringValue(entry.GetChildrenByName("fullUrl").FirstOrDefault()))));
        }
    }
}
