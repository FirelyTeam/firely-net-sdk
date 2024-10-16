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
  /// Links records for 'same' item
  /// </summary>
  /// <remarks>
  /// Identifies two or more records (resource instances) that refer to the same real-world "occurrence".
  /// </remarks>
  [Serializable]
  [DataContract]
  [FhirType("Linkage","http://hl7.org/fhir/StructureDefinition/Linkage", IsResource=true)]
  public partial class Linkage : Hl7.Fhir.Model.DomainResource
  {
    /// <summary>
    /// FHIR Type Name
    /// </summary>
    public override string TypeName { get { return "Linkage"; } }

    /// <summary>
    /// Used to distinguish different roles a resource can play within a set of linked resources.
    /// (url: http://hl7.org/fhir/ValueSet/linkage-type)
    /// (system: http://hl7.org/fhir/linkage-type)
    /// </summary>
    [FhirEnumeration("LinkageType", "http://hl7.org/fhir/ValueSet/linkage-type", "http://hl7.org/fhir/linkage-type")]
    public enum LinkageType
    {
      /// <summary>
      /// The resource represents the \"source of truth\" (from the perspective of this Linkage resource) for the underlying event/condition/etc.
      /// (system: http://hl7.org/fhir/linkage-type)
      /// </summary>
      [EnumLiteral("source"), Description("Source of Truth")]
      Source,
      /// <summary>
      /// The resource represents an alternative view of the underlying event/condition/etc.  The resource may still be actively maintained, even though it is not considered to be the source of truth.
      /// (system: http://hl7.org/fhir/linkage-type)
      /// </summary>
      [EnumLiteral("alternate"), Description("Alternate Record")]
      Alternate,
      /// <summary>
      /// The resource represents an obsolete record of the underlying event/condition/etc.  It is not expected to be actively maintained.
      /// (system: http://hl7.org/fhir/linkage-type)
      /// </summary>
      [EnumLiteral("historical"), Description("Historical/Obsolete Record")]
      Historical,
    }

    /// <summary>
    /// Item to be linked
    /// </summary>
    /// <remarks>
    /// Identifies which record considered as the reference to the same real-world occurrence as well as how the items should be evaluated within the collection of linked items.
    /// </remarks>
    [Serializable]
    [DataContract]
    [FhirType("Linkage#Item", IsNestedType=true)]
    [BackboneType("Linkage.item")]
    public partial class ItemComponent : Hl7.Fhir.Model.BackboneElement
    {
      /// <summary>
      /// FHIR Type Name
      /// </summary>
      public override string TypeName { get { return "Linkage#Item"; } }

      /// <summary>
      /// source | alternate | historical
      /// </summary>
      [FhirElement("type", InSummary=true, Order=40)]
      [DeclaredType(Type = typeof(Code))]
      [Binding("LinkageType")]
      [Cardinality(Min=1,Max=1)]
      [DataMember]
      public Code<Hl7.Fhir.Model.Linkage.LinkageType> TypeElement
      {
        get { return _TypeElement; }
        set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
      }

      private Code<Hl7.Fhir.Model.Linkage.LinkageType> _TypeElement;

      /// <summary>
      /// source | alternate | historical
      /// </summary>
      /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
      [IgnoreDataMember]
      public Hl7.Fhir.Model.Linkage.LinkageType? Type
      {
        get { return TypeElement != null ? TypeElement.Value : null; }
        set
        {
          if (value == null)
            TypeElement = null;
          else
            TypeElement = new Code<Hl7.Fhir.Model.Linkage.LinkageType>(value);
          OnPropertyChanged("Type");
        }
      }

      /// <summary>
      /// Resource being linked
      /// </summary>
      [FhirElement("resource", InSummary=true, Order=50)]
      [CLSCompliant(false)]
      [References("Resource")]
      [Cardinality(Min=1,Max=1)]
      [DataMember]
      public Hl7.Fhir.Model.ResourceReference Resource
      {
        get { return _Resource; }
        set { _Resource = value; OnPropertyChanged("Resource"); }
      }

      private Hl7.Fhir.Model.ResourceReference _Resource;

      public override IDeepCopyable CopyTo(IDeepCopyable other)
      {
        var dest = other as ItemComponent;

        if (dest == null)
        {
          throw new ArgumentException("Can only copy to an object of the same type", "other");
        }

        base.CopyTo(dest);
        if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.Linkage.LinkageType>)TypeElement.DeepCopy();
        if(Resource != null) dest.Resource = (Hl7.Fhir.Model.ResourceReference)Resource.DeepCopy();
        return dest;
      }

      public override IDeepCopyable DeepCopy()
      {
        return CopyTo(new ItemComponent());
      }

      ///<inheritdoc />
      public override bool Matches(IDeepComparable other)
      {
        var otherT = other as ItemComponent;
        if(otherT == null) return false;

        if(!base.Matches(otherT)) return false;
        if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
        if( !DeepComparable.Matches(Resource, otherT.Resource)) return false;

        return true;
      }

      public override bool IsExactly(IDeepComparable other)
      {
        var otherT = other as ItemComponent;
        if(otherT == null) return false;

        if(!base.IsExactly(otherT)) return false;
        if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
        if( !DeepComparable.IsExactly(Resource, otherT.Resource)) return false;

        return true;
      }

      [IgnoreDataMember]
      public override IEnumerable<Base> Children
      {
        get
        {
          foreach (var item in base.Children) yield return item;
          if (TypeElement != null) yield return TypeElement;
          if (Resource != null) yield return Resource;
        }
      }

      [IgnoreDataMember]
      public override IEnumerable<ElementValue> NamedChildren
      {
        get
        {
          foreach (var item in base.NamedChildren) yield return item;
          if (TypeElement != null) yield return new ElementValue("type", TypeElement);
          if (Resource != null) yield return new ElementValue("resource", Resource);
        }
      }

      protected override bool TryGetValue(string key, out object value)
      {
        switch (key)
        {
          case "type":
            value = TypeElement;
            return TypeElement is not null;
          case "resource":
            value = Resource;
            return Resource is not null;
          default:
            return base.TryGetValue(key, out value);
        }

      }

      protected override Base SetValue(string key, object value)
      {
        switch (key)
        {
          case "type":
            TypeElement = (Code<Hl7.Fhir.Model.Linkage.LinkageType>)value;
            return this;
          case "resource":
            Resource = (Hl7.Fhir.Model.ResourceReference)value;
            return this;
          default:
            return base.SetValue(key, value);
        }

      }

      protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
      {
        foreach (var kvp in base.GetElementPairs()) yield return kvp;
        if (TypeElement is not null) yield return new KeyValuePair<string,object>("type",TypeElement);
        if (Resource is not null) yield return new KeyValuePair<string,object>("resource",Resource);
      }

    }

    /// <summary>
    /// Whether this linkage assertion is active or not
    /// </summary>
    [FhirElement("active", InSummary=true, Order=90, FiveWs="FiveWs.status")]
    [DataMember]
    public Hl7.Fhir.Model.FhirBoolean ActiveElement
    {
      get { return _ActiveElement; }
      set { _ActiveElement = value; OnPropertyChanged("ActiveElement"); }
    }

    private Hl7.Fhir.Model.FhirBoolean _ActiveElement;

    /// <summary>
    /// Whether this linkage assertion is active or not
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public bool? Active
    {
      get { return ActiveElement != null ? ActiveElement.Value : null; }
      set
      {
        if (value == null)
          ActiveElement = null;
        else
          ActiveElement = new Hl7.Fhir.Model.FhirBoolean(value);
        OnPropertyChanged("Active");
      }
    }

    /// <summary>
    /// Who is responsible for linkages
    /// </summary>
    [FhirElement("author", InSummary=true, Order=100, FiveWs="FiveWs.author")]
    [CLSCompliant(false)]
    [References("Practitioner","PractitionerRole","Organization")]
    [DataMember]
    public Hl7.Fhir.Model.ResourceReference Author
    {
      get { return _Author; }
      set { _Author = value; OnPropertyChanged("Author"); }
    }

    private Hl7.Fhir.Model.ResourceReference _Author;

    /// <summary>
    /// Item to be linked
    /// </summary>
    [FhirElement("item", InSummary=true, Order=110)]
    [Cardinality(Min=1,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.Linkage.ItemComponent> Item
    {
      get { if(_Item==null) _Item = new List<Hl7.Fhir.Model.Linkage.ItemComponent>(); return _Item; }
      set { _Item = value; OnPropertyChanged("Item"); }
    }

    private List<Hl7.Fhir.Model.Linkage.ItemComponent> _Item;

    public override IDeepCopyable CopyTo(IDeepCopyable other)
    {
      var dest = other as Linkage;

      if (dest == null)
      {
        throw new ArgumentException("Can only copy to an object of the same type", "other");
      }

      base.CopyTo(dest);
      if(ActiveElement != null) dest.ActiveElement = (Hl7.Fhir.Model.FhirBoolean)ActiveElement.DeepCopy();
      if(Author != null) dest.Author = (Hl7.Fhir.Model.ResourceReference)Author.DeepCopy();
      if(Item.Any()) dest.Item = new List<Hl7.Fhir.Model.Linkage.ItemComponent>(Item.DeepCopy());
      return dest;
    }

    public override IDeepCopyable DeepCopy()
    {
      return CopyTo(new Linkage());
    }

    ///<inheritdoc />
    public override bool Matches(IDeepComparable other)
    {
      var otherT = other as Linkage;
      if(otherT == null) return false;

      if(!base.Matches(otherT)) return false;
      if( !DeepComparable.Matches(ActiveElement, otherT.ActiveElement)) return false;
      if( !DeepComparable.Matches(Author, otherT.Author)) return false;
      if( !DeepComparable.Matches(Item, otherT.Item)) return false;

      return true;
    }

    public override bool IsExactly(IDeepComparable other)
    {
      var otherT = other as Linkage;
      if(otherT == null) return false;

      if(!base.IsExactly(otherT)) return false;
      if( !DeepComparable.IsExactly(ActiveElement, otherT.ActiveElement)) return false;
      if( !DeepComparable.IsExactly(Author, otherT.Author)) return false;
      if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;

      return true;
    }

    [IgnoreDataMember]
    public override IEnumerable<Base> Children
    {
      get
      {
        foreach (var item in base.Children) yield return item;
        if (ActiveElement != null) yield return ActiveElement;
        if (Author != null) yield return Author;
        foreach (var elem in Item) { if (elem != null) yield return elem; }
      }
    }

    [IgnoreDataMember]
    public override IEnumerable<ElementValue> NamedChildren
    {
      get
      {
        foreach (var item in base.NamedChildren) yield return item;
        if (ActiveElement != null) yield return new ElementValue("active", ActiveElement);
        if (Author != null) yield return new ElementValue("author", Author);
        foreach (var elem in Item) { if (elem != null) yield return new ElementValue("item", elem); }
      }
    }

    protected override bool TryGetValue(string key, out object value)
    {
      switch (key)
      {
        case "active":
          value = ActiveElement;
          return ActiveElement is not null;
        case "author":
          value = Author;
          return Author is not null;
        case "item":
          value = Item;
          return Item?.Any() == true;
        default:
          return base.TryGetValue(key, out value);
      }

    }

    protected override Base SetValue(string key, object value)
    {
      switch (key)
      {
        case "active":
          ActiveElement = (Hl7.Fhir.Model.FhirBoolean)value;
          return this;
        case "author":
          Author = (Hl7.Fhir.Model.ResourceReference)value;
          return this;
        case "item":
          Item = (List<Hl7.Fhir.Model.Linkage.ItemComponent>)value;
          return this;
        default:
          return base.SetValue(key, value);
      }

    }

    protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
    {
      foreach (var kvp in base.GetElementPairs()) yield return kvp;
      if (ActiveElement is not null) yield return new KeyValuePair<string,object>("active",ActiveElement);
      if (Author is not null) yield return new KeyValuePair<string,object>("author",Author);
      if (Item?.Any() == true) yield return new KeyValuePair<string,object>("item",Item);
    }

  }

}

// end of file
