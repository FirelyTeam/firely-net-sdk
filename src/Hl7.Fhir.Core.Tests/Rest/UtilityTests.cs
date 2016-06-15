﻿/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System.Net;
using System.IO;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System.Threading.Tasks;

namespace Hl7.Fhir.Tests.Rest
{
    [TestClass]
#if PORTABLE45
	public class PortableUtilityTests
#else
    public class UtilityTests
#endif
    {
#if !PORTABLE45
        [TestMethod]
        public void GetResourceFormatSupportsCharset()
        {
            Assert.AreEqual(ContentType.GetResourceFormatFromContentType("text/xml;charset=ISO-8859-1"), ResourceFormat.Xml);
            Assert.AreEqual(ContentType.GetResourceFormatFromContentType("text/xml"), ResourceFormat.Xml);
        }
#endif
    }
}
