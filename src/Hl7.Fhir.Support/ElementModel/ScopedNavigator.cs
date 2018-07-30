/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.ElementModel
{
    public class ScopedNavigator : IElementNavigator, IAnnotated, IExceptionSource
    {
        private class Cache
        {
            public readonly object _lock = new object();

            public string Id;
            public IEnumerable<ScopedNavigator> ContainedResources;
            public IEnumerable<BundledResource> BundledResources;
            public string FullUrl;
        }

        private Cache _cache = new Cache();

        private IElementNavigator _wrapped = null;

        public ScopedNavigator(IElementNavigator wrapped)
        {
            //if (wrapped.GetElementDefinitionSummary() == null)
            //    throw Error.Argument("ScopedNavigator can only be used on a navigator chain that supplies type information (e.g. TypedNavigator, PocoNavigator)", nameof(wrapped));

            _wrapped = wrapped;

            if (_wrapped is IExceptionSource ies && ies.ExceptionHandler == null)
                ies.ExceptionHandler = (o, a) => ExceptionHandler.NotifyOrThrow(o, a);
        }

        public ExceptionNotificationHandler ExceptionHandler { get; set; }


        private ScopedNavigator() { }       // for Clone


        public IElementNavigator Clone()
        {
            return new ScopedNavigator()
            {
                _wrapped = this._wrapped.Clone(),
                Parent = this.Parent,
                ExceptionHandler = this.ExceptionHandler,
            };
        }

        public ScopedNavigator Parent { get; private set; }

        public string LocalLocation => Parent == null ? Location :
                        $"{Parent.Type}.{Location.Substring(Parent.Location.Length + 1)}";

        public string Name => _wrapped.Name;

        public string Type => _wrapped.Type;

        public object Value => _wrapped.Value;

        public string Location => _wrapped.Location;

        public bool MoveToNext(string nameFilter = null)
        {
            _cache = new Cache();
            return _wrapped.MoveToNext(nameFilter);
        }

        public bool MoveToFirstChild(string nameFilter = null)
        {
            ScopedNavigator me = null;

            if (this.AtResource)
            {
                me = (ScopedNavigator)this.Clone();
            }

            if (!_wrapped.MoveToFirstChild(nameFilter)) return false;

            // If the current position is a resource, we'll be the new _parentScope
            if (me != null)
                Parent = me;

            _cache = new Cache();

            var def = _wrapped.GetElementDefinitionSummary();

            return true;
        }

        public bool AtResource => _wrapped.GetElementDefinitionSummary()?.IsResource ?? false;

        public string NearestResourceType
        {
            get
            {
                var ix = LocalLocation.IndexOf('.');
                return (ix == -1) ? LocalLocation : LocalLocation.Substring(0, ix);
            }
        }

        /// <summary>
        /// The %resource context, as defined by FHIRPath
        /// </summary>
        /// <remarks>
        /// This is the original resource the current context is part of. When evaluating a datatype, 
        /// this would be the resource the element is part of. Do not go past a root resource into a bundle, 
        /// if it is contained in a bundle.
        /// </remarks>
        public IElementNavigator ResourceContext
        {
            get
            {
                var scan = this;

                while (scan.Parent != null && scan.Parent.Type != "Bundle")
                {
                    scan = scan.Parent;
                }

                return scan;
            }
        }

        public IEnumerable<ScopedNavigator> Parents()
        {
            var scan = this.Parent;

            while (scan != null)
            {
                yield return scan;

                scan = scan.Parent;
            }
        }

        public string Id()
        {
            if (_cache.Id == null)
            {
                _cache.Id = AtResource ? "#" + _wrapped.Children("id").FirstOrDefault()?.Value as string : null;
            }

            return _cache.Id;
        }

        public IEnumerable<ScopedNavigator> ContainedResources()
        {
            if (_cache.ContainedResources == null)
            {
                if (AtResource)
                    _cache.ContainedResources = this.Children("contained").Cast<ScopedNavigator>();
                else
                    _cache.ContainedResources = Enumerable.Empty<ScopedNavigator>();
            }
            return _cache.ContainedResources;
        }

        public class BundledResource
        {
            public string FullUrl;
            public ScopedNavigator Resource;
        }

        public IEnumerable<BundledResource> BundledResources()
        {
            if (_cache.BundledResources == null)
            {
                if (Type == "Bundle")
                    _cache.BundledResources = from e in this.Children("entry")
                                              let fullUrl = e.Children("fullUrl").FirstOrDefault()?.Value as string
                                              let resource = e.Children("resource").FirstOrDefault() as ScopedNavigator
                                              select new BundledResource { FullUrl = fullUrl, Resource = resource };
                else
                    _cache.BundledResources = Enumerable.Empty<BundledResource>();
            }

            return _cache.BundledResources;
        }

        public string FullUrl()
        {
            if (_cache.FullUrl == null)
            {
                foreach (var parent in Parents())
                {
                    if (parent.Type == "Bundle")
                    {
                        var fullUrl = parent.BundledResources()
                            .SingleOrDefault(be => this.Location.StartsWith(be.Resource.Location))
                            ?.FullUrl;
                        if (fullUrl != null) _cache.FullUrl = fullUrl;
                    }
                }
            }

            return _cache.FullUrl;
        }

        public IEnumerable<object> Annotations(Type type)
        {
            if (type == typeof(ScopedNavigator))
                return new[] { this };
            else
                return _wrapped.Annotations(type);
        }

    }
}
