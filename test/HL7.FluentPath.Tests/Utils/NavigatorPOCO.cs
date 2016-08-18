using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Hl7.FluentPath.Support;
using Hl7.ElementModel;
using System;

namespace Hl7.FluentPath.Tests
{
    public class ModelNavigator : IElementNavigator
    {
        public static IEnumerable<IValueProvider> CreateInput(Base model)
        {
            return new List<IValueProvider>() { new ModelNavigator(model) };
        }

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


        public string Path
        {
            get
            {
                throw new NotImplementedException();
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
                _current = _current.Children().First();
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
}
