using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.ElementModel
{
    internal class ScopedNodeWrapper : BaseScopedNode
    {
        private BaseScopedNode _current;

        public ScopedNodeWrapper(BaseScopedNode scopedNode)
        {
            ValidatedProfiles = new List<ValidatedProfile>();
            _current = scopedNode;
            ParentResource = scopedNode.ParentResource;
        }

        private ScopedNodeWrapper(BaseScopedNode parent, BaseScopedNode parentResource, BaseScopedNode scopedNode)
        {
            ValidatedProfiles = new List<ValidatedProfile>();
            _current = scopedNode;
            ParentResource = parent.AtResource ? parent : parentResource;
        }

        public override ITypedElement Current => _current.Current;

        public override string Name => _current.Name;

        public override string InstanceType => _current.InstanceType;

        public override object Value => _current.Value;

        public override string Location => _current.Location;

        public override IElementDefinitionSummary Definition => _current.Definition;

        public override ExceptionNotificationHandler ExceptionHandler { get { return _current.ExceptionHandler; } set { _current.ExceptionHandler = value; } }

        public override string LocalLocation => _current.LocalLocation;

        public override bool AtResource => _current.AtResource;

        public override IEnumerable<ITypedElement> Children(string name = null) => _current.Children(name).Select(t => new ScopedNodeWrapper(this, this.ParentResource, t as ScopedNode));

        public override IEnumerable<object> Annotations(Type type)
        {
            if (type == typeof(ScopedNodeWrapper) || type == typeof(BaseScopedNode))
                return new[] { this };
            else
                return Current.Annotations(type);
        }

        public List<ValidatedProfile> ValidatedProfiles { get; set; }
    }

    public class ValidatedProfile
    {
        public string Profile { get; }
        public Status Success { get; internal set; }

        public ValidatedProfile(string profile, Status status)
        {
            Profile = profile;
            Success = status;
        }

        public enum Status
        {
            Pending,
            Success,
            Fail
        }
    }
}
