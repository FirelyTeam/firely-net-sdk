/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

// To introduce the DSTU2 FHIR specification
//extern alias dstu2;

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using Hl7.FhirPath.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.FhirPath.Tests
{
    [TestClass]
    public class CastTests
    {
        private static readonly IScopedNode complex = new ComplexValue().ToScopedNode();
        private static readonly IEnumerable<IScopedNode> collection = ElementNode.CreateList(4, 5, complex).ToScopedNodes();
        private static readonly IEnumerable<IScopedNode> singleV = ElementNode.CreateList(4L).ToScopedNodes();
        private static readonly IEnumerable<IScopedNode> singleC = ElementNode.CreateList(complex).ToScopedNodes();
        private static readonly IEnumerable<IScopedNode> emptyColl = [];

        [TestMethod]
        public void TestUnbox()
        {
            Assert.IsNull(Typecasts.UnboxTo(emptyColl, typeof(string)));
            collection.SequenceEqual(Typecasts.UnboxTo(collection, typeof(IEnumerable<IScopedNode>)) as IEnumerable<IScopedNode>);
            Assert.AreEqual(complex, Typecasts.UnboxTo(singleC, typeof(IScopedNode)));

            Assert.AreEqual(4L, Typecasts.UnboxTo(singleV, typeof(long)));
            Assert.AreEqual(4L, Typecasts.UnboxTo(PocoNodeOrList.ForPrimitive<Integer64>(4L), typeof(long)));

            Assert.AreEqual(complex, Typecasts.UnboxTo(complex, typeof(IScopedNode)));
            Assert.IsNull(Typecasts.UnboxTo(null, typeof(string)));
            Assert.AreEqual(4L, Typecasts.UnboxTo(4L, typeof(long)));
            Assert.AreEqual("hi!", Typecasts.UnboxTo("hi!", typeof(string)));
        }

        [TestMethod]
        public void CastFromNull()
        {
            checkCast<object>(null, null);
            checkCast<IEnumerable<IScopedNode>>(null, []);
            checkCast<IScopedNode>(null, null);
            Assert.IsFalse(Typecasts.CanCastTo(null, typeof(bool)));
            checkCast<bool?>(null, null);
            checkCast<string>(null, null);
        }

        [TestMethod]
        public void CastCollection()
        {
            checkCast<object>(collection, collection);
            checkCast<IEnumerable<IScopedNode>>(collection, collection);
            Assert.IsFalse(Typecasts.CanCastTo(collection, typeof(IScopedNode)));
            Assert.IsFalse(Typecasts.CanCastTo(collection, typeof(bool)));
            Assert.IsFalse(Typecasts.CanCastTo(collection, typeof(bool?)));
            Assert.IsFalse(Typecasts.CanCastTo(collection, typeof(string)));
        }

        [TestMethod]
        public void CastComplex()
        {
            checkCast<object>(complex, complex);

            Assert.IsTrue(Typecasts.CanCastTo(complex, typeof(IEnumerable<IScopedNode>)));
            var result = (IEnumerable<IScopedNode>)Typecasts.CastTo(complex, typeof(IEnumerable<IScopedNode>));
            Assert.AreEqual(complex, result.Single());
            checkCast<IScopedNode>(complex, complex);
            Assert.IsFalse(Typecasts.CanCastTo(collection, typeof(bool)));
            Assert.IsFalse(Typecasts.CanCastTo(collection, typeof(bool?)));
            Assert.IsFalse(Typecasts.CanCastTo(collection, typeof(string)));
        }

        [TestMethod]
        public void CastValue()
        {
            checkCast<object>(4L, 4L);

            Assert.IsTrue(Typecasts.CanCastTo(4, typeof(IEnumerable<IScopedNode>)));
            var result = (IEnumerable<IScopedNode>)Typecasts.CastTo(4L, typeof(IEnumerable<IScopedNode>));
            Assert.AreEqual(4L, result.Single().Value);

            Assert.IsTrue(Typecasts.CanCastTo(4L, typeof(IScopedNode)));
            var result2 = (IScopedNode)Typecasts.CastTo(4L, typeof(IScopedNode));
            Assert.AreEqual(4L, result2.Value);

            checkCast<bool>(true, true);
            checkCast<decimal>(4L, 4m);

            checkCast<bool?>(true, true);
            checkCast<decimal?>(4L, 4m);
            checkCast<string>("hi", "hi");

            Assert.IsFalse(Typecasts.CanCastTo(4, typeof(string)));
            Assert.IsFalse(Typecasts.CanCastTo(4m, typeof(long)));
        }


        [TestMethod]
        public void CastNullable()
        {
            checkCast<object>("hi", "hi");

            Assert.IsTrue(Typecasts.CanCastTo("hi", typeof(IEnumerable<IScopedNode>)));
            var result = (IEnumerable<IScopedNode>)Typecasts.CastTo("hi", typeof(IEnumerable<IScopedNode>));
            Assert.AreEqual("hi", result.Single().Value);

            Assert.IsTrue(Typecasts.CanCastTo("hi", typeof(IScopedNode)));
            var result2 = (IScopedNode)Typecasts.CastTo("hi", typeof(IScopedNode));
            Assert.AreEqual("hi", result2.Value);

            checkCast<bool?>(true, true);
            checkCast<decimal?>(4L, 4m);
            checkCast<string>("hi", "hi");

            Assert.IsFalse(Typecasts.CanCastTo(4, typeof(string)));
            Assert.IsFalse(Typecasts.CanCastTo(4m, typeof(long?)));
        }


        private void checkCast<T>(object source, T value)
        {
            Assert.IsTrue(Typecasts.CanCastTo(source, typeof(T)));

            var result = Typecasts.CastTo(source, typeof(T));
            Assert.AreEqual(value, result);
        }

    }

    internal class ComplexValue : ITypedElement
    {
        public string Name
        {
            get
            {
                return null;
            }
        }

        public string Location
        {
            get
            {
                return null;
            }
        }

        public string InstanceType
        {
            get
            {
                return "NotAPrimitiveType";
            }
        }

        public object Value
        {
            get
            {
                return null;
            }
        }

        public IElementDefinitionSummary Definition => null;

        public IEnumerable<ITypedElement> Children(string name = null) => [];
    }
}