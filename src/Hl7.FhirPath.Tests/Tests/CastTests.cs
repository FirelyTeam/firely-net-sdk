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
using Hl7.Fhir.Specification;
using Hl7.FhirPath.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.FhirPath.Tests
{
    [TestClass]
    public class CastTests
    {
        static readonly ITypedElement complex = new ComplexValue();
        static readonly IEnumerable<ITypedElement> collection = ElementNode.CreateList(4, 5, complex);
        static readonly IEnumerable<ITypedElement> singleV = ElementNode.CreateList(4L);
        static readonly IEnumerable<ITypedElement> singleC = ElementNode.CreateList(complex);
        static readonly IEnumerable<ITypedElement> emptyColl = ElementNode.EmptyList;

        [TestMethod]
        public void TestUnbox()
        {

            Assert.IsNull(Typecasts.UnboxTo(emptyColl, typeof(string)));
            Assert.AreEqual(collection, Typecasts.UnboxTo(collection, typeof(IEnumerable<ITypedElement>)));
            Assert.AreEqual(complex, Typecasts.UnboxTo(singleC, typeof(ITypedElement)));

            Assert.AreEqual(4L, Typecasts.UnboxTo(singleV, typeof(long)));
            Assert.AreEqual(4L, Typecasts.UnboxTo(ElementNode.ForPrimitive(4L), typeof(long)));

            Assert.AreEqual(complex, Typecasts.UnboxTo(complex, typeof(ITypedElement)));
            Assert.IsNull(Typecasts.UnboxTo(null, typeof(string)));
            Assert.AreEqual(4L, Typecasts.UnboxTo(4L, typeof(long)));
            Assert.AreEqual("hi!", Typecasts.UnboxTo("hi!", typeof(string)));
        }

        [TestMethod]
        public void CastFromNull()
        {
            checkCast<object>(null, null);
            checkCast<IEnumerable<ITypedElement>>(null, ElementNode.EmptyList);
            checkCast<ITypedElement>(null, null);
            Assert.IsFalse(Typecasts.CanCastTo(null, typeof(bool)));
            checkCast<bool?>(null, null);
            checkCast<string>(null, null);
        }

        [TestMethod]
        public void CastCollection()
        {
            checkCast<object>(collection, collection);
            checkCast<IEnumerable<ITypedElement>>(collection, collection);
            Assert.IsFalse(Typecasts.CanCastTo(collection, typeof(ITypedElement)));
            Assert.IsFalse(Typecasts.CanCastTo(collection, typeof(bool)));
            Assert.IsFalse(Typecasts.CanCastTo(collection, typeof(bool?)));
            Assert.IsFalse(Typecasts.CanCastTo(collection, typeof(string)));
        }

        [TestMethod]
        public void CastComplex()
        {
            checkCast<object>(complex, complex);

            Assert.IsTrue(Typecasts.CanCastTo(complex, typeof(IEnumerable<ITypedElement>)));
            var result = (IEnumerable<ITypedElement>)Typecasts.CastTo(complex, typeof(IEnumerable<ITypedElement>));
            Assert.AreEqual(complex, result.Single());
            checkCast<ITypedElement>(complex, complex);
            Assert.IsFalse(Typecasts.CanCastTo(collection, typeof(bool)));
            Assert.IsFalse(Typecasts.CanCastTo(collection, typeof(bool?)));
            Assert.IsFalse(Typecasts.CanCastTo(collection, typeof(string)));
        }

        [TestMethod]
        public void CastValue()
        {
            checkCast<object>(4L, 4L);

            Assert.IsTrue(Typecasts.CanCastTo(4, typeof(IEnumerable<ITypedElement>)));
            var result = (IEnumerable<ITypedElement>)Typecasts.CastTo(4L, typeof(IEnumerable<ITypedElement>));
            Assert.AreEqual(4L, result.Single().Value);

            Assert.IsTrue(Typecasts.CanCastTo(4L, typeof(ITypedElement)));
            var result2 = (ITypedElement)Typecasts.CastTo(4L, typeof(ITypedElement));
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

            Assert.IsTrue(Typecasts.CanCastTo("hi", typeof(IEnumerable<ITypedElement>)));
            var result = (IEnumerable<ITypedElement>)Typecasts.CastTo("hi", typeof(IEnumerable<ITypedElement>));
            Assert.AreEqual("hi", result.Single().Value);

            Assert.IsTrue(Typecasts.CanCastTo("hi", typeof(ITypedElement)));
            var result2 = (ITypedElement)Typecasts.CastTo("hi", typeof(ITypedElement));
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

        public IEnumerable<ITypedElement> Children(string name = null) => new ITypedElement[0];
    }
}