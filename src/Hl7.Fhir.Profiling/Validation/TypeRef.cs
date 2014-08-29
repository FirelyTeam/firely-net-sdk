/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhir.Profiling
{

    public class TypeRef : IEquatable<TypeRef>
    {
        public TypeRef(string code, string profileUri = null)
        {
            this.Code = code;
            this.ProfileUri = profileUri;
        }
        public string Code;
        public string ProfileUri { get; set; }
        public Uri ResolvingUri
        {
            get
            {
                Uri uri;
                if (this.ProfileUri == null)
                {
                    string name = this.Code.ToLower(); // todo: this is a temporary fix!!!
                    uri = new Uri("http://hl7.org/fhir/profile/" + name);
                }
                else
                {
                    uri = new Uri(this.ProfileUri);
                }
                return uri;
            }
        }
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
            
            if (ProfileUri != null)
            {
                return string.Format("{0} ({1})", Code, ProfileUri);
            }
            else 
            {
                return Code;
            }
        }

        public bool Equals(TypeRef other)
        {
            return other.Code == this.Code && other.ProfileUri == this.ProfileUri;
        }
    }
}
