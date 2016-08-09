/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Model
{

    public class TypeRef : IEquatable<TypeRef>
    {
        public TypeRef(FHIRDefinedType code, string profileUri = null)
        {
            this.Code = code;

            if(profileUri != null)
                this.Profile = new Uri(profileUri);
        }
        
        public FHIRDefinedType Code;

        public Uri Profile { get; set; }
        
        public bool Unresolved
        {
            get
            {
                return (Structure == null);
            }
        }
        public Structure Structure { get; set; }
        public override string ToString()
        {
            
            if (Profile != null)
            {
                return string.Format("{0} ({1})", Code, Profile);
            }
            else 
            {
                return Code.GetLiteral();
            }
        }

        public bool Equals(TypeRef other)
        {
            return other.Code == this.Code && other.Profile == this.Profile;
        }
    }
}
