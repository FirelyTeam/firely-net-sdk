/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using System.Text.RegularExpressions;
using System.IO;
using Fhir.IO;
using Fhir.Profiling;

namespace ProfileValidator
{
    // todo: element type reference OR name reference (recursion)
    // todo: ValueSet vs. CodeSystem
    // todo: Element names are profile URI specific (names can overlap with other profiles)
    // todo: Merge Profile class (when possible) with Api.Introspection and Api.ModelInfo
    // todo: parse meaning of [x].
    class Program
    {
        static void ValidateFileSet(string resource_name)
        {
            Validation.Profile profile = new Validation.Profile();
            profile.LoadXMLValueSets("Data\\valuesets.xml");
            profile.LoadXmlFile("Data\\type-HumanName.profile.xml");
            profile.LoadXmlFile("Data\\" + resource_name + ".profile.xml");
            
            Validator validator = new Validator(profile);

            Fhir.Feed feed = FhirFile.LoadXMLFeed("Data\\" + resource_name + "-examples.xml");
            
            Console.Write("\nValidating " + resource_name + " fileset");
            
            foreach (XPathNavigator node in feed.Resources)
            {
                File.AppendAllText("c:\\temp\\output.txt", "------------------ VALIDATING RESOURCE --------------------\n");
                validator.Validate(node);
                validator.Report.Print(@"c:\temp\output.txt", false);
                Console.Write(".");
            }
            Console.WriteLine("done.");
        }

        static void Validate(params string[] resource_names)
        {
            foreach(string resource_name in resource_names)
            {
                ValidateFileSet(resource_name);
            }
        }

        static void Main(string[] args)
        {
            File.Delete("Data\\output.txt");
            Validate("patient");
            Console.ReadKey();
        }
    }
}
