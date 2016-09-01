/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.ElementModel;
using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Support;
using Hl7.FluentPath.Support;

namespace Hl7.Fhir.FluentPath
{
    public class PocoNavigator : IElementNavigator
    {
        public PocoNavigator(Base model)
        {
            if (model == null) throw Error.ArgumentNull("model");

            //_current = new PocoElementNavigator(model.TypeName, model);
            _parentPath = "";

            var me = new PocoElementNavigator(model.TypeName, model);
            _siblings = new List<PocoElementNavigator> { me };
            _index = 0;
        }

        private PocoNavigator()
        {
        }

        private IList<PocoElementNavigator> _siblings;
        private int _index;
        private string _parentPath;

        private PocoElementNavigator Current
        {
            get
            {
                return _siblings[_index];
            }
        }


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
                return Current.Value;
            }
        }

        /// <summary>
        /// Returns 
        /// </summary>
        public Base FhirValue
        {
            get
            {
#if DEBUGX
                Console.WriteLine("    -> Read Value of {0}: {1}", _current.Name, _current.Value);
#endif
                return Current.FhirValue;
            }
        }

        /// <summary>
        /// Return the FHIR TypeName
        /// </summary>
        public string TypeName
        {
            get
            {
                return Current.TypeName; // This needs to be fixed
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
                return Current.Name;
            }
        }

        public string Path
        {
            get
            {
                if (String.IsNullOrEmpty(_parentPath))
                {
                    return Current.Name;
                }
                else
                {
                    int myIndex = _siblings.Where(s => s.Name == Current.Name).ToList().IndexOf(Current);
                    return _parentPath + ".{0}[{1}]".FormatWith(Current.Name, myIndex);
                }
            }
        }

        public bool MoveToFirstChild()
        {
            if (Current.Children().Any())
            {
                _parentPath = Path;
                _siblings = Current.Children().ToList();
                _index = 0;
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
            if(_siblings.Count > _index+1)
            { 
                string oldName = Current.Name;
                _index++;
                string newName = Current.Name;
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
            var result = new PocoNavigator();

            result._siblings = this._siblings;
            result._index = this._index;
            result._parentPath = this._parentPath;
            // Console.WriteLine("Cloning: {0}", this.GetName());
            return result;
        }
    }
}
