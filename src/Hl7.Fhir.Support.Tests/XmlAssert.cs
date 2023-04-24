/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml.Linq;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.Tests
{
    public class XmlAssert
    {
        public static void AreSame(string filename, XDocument expected, XDocument actual, bool ignoreSchemaLocation = false)
        {
            areSame(actual.Root.Name.LocalName, expected.Root, actual.Root, ignoreSchemaLocation);
        }

        public static void AreSame(string filename, string expected, string actual, bool ignoreSchemaLocation = false)
        {
            XDocument exp = SerializationUtil.XDocumentFromXmlText(expected);
            XDocument act = SerializationUtil.XDocumentFromXmlText(actual);

            AreSame(filename, exp, act, ignoreSchemaLocation);
        }

        private static void areSame(string context, XElement expected, XElement actual, bool ignoreSchemaLocation)
        {
            if (expected.Name.ToString() != actual.Name.ToString())
                throw new AssertFailedException(String.Format("Expected element '{0}', actual '{1}' at '{2}'",
                    expected.Name.ToString(), actual.Name.ToString(), context));

            bool mustCheckMe(XAttribute a) => a.IsNamespaceDeclaration == false &&
                            (!ignoreSchemaLocation || a.Name != XmlNs.XSCHEMALOCATION);

            if (expected.Attributes().Where(mustCheckMe).Count() !=
                    actual.Attributes().Where(mustCheckMe).Count())
                throw new AssertFailedException(
                    String.Format("Number of attributes are not the same in element '{0}'", context));

            foreach (XAttribute attr in expected.Attributes().Where(mustCheckMe))
            {
                if (actual.Attribute(attr.Name) == null)
                    throw new AssertFailedException(
                        String.Format("Expected attribute '{0}' not found in element '{1}'", attr.Name.ToString(),
                                        context));

                AssertAreTheSame(context + "." + attr.Name, attr.Value, actual.Attribute(attr.Name).Value);
            }

            if (expected.Elements().Count() != actual.Elements().Count())
            {

                var diffElements = expected.Elements().Count() > actual.Elements().Count()
                    ? expected.Elements().ExceptByProperty(actual.Elements(), x => x.Name.LocalName).Select(a => a.Name.LocalName)
                    : actual.Elements().ExceptByProperty(expected.Elements(), x => x.Name.LocalName).Select(a => a.Name.LocalName);
                throw new AssertFailedException(
                    String.Format("Number of child elements are not the same at '{0}'. Difference: {1}", context, string.Join(',', diffElements)));
            }

            //int elemNr = 0;

            //var result = expected.Elements().Zip(actual.Elements(), (ex,ac)=> { areSame(context + "." + ex.Name.LocalName +
            //        String.Format("[{0}]",elemNr++), ex,ac); return true; } );

            var expectedList = expected.Elements().ToArray();
            var actualList = actual.Elements().ToArray();

            string currentName = "";
            int counter = 0;

            for (int elemNr = 0; elemNr < expectedList.Count(); elemNr++)
            {
                var ex = expectedList[elemNr];
                var ac = actualList[elemNr];

                if (ex.Name.LocalName != currentName)
                {
                    currentName = ex.Name.LocalName;
                    counter = 0;
                }

                areSame(context + "." + ex.Name.LocalName + String.Format("[{0}]", counter), ex, ac, ignoreSchemaLocation);
                counter++;
            }
        }


        public static void AssertAreTheSame(string context, string expected, string actual)
        {
            if (context.EndsWith("meta[0].lastUpdated[0].value") || context.EndsWith("timestamp[0].value"))
            {
                // hack: ignore meta.lastUpdated and bundle.timestamp values

                if (P.DateTime.TryParse(actual, out var actualValue) &&
                   P.DateTime.TryParse(expected, out var expectedValue))
                {
                    if (!actualValue.Equals(expectedValue))
                    {
                        throw new AssertFailedException(
                   String.Format("Attributes are not the same at {0}. Expected: '{1}', actual '{2}'",
                       context, expected, actual));
                    }
                    return;
                }
            }
            // Hack for timestamps, binaries and narrative html
            if (expected.EndsWith("+00:00")) expected = expected.Replace("+00:00", "Z");
            if (actual.EndsWith("+00:00")) actual = actual.Replace("+00:00", "Z");
            if (expected.Contains(".000+")) expected = expected.Replace(".000+", "+");
            if (actual.Contains(".000+")) actual = actual.Replace(".000+", "+");
            if (expected.Contains(".000Z")) expected = expected.Replace(".000Z", "Z");
            if (actual.Contains(".000Z")) actual = actual.Replace(".000Z", "Z");
            actual = actual.Replace("\n", "");
            actual = actual.Replace("\r", "");
            actual = actual.Replace("\t", "");
            expected = expected.Replace("\n", "");
            expected = expected.Replace("\r", "");
            expected = expected.Replace("\t", "");
            expected = expected.Trim();
            actual = actual.Trim();
            expected = expected.Replace(" ", "");
            actual = actual.Replace(" ", "");

            if (!Object.Equals(expected, actual))
            {
                throw new AssertFailedException(
                    String.Format("Attributes are not the same at {0}. Expected: '{1}', actual '{2}'",
                        context, expected, actual));
            }
        }
    }

    public static class EnumerableExtensions
    {
        public static IEnumerable<TSource> ExceptByProperty<TSource, TProperty>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TProperty> keySelector)
        {
            return first.ExceptBy(second, x => x, GenericComparer<TSource, TProperty>.Comparer(keySelector));
        }

    }

    public sealed class GenericComparer<T, TProperty> : IEqualityComparer<T>
    {
        public static IEqualityComparer<T> Comparer(Func<T, TProperty> selector)
        {
            return new GenericComparer<T, TProperty>(selector);
        }

        private readonly Func<T, TProperty> selector;

        public GenericComparer(Func<T, TProperty> selector)
        {
            this.selector = selector;
        }

        public bool Equals(T x, T y)
        {
            if (x == null || y == null) return false;

            return Equals(selector(x), selector(y));
        }

        public int GetHashCode([DisallowNull] T obj)
        {
            object value = selector(obj);

            if (value == null) return obj.GetHashCode();

            return value.GetHashCode();
        }
    }
}
