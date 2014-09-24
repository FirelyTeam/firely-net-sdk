/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Search
{
    public interface IOperationBuilder
    {
        ICriteriumBuilder Eq(decimal number);
        ICriteriumBuilder LessThan();

        IStringModifier Matches(string text);


        ITokenModifier Is(string code);

        IValueBuilder On(string dateTime);
        IValueBuilder On(DateTimeOffset dateTime);
        IValueBuilder Before();
        IValueBuilder After();

        ICriteriumBuilder References(string resource, string id);
        ICriteriumBuilder References(Uri location);
        ICriteriumBuilder References(string location);

        ICriteriumBuilder IsMissing { get; }
    }

    public interface IReferenceBuilder
    {
    }

    public interface IValueBuilder
    {
    }

    public interface ICriteriumBuilder
    {
        IOperationBuilder And(string paramName);
    }

    public interface ITokenModifier : ICriteriumBuilder
    {
        ICriteriumBuilder In(string ns);
        ICriteriumBuilder In(Uri ns);

    }

    public interface IStringModifier : ICriteriumBuilder
    {
        ICriteriumBuilder Exactly { get; }
    }
}
