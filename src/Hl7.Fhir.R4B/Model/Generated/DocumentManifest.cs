// <auto-generated/>
// Contents of: hl7.fhir.r4b.expansions@4.3.0, hl7.fhir.r4b.core@4.3.0

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
  /// A list that defines a set of documents
  /// </summary>
  /// <remarks>
  /// A collection of documents compiled for a purpose together with metadata that applies to the collection.
  /// </remarks>
  [Serializable]
  [DataContract]
  [FhirType("DocumentManifest","http://hl7.org/fhir/StructureDefinition/DocumentManifest")]
  public partial class DocumentManifest : Hl7.Fhir.Model.DomainResource, IIdentifiable<List<Identifier>>
  {
    /// <summary>
    /// FHIR Type Name
    /// </summary>
    public override string TypeName { get { return "DocumentManifest"; } }

    /// <summary>
    /// Related things
    /// </summary>
    /// <remarks>
    /// Related identifiers or resources associated with the DocumentManifest.
    /// May be identifiers or resources that caused the DocumentManifest to be created.
    /// </remarks>
    [Serializable]
    [DataContract]
    [FhirType("DocumentManifest.related", IsBackboneType=true)]
    public partial class RelatedComponent : Hl7.Fhir.Model.BackboneElement
    {
      /// <summary>
      /// FHIR Type Name
      /// </summary>
      public override string TypeName { get { return "DocumentManifest.related"; } }

      /// <summary>
      /// Identifiers of things that are related
      /// </summary>
      [FhirElement("identifier", Order=40)]
      [DataMember]
      public Hl7.Fhir.Model.Identifier Identifier
      {
        get { return _Identifier; }
        set { _Identifier = value; OnPropertyChanged("Identifier"); }
      }

      private Hl7.Fhir.Model.Identifier _Identifier;

      /// <summary>
      /// Related Resource
      /// </summary>
      [FhirElement("ref", Order=50)]
      [CLSCompliant(false)]
      [References("Resource")]
      [DataMember]
      public Hl7.Fhir.Model.ResourceReference Ref
      {
        get { return _Ref; }
        set { _Ref = value; OnPropertyChanged("Ref"); }
      }

      private Hl7.Fhir.Model.ResourceReference _Ref;

      public override IDeepCopyable CopyTo(IDeepCopyable other)
      {
        var dest = other as RelatedComponent;

        if (dest == null)
        {
          throw new ArgumentException("Can only copy to an object of the same type", "other");
        }

        base.CopyTo(dest);
        if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
        if(Ref != null) dest.Ref = (Hl7.Fhir.Model.ResourceReference)Ref.DeepCopy();
        return dest;
      }

      public override IDeepCopyable DeepCopy()
      {
        return CopyTo(new RelatedComponent());
      }

      ///<inheritdoc />
      public override bool Matches(IDeepComparable other)
      {
        var otherT = other as RelatedComponent;
        if(otherT == null) return false;

        if(!base.Matches(otherT)) return false;
        if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
        if( !DeepComparable.Matches(Ref, otherT.Ref)) return false;

        return true;
      }

      public override bool IsExactly(IDeepComparable other)
      {
        var otherT = other as RelatedComponent;
        if(otherT == null) return false;

        if(!base.IsExactly(otherT)) return false;
        if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
        if( !DeepComparable.IsExactly(Ref, otherT.Ref)) return false;

        return true;
      }

      [IgnoreDataMember]
      public override IEnumerable<Base> Children
      {
        get
        {
          foreach (var item in base.Children) yield return item;
          if (Identifier != null) yield return Identifier;
          if (Ref != null) yield return Ref;
        }
      }

      [IgnoreDataMember]
      public override IEnumerable<ElementValue> NamedChildren
      {
        get
        {
          foreach (var item in base.NamedChildren) yield return item;
          if (Identifier != null) yield return new ElementValue("identifier", Identifier);
          if (Ref != null) yield return new ElementValue("ref", Ref);
        }
      }

      protected override bool TryGetValue(string key, out object value)
      {
        switch (key)
        {
          case "identifier":
            value = Identifier;
            return Identifier is not null;
          case "ref":
            value = Ref;
            return Ref is not null;
          default:
            return base.TryGetValue(key, out value);
        }

      }

      protected override Base SetValue(string key, object value)
      {
        switch (key)
        {
          case "identifier":
            Identifier = (Hl7.Fhir.Model.Identifier)value;
            return this;
          case "ref":
            Ref = (Hl7.Fhir.Model.ResourceReference)value;
            return this;
          default:
            return base.SetValue(key, value);
        }

      }

      protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
      {
        foreach (var kvp in base.GetElementPairs()) yield return kvp;
        if (Identifier is not null) yield return new KeyValuePair<string,object>("identifier",Identifier);
        if (Ref is not null) yield return new KeyValuePair<string,object>("ref",Ref);
      }

    }

    /// <summary>
    /// Unique Identifier for the set of documents
    /// </summary>
    [FhirElement("masterIdentifier", InSummary=true, Order=90, FiveWs="FiveWs.identifier")]
    [DataMember]
    public Hl7.Fhir.Model.Identifier MasterIdentifier
    {
      get { return _MasterIdentifier; }
      set { _MasterIdentifier = value; OnPropertyChanged("MasterIdentifier"); }
    }

    private Hl7.Fhir.Model.Identifier _MasterIdentifier;

    /// <summary>
    /// Other identifiers for the manifest
    /// </summary>
    [FhirElement("identifier", InSummary=true, Order=100, FiveWs="FiveWs.identifier")]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.Identifier> Identifier
    {
      get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
      set { _Identifier = value; OnPropertyChanged("Identifier"); }
    }

    private List<Hl7.Fhir.Model.Identifier> _Identifier;

    /// <summary>
    /// current | superseded | entered-in-error
    /// </summary>
    [FhirElement("status", InSummary=true, IsModifier=true, Order=110, FiveWs="FiveWs.status")]
    [DeclaredType(Type = typeof(Code))]
    [Binding("DocumentReferenceStatus")]
    [Cardinality(Min=1,Max=1)]
    [DataMember]
    public Code<Hl7.Fhir.Model.DocumentReferenceStatus> StatusElement
    {
      get { return _StatusElement; }
      set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
    }

    private Code<Hl7.Fhir.Model.DocumentReferenceStatus> _StatusElement;

    /// <summary>
    /// current | superseded | entered-in-error
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public Hl7.Fhir.Model.DocumentReferenceStatus? Status
    {
      get { return StatusElement != null ? StatusElement.Value : null; }
      set
      {
        if (value == null)
          StatusElement = null;
        else
          StatusElement = new Code<Hl7.Fhir.Model.DocumentReferenceStatus>(value);
        OnPropertyChanged("Status");
      }
    }

    /// <summary>
    /// Kind of document set
    /// </summary>
    [FhirElement("type", InSummary=true, Order=120, FiveWs="FiveWs.class")]
    [Binding("v3Act")]
    [DataMember]
    public Hl7.Fhir.Model.CodeableConcept Type
    {
      get { return _Type; }
      set { _Type = value; OnPropertyChanged("Type"); }
    }

    private Hl7.Fhir.Model.CodeableConcept _Type;

    /// <summary>
    /// The subject of the set of documents
    /// </summary>
    [FhirElement("subject", InSummary=true, Order=130, FiveWs="FiveWs.subject")]
    [CLSCompliant(false)]
    [References("Patient","Practitioner","Group","Device")]
    [DataMember]
    public Hl7.Fhir.Model.ResourceReference Subject
    {
      get { return _Subject; }
      set { _Subject = value; OnPropertyChanged("Subject"); }
    }

    private Hl7.Fhir.Model.ResourceReference _Subject;

    /// <summary>
    /// When this document manifest created
    /// </summary>
    [FhirElement("created", Order=140, FiveWs="FiveWs.done[x]")]
    [DataMember]
    public Hl7.Fhir.Model.FhirDateTime CreatedElement
    {
      get { return _CreatedElement; }
      set { _CreatedElement = value; OnPropertyChanged("CreatedElement"); }
    }

    private Hl7.Fhir.Model.FhirDateTime _CreatedElement;

    /// <summary>
    /// When this document manifest created
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public string Created
    {
      get { return CreatedElement != null ? CreatedElement.Value : null; }
      set
      {
        if (value == null)
          CreatedElement = null;
        else
          CreatedElement = new Hl7.Fhir.Model.FhirDateTime(value);
        OnPropertyChanged("Created");
      }
    }

    /// <summary>
    /// Who and/or what authored the DocumentManifest
    /// </summary>
    [FhirElement("author", InSummary=true, Order=150, FiveWs="FiveWs.author")]
    [CLSCompliant(false)]
    [References("Practitioner","PractitionerRole","Organization","Device","Patient","RelatedPerson")]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.ResourceReference> Author
    {
      get { if(_Author==null) _Author = new List<Hl7.Fhir.Model.ResourceReference>(); return _Author; }
      set { _Author = value; OnPropertyChanged("Author"); }
    }

    private List<Hl7.Fhir.Model.ResourceReference> _Author;

    /// <summary>
    /// Intended to get notified about this set of documents
    /// </summary>
    [FhirElement("recipient", Order=160, FiveWs="FiveWs.cause")]
    [CLSCompliant(false)]
    [References("Patient","Practitioner","PractitionerRole","RelatedPerson","Organization")]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.ResourceReference> Recipient
    {
      get { if(_Recipient==null) _Recipient = new List<Hl7.Fhir.Model.ResourceReference>(); return _Recipient; }
      set { _Recipient = value; OnPropertyChanged("Recipient"); }
    }

    private List<Hl7.Fhir.Model.ResourceReference> _Recipient;

    /// <summary>
    /// The source system/application/software
    /// </summary>
    [FhirElement("source", Order=170)]
    [DataMember]
    public Hl7.Fhir.Model.FhirUri SourceElement
    {
      get { return _SourceElement; }
      set { _SourceElement = value; OnPropertyChanged("SourceElement"); }
    }

    private Hl7.Fhir.Model.FhirUri _SourceElement;

    /// <summary>
    /// The source system/application/software
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public string Source
    {
      get { return SourceElement != null ? SourceElement.Value : null; }
      set
      {
        if (value == null)
          SourceElement = null;
        else
          SourceElement = new Hl7.Fhir.Model.FhirUri(value);
        OnPropertyChanged("Source");
      }
    }

    /// <summary>
    /// Human-readable description (title)
    /// </summary>
    [FhirElement("description", InSummary=true, Order=180)]
    [DataMember]
    public Hl7.Fhir.Model.FhirString DescriptionElement
    {
      get { return _DescriptionElement; }
      set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
    }

    private Hl7.Fhir.Model.FhirString _DescriptionElement;

    /// <summary>
    /// Human-readable description (title)
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
          DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
        OnPropertyChanged("Description");
      }
    }

    /// <summary>
    /// Items in manifest
    /// </summary>
    [FhirElement("content", InSummary=true, Order=190)]
    [CLSCompliant(false)]
    [References("Resource")]
    [Cardinality(Min=1,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.ResourceReference> Content
    {
      get { if(_Content==null) _Content = new List<Hl7.Fhir.Model.ResourceReference>(); return _Content; }
      set { _Content = value; OnPropertyChanged("Content"); }
    }

    private List<Hl7.Fhir.Model.ResourceReference> _Content;

    /// <summary>
    /// Related things
    /// </summary>
    [FhirElement("related", Order=200)]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.DocumentManifest.RelatedComponent> Related
    {
      get { if(_Related==null) _Related = new List<Hl7.Fhir.Model.DocumentManifest.RelatedComponent>(); return _Related; }
      set { _Related = value; OnPropertyChanged("Related"); }
    }

    private List<Hl7.Fhir.Model.DocumentManifest.RelatedComponent> _Related;

    List<Identifier> IIdentifiable<List<Identifier>>.Identifier { get => Identifier; set => Identifier = value; }

    public override IDeepCopyable CopyTo(IDeepCopyable other)
    {
      var dest = other as DocumentManifest;

      if (dest == null)
      {
        throw new ArgumentException("Can only copy to an object of the same type", "other");
      }

      base.CopyTo(dest);
      if(MasterIdentifier != null) dest.MasterIdentifier = (Hl7.Fhir.Model.Identifier)MasterIdentifier.DeepCopy();
      if(Identifier.Any()) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
      if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.DocumentReferenceStatus>)StatusElement.DeepCopy();
      if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
      if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
      if(CreatedElement != null) dest.CreatedElement = (Hl7.Fhir.Model.FhirDateTime)CreatedElement.DeepCopy();
      if(Author.Any()) dest.Author = new List<Hl7.Fhir.Model.ResourceReference>(Author.DeepCopy());
      if(Recipient.Any()) dest.Recipient = new List<Hl7.Fhir.Model.ResourceReference>(Recipient.DeepCopy());
      if(SourceElement != null) dest.SourceElement = (Hl7.Fhir.Model.FhirUri)SourceElement.DeepCopy();
      if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
      if(Content.Any()) dest.Content = new List<Hl7.Fhir.Model.ResourceReference>(Content.DeepCopy());
      if(Related.Any()) dest.Related = new List<Hl7.Fhir.Model.DocumentManifest.RelatedComponent>(Related.DeepCopy());
      return dest;
    }

    public override IDeepCopyable DeepCopy()
    {
      return CopyTo(new DocumentManifest());
    }

    ///<inheritdoc />
    public override bool Matches(IDeepComparable other)
    {
      var otherT = other as DocumentManifest;
      if(otherT == null) return false;

      if(!base.Matches(otherT)) return false;
      if( !DeepComparable.Matches(MasterIdentifier, otherT.MasterIdentifier)) return false;
      if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
      if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
      if( !DeepComparable.Matches(Type, otherT.Type)) return false;
      if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
      if( !DeepComparable.Matches(CreatedElement, otherT.CreatedElement)) return false;
      if( !DeepComparable.Matches(Author, otherT.Author)) return false;
      if( !DeepComparable.Matches(Recipient, otherT.Recipient)) return false;
      if( !DeepComparable.Matches(SourceElement, otherT.SourceElement)) return false;
      if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
      if( !DeepComparable.Matches(Content, otherT.Content)) return false;
      if( !DeepComparable.Matches(Related, otherT.Related)) return false;

      return true;
    }

    public override bool IsExactly(IDeepComparable other)
    {
      var otherT = other as DocumentManifest;
      if(otherT == null) return false;

      if(!base.IsExactly(otherT)) return false;
      if( !DeepComparable.IsExactly(MasterIdentifier, otherT.MasterIdentifier)) return false;
      if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
      if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
      if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
      if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
      if( !DeepComparable.IsExactly(CreatedElement, otherT.CreatedElement)) return false;
      if( !DeepComparable.IsExactly(Author, otherT.Author)) return false;
      if( !DeepComparable.IsExactly(Recipient, otherT.Recipient)) return false;
      if( !DeepComparable.IsExactly(SourceElement, otherT.SourceElement)) return false;
      if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
      if( !DeepComparable.IsExactly(Content, otherT.Content)) return false;
      if( !DeepComparable.IsExactly(Related, otherT.Related)) return false;

      return true;
    }

    [IgnoreDataMember]
    public override IEnumerable<Base> Children
    {
      get
      {
        foreach (var item in base.Children) yield return item;
        if (MasterIdentifier != null) yield return MasterIdentifier;
        foreach (var elem in Identifier) { if (elem != null) yield return elem; }
        if (StatusElement != null) yield return StatusElement;
        if (Type != null) yield return Type;
        if (Subject != null) yield return Subject;
        if (CreatedElement != null) yield return CreatedElement;
        foreach (var elem in Author) { if (elem != null) yield return elem; }
        foreach (var elem in Recipient) { if (elem != null) yield return elem; }
        if (SourceElement != null) yield return SourceElement;
        if (DescriptionElement != null) yield return DescriptionElement;
        foreach (var elem in Content) { if (elem != null) yield return elem; }
        foreach (var elem in Related) { if (elem != null) yield return elem; }
      }
    }

    [IgnoreDataMember]
    public override IEnumerable<ElementValue> NamedChildren
    {
      get
      {
        foreach (var item in base.NamedChildren) yield return item;
        if (MasterIdentifier != null) yield return new ElementValue("masterIdentifier", MasterIdentifier);
        foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
        if (StatusElement != null) yield return new ElementValue("status", StatusElement);
        if (Type != null) yield return new ElementValue("type", Type);
        if (Subject != null) yield return new ElementValue("subject", Subject);
        if (CreatedElement != null) yield return new ElementValue("created", CreatedElement);
        foreach (var elem in Author) { if (elem != null) yield return new ElementValue("author", elem); }
        foreach (var elem in Recipient) { if (elem != null) yield return new ElementValue("recipient", elem); }
        if (SourceElement != null) yield return new ElementValue("source", SourceElement);
        if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
        foreach (var elem in Content) { if (elem != null) yield return new ElementValue("content", elem); }
        foreach (var elem in Related) { if (elem != null) yield return new ElementValue("related", elem); }
      }
    }

    protected override bool TryGetValue(string key, out object value)
    {
      switch (key)
      {
        case "masterIdentifier":
          value = MasterIdentifier;
          return MasterIdentifier is not null;
        case "identifier":
          value = Identifier;
          return Identifier?.Any() == true;
        case "status":
          value = StatusElement;
          return StatusElement is not null;
        case "type":
          value = Type;
          return Type is not null;
        case "subject":
          value = Subject;
          return Subject is not null;
        case "created":
          value = CreatedElement;
          return CreatedElement is not null;
        case "author":
          value = Author;
          return Author?.Any() == true;
        case "recipient":
          value = Recipient;
          return Recipient?.Any() == true;
        case "source":
          value = SourceElement;
          return SourceElement is not null;
        case "description":
          value = DescriptionElement;
          return DescriptionElement is not null;
        case "content":
          value = Content;
          return Content?.Any() == true;
        case "related":
          value = Related;
          return Related?.Any() == true;
        default:
          return base.TryGetValue(key, out value);
      }

    }

    protected override Base SetValue(string key, object value)
    {
      switch (key)
      {
        case "masterIdentifier":
          MasterIdentifier = (Hl7.Fhir.Model.Identifier)value;
          return this;
        case "identifier":
          Identifier = (List<Hl7.Fhir.Model.Identifier>)value;
          return this;
        case "status":
          StatusElement = (Code<Hl7.Fhir.Model.DocumentReferenceStatus>)value;
          return this;
        case "type":
          Type = (Hl7.Fhir.Model.CodeableConcept)value;
          return this;
        case "subject":
          Subject = (Hl7.Fhir.Model.ResourceReference)value;
          return this;
        case "created":
          CreatedElement = (Hl7.Fhir.Model.FhirDateTime)value;
          return this;
        case "author":
          Author = (List<Hl7.Fhir.Model.ResourceReference>)value;
          return this;
        case "recipient":
          Recipient = (List<Hl7.Fhir.Model.ResourceReference>)value;
          return this;
        case "source":
          SourceElement = (Hl7.Fhir.Model.FhirUri)value;
          return this;
        case "description":
          DescriptionElement = (Hl7.Fhir.Model.FhirString)value;
          return this;
        case "content":
          Content = (List<Hl7.Fhir.Model.ResourceReference>)value;
          return this;
        case "related":
          Related = (List<Hl7.Fhir.Model.DocumentManifest.RelatedComponent>)value;
          return this;
        default:
          return base.SetValue(key, value);
      }

    }

    protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
    {
      foreach (var kvp in base.GetElementPairs()) yield return kvp;
      if (MasterIdentifier is not null) yield return new KeyValuePair<string,object>("masterIdentifier",MasterIdentifier);
      if (Identifier?.Any() == true) yield return new KeyValuePair<string,object>("identifier",Identifier);
      if (StatusElement is not null) yield return new KeyValuePair<string,object>("status",StatusElement);
      if (Type is not null) yield return new KeyValuePair<string,object>("type",Type);
      if (Subject is not null) yield return new KeyValuePair<string,object>("subject",Subject);
      if (CreatedElement is not null) yield return new KeyValuePair<string,object>("created",CreatedElement);
      if (Author?.Any() == true) yield return new KeyValuePair<string,object>("author",Author);
      if (Recipient?.Any() == true) yield return new KeyValuePair<string,object>("recipient",Recipient);
      if (SourceElement is not null) yield return new KeyValuePair<string,object>("source",SourceElement);
      if (DescriptionElement is not null) yield return new KeyValuePair<string,object>("description",DescriptionElement);
      if (Content?.Any() == true) yield return new KeyValuePair<string,object>("content",Content);
      if (Related?.Any() == true) yield return new KeyValuePair<string,object>("related",Related);
    }

  }

}

// end of file
