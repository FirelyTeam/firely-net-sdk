using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Hl7.Fhir.Navigation
{
    [TestClass]
    public class NavigationTest
    {
        // Render tree to formatted string
        static string RenderTree<T>(T root) where T : INavTreeNode<T>
        {
            return root.DescendantsAndSelf().Aggregate("",
                (bc, n) => bc + n.Ancestors().Aggregate("",
                                (ac, m) => (m.FollowingSiblings().Any() ? "| " : "  ") + ac,
                                ac => ac + (n.FollowingSiblings().Any() ? "+-" : "\\-")
                            )
                            + n.ToString() + "\n"
            );
        }

        static NavTreeNode BuildTree()
        {
            var root = new NavTreeNode("Patient");
            root.AppendChild("identifier")
                    .AppendChild("use", "[use]")
                    .AppendSibling("type", "[type]")
                    .AppendSibling("system", "[system]")
                    .AppendSibling("value", "[value]")
                    .AppendSibling("period")
                        .AppendChild("start", "[start]")
                        .AppendSibling("end", "[end]")
                    .Parent
                    .AppendSibling("assigner", "[assigner]")
                .Parent
                .AppendSibling("gender", "F")
                .AppendSibling("name")
                    .AppendChild("use", "[use]")
                    .AppendSibling("text", "[text]")
                    .AppendSibling("family", "[family]")
                    .AppendSibling("given", "[given]")
                    .AppendSibling("prefix", "[prefix]")
                    .AppendSibling("suffix", "[suffix]")
                    .AppendSibling("period")
                        .AppendChild("start", "[start]")
                        .AppendSibling("end", "[end]");

            return root;
        }

        [TestMethod]
        public void Test_Nav_TreeBuilder()
        {
            var root = BuildTree();
            // TODO: Assert result...
            Debug.Print(RenderTree(root));
        }

        [TestMethod]
        public void Test_Nav_CreateFromAnonymousObject()
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

            var root = NavTreeNodeFactory.CreateFromObject(Patient, "Patient");
            // TODO: Assert result...
            Debug.Print(RenderTree(root));
        }

        [TestMethod]
        public void Test_Nav_AppendChild()
        {
            var root = new NavTreeNode("Homer");
            var child = root.AppendChild("Marge");
            var grandchild = child.AppendChild("Bart");
            var grandchild2 = child.AppendChild("Lisa");

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
        public void Test_Nav_AppendSiblings()
        {
            var root = new NavTreeNode("Homer");
            var s1 = root.AppendSibling("Marge");
            var s2 = s1.AppendSibling("Bart");
            var s3 = s2.AppendSibling("Lisa");

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
        public void Test_Nav_Children()
        {
            var root = BuildTree();
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
        public void Test_Nav_Descendants()
        {
            var root = BuildTree();
            var child = root.FirstChild;
            Assert.AreEqual(child.Name, "identifier");
            var descendants = child.Descendants().ToArray();
            var expected = new string[] { "use", "type", "system", "value", "period", "start", "end", "assigner" };
            Assert.IsTrue(descendants.Select(c => c.Name).SequenceEqual(expected));
        }
    }
}