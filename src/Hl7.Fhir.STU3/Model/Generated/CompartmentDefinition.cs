// <auto-generated/>
// Contents of: hl7.fhir.r3.expansions@3.0.2, hl7.fhir.r3.core@3.0.2

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using SystemPrimitive = Hl7.Fhir.ElementModel.Types;

/*
  Copyright (c) 2011+, HL7, Inc.
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

namespace Hl7.Fhir.Model
{
  /// <summary>
  /// Compartment Definition for a resource
  /// </summary>
  /// <remarks>
  /// A compartment definition that defines how resources are accessed on a server.
  /// In FHIR, search is not performed directly on a resource (by XML or JSON path), but on a named parameter that maps into the resource content.
  /// </remarks>
  [Serializable]
  [DataContract]
  [FhirType("CompartmentDefinition","http://hl7.org/fhir/StructureDefinition/CompartmentDefinition", IsResource=true)]
  public partial class CompartmentDefinition : Hl7.Fhir.Model.DomainResource
  {
    /// <summary>
    /// FHIR Type Name
    /// </summary>
    public override string TypeName { get { return "CompartmentDefinition"; } }

    /// <summary>
    /// How a resource is related to the compartment
    /// </summary>
    /// <remarks>
    /// Information about how a resource is related to the compartment.
    /// </remarks>
    [Serializable]
    [DataContract]
    [FhirType("CompartmentDefinition#Resource", IsNestedType=true)]
    [BackboneType("CompartmentDefinition.resource")]
    public partial class ResourceComponent : Hl7.Fhir.Model.BackboneElement
    {
      /// <summary>
      /// FHIR Type Name
      /// </summary>
      public override string TypeName { get { return "CompartmentDefinition#Resource"; } }

      /// <summary>
      /// Name of resource type
      /// </summary>
      [FhirElement("code", InSummary=true, Order=40)]
      [DeclaredType(Type = typeof(Code))]
      [Binding("ResourceType")]
      [Cardinality(Min=1,Max=1)]
      [DataMember]
      public Code<Hl7.Fhir.Model.ResourceType> CodeElement
      {
        get { return _CodeElement; }
        set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
      }

      private Code<Hl7.Fhir.Model.ResourceType> _CodeElement;

      /// <summary>
      /// Name of resource type
      /// </summary>
      /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
      [IgnoreDataMember]
      public Hl7.Fhir.Model.ResourceType? Code
      {
        get { return CodeElement != null ? CodeElement.Value : null; }
        set
        {
          if (value == null)
            CodeElement = null;
          else
            CodeElement = new Code<Hl7.Fhir.Model.ResourceType>(value);
          OnPropertyChanged("Code");
        }
      }

      /// <summary>
      /// Search Parameter Name, or chained parameters
      /// </summary>
      [FhirElement("param", InSummary=true, Order=50)]
      [Cardinality(Min=0,Max=-1)]
      [DataMember]
      public List<Hl7.Fhir.Model.FhirString> ParamElement
      {
        get { if(_ParamElement==null) _ParamElement = new List<Hl7.Fhir.Model.FhirString>(); return _ParamElement; }
        set { _ParamElement = value; OnPropertyChanged("ParamElement"); }
      }

      private List<Hl7.Fhir.Model.FhirString> _ParamElement;

      /// <summary>
      /// Search Parameter Name, or chained parameters
      /// </summary>
      /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
      [IgnoreDataMember]
      public IEnumerable<string> Param
      {
        get { return ParamElement != null ? ParamElement.Select(elem => elem.Value) : null; }
        set
        {
          if (value == null)
            ParamElement = null;
          else
            ParamElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
          OnPropertyChanged("Param");
        }
      }

      /// <summary>
      /// Additional documentation about the resource and compartment
      /// </summary>
      [FhirElement("documentation", Order=60)]
      [DataMember]
      public Hl7.Fhir.Model.FhirString DocumentationElement
      {
        get { return _DocumentationElement; }
        set { _DocumentationElement = value; OnPropertyChanged("DocumentationElement"); }
      }

      private Hl7.Fhir.Model.FhirString _DocumentationElement;

      /// <summary>
      /// Additional documentation about the resource and compartment
      /// </summary>
      /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
      [IgnoreDataMember]
      public string Documentation
      {
        get { return DocumentationElement != null ? DocumentationElement.Value : null; }
        set
        {
          if (value == null)
            DocumentationElement = null;
          else
            DocumentationElement = new Hl7.Fhir.Model.FhirString(value);
          OnPropertyChanged("Documentation");
        }
      }

      public override IDeepCopyable CopyTo(IDeepCopyable other)
      {
        var dest = other as ResourceComponent;

        if (dest == null)
        {
          throw new ArgumentException("Can only copy to an object of the same type", "other");
        }

        base.CopyTo(dest);
        if(CodeElement != null) dest.CodeElement = (Code<Hl7.Fhir.Model.ResourceType>)CodeElement.DeepCopy();
        if(ParamElement.Any()) dest.ParamElement = new List<Hl7.Fhir.Model.FhirString>(ParamElement.DeepCopy());
        if(DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
        return dest;
      }

      public override IDeepCopyable DeepCopy()
      {
        return CopyTo(new ResourceComponent());
      }

      ///<inheritdoc />
      public override bool Matches(IDeepComparable other)
      {
        var otherT = other as ResourceComponent;
        if(otherT == null) return false;

        if(!base.Matches(otherT)) return false;
        if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
        if( !DeepComparable.Matches(ParamElement, otherT.ParamElement)) return false;
        if( !DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;

        return true;
      }

      public override bool IsExactly(IDeepComparable other)
      {
        var otherT = other as ResourceComponent;
        if(otherT == null) return false;

        if(!base.IsExactly(otherT)) return false;
        if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
        if( !DeepComparable.IsExactly(ParamElement, otherT.ParamElement)) return false;
        if( !DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;

        return true;
      }

      [IgnoreDataMember]
      public override IEnumerable<Base> Children
      {
        get
        {
          foreach (var item in base.Children) yield return item;
          if (CodeElement != null) yield return CodeElement;
          foreach (var elem in ParamElement) { if (elem != null) yield return elem; }
          if (DocumentationElement != null) yield return DocumentationElement;
        }
      }

      [IgnoreDataMember]
      public override IEnumerable<ElementValue> NamedChildren
      {
        get
        {
          foreach (var item in base.NamedChildren) yield return item;
          if (CodeElement != null) yield return new ElementValue("code", CodeElement);
          foreach (var elem in ParamElement) { if (elem != null) yield return new ElementValue("param", elem); }
          if (DocumentationElement != null) yield return new ElementValue("documentation", DocumentationElement);
        }
      }

      protected override bool TryGetValue(string key, out object value)
      {
        switch (key)
        {
          case "code":
            value = CodeElement;
            return CodeElement is not null;
          case "param":
            value = ParamElement;
            return ParamElement?.Any() == true;
          case "documentation":
            value = DocumentationElement;
            return DocumentationElement is not null;
          default:
            return base.TryGetValue(key, out value);
        }

      }

      protected override Base SetValue(string key, object value)
      {
        switch (key)
        {
          case "code":
            CodeElement = (Code<Hl7.Fhir.Model.ResourceType>)value;
            return this;
          case "param":
            ParamElement = (List<Hl7.Fhir.Model.FhirString>)value;
            return this;
          case "documentation":
            DocumentationElement = (Hl7.Fhir.Model.FhirString)value;
            return this;
          default:
            return base.SetValue(key, value);
        }

      }

      protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
      {
        foreach (var kvp in base.GetElementPairs()) yield return kvp;
        if (CodeElement is not null) yield return new KeyValuePair<string,object>("code",CodeElement);
        if (ParamElement?.Any() == true) yield return new KeyValuePair<string,object>("param",ParamElement);
        if (DocumentationElement is not null) yield return new KeyValuePair<string,object>("documentation",DocumentationElement);
      }

    }

    /// <summary>
    /// Logical URI to reference this compartment definition (globally unique)
    /// </summary>
    [FhirElement("url", InSummary=true, Order=90, FiveWs="id")]
    [Cardinality(Min=1,Max=1)]
    [DataMember]
    public Hl7.Fhir.Model.FhirUri UrlElement
    {
      get { return _UrlElement; }
      set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
    }

    private Hl7.Fhir.Model.FhirUri _UrlElement;

    /// <summary>
    /// Logical URI to reference this compartment definition (globally unique)
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public string Url
    {
      get { return UrlElement != null ? UrlElement.Value : null; }
      set
      {
        if (value == null)
          UrlElement = null;
        else
          UrlElement = new Hl7.Fhir.Model.FhirUri(value);
        OnPropertyChanged("Url");
      }
    }

    /// <summary>
    /// Name for this compartment definition (computer friendly)
    /// </summary>
    [FhirElement("name", InSummary=true, Order=100)]
    [Cardinality(Min=1,Max=1)]
    [DataMember]
    public Hl7.Fhir.Model.FhirString NameElement
    {
      get { return _NameElement; }
      set { _NameElement = value; OnPropertyChanged("NameElement"); }
    }

    private Hl7.Fhir.Model.FhirString _NameElement;

    /// <summary>
    /// Name for this compartment definition (computer friendly)
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public string Name
    {
      get { return NameElement != null ? NameElement.Value : null; }
      set
      {
        if (value == null)
          NameElement = null;
        else
          NameElement = new Hl7.Fhir.Model.FhirString(value);
        OnPropertyChanged("Name");
      }
    }

    /// <summary>
    /// Name for this compartment definition (human friendly)
    /// </summary>
    [FhirElement("title", InSummary=true, Order=110)]
    [DataMember]
    public Hl7.Fhir.Model.FhirString TitleElement
    {
      get { return _TitleElement; }
      set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
    }

    private Hl7.Fhir.Model.FhirString _TitleElement;

    /// <summary>
    /// Name for this compartment definition (human friendly)
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public string Title
    {
      get { return TitleElement != null ? TitleElement.Value : null; }
      set
      {
        if (value == null)
          TitleElement = null;
        else
          TitleElement = new Hl7.Fhir.Model.FhirString(value);
        OnPropertyChanged("Title");
      }
    }

    /// <summary>
    /// draft | active | retired | unknown
    /// </summary>
    [FhirElement("status", InSummary=true, IsModifier=true, Order=120, FiveWs="status")]
    [DeclaredType(Type = typeof(Code))]
    [Binding("PublicationStatus")]
    [Cardinality(Min=1,Max=1)]
    [DataMember]
    public Code<Hl7.Fhir.Model.PublicationStatus> StatusElement
    {
      get { return _StatusElement; }
      set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
    }

    private Code<Hl7.Fhir.Model.PublicationStatus> _StatusElement;

    /// <summary>
    /// draft | active | retired | unknown
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public Hl7.Fhir.Model.PublicationStatus? Status
    {
      get { return StatusElement != null ? StatusElement.Value : null; }
      set
      {
        if (value == null)
          StatusElement = null;
        else
          StatusElement = new Code<Hl7.Fhir.Model.PublicationStatus>(value);
        OnPropertyChanged("Status");
      }
    }

    /// <summary>
    /// For testing purposes, not real usage
    /// </summary>
    [FhirElement("experimental", InSummary=true, IsModifier=true, Order=130, FiveWs="class")]
    [DataMember]
    public Hl7.Fhir.Model.FhirBoolean ExperimentalElement
    {
      get { return _ExperimentalElement; }
      set { _ExperimentalElement = value; OnPropertyChanged("ExperimentalElement"); }
    }

    private Hl7.Fhir.Model.FhirBoolean _ExperimentalElement;

    /// <summary>
    /// For testing purposes, not real usage
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public bool? Experimental
    {
      get { return ExperimentalElement != null ? ExperimentalElement.Value : null; }
      set
      {
        if (value == null)
          ExperimentalElement = null;
        else
          ExperimentalElement = new Hl7.Fhir.Model.FhirBoolean(value);
        OnPropertyChanged("Experimental");
      }
    }

    /// <summary>
    /// Date this was last changed
    /// </summary>
    [FhirElement("date", InSummary=true, Order=140, FiveWs="when.recorded")]
    [DataMember]
    public Hl7.Fhir.Model.FhirDateTime DateElement
    {
      get { return _DateElement; }
      set { _DateElement = value; OnPropertyChanged("DateElement"); }
    }

    private Hl7.Fhir.Model.FhirDateTime _DateElement;

    /// <summary>
    /// Date this was last changed
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public string Date
    {
      get { return DateElement != null ? DateElement.Value : null; }
      set
      {
        if (value == null)
          DateElement = null;
        else
          DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
        OnPropertyChanged("Date");
      }
    }

    /// <summary>
    /// Name of the publisher (organization or individual)
    /// </summary>
    [FhirElement("publisher", InSummary=true, Order=150, FiveWs="who.witness")]
    [DataMember]
    public Hl7.Fhir.Model.FhirString PublisherElement
    {
      get { return _PublisherElement; }
      set { _PublisherElement = value; OnPropertyChanged("PublisherElement"); }
    }

    private Hl7.Fhir.Model.FhirString _PublisherElement;

    /// <summary>
    /// Name of the publisher (organization or individual)
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public string Publisher
    {
      get { return PublisherElement != null ? PublisherElement.Value : null; }
      set
      {
        if (value == null)
          PublisherElement = null;
        else
          PublisherElement = new Hl7.Fhir.Model.FhirString(value);
        OnPropertyChanged("Publisher");
      }
    }

    /// <summary>
    /// Contact details for the publisher
    /// </summary>
    [FhirElement("contact", InSummary=true, Order=160)]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.ContactDetail> Contact
    {
      get { if(_Contact==null) _Contact = new List<Hl7.Fhir.Model.ContactDetail>(); return _Contact; }
      set { _Contact = value; OnPropertyChanged("Contact"); }
    }

    private List<Hl7.Fhir.Model.ContactDetail> _Contact;

    /// <summary>
    /// Natural language description of the compartment definition
    /// </summary>
    [FhirElement("description", Order=170)]
    [DataMember]
    public Hl7.Fhir.Model.Markdown DescriptionElement
    {
      get { return _DescriptionElement; }
      set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
    }

    private Hl7.Fhir.Model.Markdown _DescriptionElement;

    /// <summary>
    /// Natural language description of the compartment definition
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public string Description
    {
      get { return DescriptionElement != null ? DescriptionElement.Value : null; }
      set
      {
        if (value == null)
          DescriptionElement = null;
        else
          DescriptionElement = new Hl7.Fhir.Model.Markdown(value);
        OnPropertyChanged("Description");
      }
    }

    /// <summary>
    /// Why this compartment definition is defined
    /// </summary>
    [FhirElement("purpose", Order=180, FiveWs="why")]
    [DataMember]
    public Hl7.Fhir.Model.Markdown PurposeElement
    {
      get { return _PurposeElement; }
      set { _PurposeElement = value; OnPropertyChanged("PurposeElement"); }
    }

    private Hl7.Fhir.Model.Markdown _PurposeElement;

    /// <summary>
    /// Why this compartment definition is defined
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public string Purpose
    {
      get { return PurposeElement != null ? PurposeElement.Value : null; }
      set
      {
        if (value == null)
          PurposeElement = null;
        else
          PurposeElement = new Hl7.Fhir.Model.Markdown(value);
        OnPropertyChanged("Purpose");
      }
    }

    /// <summary>
    /// Context the content is intended to support
    /// </summary>
    [FhirElement("useContext", InSummary=true, Order=190)]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.UsageContext> UseContext
    {
      get { if(_UseContext==null) _UseContext = new List<Hl7.Fhir.Model.UsageContext>(); return _UseContext; }
      set { _UseContext = value; OnPropertyChanged("UseContext"); }
    }

    private List<Hl7.Fhir.Model.UsageContext> _UseContext;

    /// <summary>
    /// Intended jurisdiction for compartment definition (if applicable)
    /// </summary>
    [FhirElement("jurisdiction", InSummary=true, Order=200)]
    [Binding("Jurisdiction")]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.CodeableConcept> Jurisdiction
    {
      get { if(_Jurisdiction==null) _Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Jurisdiction; }
      set { _Jurisdiction = value; OnPropertyChanged("Jurisdiction"); }
    }

    private List<Hl7.Fhir.Model.CodeableConcept> _Jurisdiction;

    /// <summary>
    /// Patient | Encounter | RelatedPerson | Practitioner | Device
    /// </summary>
    [FhirElement("code", InSummary=true, Order=210)]
    [DeclaredType(Type = typeof(Code))]
    [Binding("CompartmentType")]
    [Cardinality(Min=1,Max=1)]
    [DataMember]
    public Code<Hl7.Fhir.Model.CompartmentType> CodeElement
    {
      get { return _CodeElement; }
      set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
    }

    private Code<Hl7.Fhir.Model.CompartmentType> _CodeElement;

    /// <summary>
    /// Patient | Encounter | RelatedPerson | Practitioner | Device
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public Hl7.Fhir.Model.CompartmentType? Code
    {
      get { return CodeElement != null ? CodeElement.Value : null; }
      set
      {
        if (value == null)
          CodeElement = null;
        else
          CodeElement = new Code<Hl7.Fhir.Model.CompartmentType>(value);
        OnPropertyChanged("Code");
      }
    }

    /// <summary>
    /// Whether the search syntax is supported
    /// </summary>
    [FhirElement("search", InSummary=true, Order=220)]
    [Cardinality(Min=1,Max=1)]
    [DataMember]
    public Hl7.Fhir.Model.FhirBoolean SearchElement
    {
      get { return _SearchElement; }
      set { _SearchElement = value; OnPropertyChanged("SearchElement"); }
    }

    private Hl7.Fhir.Model.FhirBoolean _SearchElement;

    /// <summary>
    /// Whether the search syntax is supported
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public bool? Search
    {
      get { return SearchElement != null ? SearchElement.Value : null; }
      set
      {
        if (value == null)
          SearchElement = null;
        else
          SearchElement = new Hl7.Fhir.Model.FhirBoolean(value);
        OnPropertyChanged("Search");
      }
    }

    /// <summary>
    /// How a resource is related to the compartment
    /// </summary>
    [FhirElement("resource", InSummary=true, Order=230)]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.CompartmentDefinition.ResourceComponent> Resource
    {
      get { if(_Resource==null) _Resource = new List<Hl7.Fhir.Model.CompartmentDefinition.ResourceComponent>(); return _Resource; }
      set { _Resource = value; OnPropertyChanged("Resource"); }
    }

    private List<Hl7.Fhir.Model.CompartmentDefinition.ResourceComponent> _Resource;

    public override IDeepCopyable CopyTo(IDeepCopyable other)
    {
      var dest = other as CompartmentDefinition;

      if (dest == null)
      {
        throw new ArgumentException("Can only copy to an object of the same type", "other");
      }

      base.CopyTo(dest);
      if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
      if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
      if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
      if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.PublicationStatus>)StatusElement.DeepCopy();
      if(ExperimentalElement != null) dest.ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)ExperimentalElement.DeepCopy();
      if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
      if(PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
      if(Contact.Any()) dest.Contact = new List<Hl7.Fhir.Model.ContactDetail>(Contact.DeepCopy());
      if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.Markdown)DescriptionElement.DeepCopy();
      if(PurposeElement != null) dest.PurposeElement = (Hl7.Fhir.Model.Markdown)PurposeElement.DeepCopy();
      if(UseContext.Any()) dest.UseContext = new List<Hl7.Fhir.Model.UsageContext>(UseContext.DeepCopy());
      if(Jurisdiction.Any()) dest.Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(Jurisdiction.DeepCopy());
      if(CodeElement != null) dest.CodeElement = (Code<Hl7.Fhir.Model.CompartmentType>)CodeElement.DeepCopy();
      if(SearchElement != null) dest.SearchElement = (Hl7.Fhir.Model.FhirBoolean)SearchElement.DeepCopy();
      if(Resource.Any()) dest.Resource = new List<Hl7.Fhir.Model.CompartmentDefinition.ResourceComponent>(Resource.DeepCopy());
      return dest;
    }

    public override IDeepCopyable DeepCopy()
    {
      return CopyTo(new CompartmentDefinition());
    }

    ///<inheritdoc />
    public override bool Matches(IDeepComparable other)
    {
      var otherT = other as CompartmentDefinition;
      if(otherT == null) return false;

      if(!base.Matches(otherT)) return false;
      if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
      if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
      if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
      if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
      if( !DeepComparable.Matches(ExperimentalElement, otherT.ExperimentalElement)) return false;
      if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
      if( !DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
      if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
      if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
      if( !DeepComparable.Matches(PurposeElement, otherT.PurposeElement)) return false;
      if( !DeepComparable.Matches(UseContext, otherT.UseContext)) return false;
      if( !DeepComparable.Matches(Jurisdiction, otherT.Jurisdiction)) return false;
      if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
      if( !DeepComparable.Matches(SearchElement, otherT.SearchElement)) return false;
      if( !DeepComparable.Matches(Resource, otherT.Resource)) return false;

      return true;
    }

    public override bool IsExactly(IDeepComparable other)
    {
      var otherT = other as CompartmentDefinition;
      if(otherT == null) return false;

      if(!base.IsExactly(otherT)) return false;
      if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
      if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
      if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
      if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
      if( !DeepComparable.IsExactly(ExperimentalElement, otherT.ExperimentalElement)) return false;
      if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
      if( !DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
      if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
      if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
      if( !DeepComparable.IsExactly(PurposeElement, otherT.PurposeElement)) return false;
      if( !DeepComparable.IsExactly(UseContext, otherT.UseContext)) return false;
      if( !DeepComparable.IsExactly(Jurisdiction, otherT.Jurisdiction)) return false;
      if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
      if( !DeepComparable.IsExactly(SearchElement, otherT.SearchElement)) return false;
      if( !DeepComparable.IsExactly(Resource, otherT.Resource)) return false;

      return true;
    }

    [IgnoreDataMember]
    public override IEnumerable<Base> Children
    {
      get
      {
        foreach (var item in base.Children) yield return item;
        if (UrlElement != null) yield return UrlElement;
        if (NameElement != null) yield return NameElement;
        if (TitleElement != null) yield return TitleElement;
        if (StatusElement != null) yield return StatusElement;
        if (ExperimentalElement != null) yield return ExperimentalElement;
        if (DateElement != null) yield return DateElement;
        if (PublisherElement != null) yield return PublisherElement;
        foreach (var elem in Contact) { if (elem != null) yield return elem; }
        if (DescriptionElement != null) yield return DescriptionElement;
        if (PurposeElement != null) yield return PurposeElement;
        foreach (var elem in UseContext) { if (elem != null) yield return elem; }
        foreach (var elem in Jurisdiction) { if (elem != null) yield return elem; }
        if (CodeElement != null) yield return CodeElement;
        if (SearchElement != null) yield return SearchElement;
        foreach (var elem in Resource) { if (elem != null) yield return elem; }
      }
    }

    [IgnoreDataMember]
    public override IEnumerable<ElementValue> NamedChildren
    {
      get
      {
        foreach (var item in base.NamedChildren) yield return item;
        if (UrlElement != null) yield return new ElementValue("url", UrlElement);
        if (NameElement != null) yield return new ElementValue("name", NameElement);
        if (TitleElement != null) yield return new ElementValue("title", TitleElement);
        if (StatusElement != null) yield return new ElementValue("status", StatusElement);
        if (ExperimentalElement != null) yield return new ElementValue("experimental", ExperimentalElement);
        if (DateElement != null) yield return new ElementValue("date", DateElement);
        if (PublisherElement != null) yield return new ElementValue("publisher", PublisherElement);
        foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
        if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
        if (PurposeElement != null) yield return new ElementValue("purpose", PurposeElement);
        foreach (var elem in UseContext) { if (elem != null) yield return new ElementValue("useContext", elem); }
        foreach (var elem in Jurisdiction) { if (elem != null) yield return new ElementValue("jurisdiction", elem); }
        if (CodeElement != null) yield return new ElementValue("code", CodeElement);
        if (SearchElement != null) yield return new ElementValue("search", SearchElement);
        foreach (var elem in Resource) { if (elem != null) yield return new ElementValue("resource", elem); }
      }
    }

    protected override bool TryGetValue(string key, out object value)
    {
      switch (key)
      {
        case "url":
          value = UrlElement;
          return UrlElement is not null;
        case "name":
          value = NameElement;
          return NameElement is not null;
        case "title":
          value = TitleElement;
          return TitleElement is not null;
        case "status":
          value = StatusElement;
          return StatusElement is not null;
        case "experimental":
          value = ExperimentalElement;
          return ExperimentalElement is not null;
        case "date":
          value = DateElement;
          return DateElement is not null;
        case "publisher":
          value = PublisherElement;
          return PublisherElement is not null;
        case "contact":
          value = Contact;
          return Contact?.Any() == true;
        case "description":
          value = DescriptionElement;
          return DescriptionElement is not null;
        case "purpose":
          value = PurposeElement;
          return PurposeElement is not null;
        case "useContext":
          value = UseContext;
          return UseContext?.Any() == true;
        case "jurisdiction":
          value = Jurisdiction;
          return Jurisdiction?.Any() == true;
        case "code":
          value = CodeElement;
          return CodeElement is not null;
        case "search":
          value = SearchElement;
          return SearchElement is not null;
        case "resource":
          value = Resource;
          return Resource?.Any() == true;
        default:
          return base.TryGetValue(key, out value);
      }

    }

    protected override Base SetValue(string key, object value)
    {
      switch (key)
      {
        case "url":
          UrlElement = (Hl7.Fhir.Model.FhirUri)value;
          return this;
        case "name":
          NameElement = (Hl7.Fhir.Model.FhirString)value;
          return this;
        case "title":
          TitleElement = (Hl7.Fhir.Model.FhirString)value;
          return this;
        case "status":
          StatusElement = (Code<Hl7.Fhir.Model.PublicationStatus>)value;
          return this;
        case "experimental":
          ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)value;
          return this;
        case "date":
          DateElement = (Hl7.Fhir.Model.FhirDateTime)value;
          return this;
        case "publisher":
          PublisherElement = (Hl7.Fhir.Model.FhirString)value;
          return this;
        case "contact":
          Contact = (List<Hl7.Fhir.Model.ContactDetail>)value;
          return this;
        case "description":
          DescriptionElement = (Hl7.Fhir.Model.Markdown)value;
          return this;
        case "purpose":
          PurposeElement = (Hl7.Fhir.Model.Markdown)value;
          return this;
        case "useContext":
          UseContext = (List<Hl7.Fhir.Model.UsageContext>)value;
          return this;
        case "jurisdiction":
          Jurisdiction = (List<Hl7.Fhir.Model.CodeableConcept>)value;
          return this;
        case "code":
          CodeElement = (Code<Hl7.Fhir.Model.CompartmentType>)value;
          return this;
        case "search":
          SearchElement = (Hl7.Fhir.Model.FhirBoolean)value;
          return this;
        case "resource":
          Resource = (List<Hl7.Fhir.Model.CompartmentDefinition.ResourceComponent>)value;
          return this;
        default:
          return base.SetValue(key, value);
      }

    }

    protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
    {
      foreach (var kvp in base.GetElementPairs()) yield return kvp;
      if (UrlElement is not null) yield return new KeyValuePair<string,object>("url",UrlElement);
      if (NameElement is not null) yield return new KeyValuePair<string,object>("name",NameElement);
      if (TitleElement is not null) yield return new KeyValuePair<string,object>("title",TitleElement);
      if (StatusElement is not null) yield return new KeyValuePair<string,object>("status",StatusElement);
      if (ExperimentalElement is not null) yield return new KeyValuePair<string,object>("experimental",ExperimentalElement);
      if (DateElement is not null) yield return new KeyValuePair<string,object>("date",DateElement);
      if (PublisherElement is not null) yield return new KeyValuePair<string,object>("publisher",PublisherElement);
      if (Contact?.Any() == true) yield return new KeyValuePair<string,object>("contact",Contact);
      if (DescriptionElement is not null) yield return new KeyValuePair<string,object>("description",DescriptionElement);
      if (PurposeElement is not null) yield return new KeyValuePair<string,object>("purpose",PurposeElement);
      if (UseContext?.Any() == true) yield return new KeyValuePair<string,object>("useContext",UseContext);
      if (Jurisdiction?.Any() == true) yield return new KeyValuePair<string,object>("jurisdiction",Jurisdiction);
      if (CodeElement is not null) yield return new KeyValuePair<string,object>("code",CodeElement);
      if (SearchElement is not null) yield return new KeyValuePair<string,object>("search",SearchElement);
      if (Resource?.Any() == true) yield return new KeyValuePair<string,object>("resource",Resource);
    }

  }

}

// end of file
