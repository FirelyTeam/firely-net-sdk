using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Navigation
{
    public interface IAnnotatable
    {
        /// <summary>
        /// Gets the first annotation object of the specified type.
        /// </summary>
        /// <param name="type">The Type of the annotation to retrieve.</param>
        /// <returns>The Object that contains the first annotation object that matches the specified type, or null if no annotation is of the specified type.</returns>
        object Annotation(Type type);

        /// <summary>
        /// Get the first annotation object of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the annotation to retrieve.</typeparam>
        /// <returns>The first annotation object that matches the specified type, or null if no annotation is of the specified type.</returns>
        T Annotation<T>() where T : class;

        /// <summary>
        /// Gets a collection of annotations of the specified type.
        /// </summary>
        /// <param name="type">The Type of the annotations to retrieve.</param>
        /// <returns>An IEnumerable&lt;T&gt; of Object that contains the annotations that match the specified type.</returns>
        IEnumerable<object> Annotations(Type type);

        /// <summary>
        /// Gets a collection of annotations of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the annotations to retrieve.</typeparam>
        /// <returns>An IEnumerable&lt;T&gt; that contains the annotations that match the specified type.</returns>
        IEnumerable<T> Annotations<T>() where T : class;

        /// <summary>
        /// Adds an object to the annotation list.
        /// </summary>
        /// <param name="annotation">An Object that contains the annotation to add.</param>
        void AddAnnotation(object annotation);

        /// <summary>
        /// Removes the annotations of the specified type.
        /// </summary>
        /// <param name="type">The Type of annotations to remove.</param>
        void RemoveAnnotations(Type type);

        /// <summary>
        /// Removes the annotations of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of annotations to remove.</typeparam>
        void RemoveAnnotations<T>() where T : class;
    }
}
