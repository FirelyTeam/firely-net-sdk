using Hl7.Fhir.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Hl7.Fhir.Specification.Schema.Tags
{
    public abstract class SchemaTag
    {
        public abstract SchemaTag Merge(SchemaTag other);
    }

    public class SchemaTags : ReadOnlyCollection<SchemaTag>
    {
        public static readonly SchemaTags Success = new SchemaTags(ResultTag.Success);
        public static readonly SchemaTags Failure = new SchemaTags(ResultTag.Failure);
        public static readonly SchemaTags Undecided = new SchemaTags(ResultTag.Undecided);

        public SchemaTags(params SchemaTag[] tags) : this(tags.AsEnumerable())
        {
        }
            
        public SchemaTags(IEnumerable<SchemaTag> collection) : base(normalize(collection).ToList())
        {
        }

        public IEnumerable<SchemaTags> Collection => new[] { this };

        public static SchemaTags operator +(SchemaTags left, SchemaTags right)
            => new SchemaTags(left.Union(right));

        public static SchemaTags operator +(SchemaTags left, SchemaTag right)
                => new SchemaTags(left.Union(new[] { right }));


        private static IEnumerable<SchemaTag> normalize(IEnumerable<SchemaTag> tags)
            => from sa in tags
               group sa by sa.GetType() into grp
               select grp.Aggregate((sum, other) => sum.Merge(other));

        public ResultTag Result => this.OfType<ResultTag>().Single();
    }

    public static class SchemaTagExtensions
    {
        public static IEnumerable<SchemaTags> Combine(this IEnumerable<SchemaTags> left, IEnumerable<SchemaTags> right)
            => from leftST in left
               from rightST in right
               select leftST + rightST;
    }
}

