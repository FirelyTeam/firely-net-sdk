extern alias dstu2;

using dstu2::Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.FluentPath;

namespace Hl7.Fhir.Tests.FhirPath
{
    public class ModelNavigator : IElementNavigator
    {
        public ModelNavigator(Base model)
        {
            if (model != null)
            {
                // The current implementation of FluentPath has a dummy (root)
                // element ahead of the real resoure root.
                // uncomment this section (and remove the below) to remove that.
                // _model = model;
                // _current = new ElementNavigator(model.TypeName, model);

                var root = new PocoRootObject();
                root.Value = model;
                root.ObjectName = model.TypeName;
                _model = root;
                _current = new ElementNavigator(model.TypeName, root);
            }
        }

        public ModelNavigator(Base model, ElementNavigator current)
        {
            _model = model;
            _current = current;
        }

        private Base _model;
        private ElementNavigator _current;

        /// <summary>
        /// Returns 
        /// </summary>
        public object Value
        {
            get
            {
                if (_current != null)
                {
#if DEBUG
                    Console.WriteLine("    -> Read Value of {0}: {1}",
                        _current.Name,
                        _current.ActualValue);
#endif
                    return _current.ActualValue;
                }
                return null;
            }
        }

        /// <summary>
        /// Return the FHIR TypeName
        /// </summary>
        public string TypeName
        {
            get
            {
                if (_current != null)
                    return _current.GetTypeName(); // This needs to be fixed
                return "Element"; // unknown type, so just returning Element for now
            }
        }

        /// <summary>
        /// The FHIR TypeName is also returned for the name of the root element
        /// </summary>
        public string Name
        {
            get
            {
                if (_current != null)
                {
#if DEBUG
                    Console.WriteLine("Read Name: {0} (value = {1})", _current.Name, _current.ActualValue);
#endif
                    return _current.Name;
                }
                return null;
            }
        }

        private string GetName()
        {
            if (_current != null)
            {
                return _current.Name;
            }
            return null;
        }

        public bool MoveToFirstChild()
        {
            if (_current != null && _current.Children().Count > 0)
            {
                string oldName = this.GetName();
                _current = _current.Children()[0];
                // Console.WriteLine("Move To First Child of {0} -> {1}", oldName, this.GetName());
                return true;
            }
            // Console.WriteLine("Move To First Child of {0} failed, no children", this.GetName());
            return false;
        }

        /// <summary>
        /// Move onto the next element in the list
        /// </summary>
        /// <returns>
        /// true if there was a next element, false if it was the last element
        /// </returns>
        public bool MoveToNext()
        {
            if (_current != null && _current.NextSibbling != null)
            {
                string oldName = this.GetName();
                _current = _current.NextSibbling;
                string newName = this.GetName();
                // Console.WriteLine("Move Next: {0} -> {1}", oldName, newName);
                return true;
            }
            // Console.WriteLine("Move Next: {0} (no more sibblings, didn't move)", this.GetName());
            return false;
        }

        /// <summary>
        /// Clone this navigator
        /// </summary>
        /// <returns></returns>
        public IElementNavigator Clone()
        {
            // Console.WriteLine("Cloning: {0}", this.GetName());
            return new ModelNavigator(_model, _current);
        }
    }

    public class PocoRootObject : Base
    {
        public string ObjectName;

        public Base Value { get; set; }

        public override string TypeName
        {
            get
            {
                return "(root)";
            }
        }

        public override IDeepCopyable DeepCopy()
        {
            throw new NotImplementedException();
        }

        public override bool IsExactly(IDeepComparable other)
        {
            throw new NotImplementedException();
        }

        public override bool Matches(IDeepComparable pattern)
        {
            throw new NotImplementedException();
        }
    }

    [System.Diagnostics.DebuggerDisplay(@"\{Name = {_name} Value = {_value}}")] // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
    public class ElementNavigator
    {
        static dstu2::Hl7.Fhir.Introspection.ClassMapping GetMappingForType(Type elementType)
        {
            dstu2::Hl7.Fhir.Serialization.ResourceReader rdr = new dstu2::Hl7.Fhir.Serialization.ResourceReader(null, null);
            dstu2::Hl7.Fhir.Introspection.ModelInspector inspector;
            var ti = rdr.GetType().GetField("_inspector", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            inspector = ti.GetValue(rdr) as dstu2::Hl7.Fhir.Introspection.ModelInspector;
            // inspector = dstu2::Hl7.Fhir.Serialization.SerializationConfig.Inspector;
            return inspector.FindClassMappingByType(elementType);
        }

        public ElementNavigator(string name, object value)
        {
            _name = name;
            _value = value;

            var elementType = value.GetType();
            if (!(this._value is PocoRootObject))
            {
                _mapping = GetMappingForType(elementType);
                if (_mapping == null)
                {
                    // unknown type encountered
                    Console.WriteLine("Unkown type encountered");
                }
            }
        }

        private string _name;
        private object _value;
        private dstu2::Hl7.Fhir.Introspection.ClassMapping _mapping;
        private ElementNavigator _nextSibbling;

        public string Name { get { return _name; } }
        public ElementNavigator NextSibbling { get { return _nextSibbling; } }

        public string GetTypeName()
        {
            return _mapping.Name;
        }
        public object ActualValue
        {
            get
            {
                // This is only required where the element is a primitive
                object result = _value;
                if (result is Primitive)
                    return (result as Primitive).ObjectValue;
                return null; // result;
            }
        }

        private List<ElementNavigator> _children;

        public List<ElementNavigator> Children()
        {
            if (_children != null)
                return _children;
            List<ElementNavigator> children = new List<ElementNavigator>();
            _children = children;
            if (_value == null)
                return children;
            if (_mapping == null && this._value is PocoRootObject)
            {
                var po = this._value as PocoRootObject;
                children.Add(new ElementNavigator(po.ObjectName, po.Value));
                return children;
            }
            ElementNavigator previousChild = null;
            foreach (var item in _mapping.PropertyMappings)
            {
                if (item.IsPrimitive && item.Name.ToLower() == "value")
                    continue;
                var itemValue = item.GetValue(_value);
                if (itemValue != null)
                {
                    if (item.IsCollection)
                    {
                        foreach (var colItem in (itemValue as System.Collections.IList))
                        {
                            if (colItem != null)
                            {
                                children.Add(new ElementNavigator(item.Name, colItem));
                                if (previousChild != null)
                                    previousChild._nextSibbling = children.Last();
                                previousChild = children.Last();
                            }
                        }
                    }
                    else
                    {
                        children.Add(new ElementNavigator(item.Name, itemValue));
                        if (previousChild != null)
                            previousChild._nextSibbling = children.Last();
                        previousChild = children.Last();
                    }
                }
            }
            return children;
        }
    }

}
