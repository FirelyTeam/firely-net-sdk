#if NET40
using System.Collections;
using System.Collections.Generic;

namespace System.Collections.Generic
{
    public interface IReadOnlyCollection<out T> : IEnumerable<T>, IEnumerable
    {
        //
        // Summary:
        //     Gets the number of elements in the collection.
        //
        // Returns:
        //     The number of elements in the collection.
        int Count { get; }
    }

    public class ReadOnlyList<T> : List<T>, IReadOnlyCollection<T>
    {
        public ReadOnlyList()
            : base()
        {
        }

        public ReadOnlyList(IEnumerable<T> collection)
            : base(collection)
        {
        }
    }

    public static class ListExtensions
    {
        public static IReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> collection)
        {
            return new ReadOnlyList<T>(collection);
        }
    }
}
#endif