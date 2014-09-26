/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hl7.Fhir.Specification
{
    public enum StructureForm
    {
        Snapshot,
        Differential
    }


    public static class ProfileExtensionExtensions
    {
        //public const string STRUCTURE_PROFILE_URI = "http://fhir.furore.com/Profiles/navigation-extensions#profile-location";
        public const string STRUCTURE_FORM = "http://hl7.org/fhir/Profile/tools-extensions#profile-structure-form";
        public const string STRUCTURE_BASE_URI = "http://hl7.org/fhir/Profile/tools-extensions#profile-base";

        //
        // STRUCTURE_FORM
        //
        public static StructureForm? GetStructureForm(this Profile.ProfileStructureComponent structure)
        {
            var val = structure.GetExtensionValue(STRUCTURE_FORM) as Code;
            StructureForm? form = null;
            if(val != null) form = (StructureForm)Enum.Parse(typeof(StructureForm), val.Value);

            return form;
        }

        public static void SetStructureForm(this Profile.ProfileStructureComponent structure, StructureForm form)
        {
            structure.SetExtension(STRUCTURE_FORM, new Code(form.ToString()));        
        }

        public static void RemoveStructureForm(this Profile.ProfileStructureComponent structure)
        {
            structure.RemoveExtension(STRUCTURE_FORM);
        }


        //
        // STRUCTURE_BASE_URI
        //
        public static string GetStructureBaseUri(this Profile.ProfileStructureComponent structure)
        {
            var val = structure.GetExtensionValue(STRUCTURE_BASE_URI) as FhirUri;
            return val != null ? val.Value : null;
        }

        public static void SetStructureBaseUri(this Profile.ProfileStructureComponent structure, string baseUri)
        {
            structure.SetExtension(STRUCTURE_BASE_URI, new FhirUri(baseUri));
        }

        public static void RemoveStructureBaseUri(this Profile.ProfileStructureComponent structure)
        {
            structure.RemoveExtension(STRUCTURE_BASE_URI);
        }

        
        //public static string GetProfileLocation(this Element structure)
        //{
        //    var val = structure.GetExtensionValue(STRUCTURE_PROFILE_URI) as FhirUri;
        //    return val != null ? val.Value : null;
        //}

        //public static string GetProfileLocation(this Profile profile)
        //{
        //    var val = profile.GetExtensionValue(STRUCTURE_PROFILE_URI) as FhirUri;
        //    return val != null ? val.Value : null;
        //}


        //public static void SetProfileLocation(this Element structure, string profileAddress)
        //{
        //    structure.SetExtension(STRUCTURE_PROFILE_URI, new FhirUri(profileAddress));
        //}

        //public static void RemoveProfileLocation(this Element structure)
        //{
        //    structure.RemoveExtension(STRUCTURE_PROFILE_URI);
        //}

        //public static void SetProfileLocation(this Profile profile, Uri profileUri)
        //{
        //    var uri = profileUri.ToString();

        //    profile.SetExtension(STRUCTURE_PROFILE_URI, new FhirUri(profileUri));

        //    if (profile.Structure != null)
        //        foreach (var structure in profile.Structure)
        //            structure.SetProfileLocation(uri);

        //    if (profile.Extension != null)
        //        foreach (var extension in profile.Extension)
        //            extension.SetProfileLocation(uri);
        //}

        //public static void RemoveProfileLocation(this Profile profile)
        //{
        //    profile.RemoveExtension(STRUCTURE_PROFILE_URI);

        //    if (profile.Structure != null)
        //        foreach (var structure in profile.Structure)
        //            structure.RemoveProfileLocation();

        //    if (profile.Extension != null)
        //        foreach (var extension in profile.Extension)
        //            extension.RemoveProfileLocation();
        //}
    }
}

