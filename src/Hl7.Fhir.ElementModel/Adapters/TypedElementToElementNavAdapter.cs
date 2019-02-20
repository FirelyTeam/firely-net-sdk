/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System.Linq;
using System.Collections.Generic;
using System;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.ElementModel.Adapters
{
    internal class TypedElementToElementNavAdapter : BaseNodeToNavAdapter
    {
        public TypedElementToElementNavAdapter(ITypedElement element)
        {
            if (element == null) throw Error.ArgumentNull(nameof(element));
            Initialize(element);
        }

        private TypedElementToElementNavAdapter() { }  // for clone

        protected override BaseNodeToNavAdapter NewClone() => new TypedElementToElementNavAdapter();

        protected override IList<ITypedElement> GetChildren() => Current.Children().ToList();
    }
}