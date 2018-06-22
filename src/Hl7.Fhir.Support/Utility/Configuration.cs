using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Support.Utility
{
#if NET40
    public interface IReadOnlyList<out T> : IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
    {
        T this[int index] { get; }
    }
    
    public interface IReadOnlyCollection<out T> : IEnumerable<T>, IEnumerable
    {
        int Count { get; }
    }
#endif

    public class ValidatorSettings
    {
        public static implicit operator Configuration(ValidatorSettings x)
        {
            return new Configuration(x);
        }
    }


    public class Configuration : IReadOnlyList<object>
    {
        private List<object> _contents;

        public Configuration(IEnumerable<object> list)
        {
            _contents = new List<object>(list);
        }

        public Configuration(object item)
        {
            _contents = new List<object> { item };
        }

        public object this[int index] => _contents[index];

        public int Count => _contents.Count;

        public IEnumerator<object> GetEnumerator() => ((IReadOnlyList<object>)_contents).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IReadOnlyList<object>)_contents).GetEnumerator();

        public T Get<T>() => this.OfType<T>().FirstOrDefault();
    }
}
