extern alias dstu2;

using dstu2::Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.FluentPath;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Tests.FhirPath
{
    public class ModelNavigator : IElementNavigator
    {
        public ModelNavigator(Base model)
        {
            if (model == null) throw Error.ArgumentNull("model");

            _current = new ElementNavigator(model.TypeName, model);
        }

        internal ModelNavigator(ElementNavigator current)
        {
            _current =  current;
        }

        private ElementNavigator _current;

        /// <summary>
        /// Returns 
        /// </summary>
        public object Value
        {
            get
            {
#if DEBUGX
                Console.WriteLine("    -> Read Value of {0}: {1}", _current.Name, _current.Value);
#endif
                return _current.Value;
            }
        }

        /// <summary>
        /// Return the FHIR TypeName
        /// </summary>
        public string TypeName
        {
            get
            {
                return _current.TypeName; // This needs to be fixed
            }
        }

        /// <summary>
        /// The FHIR TypeName is also returned for the name of the root element
        /// </summary>
        public string Name
        {
            get
            {
#if DEBUGX
                Console.WriteLine("Read Name: {0} (value = {1})", _current.Name, _current.Value);
#endif
                return _current.Name;
            }
        }
        public bool MoveToFirstChild()
        {
            if (_current.Children().Any())
            {
                _current = _current.Children()[0];
                return true;
            }

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
            if (_current != null && _current.Next != null)
            {
                string oldName = this.Name;
                _current = _current.Next;
                string newName = this.Name;
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
            return new ModelNavigator(_current);
        }
    }


    internal class ElementNavigator
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

        internal ElementNavigator(string name, Base value)
        {
            if (value == null) throw Error.ArgumentNull("value");

            _pocoElement = value;
            Name = name;

            _mapping = GetMappingForType(value.GetType());

            if (_mapping == null)
                Console.WriteLine("Unkown type '{0}' encountered".FormatWith(value.GetType().Name));
        }

        private Base _pocoElement;
        private dstu2::Hl7.Fhir.Introspection.ClassMapping _mapping;

        public string Name { get; private set; }

        public object Value
        {
            get
            {
                if (_pocoElement is FhirDateTime)
                    return ((FhirDateTime)_pocoElement).ToDateTimeOffset();
                else if (_pocoElement is dstu2.Hl7.Fhir.Model.Time)
                    return FluentPath.Time.Parse(((dstu2.Hl7.Fhir.Model.Time)_pocoElement).Value);
                else if ((_pocoElement is dstu2.Hl7.Fhir.Model.Date))
                    return FluentPath.PartialDateTime.Parse(((dstu2.Hl7.Fhir.Model.Date)_pocoElement).Value);
                else if (_pocoElement is Primitive)
                    return ((Primitive)_pocoElement).ObjectValue;
                else
                    return null;
            }
        }

        public string TypeName
        {
            get
            {
#if DEBUG
                Console.WriteLine("Read TypeName '{0}' for Element '{1}' (value '{2}')".FormatWith(_mapping.Name, Name, Value ?? "(nothing)"));
#endif

                return _pocoElement.TypeName;
                //return _mapping.Name;
            }
        }


        public ElementNavigator Next { get; private set; }

        private List<ElementNavigator> _children;

        public List<ElementNavigator> Children()
        {
            // Cache children
            if (_children != null) return _children;

            _children = new List<ElementNavigator>();

            ElementNavigator previousChild = null;

            foreach (var item in _mapping.PropertyMappings)
            {
                // Don't expose "value" as a child, that's our ValueProvider.Value (if we're a primitive)
                if (item.IsPrimitive)
                    continue;

                var itemValue = item.GetValue(_pocoElement);

                if (itemValue != null)
                {
                    if (item.IsCollection)
                    {
                        foreach (var colItem in (itemValue as System.Collections.IList))
                        {
                            if (colItem != null)
                            {
                                _children.Add(new ElementNavigator(item.Name, (Base)colItem));
                                if (previousChild != null)
                                    previousChild.Next = _children.Last();
                                previousChild = _children.Last();
                            }
                        }
                    }
                    else
                    {
                        _children.Add(new ElementNavigator(item.Name, (Base)itemValue));
                        if (previousChild != null)
                            previousChild.Next = _children.Last();
                        previousChild = _children.Last();
                    }
                }
            }

            return _children;
        }
    }
}
