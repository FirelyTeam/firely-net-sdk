using Hl7.Fhir.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    public abstract class SchemaAnnotation
    {
        // Does merging make sense? You are maybe losing details?
        public abstract SchemaAnnotation Merge(SchemaAnnotation other);
    }

    public class SchemaAnnotations : ReadOnlyCollection<SchemaAnnotation>, IAnnotated
    {
        public SchemaAnnotations(IEnumerable<SchemaAnnotation> collection) : base(collection.ToList())
        {
        }

        public IEnumerable<object> Annotations(Type type) => this.OfType(type);

        public static SchemaAnnotations operator +(SchemaAnnotations left, SchemaAnnotations right)
            => new SchemaAnnotations(left.Union(right));

        public SchemaAnnotations Merge()
        {
            return new SchemaAnnotations(
                this
                .GroupBy(sa => sa.GetType())
                .Select(grp => grp.Aggregate((sum, other) => sum.Merge(other))));
        }

        public ResultAnnotation Result => this.OfType<ResultAnnotation>().Single();
    }

}
