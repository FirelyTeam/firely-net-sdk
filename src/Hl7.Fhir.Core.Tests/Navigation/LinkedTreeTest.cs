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

namespace Hl7.Fhir.Navigation
{
    [TestClass]
    public class LinkedTreeTest
    {
        // Render tree to formatted string
        static string RenderTree<T>(T root) where T : IDoublyLinkedTree<T>
        {
            return root.DescendantsAndSelf().Aggregate("",
                (bc, n) => bc + n.Ancestors().Aggregate("",
                                (ac, m) => (m.FollowingSiblings().Any() ? "| " : "  ") + ac,
                                ac => ac + (n.FollowingSiblings().Any() ? "+-" : "\\-")
                            )
                            + n.ToString() + "\n"
            );
        }

        DoublyLinkedTree CreatePatientTree()
        {
            var root = DoublyLinkedTree.Create("Patient");

            root.AddLastChild("identifier")
                    .AddLastChild("use", "...use...")
                    .AddLastSibling("type", "...type...")
                    .AddLastSibling("system", "...system...")
                    .AddLastSibling("value", "0123456789")
                    .AddLastSibling("period")
                        .AddLastChild("start", "20151127 12:00")
                        .AddLastSibling("end", "20151130 18:00")
                    .Parent
                    .AddLastSibling("assigner", "Dr. House")
                .Parent
                .AddLastSibling("gender", "F")
                .AddLastSibling("name")
                    .AddLastChild("use", "...use...")
                    .AddLastSibling("text", "Prof. Dr. Ir. P. Akkermans MBA")
                    .AddLastSibling("family", "Akkermans")
                    .AddLastSibling("given", "Piet")
                    .AddLastSibling("prefix", "Prof. Dr. Ir.")
                    .AddLastSibling("suffix", "MBA")
                    .AddLastSibling("period")
                        .AddLastChild("start", "20151201 03:15")
                        .AddLastSibling("end", "20151231 23:45");

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
                        },
                    }
                };

            var root = LinkedTreeFactory.CreateFromObject(Patient, "Patient");
            // TODO: Assert result...
            Debug.Print(RenderTree(root));
        }

        [TestMethod]
        public void Test_Tree_Builder()
        {
            var root = CreatePatientTree();
            // TODO: Assert result...
            Debug.Print(RenderTree(root));
        }

        [TestMethod]
        public void Test_Tree_AddChild()
        {
            var root = DoublyLinkedTree.Create("Homer");
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
            var root = DoublyLinkedTree.Create("Homer");
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
            var root = CreatePatientTree();
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
            var root = CreatePatientTree();
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
            var root = CreatePatientTree();
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
            var root = CreatePatientTree();
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
            var root = CreatePatientTree();

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

            var start_values = period_starts.OfType<IValue<string>>();
            var maxStart = start_values.Max(n => n.Value);
            var maxNode = start_values.First(n => n.Value == maxStart);
            Debug.Print("Max start = {0}", maxNode.Value);
        }
    }
}
