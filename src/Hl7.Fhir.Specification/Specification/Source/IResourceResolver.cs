/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.IO;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Specification.Source
{
    public interface IResourceResolver // open ended domain
    {
        /// <summary>
        /// Find resources based on its relative or absolute uri.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        Resource ResolveByUri(string uri);


        /// <summary>
        /// Find a (conformance) resource based on its canonical uri
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        Resource ResolveByCanonicalUri(string uri);
    }

}
