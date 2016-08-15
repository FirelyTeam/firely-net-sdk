/*
  Copyright (c) 2011-2012, HL7, Inc
  All rights reserved.
  
  Redistribution and use in source and binary forms, with or without modification, 
  are permitted provided that the following conditions are met:
  
   * Redistributions of source code must retain the above copyright notice, this 
     list of conditions and the following disclaimer.
   * Redistributions in binary form must reproduce the above copyright notice, 
     this list of conditions and the following disclaimer in the documentation 
     and/or other materials provided with the distribution.
   * Neither the name of HL7 nor the names of its contributors may be used to 
     endorse or promote products derived from this software without specific 
     prior written permission.
  
  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
  IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
  POSSIBILITY OF SUCH DAMAGE.
  
*/

using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.FluentPath.Support;
using Hl7.ElementModel;

namespace Hl7.Fhir.FluentPath
{
    public class ModelNavigator : IElementNavigator
    {
        public static IEnumerable<IValueProvider> CreateInput(Base model)
        {
            return new List<IValueProvider>() { new ModelNavigator(model) };
        }

        public ModelNavigator(Base model)
        {
            if (model == null) throw new ArgumentNullException("model");

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
        /// Returns 
        /// </summary>
        public Base FhirValue
        {
            get
            {
#if DEBUGX
                Console.WriteLine("    -> Read Value of {0}: {1}", _current.Name, _current.Value);
#endif
                return _current.FhirValue;
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
