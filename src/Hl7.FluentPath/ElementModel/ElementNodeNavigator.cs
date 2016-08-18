using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.ElementModel
{
    public class ElementNodeNavigator : IElementNavigator
    {
        private IList<IElementNode> _siblings;
        private int _index;

        public IElementNode Current
        {
            get { return _siblings[_index]; }
        }


        private ElementNodeNavigator()
        {
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
                return Current.Path;
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
            if(Current.Children != null && Current.Children.Any())
            {
                _index = 0;
                _siblings = Current.Children;
                return true;
            }

            return false;
        }

        public bool MoveToNext()
        {
            if(_siblings.Count > _index+1)
            {
                _index += 1;
                return true;
            }

            return false;
        }
    }

}