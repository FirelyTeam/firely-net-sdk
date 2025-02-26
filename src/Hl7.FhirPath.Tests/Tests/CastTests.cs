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
        private static readonly PocoNode complex = new DynamicDataType{DynamicTypeName = "NotAPrimitiveType"}.ToPocoNode();
        private static readonly IEnumerable<PocoNode> collection = PocoNode.FromAnyList([4, 5, complex]);
        private static readonly IEnumerable<PocoNode> singleV = PocoNode.FromAnyList([4L]);
        private static readonly IEnumerable<PocoNode> singleC = PocoNode.FromAnyList([complex]);
        private static readonly IEnumerable<PocoNode> emptyColl = [];

        [TestMethod]
        public void TestUnbox()
        {
            Assert.IsNull(Typecasts.UnboxTo(emptyColl, typeof(string)));
            collection.SequenceEqual(Typecasts.UnboxTo(collection, typeof(IEnumerable<PocoNode>)) as IEnumerable<PocoNode>);
            Assert.AreEqual(complex, Typecasts.UnboxTo(singleC, typeof(PocoNode)));

            Assert.AreEqual(4L, Typecasts.UnboxTo(singleV, typeof(long)));
            Assert.AreEqual(4L, Typecasts.UnboxTo(PocoNode.ForPrimitive<Integer64>("4"), typeof(long)));

            Assert.AreEqual(complex, Typecasts.UnboxTo(complex, typeof(PocoNode)));
            Assert.IsNull(Typecasts.UnboxTo(null, typeof(string)));
            Assert.AreEqual(4L, Typecasts.UnboxTo(4L, typeof(long)));
            Assert.AreEqual("hi!", Typecasts.UnboxTo("hi!", typeof(string)));
        }

        [TestMethod]
        public void CastFromNull()
        {
            checkCast<object>(null, null);
            checkCast<IEnumerable<PocoNode>>(null, []);
            checkCast<PocoNode>(null, null);
            Assert.IsFalse(Typecasts.CanCastTo(null, typeof(bool)));
            checkCast<bool?>(null, null);
            checkCast<string>(null, null);
        }

        [TestMethod]
        public void CastCollection()
        {
            checkCast<object>(collection, collection);
            checkCast<IEnumerable<PocoNode>>(collection, collection);
            Assert.IsFalse(Typecasts.CanCastTo(collection, typeof(PocoNode)));
            Assert.IsFalse(Typecasts.CanCastTo(collection, typeof(bool)));
            Assert.IsFalse(Typecasts.CanCastTo(collection, typeof(bool?)));
            Assert.IsFalse(Typecasts.CanCastTo(collection, typeof(string)));
        }

        [TestMethod]
        public void CastComplex()
        {
            checkCast<object>(complex, complex);

            Assert.IsTrue(Typecasts.CanCastTo(complex, typeof(IEnumerable<PocoNode>)));
            var result = (IEnumerable<PocoNode>)Typecasts.CastTo(complex, typeof(IEnumerable<PocoNode>));
            Assert.AreEqual(complex, result.Single());
            checkCast<PocoNode>(complex, complex);
            Assert.IsFalse(Typecasts.CanCastTo(collection, typeof(bool)));
            Assert.IsFalse(Typecasts.CanCastTo(collection, typeof(bool?)));
            Assert.IsFalse(Typecasts.CanCastTo(collection, typeof(string)));
        }

        [TestMethod]
        public void CastValue()
        {
            checkCast<object>(4L, 4L);

            Assert.IsTrue(Typecasts.CanCastTo(4, typeof(IEnumerable<PocoNode>)));
            var result = (IEnumerable<PocoNode>)Typecasts.CastTo(4L, typeof(IEnumerable<PocoNode>));
            Assert.AreEqual(4L, result.Single().GetValue());

            Assert.IsTrue(Typecasts.CanCastTo(4L, typeof(PocoNode)));
            var result2 = (PocoNode)Typecasts.CastTo(4L, typeof(PocoNode));
            Assert.AreEqual(4L, result2.GetValue());

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

            Assert.IsTrue(Typecasts.CanCastTo("hi", typeof(IEnumerable<PocoNode>)));
            var result = (IEnumerable<PocoNode>)Typecasts.CastTo("hi", typeof(IEnumerable<PocoNode>));
            Assert.AreEqual("hi", result.Single().GetValue());

            Assert.IsTrue(Typecasts.CanCastTo("hi", typeof(PocoNode)));
            var result2 = (PocoNode)Typecasts.CastTo("hi", typeof(PocoNode));
            Assert.AreEqual("hi", result2.GetValue());

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
}