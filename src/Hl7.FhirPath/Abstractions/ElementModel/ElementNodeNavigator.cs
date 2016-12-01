using System.Linq;
using System.Collections.Generic;

namespace Hl7.ElementModel
{
    public struct ElementNodeNavigator : IElementNavigator
    {
        private IList<ElementNode> _siblings;
        private int _index;

        public ElementNode Current
        {
            get { return _siblings[_index]; }
        }

        public ElementNodeNavigator(ElementNode wrapped)
        {
            _siblings = new List<ElementNode> { wrapped };
            _index = 0;
        }

        public string Name
        {
            get
            {
                return Current.Name;
            }
        }

        public string TypeName
        {
            get
            {
                return Current.TypeName;
            }
        }

        public string Path
        {
            get
            {
                return Current.PathString();
            }
        }


        public object Value
        {
            get
            {
                return Current.Value;
            }
        }

        public IElementNavigator Clone()
        {
            var r = new ElementNodeNavigator();

            r._siblings = this._siblings;
            r._index = this._index;

            return r;
        }

        public bool MoveToFirstChild()
        {
            if (Current.Children != null && Current.Children.Any())
            {
                _siblings = Current.Children;
                _index = 0;
                return true;
            }

            return false;
        }

        public bool MoveToNext()
        {
            if (_siblings.Count > _index + 1)
            {
                _index += 1;
                return true;
            }

            return false;
        }
    }

}