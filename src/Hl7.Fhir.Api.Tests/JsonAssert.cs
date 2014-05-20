/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Hl7.Fhir.Serialization;
using Newtonsoft.Json;
using System.IO;

namespace Hl7.Fhir.Test
{
    public class JsonAssert
    {
        public static void AreSame(JObject expected, JObject actual)
        {
            areSame(expected.Root, actual.Root);
        }

        public static void AreSame(string expected, string actual)
        {
            JObject exp = JObject.Parse(expected);
            JObject act = JObject.Parse(actual);

            AreSame(exp, act);
        }

        private static void areSame(JToken left, JToken right)
        {
            if (left.Type != right.Type)
                throw new AssertFailedException("Token type is not the same at " + right.Path);

            if (left.Type == JTokenType.Array)
            {
                var la = (JArray)left;
                var ra = (JArray)right;

                if(la.Count != ra.Count)
                    throw new AssertFailedException("Array size is not the same at " + right.Path);

                for(var i=0; i<la.Count; i++)
                    areSame(la[i],ra[i]);
            }

            else if (left.Type == JTokenType.Object)
            {
                var lo = (JObject)left;
                var ro = (JObject)right;

                if (lo.Count != ro.Count)
                    throw new AssertFailedException("Object does not have same membercount at " + right.Path);

                foreach (var lMember in lo)
                {
                    JToken rMember;

                    if (!ro.TryGetValue(lMember.Key, out rMember))
                        throw new AssertFailedException(String.Format("Expected member {0} not found in actual at " + left.Path, lMember.Key));

                    areSame(lMember.Value, rMember);
                }
            }

            else
            {
               if(!JToken.DeepEquals(left, right))
                   throw new AssertFailedException(String.Format("Values not the same at " + left.Path));
            }
        }

      
    }
}
