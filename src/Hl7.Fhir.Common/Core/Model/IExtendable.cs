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



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Model
{
    public interface IExtendable
    {
        List<Extension> Extension { get; set; }
    }

    public interface IModifierExtendable : IExtendable
    {
        List<Extension> ModifierExtension { get; set; }
    }

    public static class ExtensionExtensions
    {
        public static IEnumerable<Extension> AllExtensions(this IExtendable extendable)
        {
            if (extendable is IModifierExtendable)
                return ((IModifierExtendable)extendable).ModifierExtension.Concat(extendable.Extension);
            else
                return extendable.Extension;
        }

        /// <summary>
        /// Return the first extension with the given uri, or null if none was found
        /// </summary>
        /// <param name="extendable"></param>
        /// <param name="uri"></param>
        /// <returns>The first uri, or null if no extension with the given uri was found.</returns>
        public static Extension GetExtension(this IExtendable extendable, string uri)
        {
            return extendable.AllExtensions().FirstOrDefault(ext => ext.Url == uri);
        }


        /// <summary>
        /// Find all extensions with the given uri.
        /// </summary>
        /// <param name="extendable"></param>
        /// <param name="uri"></param>
        /// <returns>The list of extensions with a matching uri, or empty list if none were found.</returns>
        public static IEnumerable<Extension> GetExtensions(this IExtendable extendable, string uri)
        {
            return extendable.AllExtensions().Where(ext => ext.Url == uri);
        }


        /// <summary>
        /// Find all extensions with the given uri.
        /// </summary>
        /// <param name="extendable"></param>
        /// <param name="uri"></param>
        /// <returns>The list of extensions with a matching uri, or empty list if none were found.</returns>
        /// <remarks>If multiple extensions with the same uri are found, this function returns the first modifier extensions,
        /// otherwise the first normal extension.</remarks>        
        public static IEnumerable<Extension> GetExtensions(this IModifierExtendable extendable, string uri)
        {
            return extendable.AllExtensions().Where(ext => ext.Url == uri);
        }

       
        /// <summary>
        /// Return the first extension with the given uri as a string, or null if none was found
        /// </summary>
        /// <param name="extendable"></param>
        /// <param name="uri"></param>
        /// <returns>The first uri, or null if no extension with the given uri was found.</returns>
        public static string GetStringExtension(this IExtendable extendable, string uri)
        {
            var ext = extendable.GetExtension(uri);

            if (ext != null && ext.Value != null && ext.Value is FhirString)
                    return ((FhirString)ext.Value).Value;

            return null;
        }


        public static bool? GetBoolExtension(this IExtendable extendable, string uri)
        {
            var ext = extendable.GetExtension(uri);

            if (ext != null && ext.Value != null && ext.Value is FhirBoolean)
                return ((FhirBoolean)ext.Value).Value;

            return null;
        }


        public static int? GetIntegerExtension(this IExtendable extendable, string uri)
        {
            var value = extendable.GetExtensionValue<Integer>(uri);

            if (value != null)
                return value.Value;
            else
                return null;
        }


        public static T GetExtensionValue<T>(this IExtendable extendable, string uri) where T : Element
        {
            var ext = extendable.GetExtension(uri);

            if (ext != null && ext.Value != null && ext.Value is T)
                return (T)ext.Value;

            return null;
        }


        public static void SetIntegerExtension(this IExtendable extendable, string uri, int value)
        {
            extendable.SetExtension(uri, new Integer(value));
        }

        public static void SetStringExtension(this IExtendable extendable, string uri, string value)
        {
            extendable.SetExtension(uri, new FhirString(value));
        }

        public static void SetBoolExtension(this IExtendable extendable, string uri, bool value)
        {
            extendable.SetExtension(uri, new FhirBoolean(value));
        }


        public static bool HasExtensions(this IExtendable extendable)
        {
            return !extendable.Extension.IsNullOrEmpty();
        }

        public static bool HasExtensions(this IModifierExtendable extendable)
        {
            return !extendable.Extension.IsNullOrEmpty() && !extendable.ModifierExtension.IsNullOrEmpty();
        }


        /// <summary>
        /// Add an extension with the given uri and value
        /// </summary>
        /// <param name="extendable"></param>
        /// <param name="uri"></param>
        /// <param name="value"></param>
        /// <param name="isModifier"></param>
        /// <returns>The newly added Extension</returns>
        public static Extension AddExtension(this IExtendable extendable, string uri, Element value, bool isModifier=false)
        {
            var newExtension = new Extension() { Url = uri, Value = value };

            if (isModifier == true && extendable is IModifierExtendable)
                ((IModifierExtendable)extendable).ModifierExtension.Add(newExtension);
            else
                extendable.Extension.Add(newExtension);

            return newExtension;
        }


        /// <summary>
        /// Remove all extensions with the current uri, if any.
        /// </summary>
        /// <param name="extendable"></param>
        /// <param name="uri"></param>
        public static void RemoveExtension(this IExtendable extendable, string uri)
        {
            var remove = extendable.Extension.Where(ext => ext.Url == uri).ToList();
           
            foreach(var ext in remove)
                extendable.Extension.Remove(ext);

            if (extendable is IModifierExtendable)
            {
                var me = (IModifierExtendable)extendable;
                remove = me.ModifierExtension.Where(ext => ext.Url == uri).ToList();

                foreach (var ext in remove)
                    me.ModifierExtension.Remove(ext);
            }
        }


        /// <summary>
        /// Add an extension with the given uri and value, removing any pre-existsing extensions
        /// with the same uri.
        /// </summary>
        /// <param name="extendable"></param>
        /// <param name="uri"></param>
        /// <param name="value"></param>
        /// <param name="isModifier"></param>
        /// <returns>The newly added extension</returns>
        public static Extension SetExtension(this IExtendable extendable, string uri, Element value, bool isModifier=false)
        {
            if (extendable.Extension == null)
                extendable.Extension = new List<Extension>();

            RemoveExtension(extendable, uri);

            return AddExtension(extendable, uri, value, isModifier);
        }
    }
}


