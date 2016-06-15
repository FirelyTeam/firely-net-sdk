﻿/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System.Diagnostics;
using System.IO;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Serialization;
using System.Reflection;
using System.Xml;
using System.Collections.Generic;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
#if PORTABLE45
	public class PortableProfileNavigationTest
#else
    public class ProfileNavigationTest
#endif
    {      
        [TestMethod]
        public void TestChildNavigation()
        {
            var nav = createTestNav();

            Assert.IsTrue(nav.HasChildren);
            Assert.IsFalse(nav.MoveToNext());

            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.AreEqual(0, nav.OrdinalPosition);    // A
            Assert.AreEqual("A", nav.Path);

            Assert.IsTrue(nav.MoveToFirstChild());      // A.B
            Assert.AreEqual(1, nav.OrdinalPosition);
            Assert.AreEqual("A.B", nav.Path);

            Assert.IsTrue(nav.MoveToFirstChild());      // A.B.C1
            Assert.AreEqual(2, nav.OrdinalPosition);
            Assert.IsFalse(nav.MoveToFirstChild());
            Assert.AreEqual("A.B.C1", nav.Path);

            Assert.IsTrue(nav.MoveToNext());       // A.B.C2 
            Assert.AreEqual(3, nav.OrdinalPosition);
            Assert.AreEqual("A.B.C2", nav.Path);

            Assert.IsFalse(nav.MoveToNext());
            Assert.IsTrue(nav.MoveToParent());   // Back to first A.B
            Assert.AreEqual(1, nav.OrdinalPosition);
            Assert.IsTrue(nav.MoveToNext());  // 2nd A.B
            Assert.AreEqual(4, nav.OrdinalPosition);
            Assert.IsFalse(nav.HasChildren);
            Assert.IsFalse(nav.MoveToFirstChild());
            Assert.AreEqual(4, nav.OrdinalPosition);

            Assert.IsTrue(nav.MoveToNext());  // 3rd A.B
            Assert.IsTrue(nav.MoveToNext());  // A.D
            Assert.IsFalse(nav.MoveToFirstChild());
            Assert.IsFalse(nav.MoveToNext());            
            Assert.AreEqual(8, nav.OrdinalPosition); 
            Assert.AreEqual("A.D", nav.Path);
            
            Assert.IsTrue(nav.MoveToPrevious());        // A.B
            Assert.AreEqual(5, nav.OrdinalPosition);
            Assert.AreEqual("A.B", nav.Path);

            Assert.IsTrue(nav.MoveToParent());
            Assert.AreEqual(0, nav.OrdinalPosition);

            Assert.IsTrue(nav.MoveToParent());  // root
            Assert.IsFalse(nav.MoveToParent()); // beyond root
        }

        [TestMethod]
        public void TestChildNavigationNamed()
        {
            var nav = createTestNav();
            nav.MoveToFirstChild();     // A

            Assert.IsTrue(nav.MoveToChild("B"));
            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual(4, nav.OrdinalPosition);
            Assert.IsTrue(nav.MoveToNext("B"));
            Assert.AreEqual(5, nav.OrdinalPosition);
            Assert.IsFalse(nav.MoveToNext("B"));

            nav.Reset();
            nav.MoveToFirstChild();     // A

            Assert.IsTrue(nav.MoveToChild("B"));
            Assert.IsTrue(nav.MoveToChild("C2"));
            Assert.IsFalse(nav.MoveToNext());
            Assert.AreEqual(3, nav.OrdinalPosition);
            Assert.IsTrue(nav.MoveToParent());          
            Assert.IsTrue(nav.MoveToChild("C2"));
            Assert.IsTrue(nav.MoveToParent());          // 1st A.B
            Assert.IsFalse(nav.MoveToNext("X"));
            Assert.IsTrue(nav.MoveToNext("D"));
            Assert.AreEqual(8, nav.OrdinalPosition);

            Assert.IsFalse(nav.MoveToPrevious("X"));
            Assert.IsTrue(nav.MoveToPrevious("B"));     // 3rd A.B
            Assert.AreEqual(5, nav.OrdinalPosition);
            Assert.IsTrue(nav.MoveToPrevious("B"));    // 2nd A.B
            Assert.IsTrue(nav.MoveToPrevious("B"));    // 1st A.B
            Assert.AreEqual(1, nav.OrdinalPosition);
        }

      
        [TestMethod]
        public void TestDeletions()
        {
            var nav = createTestNav();

            nav.MoveToFirstChild();     // A
            Assert.IsTrue(nav.MoveToChild("B"));
            Assert.IsFalse(nav.Delete(), "still has children");

            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.IsTrue(nav.MoveToNext());            // A.B.C2
            Assert.IsTrue(nav.Delete());
            Assert.AreEqual("A.B.C1", nav.Path, "Did not move back to sibling");
            Assert.IsTrue(nav.Delete());
            Assert.AreEqual(1, nav.OrdinalPosition, "Did not move back to parent");

            Assert.IsTrue(nav.MoveToNext());

            Assert.IsTrue(nav.Delete());  // on 2nd A.B, after deletion will be on "last" A.B (with children)
            Assert.IsFalse(nav.Delete());  // 3rd A.B cannot yet be deleted
            Assert.IsTrue(nav.DeleteTree());    // delete the whole A.B tree

            Assert.AreEqual("A.D", nav.Path);  // should leave us on A.D

            Assert.IsTrue(nav.Delete());  // A.D
            Assert.AreEqual("A.B", nav.Path);

            Assert.IsTrue(nav.Delete());    // remove 1st A.B
            Assert.AreEqual(1, nav.Count);  // only A left

            Assert.IsTrue(nav.Delete());
            Assert.AreEqual(0, nav.Count);
            Assert.IsNull(nav.Current);

            Assert.IsFalse(nav.Delete());

            nav = createTestNav();
            nav.MoveToFirstChild();
            Assert.IsTrue(nav.DeleteTree());
            Assert.AreEqual(0, nav.Count);
            Assert.IsNull(nav.Current);
        }


        [TestMethod]
        public void TestModificationResilience()
        {
            var nav = createTestNav();

            Assert.IsTrue(nav.JumpToFirst("A.D"));

            var nav2 = new ElementNavigator(nav);

            // Delete A.D in nav
            Assert.IsTrue(nav.Delete()); 

            // Should still be there in nav2
            Assert.IsFalse(nav.JumpToFirst("A.D"));
            Assert.IsTrue(nav2.JumpToFirst("A.D"));
        }


        [TestMethod]
        public void TestBookmarks()
        {
            var nav = createTestNav();

            Assert.IsTrue(nav.MoveToFirstChild());  // A
            Assert.IsTrue(nav.MoveToFirstChild());  // A.B
            var bm1 = nav.Bookmark();
            var pos1 = nav.OrdinalPosition;

            Assert.IsTrue(nav.MoveToNext());        // 2nd A.B
            var bm2 = nav.Bookmark();
            var pos2 = nav.OrdinalPosition;

            Assert.AreNotEqual(pos1, pos2);
            Assert.AreNotEqual(bm1.data, bm2.data);

            nav.ReturnToBookmark(bm1);
            Assert.AreEqual(pos1, nav.OrdinalPosition);
            Assert.IsTrue(nav.IsAtBookmark(bm1));
            nav.ReturnToBookmark(bm2);
            Assert.AreEqual(pos2, nav.OrdinalPosition);
            Assert.IsTrue(nav.IsAtBookmark(bm2));
        }
      

        [TestMethod]
        public void TestAbsoluteMoves()
        {
            var nav = createTestNav();

            Assert.IsTrue(nav.JumpToFirst("A"));
            Assert.AreEqual(0, nav.OrdinalPosition);

            Assert.IsTrue(nav.JumpToFirst("A.B.C2"));
            Assert.AreEqual(3, nav.OrdinalPosition);

            Assert.IsFalse(nav.JumpToFirst("A.B.C1.E"));
            Assert.AreEqual(3, nav.OrdinalPosition);

            Assert.IsTrue(nav.JumpToFirst("A.B"));
            Assert.IsTrue(nav.MoveToNext("B"));
            Assert.AreEqual(4, nav.OrdinalPosition);
        }


        [TestMethod]
        public void TestFindAndApproach()
        {
            var nav = createTestNav();

            var res = nav.Approach("A");
            Assert.AreEqual(1, res.Count());
            res = nav.Find("A");
            Assert.AreEqual(1, res.Count());

            res = nav.Approach("A.B.C1");
            Assert.AreEqual(3, res.Count());
            res = nav.Find("A.B.C1");
            Assert.AreEqual(2, res.Count());

            res = nav.Approach("A.B.C1.E");
            Assert.AreEqual(2, res.Count()); 
            nav.ReturnToBookmark(res.First());
            Assert.AreEqual(2, nav.OrdinalPosition);   
            nav.ReturnToBookmark(res.Skip(1).First());
            Assert.AreEqual(4, nav.OrdinalPosition);  

            res = nav.Find("A.B.C1.E");
            Assert.AreEqual(0, res.Count());

            res = nav.Approach("A.B.C1.D.X"); 
            Assert.AreEqual(3, res.Count());
            nav.ReturnToBookmark(res.First());
            Assert.AreEqual(2, nav.OrdinalPosition);   // This one's still on the way
            nav.ReturnToBookmark(res.Skip(1).First());
            Assert.AreEqual(4, nav.OrdinalPosition);   // Via the one but last entry, this one approaches via A.B.C1.D
            nav.ReturnToBookmark(res.Skip(2).First());
            Assert.AreEqual(7, nav.OrdinalPosition);   // Via the one but last entry, this one approaches via A.B.C1.D
            
            res = nav.Find("A.B.C1.D.X");
            Assert.AreEqual(0, res.Count());

            res = nav.Approach("A.B.C1.D");
            Assert.AreEqual(3, res.Count());
            res = nav.Find("A.B.C1.D");
            Assert.AreEqual(1, res.Count());

            res = nav.Approach("A.B");
            Assert.AreEqual(3, res.Count());
            res = nav.Find("A.B");
            Assert.AreEqual(3, res.Count());

            res = nav.Approach("A.B.X");
            Assert.AreEqual(1, res.Count());
            res = nav.Find("A.B.X");
            Assert.AreEqual(0, res.Count());
        }


        [TestMethod]
        public void TestRebase()
        {
            var struc = createTestStructure();

            struc.Snapshot.Rebase("Parent.child1");

            Assert.AreEqual("Parent.child1", struc.Snapshot.Element[0].Path);
            Assert.AreEqual("Parent.child1.B", struc.Snapshot.Element[1].Path);
            Assert.AreEqual("Parent.child1.B.C1", struc.Snapshot.Element[2].Path);
            Assert.AreEqual("Parent.child1.B.C2", struc.Snapshot.Element[3].Path);
            Assert.AreEqual("Parent.child1.B", struc.Snapshot.Element[4].Path);
        }


        [TestMethod]
        public void TestSiblingAlterations()
        {
            var nav = createTestNav();
            var newCNode = new ElementDefinition() { Path = "X.C" };

            Assert.AreEqual(9, nav.Count);

            nav.MoveToFirstChild();
            Assert.IsTrue(nav.MoveToChild("D"));
            Assert.IsTrue(nav.InsertBefore(newCNode));
            Assert.AreEqual("A.C", nav.Path);
            Assert.AreEqual(8, nav.OrdinalPosition);
            Assert.AreEqual(10, nav.Count);

            Assert.IsTrue(nav.Delete());    // delete new "C" node we just created
            Assert.AreEqual(8, nav.OrdinalPosition);
            Assert.AreEqual(9, nav.Count);

            Assert.IsTrue(nav.MoveToPrevious()); // 3rd "A.B" node
            Assert.IsTrue(nav.InsertAfter(newCNode));
            Assert.AreEqual("A.C", nav.Path);
            Assert.AreEqual(8, nav.OrdinalPosition);
            Assert.AreEqual(10, nav.Count);

            Assert.IsTrue(nav.Delete());    // delete new "C" node we just created
            Assert.AreEqual(8, nav.OrdinalPosition); 
            Assert.AreEqual(9, nav.Count);

            Assert.IsTrue(nav.InsertAfter(newCNode));
            Assert.AreEqual("A.C", nav.Path);
            Assert.AreEqual(9, nav.OrdinalPosition);
            Assert.AreEqual(10, nav.Count);

            Assert.IsTrue(nav.Delete());    // delete new "C" node we just created
            Assert.AreEqual(8, nav.OrdinalPosition);
            Assert.AreEqual(9, nav.Count);

            Assert.IsTrue(nav.MoveToParent());
            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.IsTrue(nav.InsertBefore(newCNode));
            Assert.AreEqual("A.C", nav.Path);
            Assert.AreEqual(1, nav.OrdinalPosition);
            Assert.AreEqual(10, nav.Count);
        }

        [TestMethod]
        public void TestChildAlterations()
        {
            var nav = createTestNav();

            var newENode = new ElementDefinition() { Path = "X.Y.E" };
            var newC3Node = new ElementDefinition() { Path = "X.Y.C3" };

            Assert.IsTrue(nav.JumpToFirst("A.B.C1.D"));
            Assert.IsTrue(nav.InsertFirstChild(newENode));
            Assert.AreEqual(8,nav.OrdinalPosition);
            Assert.IsTrue(nav.MoveToParent());
            Assert.AreEqual("A.B.C1.D", nav.Path);
            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.IsTrue(nav.Delete());   
            Assert.AreEqual("A.B.C1.D", nav.Path);  // should have moved back to parent (single child deleted)

            Assert.IsTrue(nav.JumpToFirst("A.D"));
            Assert.IsTrue(nav.InsertFirstChild(newENode));
            Assert.AreEqual(9,nav.OrdinalPosition);
            Assert.IsTrue(nav.MoveToParent());
            Assert.AreEqual("A.D", nav.Path);
            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.IsTrue(nav.Delete());   
            Assert.AreEqual("A.D", nav.Path);  // should have moved back to parent (single child deleted)

            nav.Reset();
            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.IsTrue(nav.MoveToFirstChild()); // A.B
            Assert.IsTrue(nav.AppendChild(newC3Node));
            Assert.AreEqual("A.B.C3",nav.Path);
            Assert.AreEqual(4, nav.OrdinalPosition);

            Assert.IsTrue(nav.Delete());
            Assert.AreEqual(3, nav.OrdinalPosition);
            Assert.IsTrue(nav.MoveToPrevious());        // A.B.C1
            Assert.IsTrue(nav.Delete());
            Assert.AreEqual(2, nav.OrdinalPosition);
            Assert.IsTrue(nav.Delete());
            Assert.AreEqual(1, nav.OrdinalPosition);    // Moved back to parent?
        }


        [TestMethod]
        public void CopyChildTree()
        {
            var dest = createTestNav();

            var struc = new StructureDefinition();
            struc.Snapshot = new StructureDefinition.SnapshotComponent();
            struc.Snapshot.Element = new List<ElementDefinition>();
            var e = struc.Snapshot.Element;

            e.Add(new ElementDefinition() { Path = "X" });
            e.Add(new ElementDefinition() { Path = "X.Y1" });
            e.Add(new ElementDefinition() { Path = "X.Y2" });
            e.Add(new ElementDefinition() { Path = "X.Y2.Z1" });
            e.Add(new ElementDefinition() { Path = "X.Y2.Z2" });
            var source = new ElementNavigator(e);

            Assert.IsTrue(dest.JumpToFirst("A.D"));
            var dstPos = dest.OrdinalPosition;

            source.MoveToFirstChild();
            var srcPos = source.OrdinalPosition;

            Assert.IsTrue(dest.CopyChildren(source));
            Assert.AreEqual(srcPos, source.OrdinalPosition, "source did not remain on original position");
            Assert.AreEqual(dstPos, dest.OrdinalPosition, "dest did not remain on original position");

            Assert.IsTrue(dest.MoveToFirstChild());
            Assert.AreEqual("Y1", dest.PathName);
            Assert.IsTrue(dest.MoveToNext());
            Assert.AreEqual("Y2", dest.PathName);
            Assert.IsFalse(dest.MoveToNext());
            Assert.IsTrue(dest.MoveToFirstChild());
            Assert.AreEqual("Z1", dest.PathName);
            Assert.IsTrue(dest.MoveToNext());
            Assert.AreEqual("Z2", dest.PathName);
            Assert.IsFalse(dest.MoveToNext());
        }

        [TestMethod]
        public void LocateElementByName()
        {
            var nav = createTestNav();
            nav.JumpToFirst("A.B.C1.D");
            nav.Current.Name = "A-Named-Constraint";

            nav.Reset();
            Assert.IsTrue(nav.JumpToNameReference("A-Named-Constraint"));
            Assert.AreEqual(7, nav.OrdinalPosition);

            Assert.IsFalse(nav.JumpToNameReference("IDontExist"));
        }


        [TestMethod]
        public void TestNodeDuplication()
        {
            var nav = createTestNav();
            nav.JumpToFirst("A.B");

            var count = nav.Count;
            var pos = nav.OrdinalPosition;
            var bm = nav.Bookmark();
            nav.Duplicate();
            
            Assert.AreEqual(count + 3, nav.Count);
            Assert.AreEqual(pos, nav.OrdinalPosition-3);

            // Check original
            nav.ReturnToBookmark(bm);
            Assert.AreEqual("A.B", nav.Path);
            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.AreEqual("A.B.C1", nav.Path);
            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual("A.B.C2", nav.Path);
            Assert.IsFalse(nav.MoveToNext());
            nav.MoveToParent();

            // Check copy
            nav.MoveToNext();
            Assert.AreEqual("A.B", nav.Path);
            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.AreEqual("A.B.C1", nav.Path);
            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual("A.B.C2", nav.Path);
            Assert.IsFalse(nav.MoveToNext());
        }


        [TestMethod]
        public void TestChildDeletions()
        {
            var nav = createTestNav();

            nav.MoveToFirstChild();     // A
            Assert.IsTrue(nav.MoveToChild("B"));
            Assert.IsTrue(nav.DeleteChildren());
            Assert.AreEqual(1, nav.OrdinalPosition);
            Assert.IsFalse(nav.HasChildren);
        }


        private static ElementNavigator createTestNav()
        {
            var struc = createTestStructure();
            return new ElementNavigator(struc.Snapshot.Element);
        }

        private static StructureDefinition createTestStructure()
        {
            var struc = new StructureDefinition();
            struc.Snapshot = new StructureDefinition.SnapshotComponent();
            struc.Snapshot.Element = new List<ElementDefinition>();
            var e = struc.Snapshot.Element;

            e.Add(new ElementDefinition() { Path = "A" });
            e.Add(new ElementDefinition() { Path = "A.B" });
            e.Add(new ElementDefinition() { Path = "A.B.C1" });
            e.Add(new ElementDefinition() { Path = "A.B.C2" });
            e.Add(new ElementDefinition() { Path = "A.B" });
            e.Add(new ElementDefinition() { Path = "A.B" });
            e.Add(new ElementDefinition() { Path = "A.B.C1" });
            e.Add(new ElementDefinition() { Path = "A.B.C1.D" });
            e.Add(new ElementDefinition() { Path = "A.D" });
            return struc;
        }
    }
}