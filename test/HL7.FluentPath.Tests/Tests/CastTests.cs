/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

// To introduce the DSTU2 FHIR specification
//extern alias dstu2;

using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.ElementModel;
using Hl7.FluentPath;
using Hl7.FluentPath.Expressions;
using Xunit;

namespace Hl7.FluentPath.Tests
{
    public class CastTests
    {
        static IValueProvider complex = new ComplexValue();
        static IEnumerable<IValueProvider> collection = new IValueProvider[] { new ConstantValue(4), new ConstantValue(5), complex };
        static IEnumerable<IValueProvider> singleV = new IValueProvider[] { new ConstantValue(4) };
        static IEnumerable<IValueProvider> singleC = new IValueProvider[] { complex };
        static IEnumerable<IValueProvider> emptyColl = new IValueProvider[] { };

        [Fact]
        public void TestUnbox()
        {

            Assert.Equal(null, Typecasts.Unbox(emptyColl, typeof(string)));
            Assert.Equal(collection,Typecasts.Unbox(collection, typeof(IEnumerable<IValueProvider>)));
            Assert.Equal(complex, Typecasts.Unbox(singleC, typeof(IValueProvider)));

            Assert.Equal(4L, Typecasts.Unbox(singleV, typeof(long)));
            Assert.Equal(4L, Typecasts.Unbox(new ConstantValue(4), typeof(long)));

            Assert.Equal(complex, Typecasts.Unbox(complex, typeof(IValueProvider)));
            Assert.Equal(null, Typecasts.Unbox(null, typeof(string)));
            Assert.Equal(4L, Typecasts.Unbox(4L, typeof(long)));
            Assert.Equal("hi!", Typecasts.Unbox("hi!", typeof(string)));
        }

        [Fact]
        public void CastFromNull()
        {
            checkCast<object>(null, null);
            checkCast<IEnumerable<IValueProvider>>(null, FhirValueList.Empty);
            checkCast<IValueProvider>(null, null);
            Assert.False(Typecasts.CanCastTo(null, typeof(bool)));
            checkCast<bool?>(null, null);
            checkCast<string>(null, null);
        }

        [Fact]
        public void CastCollection()
        {
            checkCast<object>(collection, collection);
            checkCast<IEnumerable<IValueProvider>>(collection, collection);
            Assert.False(Typecasts.CanCastTo(collection, typeof(IValueProvider)));
            Assert.False(Typecasts.CanCastTo(collection, typeof(bool)));
            Assert.False(Typecasts.CanCastTo(collection, typeof(bool?)));
            Assert.False(Typecasts.CanCastTo(collection, typeof(string)));
        }

        [Fact]
        public void CastComplex()
        {
            checkCast<object>(complex, complex);

            Assert.True(Typecasts.CanCastTo(complex, typeof(IEnumerable<IValueProvider>)));
            var result = (IEnumerable<IValueProvider>)Typecasts.CastTo(complex, typeof(IEnumerable<IValueProvider>));
            Assert.Equal(complex,result.Single());
            checkCast<IValueProvider>(complex, complex );
            Assert.False(Typecasts.CanCastTo(collection, typeof(bool)));
            Assert.False(Typecasts.CanCastTo(collection, typeof(bool?)));
            Assert.False(Typecasts.CanCastTo(collection, typeof(string)));
        }


        [Fact]
        public void CastValue()
        {
            checkCast<object>(4L, 4L);

            Assert.True(Typecasts.CanCastTo(4, typeof(IEnumerable<IValueProvider>)));
            var result = (IEnumerable<IValueProvider>)Typecasts.CastTo(4L, typeof(IEnumerable<IValueProvider>));
            Assert.Equal(4L, result.Single().Value);

            Assert.True(Typecasts.CanCastTo(4L, typeof(IValueProvider)));
            var result2 = (IValueProvider)Typecasts.CastTo(4L, typeof(IValueProvider));
            Assert.Equal(4L, result2.Value);

            checkCast<bool>(true, true);
            checkCast<decimal>(4L, 4m);

            checkCast<bool?>(true, true);
            checkCast<decimal?>(4L, 4m);
            checkCast<string>("hi", "hi");

            Assert.False(Typecasts.CanCastTo(4, typeof(string)));
            Assert.False(Typecasts.CanCastTo(4m, typeof(long)));
        }


        [Fact]
        public void CastNullable()
        {
            checkCast<object>("hi", "hi");
            
            Assert.True(Typecasts.CanCastTo("hi", typeof(IEnumerable<IValueProvider>)));
            var result = (IEnumerable<IValueProvider>)Typecasts.CastTo("hi", typeof(IEnumerable<IValueProvider>));
            Assert.Equal("hi", result.Single().Value);

            Assert.True(Typecasts.CanCastTo("hi", typeof(IValueProvider)));
            var result2 = (IValueProvider)Typecasts.CastTo("hi", typeof(IValueProvider));
            Assert.Equal("hi", result2.Value);

            checkCast<bool?>(true, true);
            checkCast<decimal?>(4L, 4m);
            checkCast<string>("hi", "hi");

            Assert.False(Typecasts.CanCastTo(4, typeof(string)));
            Assert.False(Typecasts.CanCastTo(4m, typeof(long?)));
        }


        private void checkCast<T>(object source, T value)
        {
            Assert.True(Typecasts.CanCastTo(source, typeof(T)));

            var result = Typecasts.CastTo(source, typeof(T));
            Assert.Equal(value, result);
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