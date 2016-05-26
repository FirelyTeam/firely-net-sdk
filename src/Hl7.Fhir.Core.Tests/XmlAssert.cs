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
            XDocument exp = SerializationUtil.XDocumentFromXmlText(expected);
            XDocument act = SerializationUtil.XDocumentFromXmlText(actual);

            AreSame(exp, act);
        }

        private static void areSame(string context, XElement expected, XElement actual)
        {
            //if (expected.Name.ToString() != actual.Name.ToString())
            //    throw new AssertFailedException(String.Format("Expected element '{0}', actual '{1}' at '{2}'",
            //        expected.Name.ToString(), actual.Name.ToString(), context));

            if (expected.Attributes().Count() != actual.Attributes().Count())
                throw new AssertFailedException(
                    String.Format("Number of attributes are not the same in element '{0}'",context));

            foreach (XAttribute attr in expected.Attributes())
            {
                if (actual.Attribute(attr.Name) == null)
                    throw new AssertFailedException(
                        String.Format("Expected attribute '{0}' not found in element '{1}'", attr.Name.ToString(),
                                        context));

                AssertAreTheSame(context + "." + attr.Name, attr.Value, actual.Attribute(attr.Name).Value);
            }

            if (expected.Elements().Count() != actual.Elements().Count())
                    throw new AssertFailedException(
                        String.Format("Number of child elements are not the same at '{0}'", context));

            //int elemNr = 0;

            //var result = expected.Elements().Zip(actual.Elements(), (ex,ac)=> { areSame(context + "." + ex.Name.LocalName +
            //        String.Format("[{0}]",elemNr++), ex,ac); return true; } );

            var expectedList = expected.Elements().ToArray();
            var actualList = actual.Elements().ToArray();

            string currentName = "";
            int counter = 0;

            for(int elemNr=0; elemNr < expectedList.Count(); elemNr++)
            {
                var ex = expectedList[elemNr];
                var ac = actualList[elemNr];

                if(ex.Name.LocalName != currentName)
                {
                    currentName = ex.Name.LocalName;
                    counter = 0;
                }

                areSame(context + "." + ex.Name.LocalName + String.Format("[{0}]", counter), ex, ac);
                counter++;
            }
        }


        public static void AssertAreTheSame(string context, string expected, string actual)
        {
            // Hack for timestamps, binaries and narrative html
            if (expected.EndsWith("+00:00")) expected = expected.Replace("+00:00", "Z");
            if (actual.EndsWith("+00:00")) actual = actual.Replace("+00:00", "Z");
            if (expected.Contains(".000+")) expected = expected.Replace(".000+", "+");
            if (actual.Contains(".000+")) actual = actual.Replace(".000+", "+");
            actual = actual.Replace("\n", "");
            actual = actual.Replace("\r", "");
            actual = actual.Replace(" ", "");
            expected = expected.Replace("\n", "");
            expected = expected.Replace("\r", "");
            expected = expected.Replace(" ", "");

            if (!Object.Equals(expected, actual))
            {
                throw new AssertFailedException(
                    String.Format("Attributes are not the same at {0}. Expected: '{1}', actual '{2}'",
                        context, expected, actual));
            }
        }
    }
}
