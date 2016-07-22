/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

// To introduce the DSTU2 FHIR specification
extern alias dstu2;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Sprache;
using Hl7.Fhir.FluentPath;
using Hl7.Fhir.FluentPath.InstanceTree;
using Hl7.Fhir.Navigation;
using Hl7.Fhir.FluentPath.Functions;
using Hl7.Fhir.FluentPath.Binding;

namespace Hl7.Fhir.Tests.FhirPath
{
    [TestClass]
#if PORTABLE45
	public class PortableCastTests
#else
    public class CastTests
#endif
    {
        static IValueProvider complex = new ComplexValue();
        static IEnumerable<IValueProvider> collection = new IValueProvider[] { new ConstantValue(4), new ConstantValue(5), complex };
        static IEnumerable<IValueProvider> singleV = new IValueProvider[] { new ConstantValue(4) };
        static IEnumerable<IValueProvider> singleC = new IValueProvider[] { complex };
        static IEnumerable<IValueProvider> emptyColl = new IValueProvider[] { };

        [TestMethod, TestCategory("FhirPath")]
        public void TestUnbox()
        {

            Assert.AreEqual(null, Typecasts.Unbox(emptyColl));
            Assert.AreEqual(collection,Typecasts.Unbox(collection));
            Assert.AreEqual(complex, Typecasts.Unbox(singleC));

            Assert.AreEqual(4L, Typecasts.Unbox(singleV));
            Assert.AreEqual(4L, Typecasts.Unbox(new ConstantValue(4)));

            Assert.AreEqual(complex, Typecasts.Unbox(complex));
            Assert.AreEqual(null, Typecasts.Unbox(null));
            Assert.AreEqual(4L, Typecasts.Unbox(4L));
            Assert.AreEqual("hi!", Typecasts.Unbox("hi!"));
        }

        [TestMethod, TestCategory("FhirPath")]
        public void CastFromNull()
        {
            checkCast<object>(null, null);
            checkCast<IEnumerable<IValueProvider>>(null, FhirValueList.Empty);
            checkCast<IValueProvider>(null, null);
            Assert.IsFalse(Typecasts.CanCastTo(null, typeof(bool)));
            checkCast<bool?>(null, null);
            checkCast<string>(null, null);
        }

        [TestMethod, TestCategory("FhirPath")]
        public void CastCollection()
        {
            checkCast<object>(collection, collection);
            checkCast<IEnumerable<IValueProvider>>(collection, collection);
            Assert.IsFalse(Typecasts.CanCastTo(collection, typeof(IValueProvider)));
            Assert.IsFalse(Typecasts.CanCastTo(collection, typeof(bool)));
            Assert.IsFalse(Typecasts.CanCastTo(collection, typeof(bool?)));
            Assert.IsFalse(Typecasts.CanCastTo(collection, typeof(string)));
        }

        [TestMethod, TestCategory("FhirPath")]
        public void CastComplex()
        {
            checkCast<object>(complex, complex);

            Assert.IsTrue(Typecasts.CanCastTo(complex, typeof(IEnumerable<IValueProvider>)));
            var result = (IEnumerable<IValueProvider>)Typecasts.CastTo(complex, typeof(IEnumerable<IValueProvider>));
            Assert.AreEqual(complex,result.Single());
            checkCast<IValueProvider>(complex, complex );
            Assert.IsFalse(Typecasts.CanCastTo(collection, typeof(bool)));
            Assert.IsFalse(Typecasts.CanCastTo(collection, typeof(bool?)));
            Assert.IsFalse(Typecasts.CanCastTo(collection, typeof(string)));
        }


        [TestMethod, TestCategory("FhirPath")]
        public void CastValue()
        {
            checkCast<object>(4L, 4L);

            Assert.IsTrue(Typecasts.CanCastTo(4, typeof(IEnumerable<IValueProvider>)));
            var result = (IEnumerable<IValueProvider>)Typecasts.CastTo(4L, typeof(IEnumerable<IValueProvider>));
            Assert.AreEqual(4L, result.Single().Value);

            Assert.IsTrue(Typecasts.CanCastTo(4L, typeof(IValueProvider)));
            var result2 = (IValueProvider)Typecasts.CastTo(4L, typeof(IValueProvider));
            Assert.AreEqual(4L, result2.Value);

            checkCast<bool>(true, true);
            checkCast<decimal>(4L, 4m);

            checkCast<bool?>(true, true);
            checkCast<decimal?>(4L, 4m);
            checkCast<string>("hi", "hi");

            Assert.IsFalse(Typecasts.CanCastTo(4, typeof(string)));
            Assert.IsFalse(Typecasts.CanCastTo(4m, typeof(long)));
        }


        [TestMethod, TestCategory("FhirPath")]
        public void CastNullable()
        {
            checkCast<object>("hi", "hi");
            
            Assert.IsTrue(Typecasts.CanCastTo("hi", typeof(IEnumerable<IValueProvider>)));
            var result = (IEnumerable<IValueProvider>)Typecasts.CastTo("hi", typeof(IEnumerable<IValueProvider>));
            Assert.AreEqual("hi", result.Single().Value);

            Assert.IsTrue(Typecasts.CanCastTo("hi", typeof(IValueProvider)));
            var result2 = (IValueProvider)Typecasts.CastTo("hi", typeof(IValueProvider));
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
            Assert.AreEqual(value, Typecasts.CastTo(source, typeof(T)));
        }

    }

    internal class ComplexValue : IValueProvider
    {
        public object Value
        {
            get
            {
                return null;
            }
        }
    }
}