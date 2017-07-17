
using System;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.ElementModel
{
    public struct PathNavigator : IElementNavigator
    {
        private IElementNavigator _navigator;
        private string _parentPath;
        private int _index;

        public PathNavigator(IElementNavigator navigator, string path = null) : this(navigator, null, 0) { }

        private PathNavigator(IElementNavigator navigator, string path, int index)
        {
            _navigator = navigator;
            _parentPath = path;
            _index = index;
        }

        public IElementNavigator Clone()
        {
            var n = _navigator.Clone();
            return new PathNavigator(n, this.Location);
        }


        public string Name
        {
            get
            {
                return _navigator.Name;
            }
        }

        public string Location
        {
            get
            {
                return _parentPath + ".{0}[{1}]".FormatWith(this.Name, _index);
            }
        }

        public string Type
        {
            get
            {
                return _navigator.Type;
            }
        }

        public object Value
        {
            get
            {
                return _navigator.Value;
            }
        }


        public bool MoveToFirstChild(string nameFilter = null)
        {
            bool exists = _navigator.MoveToFirstChild(nameFilter);
            if (exists) _index = 0;
            return exists;
        }

        public bool MoveToNext(string nameFilter = null)
        {
            bool exists = _navigator.MoveToNext(nameFilter);
            if (exists) _index++;
            return exists;
        }
    }
}
