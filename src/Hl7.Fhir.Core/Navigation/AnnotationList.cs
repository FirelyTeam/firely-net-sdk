using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Navigation
{
    internal class AnnotationList : List<AnnotationEntry>
    {
        public override string ToString()
        {
            if(this.Any())
            {
                var result = new StringBuilder();

                result.Append("@[");
                result.Append(string.Join(", ", this.Select(o => o.ToString())));
                result.Append("]");

                return result.ToString();
            }

            return String.Empty;            
        }
    }

    internal struct AnnotationEntry
    {
        public AnnotationEntry(object annotation)
        {
            Type = annotation.GetType();
            Annotation = annotation;
        }

        // [WMR] Instead implement IValueProvider / IValueProvider<T> or similar pattern
        public Type Type;
        public object Annotation;

        public override string ToString()
        {
            return "(" + Type.Name + "): " + Annotation.ToString();
        }
    }
 
    internal static class AnnotationListExtensions
    {
        public static IEnumerable<AnnotationEntry> FilterByType(this IEnumerable<AnnotationEntry> me, Type t)
        {
            return me.Where(e => e.Type == t);
        }

        public static void AddAnnotation(this IList<AnnotationEntry> me, object annotation)
        {
            me.Add(new AnnotationEntry(annotation));
        }

        public static void RemoveAnnotation(this IList<AnnotationEntry> me, Type t)
        {
            var annotations = me.FilterByType(t).ToArray();

            foreach (var found in annotations)
                me.Remove(found);
        }
    }
}
