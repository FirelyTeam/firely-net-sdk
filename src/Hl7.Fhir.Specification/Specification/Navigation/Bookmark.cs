/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Specification.Navigation
{
    public struct Bookmark
    {
        // [WMR 20160720] Changed to internal, for encapsulation
        internal object data;

        // [WMR 20160720] NEW
        public bool IsEmpty => data == null;

        // Singleton
        public static readonly Bookmark Empty = new Bookmark();

        // [WMR 20160802] NEW
        internal static Bookmark FromElement(ElementDefinition element)
        {
            if (element == null) throw Error.ArgumentNull(nameof(element));
            return new Bookmark() { data = element };
        }
    }
}
