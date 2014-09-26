using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Model
{
    public static class UriHelper
    {
        public static Uri BASEPROFILE = new Uri("http://hl7.org/fhir/Profile");
        
        public static Uri BaseProfileUriFor(string type)
        {
            return new Uri(BASEPROFILE+ "/" + type);
        }

        public static bool IsBaseProfile(Uri uri)
        {
            return (BASEPROFILE.IsBaseOf(uri));
        }

        /*
        public static Uri ResolvingUri(TypeRef typeref)
        {
            if (typeref.ProfileUri == null)
            {
                return BaseProfileUriFor(typeref.Code);
            }
            else
            {
                return new Uri(typeref.ProfileUri);
            }
        }
        */

        public static Uri Identify(Structure structure, TypeRef typeref)
        {
            string name = typeref.Code;
            Uri uri;

            if ((name == "ResourceReference"))
            {
                uri = BaseProfileUriFor(name);
            }
            else if (typeref.ProfileUri != null)
            {
                
                if (typeref.ProfileUri.StartsWith("#"))
                {
                    uri = new Uri(structure.ProfileUri + typeref.ProfileUri);
                }
                else if (typeref.ProfileUri != null)
                {
                    uri = new Uri(typeref.ProfileUri);
                }
                else 
                {
                    uri = BaseProfileUriFor(name);
                }
            }
            else // typeref.profileuri == null
            {
                uri = BaseProfileUriFor(name);
            }

            return uri;
        }

        public static Uri Identify(Structure structure)
        {
            string name = structure.Name ?? structure.Type;
            if (IsBaseProfile(structure.ProfileUri))
            {
                return BaseProfileUriFor(name);
            }
            else
            {
                return AddAnchor(structure.ProfileUri, name);
            }
            
        }

        public static bool HasAnchor(this Uri uri)
        {
            string s = uri.ToString();
            int p = s.IndexOf('#');
            return p >= 0;
        }
        public static Uri RemoveAnchor(this Uri uri)
        {
            string s = uri.ToString();
            int p = s.IndexOf('#');
            if (p >= 0) 
            {
                s = s.Remove(p);
            }
            return new Uri(s);
        }

        public static Uri AddAnchor(this Uri uri, string anchor)
        {
            Uri u = RemoveAnchor(uri);
            u = new Uri(uri.ToString() + "#" + anchor);
            return u;
        }

        public static void SetStructureIdentification(Structure structure, Uri uri)
        {
            structure.ProfileUri = uri.RemoveAnchor();
            if (HasAnchor(uri))
            {
                structure.Uri = uri;
            }
            else
            {
                structure.Uri = Identify(structure);
            }
        }

        public static bool Equal(Uri A, Uri B)
        {
            // Uri == Uri werkt niet! Want dan wordt de anchor er af gehaald!

            return A.ToString() == B.ToString();
        }

        public static void SetStructureIdentification(IEnumerable<Structure> structures, Uri uri)
        {
            foreach(Structure structure in structures)
            {
                SetStructureIdentification(structure, uri);
            }
        }

        public static void SetTypeRefIdentification(Structure structure, TypeRef typeref)
        {
            typeref.Uri = Identify(structure, typeref);
        }

    }


}
