/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Validation
{
    internal class Scope
    {
        public Scope Parent { get; private set; }

        public string Uri;

        public IElementNavigator Container { get; private set; }
        public string Path { get; private set; }

        public int UseCount { get; private set; }

        public bool IsBundle { get; private set; }

        private Lazy<List<Scope>> _children;

        public List<Scope> Children { get { return _children.Value; } }

        public Scope(IElementNavigator container, string uri)
        {
            Container = container.Clone();
            Path = container.Location;
            IsBundle = ModelInfo.FhirTypeNameToFhirType(container.Type) == FHIRAllTypes.Bundle;
            Uri = uri;

           _children = new Lazy<List<Scope>>(createScopeListForContainer);
        }

        private List<Scope> createScopeListForContainer()
        {
            var children = ReferenceHarvester.Harvest(Container).ToList();
            foreach (var child in children)
            {
                child.Parent = this;
                child.UseCount = 1;
            }

            return children;
        }

        public Scope FindNearestScope(string path)
        {
            if (!path.StartsWith(Path)) return null;

            if (path == Path) return this;

            var candidateParent = Children.SingleOrDefault(c => path.StartsWith(c.Path));

            if (candidateParent != null)
                return candidateParent.FindNearestScope(path);
            else
                return this;
        }

        public Scope FindScope(string path)
        {
            var result = FindNearestScope(path);

            return result.Path == path ? result : null;
        }

        public bool Add(Scope child)
        {
            var nearestChild = FindNearestScope(child.Path);

            if(nearestChild == null)
                throw Error.Argument(nameof(child),
                      "Tried to add a child with path '{0}' which is not part of this tree ('{1}')".FormatWith(child.Path, Path));

            if (nearestChild.Path == child.Path)
            {
                nearestChild.UseCount += 1;
                return false;
            }

            if (!nearestChild.Children.Any(c => c.Path == child.Path))
            {
                child.Parent = nearestChild;
                child.UseCount = 1;
                nearestChild.Children.Add(child);
                return true;
            }
            else
                return false;
        }

        public bool Remove(string path)
        {
            var scope = FindNearestScope(path);

            if (scope == null)
                throw Error.Argument(nameof(path),
                      "Tried to remove a child with path '{0}' which is not part of this tree ('{1}')".FormatWith(path, Path));

            if(scope.Path == path && scope.Parent != null)
            {
                var child = scope.Parent.Children.Single(s => s.Path == path);
                child.UseCount -= 1;

                if (child.UseCount < 0)
                    throw Error.InvalidOperation("Usecount went below 0 for path '{0}'".FormatWith(child.Path));

                if(child.UseCount == 0)
                    scope.Parent.Children.Remove(child);

                return true;
            }

            return false;
        }

        public IEnumerable<Scope> Containers()
        {
            var scan = this;

            while (scan != null)
            {
                yield return scan;

                scan = scan.Parent;
            }
        }


        public Scope ResolveChild(string uri)
        {
            return Children.SingleOrDefault(s => s.Uri == uri);
        }



        //public bool Remove(Scope node)
        //{
        //    if (!node.Path.StartsWith(Path))
        //        throw Error.Argument(nameof(node),
        //            "Tried to remove a node with path '{0}' that is not part of this tree ('{1}')".FormatWith(node.Path, Path));

        //    var childToRemove = Children.SingleOrDefault(c => node.Path == c.Path);
        //    if (childToRemove != null)
        //    {
        //        Children.Remove(childToRemove);
        //        return true;
        //    }

        //    var candidateParent = Children.SingleOrDefault(c => node.Path.StartsWith(c.Path));

        //    if (candidateParent != null)
        //        return candidateParent.Remove(node);
        //    else
        //        throw Error.Argument(nameof(node),
        //            "Tried to remove an orphan child with path '{0}' from scope at path '{1}'".FormatWith(node.Path, Path));

        //}
    }


    internal class ScopeTracker
    {
        private Scope _root = null;
        public void Enter(IElementNavigator instance)
        {
            if (isContainer(instance))
            {
                var newScope = new Scope(instance, null);
                if (_root == null)
                    _root = newScope;
                else
                    _root.Add(newScope);
            }
        }

        public void Leave(IElementNavigator instance)
        {
            if (isContainer(instance))
            {
                if (_root.Path == instance.Location)
                    _root = null;
                else
                    _root.Remove(instance.Location);
            }
        }

        private static bool isContainer(IElementNavigator instance)
        {
            if (instance.Type == null) return false;

            return ModelInfo.FhirTypeNameToFhirType(instance.Type) == FHIRAllTypes.Bundle ||
                                    ModelInfo.IsKnownResource(instance.Type);
        }

        public IElementNavigator ResourceContext(IElementNavigator instance)
        {
            if (_root == null) return null;     // No parent scope found yet (i.e. validating an isolated datatype instance)

            var parent = _root.FindNearestScope(instance.Location);
            return parent != null ? parent.Container : null;
        }

        public string ContextFullUrl(IElementNavigator instance)
        {
            if (_root == null) return null;   // No parent scope found yet (i.e. validating an isolated datatype instance)

            var scope = _root.FindNearestScope(instance.Location);
            if (scope == null) return null;

            foreach (var container in scope.Containers())
            {
                if (container.Uri != null && !container.Uri.StartsWith("#"))
                    return container.Uri;
            }

            return null;
        }

        private IElementNavigator findUri(IElementNavigator instance, string uri)
        {
            if (_root == null) return null;   // No parent scope found yet (i.e. validating an isolated datatype instance)

            var scope = _root.FindNearestScope(instance.Location);
            if (scope == null) return null;

            foreach (var container in scope.Containers())
            {
                var result = container.ResolveChild(uri);
                if (result != null) return result.Container;
            }

            return null;
        }


        public IElementNavigator Resolve(IElementNavigator instance, string reference)
        {
            var identity = new ResourceIdentity(reference);

            if (identity.Form == ResourceIdentityForm.RelativeRestUrl)
            {
                // Relocate the relative url on the base given in the fullUrl of the entry (if applicable)
                var fullUrl = ContextFullUrl(instance);

                if (fullUrl != null)
                {
                    var parentIdentity = new ResourceIdentity(fullUrl);
                    if (parentIdentity.BaseUri != null)
                        identity = identity.WithBase(parentIdentity.BaseUri);
                }
            }

            IElementNavigator referencedResource = null;

            if (identity.Form == ResourceIdentityForm.Local || identity.Form == ResourceIdentityForm.AbsoluteRestUrl || identity.Form == ResourceIdentityForm.Urn)
                referencedResource = findUri(instance, identity.ToString());

            return referencedResource;
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

            if (ModelInfo.FhirTypeNameToFhirType(scanner.Type) == FHIRAllTypes.Bundle)
            {
                return HarvestBundle(scanner);
            }
            else if (ModelInfo.IsKnownResource(instance.Type))
            {
                return HarvestResource(instance);
            }
            else
                return Enumerable.Empty<Scope>();
        }

        public static IEnumerable<Scope> HarvestResource(IElementNavigator instance)
        {
            return instance.Children("contained")
                    .Select(child =>
                        new Scope(child,
                            "#" + getStringValue(child.Children("id").FirstOrDefault())));                            
        }

        public static IEnumerable<Scope> HarvestBundle(IElementNavigator instance)
        {
            return instance.Children("entry")
                    .SelectMany(entry =>
                        entry.Children("resource").Take(1)
                            .Select(res =>
                                new Scope(res,
                                    getStringValue(entry.Children("fullUrl").FirstOrDefault()))));
        }
    }
}
