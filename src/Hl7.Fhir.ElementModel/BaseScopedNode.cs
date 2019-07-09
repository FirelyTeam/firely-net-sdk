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
        public virtual ITypedElement Current { get; protected set; }
        public virtual BaseScopedNode ParentResource { get; protected set; }
        public virtual string LocalLocation => ParentResource == null ? Location :
                        $"{ParentResource.InstanceType}.{Location.Substring(ParentResource.Location.Length + 1)}";
        public virtual bool AtResource => Current.Definition?.IsResource ?? false;
        public virtual IEnumerable<IBundledResource> BundledResources()
        {
            if (CacheInstance.BundledResources == null)
            {
                if (InstanceType == "Bundle")
                    CacheInstance.BundledResources = from e in this.Children("entry")
                                              let fullUrl = e.Children("fullUrl").FirstOrDefault()?.Value as string
                                              let resource = e.Children("resource").FirstOrDefault() as BaseScopedNode
                                              select new BundledResource { FullUrl = fullUrl, Resource = resource };
                else
                    CacheInstance.BundledResources = Enumerable.Empty<BundledResource>();
            }

            return CacheInstance.BundledResources;
        }

        public virtual string Id()
        {
            if (CacheInstance.Id == null)
            {
                CacheInstance.Id = AtResource ? "#" + Current.Children("id").FirstOrDefault()?.Value as string : null;
            }

            return CacheInstance.Id;
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
            if (CacheInstance.FullUrl == null)
            {
                foreach (var parent in ParentResources())
                {
                    if (parent.InstanceType == "Bundle")
                    {
                        var fullUrl = parent.BundledResources()
                            .SingleOrDefault(be => this.Location.StartsWith(be.Resource.Location))
                            ?.FullUrl;
                        if (fullUrl != null) CacheInstance.FullUrl = fullUrl;
                    }
                }
            }

            return CacheInstance.FullUrl;
        }

        public virtual IEnumerable<BaseScopedNode> ContainedResources()
        {
            if (CacheInstance.ContainedResources == null)
            {
                CacheInstance.ContainedResources = AtResource?
                    this.Children("contained").Cast<BaseScopedNode>().ToList():
                    Enumerable.Empty<BaseScopedNode>();
            }
            return CacheInstance.ContainedResources;
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

        public virtual string Name => Current.Name;
        public virtual string InstanceType => Current.InstanceType;
        public virtual object Value => Current.Value;
        public virtual string Location => Current.Location;
        public virtual IElementDefinitionSummary Definition => Current.Definition;

        //IExceptionSource
        public virtual ExceptionNotificationHandler ExceptionHandler { get; set; }

        protected class Cache
        {
            public string Id;
            public IEnumerable<BaseScopedNode> ContainedResources;
            public IEnumerable<IBundledResource> BundledResources;
            public IEnumerable<BaseScopedNode> Children;
            public string FullUrl;
        }

        protected Cache CacheInstance = new Cache();

        public class BundledResource : IBundledResource
        {
            public string FullUrl { get; set; }
            public BaseScopedNode Resource { get; set; }
        }
    }
}
