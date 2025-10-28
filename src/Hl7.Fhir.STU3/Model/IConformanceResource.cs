/*
  Copyright (c) 2011-2012, HL7, Inc
  All rights reserved.
  
  Redistribution and use in source and binary forms, with or without modification, 
  are permitted provided that the following conditions are met:
  
   * Redistributions of source code must retain the above copyright notice, this 
     list of conditions and the following disclaimer.
   * Redistributions in binary form must reproduce the above copyright notice, 
     this list of conditions and the following disclaimer in the documentation 
     and/or other materials provided with the distribution.
   * Neither the name of HL7 nor the names of its contributors may be used to 
     endorse or promote products derived from this software without specific 
     prior written permission.
  
  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
  IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
  POSSIBILITY OF SUCH DAMAGE.
  
*/

using Hl7.Fhir.Introspection;
using System;
using System.Linq;

#nullable enable

namespace Hl7.Fhir.Model;

public partial class StructureDefinition : IVersionableConformanceResource;

public partial class ValueSet : IVersionableConformanceResource;

public partial class SearchParameter : IVersionableConformanceResource;

public partial class OperationDefinition : IVersionableConformanceResource;

public partial class CapabilityStatement : IVersionableConformanceResource;

public partial class MessageDefinition : IVersionableConformanceResource;

public partial class ImplementationGuide : IVersionableConformanceResource
{
    [Obsolete("This property is not a part of the official FHIR specification", true)]
    public Markdown? PurposeElement
    {
        get => null;
        set => throw new NotImplementedException();
    }

    [Obsolete("This property is not a part of the official FHIR specification", true)]
    public string? Purpose
    {
        get => null;
        set => throw new NotImplementedException();
    }
}

public partial class CompartmentDefinition : IConformanceResource;
public partial class StructureMap : IVersionableConformanceResource;
public partial class GraphDefinition : IVersionableConformanceResource;
public partial class CodeSystem : IVersionableConformanceResource;
public partial class ConceptMap : IVersionableConformanceResource;
public partial class TestScript : IVersionableConformanceResource;
public partial class Library : IVersionableConformanceResource;
public partial class ExpansionProfile : IVersionableConformanceResource
{
    [Obsolete("This property is not a part of the official FHIR specification", true)]
    public Markdown? PurposeElement
    {
        get => null;
        set => throw new NotImplementedException();
    }

    [Obsolete("This property is not a part of the official FHIR specification", true)]
    public string? Purpose
    {
        get => null;
        set => throw new NotImplementedException();
    }
}

public partial class Questionnaire : IVersionableConformanceResource;

public partial class DataElement : IConformanceResource
{
    [Obsolete("This property is not a part of the official FHIR specification", true)]
    public Markdown? DescriptionElement
    {
        get => null;
        set => throw new NotImplementedException();
    }

    [Obsolete("This property is not a part of the official FHIR specification", true)]
    public Markdown? PurposeElement
    {
        get => null;
        set => throw new NotImplementedException();
    }

    [Obsolete("This property is not a part of the official FHIR specification", true)]
    public string? Description
    {
        get => null;
        set => throw new NotImplementedException();
    }

    [Obsolete("This property is not a part of the official FHIR specification", true)]
    public string? Purpose
    {
        get => null;
        set => throw new NotImplementedException();
    }
}


public partial class NamingSystem : IConformanceResource
{
    [Obsolete("This property is not a part of the official FHIR specification", true)]
    public Markdown? PurposeElement
    {
        get => null;
        set => throw new NotImplementedException();
    }

    [Obsolete("This property is not a part of the official FHIR specification", true)]
    public bool? Experimental
    {
        get => null;
        set => throw new NotImplementedException();
    }

    [Obsolete("This property is not a part of the official FHIR specification", true)]
    public FhirBoolean? ExperimentalElement
    {
        get => null;
        set => throw new NotImplementedException();
    }

    /// <summary>
    /// Will return the (first) preferred UniqueId, or the first UniqueId if there is no preferred UniqueId
    /// </summary>
    public string? Url
    {
        get
        {
            var preferred = UniqueId.FirstOrDefault(id => id.Preferred == true)?.Value;
            return preferred ?? UniqueId.FirstOrDefault()?.Value;
        }
        set { throw new NotImplementedException(); }
    }

    public FhirUri? UrlElement
    {
        get => Url != null ? new FhirUri(Url) : null;
        set => throw new NotImplementedException();
    }

    public string? Purpose
    {
        get => null;
        set => throw new NotImplementedException();
    }
}