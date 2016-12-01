using Furore.Support;
using Hl7.ElementModel;

namespace Hl7.Fhir.Serialization
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

        public string Name
        {
            get
            {
                return _navigator.Name;
            }
        }

        public string Path
        {
            get
            {
                return _parentPath + ".{0}[{1}]".FormatWith(this.Name, _index);
            }
        }

        public string TypeName
        {
            get
            {
                return _navigator.TypeName;
            }
        }

        public object Value
        {
            get
            {
                return _navigator.Value;
            }
        }

        public IElementNavigator Clone()
        {
            var n = _navigator.Clone();
            return new PathNavigator(n, this.Path);
        }

        public bool MoveToFirstChild()
        {
            bool exists = _navigator.MoveToFirstChild();
            if (exists) _index = 0;
            return exists;
        }

        public bool MoveToNext()
        {
            bool exists = _navigator.MoveToNext();
            if (exists) _index++;
            return exists;
        }
    }
}
