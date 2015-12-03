using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Navigation
{
    internal class AnnotationList : List<AnnotationEntry>
    {
    }

    internal struct AnnotationEntry
    {
        public AnnotationEntry(object annotation)
        {
            Type = annotation.GetType();
            Annotation = annotation;
        }
    
        public Type Type;
        public object Annotation;
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
