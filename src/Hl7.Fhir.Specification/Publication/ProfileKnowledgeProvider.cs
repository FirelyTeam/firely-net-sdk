using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;

namespace Hl7.Fhir.Publication
{
    internal class ProfileKnowledgeProvider
    {
        private StructureLoader _loader;

        internal ProfileKnowledgeProvider(string baseUrl)
        {
            _loader = new StructureLoader(ArtifactResolver.CreateCachedDefault());
            _baseUrl = baseUrl;
        }

        internal Model.Profile.ProfileExtensionDefnComponent getExtensionDefinition(Model.Profile profile, string url)
        {
            if (url.StartsWith("#"))
            {
                var extAnchor = url.Substring(1);
                return profile.ExtensionDefn.Where(ext => ext.Code == extAnchor).FirstOrDefault();
            }
            else
                return _loader.LocateExtension(new Uri(url));
        }

        string _baseUrl;

        internal string getLinkFor(string typename)
        {
            //TODO: Make this dependent on the DSTU1/2 etc website
            //TODO: There are more flavours (like narrative, extension, etc.)
            if (isDataType(typename) || isPrimitive(typename))
                return _baseUrl + "datatypes.html#" + typename.ToLower();
            else if(ModelInfo.IsKnownResource(typename))
                return _baseUrl + typename.ToLower() + ".html";
            else if(typename == "Extension")
                return _baseUrl + "extensibility.html#Extension";
            else
                return "todo.html";
        }


        //TODO: Determine dynamically based on core profiles?
        internal bool isDataType(String value)
        {
            return new[] { "Identifier", "HumanName", "Address", "ContactPoint", "Timing", "Quantity", "Attachment", "Range",
                  "Period", "Ratio", "CodeableConcept", "Coding", "SampledData", "Age", "Distance", "Duration", "Count", "Money" }.Contains(value);
        }

        internal bool isReference(String value)
        {
            return value == "ResourceReference";
        }

        internal bool isPrimitive(String value)
        {
            return new[] { "boolean", "integer", "decimal", "base64Binary", "instant", "string", "date", "dateTime", "code", "oid", "uuid", "id" }.Contains(value);
        }

        internal string getLinkForExtension(Model.Profile profile, string url)
        {
            return "todo.html";
            //String fn;
            //String code;
            //if (url.StartsWith("#"))
            //{
            //    code = url.Substring(1);
            //}
            //else
            //{
            //    String[] path = url.Split("#");
            //    code = path[1];
            //    profile = definitions.getProfileByURL(path[0]);
            //}

            //if (profile != null)
            //{
            //    fn = (String)profile.getTag("filename");
            //    return Utilities.changeFileExt(fn, ".html");
            //}
            //return null;
        }

        internal string getLinkForProfile(Model.Profile profile, string p)
        {
            return "todo.html" + "|" + profile.Name;
            //        String fn;
            //if (!url.startsWith("#")) {
            //  String[] path = url.split("#");
            //  profile = definitions.getProfileByURL(path[0]);
            //  if (profile == null && url.startsWith("Profile/"))
            //    return "hspc-"+url.substring(8)+".html|"+url.substring(8);
            //}
            //if (profile != null) {
            //  fn = profile.getTag("filename")+"|"+profile.getNameSimple();
            //  return Utilities.changeFileExt(fn, ".html");
            //}
            //return null;
        }



        internal string resolveBinding(Model.Profile.ElementDefinitionBindingComponent elementDefinitionBindingComponent)
        {
            return "todo.html";
            //  if (binding.getReference() == null)
            //    return null;
            //  if (binding.getReference() instanceof UriType) {
            //    String ref = ((UriType) binding.getReference()).getValue();
            //    if (ref.startsWith("http://hl7.org/fhir/v3/vs/"))
            //      return "v3/"+ref.substring(26)+"/index.html";
            //    else
            //      return ref;
            //  } else {
            //    String ref = ((Reference) binding.getReference()).getReferenceSimple();
            //    if (ref.startsWith("ValueSet/")) {
            //      ValueSet vs = definitions.getValuesets().get(ref.substring(8));
            //      if (vs == null)
            //        return ref.substring(9)+".html";
            //      else
            //        return (String) vs.getTag("filename");
            //    } else if (ref.startsWith("http://hl7.org/fhir/vs/")) {
            //      if (new File(Utilities.path(folders.dstDir, "valueset-"+ref.substring(23)+".html")).exists())
            //        return "valueset-"+ref.substring(23)+".html";
            //      else
            //        return ref.substring(23)+".html";
            //    }  else if (ref.startsWith("http://hl7.org/fhir/v3/vs/"))
            //      return "v3/"+ref.substring(26)+"/index.html"; 
            //    else
            //      return ref;
            //  } 
        }

        internal bool hasLinkFor(string typeRefCode)
        {
            return isDataType(typeRefCode) || isPrimitive(typeRefCode) || typeRefCode == "Extension" || ModelInfo.IsKnownResource(typeRefCode);
        }
    }

}
