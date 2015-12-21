/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Navigation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hl7.Fhir.Test.Navigation
{
    [TestClass]
    public class LinkedTreeTest
    {
        // Render tree to formatted string
        internal static string RenderTree<T>(T root) where T : IDoublyLinkedTree<T>
        {
            return root.DescendantsAndSelf().Aggregate("",
                (bc, n) => bc + n.Ancestors().Aggregate("",
                                (ac, m) => (m.FollowingSiblings().Any() ? "| " : "  ") + ac,
                                ac => ac + (n.FollowingSiblings().Any() ? "+-" : "\\-")
                            )
                            + n.ToString() + "\n"
            );
        }

        FhirNavigationTree CreateFhirNavigationTree()
        {
            var root = FhirNavigationTree.Create("Patient");

            root.AddFirstChild("identifier")
                    .AddFirstChild("use", "...use...")
                    .AddNextSibling("type", "...type...")
                    .AddNextSibling("system", "...system...")
                    .AddNextSibling("value", "0123456789")
                    .AddNextSibling("period")
                        // .AddFirstChild("start", "20151127 12:00")
                        // .AddNextSibling("end", "20151127 18:00")
                        .AddFirstChild("start", new DateTime(2015, 11, 27, 12, 00, 00))
                        .AddNextSibling("end", new DateTime(2015, 11, 27, 18, 00, 00))
                    .Parent
                    //.AddLastSibling("assigner", "Dr. House")
                    .LastSibling()
                    .AddNextSibling("assigner", "Dr. House")
                .Parent
                //.AddLastSibling("gender", "F")
                .LastSibling()
                .AddNextSibling("gender", "F")
                .AddNextSibling("name")
                    .AddFirstChild("use", "...use...")
                    .AddNextSibling("text", "Prof. Dr. Ir. P. Akkermans MBA")
                    .AddNextSibling("family", "Akkermans")
                    .AddNextSibling("given", "Piet")
                    .AddNextSibling("prefix", "Prof. Dr. Ir.")
                    .AddNextSibling("suffix", "MBA")
                    .AddNextSibling("period")
                        // .AddFirstChild("start", "20151231 14:00")
                        // .AddNextSibling("end", "20151230 12:00");
                        .AddFirstChild("start", new DateTime(2015, 12, 31, 14, 00, 00))
                        .AddNextSibling("end", new DateTime(2015, 12, 30, 12, 00, 00));

            return root;
        }

     
        [TestMethod]
        public void Test_Tree_CreateFromAnonymousObject()
        {

            var Patient =
                new
                {
                    identifier = new
                    {
                        use = "[use]",
                        type = "[type]",
                        system = "[system]",
                        value = "[value]",
                        period = new
                        {
                            start = "[start]",
                            end = "[end]"
                        },
                        assigner = "[assigner]"
                    },
                    gender = "F",
                    name = new
                    {
                        use = "[use]",
                        text = "[text]",
                        family = "[family]",
                        given = "[given]",
                        prefix = "[prefix]",
                        suffix = "[suffix]",
                        period = new
                        {
                            start = "[start]",
                            end = "[end]"
                        }
                    }

                    ,valueString = "string"
                    ,valueInt = 3
                    ,valueBoolean = true // new Model.FhirBoolean(true)
                };

            // var root = LinkedTreeFactory.CreateFromObject("Patient", Patient);
            var root = FhirNavigationTree.Create("Patient");
            root.AddChildrenFromObject(Patient);
            Debug.Print(RenderTree(root));

            Assert.AreEqual(root.Name, "Patient");

            // Test dynamic generation of separately typed node values
            var valueString = root.FirstChild("valueString");
            Assert.IsNotNull(valueString);
            Assert.AreEqual(valueString.ValueType, typeof(string));

            var valueInt = root.FirstChild("valueInt");
            Assert.IsNotNull(valueInt);
            Assert.AreEqual(valueInt.ValueType, typeof(int));

            var valueBoolean = root.FirstChild("valueBoolean");
            Assert.IsNotNull(valueBoolean);
            Assert.AreEqual(valueBoolean.ValueType, typeof(bool));
        }

        [TestMethod]
        public void Test_Tree_CreateFhirNavigationTree()
        {
            var root = CreateFhirNavigationTree();
            // TODO: Assert result...
            Debug.Print(RenderTree(root));
        }

        [TestMethod]
        public void Test_Tree_AddChild()
        {
            var root = FhirNavigationTree.Create("Homer");
            var child = root.AddLastChild("Marge");
            var grandchild = child.AddLastChild("Bart");
            var grandchild2 = child.AddLastChild("Lisa");

            Assert.AreEqual(root.FirstChild, child);
            Assert.IsNull(root.Parent);
            Assert.IsNull(root.PreviousSibling);
            Assert.IsNull(root.NextSibling);

            Assert.AreEqual(child.FirstChild, grandchild);
            Assert.AreEqual(child.Parent, root);
            Assert.IsNull(child.PreviousSibling);
            Assert.IsNull(child.NextSibling);

            Assert.IsNull(grandchild.FirstChild);
            Assert.AreEqual(grandchild.Parent, child);
            Assert.IsNull(grandchild.PreviousSibling);
            Assert.AreEqual(grandchild.NextSibling, grandchild2);

            Assert.IsNull(grandchild2.FirstChild);
            Assert.AreEqual(grandchild2.Parent, child);
            Assert.AreEqual(grandchild2.PreviousSibling, grandchild);
            Assert.IsNull(grandchild2.NextSibling);
        }

        [TestMethod]
        public void Test_Tree_AddSiblings()
        {
            var root = FhirNavigationTree.Create("Homer");
            var s1 = root.AddLastSibling("Marge");
            var s2 = s1.AddLastSibling("Bart");
            var s3 = s2.AddLastSibling("Lisa");

            Assert.IsNull(root.FirstChild);
            Assert.IsNull(root.Parent);
            Assert.IsNull(root.PreviousSibling);
            Assert.AreEqual(root.NextSibling, s1);

            Assert.IsNull(s1.FirstChild);
            Assert.IsNull(s1.Parent);
            Assert.AreEqual(s1.PreviousSibling, root);
            Assert.AreEqual(s1.NextSibling, s2);

            Assert.IsNull(s2.FirstChild);
            Assert.IsNull(s2.Parent);
            Assert.AreEqual(s2.PreviousSibling, s1);
            Assert.AreEqual(s2.NextSibling, s3);

            Assert.IsNull(s3.FirstChild);
            Assert.IsNull(s3.Parent);
            Assert.AreEqual(s3.PreviousSibling, s2);
            Assert.IsNull(s3.NextSibling);
        }

        [TestMethod]
        public void Test_Tree_Children()
        {
            var root = CreateFhirNavigationTree();
            Assert.AreEqual(root.FirstChild.Name, "identifier");
            Assert.AreEqual(root.LastChild().Name, "name");

            var children = root.Children();
            var expected = new string[] { "identifier", "gender", "name" };
            Assert.IsTrue(children.Select(c => c.Name).SequenceEqual(expected));

            children = children.First().Children();
            expected = new string[] { "use", "type", "system", "value", "period", "assigner" };
            Assert.IsTrue(children.Select(c => c.Name).SequenceEqual(expected));
        }

        [TestMethod]
        public void Test_Tree_Descendants()
        {
            var root = CreateFhirNavigationTree();
            var child = root.FirstChild;
            Assert.AreEqual(child.Name, "identifier");

            var descendants = child.Descendants();
            var expected = new string[] { "use", "type", "system", "value", "period", "start", "end", "assigner" };
            Assert.IsTrue(descendants.Select(c => c.Name).SequenceEqual(expected));

            // Test on a single leaf element
            child = child.FirstChild;
            Assert.AreEqual(child.Name, "use");
            Assert.IsNull(child.FirstChild);
            descendants = child.Descendants();
            var l = descendants.ToList();
            bool result = l.Count() == 0; // !descendants.Any();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_Tree_Siblings()
        {
            var root = CreateFhirNavigationTree();
            var child = root.FirstChild.FirstChild;
            Assert.AreEqual(child.Name, "use");

            var siblings = child.FollowingSiblings().ToArray();
            var expected = new string[] { "type", "system", "value", "period", "assigner" };
            Assert.IsTrue(siblings.Select(c => c.Name).SequenceEqual(expected));

            child = child.LastSibling();
            Assert.AreEqual(child.Name, "assigner");
            siblings = child.PrecedingSiblings().ToArray();
            expected = new string[] { "use", "type", "system", "value", "period" };
            Assert.IsTrue(siblings.Select(c => c.Name).SequenceEqual(expected.Reverse()));
        }

        [TestMethod]
        public void Test_Tree_Ancestors()
        {
            var root = CreateFhirNavigationTree();
            var child = root.FirstChild.FirstChild;
            Assert.AreEqual(child.Name, "use");
            child = child.FollowingSiblings().First(n => n.Name == "period");
            Assert.AreEqual(child.Name, "period");
            child = child.FirstChild;
            Assert.AreEqual(child.Name, "start");

            var ancestors = child.Ancestors();
            var expected = new string[] { "Patient", "identifier", "period" };
            Assert.IsTrue(ancestors.Select(c => c.Name).SequenceEqual(expected.Reverse()));

            child = child.NextSibling;
            Assert.AreEqual(child.Name, "end");
            ancestors = child.Ancestors();
            Assert.IsTrue(ancestors.Select(c => c.Name).SequenceEqual(expected.Reverse()));
        }

        [TestMethod]
        public void Test_Tree_SimpleExpression()
        {
            var root = CreateFhirNavigationTree();

            Debug.Print("===== Full tree =====");
            Debug.Print(RenderTree(root));

            var period_starts = root.Descendants().Where(n => n.Name == "start" && n.Parent.Name == "period");

            Assert.IsTrue(period_starts.All(n => n.Name == "start"));
            Assert.IsTrue(period_starts.All(n => n.Parent.Name == "period"));
            Assert.AreEqual(period_starts.Count(), 2);

            Debug.Print("===== period.start nodes: =====");
            foreach (var item in period_starts)
            {
                Debug.Print(item.ToString());
            }

            var start_values = period_starts.OfType<IValueProvider<DateTime>>();
            var maxStart = start_values.Max(n => n.Value);
            var maxNode = start_values.First(n => n.Value == maxStart);
            Debug.Print("Max start = {0}", maxNode.Value);
        }

        [TestMethod]
        public void Test_Tree_LinqExpression()
        {
            var root = CreateFhirNavigationTree();
            var nodeSet = root.ToEnumerable();

            // Test 1: get all descendants

            var result = nodeSet
                .SelectMany(n => n.Descendants());
            var result2 = from node in nodeSet
                          from d in node.Descendants()
                          select d;
            Assert.IsTrue(result.SequenceEqual(result2));

            // Test 2: get all start nodes

            const string startNode = "start";
            result = nodeSet
                .SelectMany(n => n.Descendants())
                .Where(n => n.Name == startNode);

            result2 = from node in nodeSet
                      from d in node.Descendants()
                      where d.Name == startNode
                      select d;
            Assert.IsTrue(result.SequenceEqual(result2));
            Assert.AreEqual(result.Count(), 2);

            // Test 3: get all period nodes having start.value > end.value

            const string periodNode = "period";
            const string endNode = "end";
            result = nodeSet
                .SelectMany(n => n.Descendants())
                .Where(n => n.Name == periodNode)
                .Where(
                    n => DateTime.Compare(
                        n[startNode].FirstOrDefault().GetValue<DateTime>(),
                        n[endNode].FirstOrDefault().GetValue<DateTime>()
                    ) > 0
                );
            Assert.AreEqual(result.Count(), 1);

            // Test 4: filter descendant nodes via path indexer

            result = root["identifier", "period", "start"];
            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.First().Name == startNode);

            result2 = from node in root["identifier", "period", "start"]
                      select node;
            Assert.IsTrue(result2.SequenceEqual(result));

            result2 = from r in nodeSet
                      from x in r["identifier"]
                      from y in x["period"]
                      from z in y["start"]
                      select z;
            Assert.IsTrue(result2.SequenceEqual(result));

            result2 = root["identifier"].SelectMany(n => n["period"]).SelectMany(n => n["start"]);
            Assert.IsTrue(result2.SequenceEqual(result));

            var singleNode = root.FirstChild("identifier").FirstChild("period").FirstChild("start");
            Assert.AreEqual(singleNode, result.Last());
            Assert.AreEqual(singleNode.Name, startNode);

            // Example expressions:

            // 1. primitive constant: int, boolean, string
            // 2. item: return all items from the context (input) with name = 'item'
            // 3. parent.child: 
            //     a. enumerate all items from the context (input) with name = 'parent'
            //     b. enumerate all items from the context (result) with name = 'child'
            // 4. item[.item[.item[...]]]
            //     * enumerate all items from the context (input) with name = '[item]'
            //     * recursively enumerate terms and match deeper items from context = intermediate result
            // 5. function(expr): stand-alone function on a constant expression, e.g. string-length()
            // 6. item.(x | y): binary set operation on nodesets
            // 7. nodeSet.function(): instance function on a result set, e.g. nodes.count()


            // context: DiagnosticReport
            // result.resolve().(referenceRange | component.referenceRange).where(meaning.coding.where(system = %sct and code = "123456").any()).text


            // SNIP===========8<================8<==========
            // EK: This is my suggestion for the "ideal" API. See how far we can get ;-)
            // (context: DiagnosticReport)

            //result.resolve().(referenceRange | component.referenceRange)
            //     .where(meaning.coding.where(system=%sct and code="123456").any()).text


            //const string SNOMEDCT = "http://snomed.org/sct";

            //var dr = client.Read<DiagnosticReport>(...);
            //var tree = dr.ToElementTree();      // denkbeeldige extension method die POCO -> ITreeNode doet

            //var obs = tree["result"].Resolve(client);
            //var ranges = obs["referenceRange"].Union(obs["component"]["referenceRange"]);
            //var selectedRanges = ranges.Where(r => r["meaning"]["coding"]
            //            .Where(c => c["system"] == SNOMEDCT && c["code"] == "123456")
            //            .Where(c => c["system"].Any(c => c.Value == "SNOMEDCT")
            //            //			.Where(c => c["system"].SequenceEquals(SNOMEDCT.ToCollection()) 
            //            //					&& c["code"].SequenceEquals("123456".ToCollection()))
            //            .Any());
            //var textNodes = selectedRanges.Select(r => r["text"]);

            // (en nu nog weer unwrappen)
            // SNIP===========8<================8<==========

            // var tests = textNodes.Select(tn => tb.Value);	// Haalt string uit ITreeNode Value

            var global_sct = "http://snomed.org/sct"; // resolve variables from global evaluation context
            var result3 = from node in nodeSet
                          from resultElem in node["result"]
                          from observation in resultElem.Resolve()
                          from range in observation["referenceRange"].Union(
                                // from component in observation["component"]
                                // from componentRange in component["referenceRange"]
                                from componentRange in observation["component", "referenceRange"]
                                select componentRange
                          )
                          where (
                                    // from meaning in range["meaning"]
                                    // from coding in meaning["coding"]
                                    from coding in range["meaning", "coding"]

                                    //where (
                                    //    (
                                    //        // Rule: compare sequence to scalar => sequence.Any(scalar)
                                    //        from system in coding["system"]    // 0...1
                                    //        from code in coding["code"]        // 0...1
                                    //        where system.GetValue<string>() == global_sct
                                    //           && code.GetValue<string>() == "123456"
                                    //        select system
                                    //    ).Any()
                                    //)

                                    // Assumption: system and code have cardinality 0...1
                                    where coding.FirstChild("system").GetValue<string>() == global_sct
                                    where coding.FirstChild("code").GetValue<string>() == "123456"

                                    // Generic predicate to handle any cardinality
                                    // Definition: match nodeset to scalar => true if any node matches the scalar
                                    // where coding["system"].Any(s => s.GetValue<string>() == global_sct)
                                    // where coding["code"].Any(s => s.GetValue<string>() == "123456")

                                    select coding
                                ).Any()
                          select range.GetValue<string>();
        }

    }

    // Mock implementation for extension methods, to test LINQ syntax
    static class TestExtensions
    {
        public static IEnumerable<T> Resolve<T>(this T tree) where T : FhirNavigationTree
        {
            throw new NotImplementedException();
        }
    }
}