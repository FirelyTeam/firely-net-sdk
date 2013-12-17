/*
  Copyright (c) 2011-2013, HL7, Inc.
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

namespace Hl7.Fhir.Support
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public sealed class NotMappedAttribute : Attribute
    {
        // This is a positional argument
        public NotMappedAttribute()
        {
            // This attribute is just a marker, no functionality or data
        }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class FhirTypeAttribute : Attribute
    {
        readonly string name;

        public FhirTypeAttribute()
        {
            // No arg constructor - use defaults
        }

        public FhirTypeAttribute(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return name; }
        }

        public string Profile { get; set; }

        public bool IsResource { get; set; }
    }


    [AttributeUsage(AttributeTargets.Enum, Inherited = false, AllowMultiple = false)]
    public sealed class FhirEnumerationAttribute : Attribute
    {
        readonly string bindingName;

        // This is a positional argument
        public FhirEnumerationAttribute(string bindingName)
        {
            this.bindingName = bindingName;
        }

        public string BindingName
        {
            get { return bindingName; }
        }
    }


    /// <summary>
    /// Xml Serialization used for primitive values
    /// </summary>
    public enum XmlSerializationHint
    {
        None,
        Attribute, 
        TextNode,
        XhtmlElement
    }


    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class FhirElementAttribute : Attribute
    {
        readonly string name;

        // This is a positional argument
        public FhirElementAttribute(string name)
        {
            this.name = name;
            this.XmlSerialization = XmlSerializationHint.None;
        }

        public string Name
        {
            get { return name; }
        }

        public bool IsPrimitiveValue { get; set; }

        public XmlSerializationHint XmlSerialization { get; set; }

        public int Order { get; set; }
    }



    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class EnumLiteralAttribute : Attribute
    {
        readonly string literal;

        // This is a positional argument
        public EnumLiteralAttribute(string literal)
        {
            this.literal = literal;
        }

        public string Literal
        {
            get { return literal; }
        }
    }


    public enum WildcardChoice
    {
        AnyResource,
        AnyDatatype
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class ChoiceAttribute : Attribute
    {
        // This is a positional argument
        public ChoiceAttribute(string typeName, Type type)
        {
            TypeName = typeName;
            Type = type;
            Wildcard = null;
        }

        public ChoiceAttribute(WildcardChoice choice)
        {
            Wildcard = choice;
        }

        public WildcardChoice? Wildcard { get; private set; }

        public string TypeName { get; private set; }

        public Type Type { get; private set; }
    }
}
