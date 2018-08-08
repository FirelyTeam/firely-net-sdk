/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
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
using Hl7.Fhir.ElementModel;
using Hl7.FhirPath.Expressions;
using Xunit;

namespace Hl7.FhirPath.Tests
{
    public class CastTests
    {
        static readonly IElementNavigator complex = new ComplexValue();
        static readonly IEnumerable<IElementNavigator> collection = new IElementNavigator[] { new ConstantValue(4), new ConstantValue(5), complex };
        static readonly IEnumerable<IElementNavigator> singleV = new IElementNavigator[] { new ConstantValue(4) };
        static readonly IEnumerable<IElementNavigator> singleC = new IElementNavigator[] { complex };
        static readonly IEnumerable<IElementNavigator> emptyColl = new IElementNavigator[] { };

        [Fact]
        public void TestUnbox()
        {

            Assert.Null(Typecasts.Unbox(emptyColl, typeof(string)));
            Assert.Equal(collection,Typecasts.Unbox(collection, typeof(IEnumerable<IElementNavigator>)));
            Assert.Equal(complex, Typecasts.Unbox(singleC, typeof(IElementNavigator)));

            Assert.Equal(4L, Typecasts.Unbox(singleV, typeof(long)));
            Assert.Equal(4L, Typecasts.Unbox(new ConstantValue(4), typeof(long)));

            Assert.Equal(complex, Typecasts.Unbox(complex, typeof(IElementNavigator)));
            Assert.Null(Typecasts.Unbox(null, typeof(string)));
            Assert.Equal(4L, Typecasts.Unbox(4L, typeof(long)));
            Assert.Equal("hi!", Typecasts.Unbox("hi!", typeof(string)));
        }

        [Fact]
        public void CastFromNull()
        {
            checkCast<object>(null, null);
            checkCast<IEnumerable<IElementNavigator>>(null, FhirValueList.Empty);
            checkCast<IElementNavigator>(null, null);
            Assert.False(Typecasts.CanCastTo(null, typeof(bool)));
            checkCast<bool?>(null, null);
            checkCast<string>(null, null);
        }

        [Fact]
        public void CastCollection()
        {
            checkCast<object>(collection, collection);
            checkCast<IEnumerable<IElementNavigator>>(collection, collection);
            Assert.False(Typecasts.CanCastTo(collection, typeof(IElementNavigator)));
            Assert.False(Typecasts.CanCastTo(collection, typeof(bool)));
            Assert.False(Typecasts.CanCastTo(collection, typeof(bool?)));
            Assert.False(Typecasts.CanCastTo(collection, typeof(string)));
        }

        [Fact]
        public void CastComplex()
        {
            checkCast<object>(complex, complex);

            Assert.True(Typecasts.CanCastTo(complex, typeof(IEnumerable<IElementNavigator>)));
            var result = (IEnumerable<IElementNavigator>)Typecasts.CastTo(complex, typeof(IEnumerable<IElementNavigator>));
            Assert.Equal(complex,result.Single());
            checkCast<IElementNavigator>(complex, complex );
            Assert.False(Typecasts.CanCastTo(collection, typeof(bool)));
            Assert.False(Typecasts.CanCastTo(collection, typeof(bool?)));
            Assert.False(Typecasts.CanCastTo(collection, typeof(string)));
        }


        [Fact]
        public void CastValue()
        {
            checkCast<object>(4L, 4L);

            Assert.True(Typecasts.CanCastTo(4, typeof(IEnumerable<IElementNavigator>)));
            var result = (IEnumerable<IElementNavigator>)Typecasts.CastTo(4L, typeof(IEnumerable<IElementNavigator>));
            Assert.Equal(4L, result.Single().Value);

            Assert.True(Typecasts.CanCastTo(4L, typeof(IElementNavigator)));
            var result2 = (IElementNavigator)Typecasts.CastTo(4L, typeof(IElementNavigator));
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
            
            Assert.True(Typecasts.CanCastTo("hi", typeof(IEnumerable<IElementNavigator>)));
            var result = (IEnumerable<IElementNavigator>)Typecasts.CastTo("hi", typeof(IEnumerable<IElementNavigator>));
            Assert.Equal("hi", result.Single().Value);

            Assert.True(Typecasts.CanCastTo("hi", typeof(IElementNavigator)));
            var result2 = (IElementNavigator)Typecasts.CastTo("hi", typeof(IElementNavigator));
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

    internal class ComplexValue : IElementNavigator
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

        public string Type
        {
            get
            {
                return null;
            }
        }

        public object Value
        {
            get
            {
                return null;
            }
        }

        public IElementNavigator Clone()
        {
            // todo: 
            throw new NotImplementedException();
        }

        public bool MoveToFirstChild(string nameFilter = null)
        {
            return false;
        }

        public bool MoveToNext(string nameFilter = null)
        {
            return false;
        }
    }
}