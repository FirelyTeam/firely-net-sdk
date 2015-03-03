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
using Hl7.Fhir.Support;
using Hl7.Fhir.Serialization;


namespace Hl7.Fhir.Rest
{
    public interface IEntryBuilder
    {
        Bundle Build();
        IInteractionBuilder Add();
    }

    public interface IReadEntryBuilder : IEntryBuilder
    {
        IEntryBuilder IfNoneMatch(string eTag);
        IEntryBuilder IfModifiedSince(DateTimeOffset dto);
    }

    public interface IUpdateEntryBuilder : IEntryBuilder
    {
        IEntryBuilder IfMatch(string eTag);
    }

    public interface IHistoryEntryBuilder : IEntryBuilder
    {
        IHistoryEntryBuilder SummaryOnly();
        IHistoryEntryBuilder PageSize(int size);
        IHistoryEntryBuilder Since(DateTimeOffset since);
    }


    public interface IInteractionBuilder
    {
        IHistoryEntryBuilder CollectionHistory(string resourceType);
        IEntryBuilder Conformance();
        IEntryBuilder Create(Hl7.Fhir.Model.Resource body);
        IEntryBuilder Delete(string resourceType, string id);
        IEntryBuilder Get(string url);
        IEntryBuilder Get(Uri uri);
        IReadEntryBuilder Read(string resourceType, string id);
        IHistoryEntryBuilder ResourceHistory(string resourceType, string id);
        IEntryBuilder ResourceOperation(string resourceType, string id, string vid, string name, Hl7.Fhir.Model.Parameters parameters);
        IEntryBuilder Search(SearchParams q, string resourceType = null);
        IEntryBuilder Search(string resourceType = null);
        IHistoryEntryBuilder ServerHistory();
        IEntryBuilder ServerOperation(string name, Hl7.Fhir.Model.Parameters parameters);
        IEntryBuilder Transaction(Hl7.Fhir.Model.Bundle transaction);
        IEntryBuilder TypeOperation(string resourceType, string name, Hl7.Fhir.Model.Parameters parameters);
        IUpdateEntryBuilder Update(string id, Hl7.Fhir.Model.Resource body);
        IEntryBuilder VRead(string resourceType, string id, string vid);
    }
}
