using System.Linq;
using System.Collections.Generic;

namespace Hl7.Fhir.ElementModel
{
    public struct ElementNodeNavigator : IElementNavigator
    {
        private IList<IElementNode> _siblings;
        private int _index;

        public IElementNode Current
        {
            get { return _siblings[_index]; }
        }

        public ElementNodeNavigator(IElementNode wrapped)
        {
            _siblings = new List<IElementNode> { wrapped };
            _index = 0;
        }

        public string Name
        {
            get
            {
                return Current.Name;
            }
        }

        public string Type
        {
            get
            {
                return Current.Type;
            }
        }

        public string Location
        {
            get
            {
                return Current.BuildPath();
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