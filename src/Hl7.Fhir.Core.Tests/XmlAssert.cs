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
using System.Xml.Linq;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Tests
{
    public class XmlAssert
    {
        public static void AreSame(XDocument expected, XDocument actual)
        {
            areSame(actual.Root.Name.LocalName, expected.Root, actual.Root);
        }

        public static void AreSame(string expected, string actual)
        {
            XDocument exp = FhirParser.XDocumentFromXml(expected);
            XDocument act = FhirParser.XDocumentFromXml(actual);

            AreSame(exp, act);
        }

        private static void areSame(string context, XElement expected, XElement actual)
        {
            if (expected.Name.ToString() != actual.Name.ToString())
                throw new AssertFailedException(String.Format("Expected element '{0}', actual '{1}' at '{2}'",
                    expected.Name.ToString(), actual.Name.ToString(), context));

            if (expected.Attributes().Count() != actual.Attributes().Count())
                throw new AssertFailedException(
                    String.Format("Number of attributes are not the same in element '{0}'",context));

            foreach (XAttribute attr in expected.Attributes())
            {
                if (actual.Attribute(attr.Name) == null)
                    throw new AssertFailedException(
                        String.Format("Expected attribute '{0}' not found in element '{1}'", attr.Name.ToString(),
                                        context));

                if (actual.Attribute(attr.Name).Value != attr.Value)
                    throw new AssertFailedException(
                        String.Format("Attributes '{0}' are not the same at {1}. Expected: '{2}', actual '{3}'",
                            attr.Name.ToString(), context, attr.Value, actual.Value));
            }

            if (expected.Elements().Count() != actual.Elements().Count())
                    throw new AssertFailedException(
                        String.Format("Number of child elements are not the same at '{0}'", context));

            //int elemNr = 0;

            //var result = expected.Elements().Zip(actual.Elements(), (ex,ac)=> { areSame(context + "." + ex.Name.LocalName +
            //        String.Format("[{0}]",elemNr++), ex,ac); return true; } );

            var expectedList = expected.Elements().ToArray();
            var actualList = expected.Elements().ToArray();

            for(int elemNr=0; elemNr < expectedList.Count(); elemNr++)
            {
                var ex = expectedList[elemNr];
                var ac = actualList[elemNr];

                areSame(context + "." + ex.Name.LocalName + String.Format("[{0}]", elemNr), ex, ac);
            }
        }

      
    }
}
