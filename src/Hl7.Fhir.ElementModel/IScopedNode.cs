using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.ElementModel
{
    public abstract class BaseScopedNode : ITypedElement, IAnnotated, IExceptionSource
    {
        public ITypedElement Current { get; }
        public BaseScopedNode ParentResource { get; }
        public string LocalLocation { get; }
        public bool AtResource { get; }
        public virtual IEnumerable<IBundledResource> BundledResources()
        {
            if (_cache.BundledResources == null)
            {
                if (InstanceType == "Bundle")
                    _cache.BundledResources = from e in this.Children("entry")
                                              let fullUrl = e.Children("fullUrl").FirstOrDefault()?.Value as string
                                              let resource = e.Children("resource").FirstOrDefault() as BaseScopedNode
                                              select new BundledResource { FullUrl = fullUrl, Resource = resource };
                else
                    _cache.BundledResources = Enumerable.Empty<BundledResource>();
            }

            return _cache.BundledResources;
        }

        public virtual string Id()
        {
            if (_cache.Id == null)
            {
                _cache.Id = AtResource ? "#" + Current.Children("id").FirstOrDefault()?.Value as string : null;
            }

            return _cache.Id;
        }        

        public virtual IEnumerable<BaseScopedNode> ParentResources()
        {
            var scan = this.ParentResource;

            while (scan != null)
            {
                yield return scan;

                scan = scan.ParentResource;
            }
        }

        public virtual string FullUrl()
        {
            if (_cache.FullUrl == null)
            {
                foreach (var parent in ParentResources())
                {
                    if (parent.InstanceType == "Bundle")
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

        public virtual IEnumerable<BaseScopedNode> ContainedResources()
        {
            if (_cache.ContainedResources == null)
            {
                _cache.ContainedResources = AtResource?
                    this.Children("contained").Cast<BaseScopedNode>():
                    Enumerable.Empty<BaseScopedNode>();
            }
            return _cache.ContainedResources;
        }


        //IAnnotated
        public abstract IEnumerable<ITypedElement> Children(string name = null);
        public abstract IEnumerable<object> Annotations(Type type);

        //ITypedElement
        public virtual ITypedElement ResourceContext
        {
            get
            {
                BaseScopedNode scan = this;

                while (scan.ParentResource != null && scan.ParentResource.InstanceType != "Bundle")
                {
                    scan = scan.ParentResource;
                }

                return scan as ITypedElement;
            }
        }

        public string Name { get; }
        public string InstanceType { get; }
        public object Value { get; }
        public string Location { get; }
        public IElementDefinitionSummary Definition { get; }

        //IExceptionSource
        public virtual ExceptionNotificationHandler ExceptionHandler { get; set; }

        private class Cache
        {
            public readonly object _lock = new object();

            public string Id;
            public IEnumerable<BaseScopedNode> ContainedResources;
            public IEnumerable<IBundledResource> BundledResources;
            public string FullUrl;
        }

        private Cache _cache = new Cache();

        public class BundledResource : IBundledResource
        {
            public string FullUrl { get; set; }
            public BaseScopedNode Resource { get; set; }
        }
    }
}
