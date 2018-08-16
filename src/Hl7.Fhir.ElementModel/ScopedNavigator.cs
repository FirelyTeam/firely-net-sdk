/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.ElementModel
{
    /// <summary>
    /// The ScopedNavigator class has been made obsolete, please change to the class ScopedNode instead
    /// 
    /// To convert from ScopedNavigator to ScopedNode do the following:
    /// <code>
    /// ScopedNavigator nav; // assignment
    /// 
    /// ScopedNode node = new ScopedNode(nav.ToElementNode());
    /// </code>
    /// </summary>
    [Obsolete("Use class ScopedNode instead")]
    public class ScopedNavigator : IElementNavigator, IAnnotated, IExceptionSource
    {
        public string Name => throw new System.NotImplementedException();

        public string Type => throw new System.NotImplementedException();

        public object Value => throw new System.NotImplementedException();

        public string Location => throw new System.NotImplementedException();

        public ExceptionNotificationHandler ExceptionHandler { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IEnumerable<object> Annotations(Type type)
        {
            throw new NotImplementedException();
        }

        public IElementNavigator Clone()
        {
            throw new System.NotImplementedException();
        }

        public bool MoveToFirstChild(string nameFilter = null)
        {
            throw new System.NotImplementedException();
        }

        public bool MoveToNext(string nameFilter = null)
        {
            throw new System.NotImplementedException();
        }
    }
}