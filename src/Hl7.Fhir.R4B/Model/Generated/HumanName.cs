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
  /// Name of a human - parts and usage
  /// </summary>
  /// <remarks>
  /// A human's name with the ability to identify parts and usage.
  /// Names may be changed, or repudiated, or people may have different names in different contexts. Names may be divided into parts of different type that have variable significance depending on context, though the division into parts does not always matter. With personal names, the different parts might or might not be imbued with some implicit meaning; various cultures associate different importance with the name parts and the degree to which systems must care about name parts around the world varies widely.
  /// </remarks>
  [Serializable]
  [DataContract]
  [FhirType("HumanName","http://hl7.org/fhir/StructureDefinition/HumanName")]
  public partial class HumanName : Hl7.Fhir.Model.DataType
  {
    /// <summary>
    /// FHIR Type Name
    /// </summary>
    public override string TypeName { get { return "HumanName"; } }

    /// <summary>
    /// The use of a human name.
    /// (url: http://hl7.org/fhir/ValueSet/name-use)
    /// (system: http://hl7.org/fhir/name-use)
    /// </summary>
    [FhirEnumeration("NameUse", "http://hl7.org/fhir/ValueSet/name-use", "http://hl7.org/fhir/name-use")]
    public enum NameUse
    {
      /// <summary>
      /// Known as/conventional/the one you normally use.
      /// (system: http://hl7.org/fhir/name-use)
      /// </summary>
      [EnumLiteral("usual"), Description("Usual")]
      Usual,
      /// <summary>
      /// The formal name as registered in an official (government) registry, but which name might not be commonly used. May be called \"legal name\".
      /// (system: http://hl7.org/fhir/name-use)
      /// </summary>
      [EnumLiteral("official"), Description("Official")]
      Official,
      /// <summary>
      /// A temporary name. Name.period can provide more detailed information. This may also be used for temporary names assigned at birth or in emergency situations.
      /// (system: http://hl7.org/fhir/name-use)
      /// </summary>
      [EnumLiteral("temp"), Description("Temp")]
      Temp,
      /// <summary>
      /// A name that is used to address the person in an informal manner, but is not part of their formal or usual name.
      /// (system: http://hl7.org/fhir/name-use)
      /// </summary>
      [EnumLiteral("nickname"), Description("Nickname")]
      Nickname,
      /// <summary>
      /// Anonymous assigned name, alias, or pseudonym (used to protect a person's identity for privacy reasons).
      /// (system: http://hl7.org/fhir/name-use)
      /// </summary>
      [EnumLiteral("anonymous"), Description("Anonymous")]
      Anonymous,
      /// <summary>
      /// This name is no longer in use (or was never correct, but retained for records).
      /// (system: http://hl7.org/fhir/name-use)
      /// </summary>
      [EnumLiteral("old"), Description("Old")]
      Old,
      /// <summary>
      /// A name used prior to changing name because of marriage. This name use is for use by applications that collect and store names that were used prior to a marriage. Marriage naming customs vary greatly around the world, and are constantly changing. This term is not gender specific. The use of this term does not imply any particular history for a person's name.
      /// (system: http://hl7.org/fhir/name-use)
      /// </summary>
      [EnumLiteral("maiden"), Description("Name changed for Marriage")]
      Maiden,
    }

    /// <summary>
    /// usual | official | temp | nickname | anonymous | old | maiden
    /// </summary>
    [FhirElement("use", InSummary=true, IsModifier=true, Order=30)]
    [DeclaredType(Type = typeof(Code))]
    [Binding("NameUse")]
    [DataMember]
    public Code<Hl7.Fhir.Model.HumanName.NameUse> UseElement
    {
      get { return _UseElement; }
      set { _UseElement = value; OnPropertyChanged("UseElement"); }
    }

    private Code<Hl7.Fhir.Model.HumanName.NameUse> _UseElement;

    /// <summary>
    /// usual | official | temp | nickname | anonymous | old | maiden
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public Hl7.Fhir.Model.HumanName.NameUse? Use
    {
      get { return UseElement != null ? UseElement.Value : null; }
      set
      {
        if (value == null)
          UseElement = null;
        else
          UseElement = new Code<Hl7.Fhir.Model.HumanName.NameUse>(value);
        OnPropertyChanged("Use");
      }
    }

    /// <summary>
    /// Text representation of the full name
    /// </summary>
    [FhirElement("text", InSummary=true, Order=40)]
    [DataMember]
    public Hl7.Fhir.Model.FhirString TextElement
    {
      get { return _TextElement; }
      set { _TextElement = value; OnPropertyChanged("TextElement"); }
    }

    private Hl7.Fhir.Model.FhirString _TextElement;

    /// <summary>
    /// Text representation of the full name
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public string Text
    {
      get { return TextElement != null ? TextElement.Value : null; }
      set
      {
        if (value == null)
          TextElement = null;
        else
          TextElement = new Hl7.Fhir.Model.FhirString(value);
        OnPropertyChanged("Text");
      }
    }

    /// <summary>
    /// Family name (often called 'Surname')
    /// </summary>
    [FhirElement("family", InSummary=true, Order=50)]
    [DataMember]
    public Hl7.Fhir.Model.FhirString FamilyElement
    {
      get { return _FamilyElement; }
      set { _FamilyElement = value; OnPropertyChanged("FamilyElement"); }
    }

    private Hl7.Fhir.Model.FhirString _FamilyElement;

    /// <summary>
    /// Family name (often called 'Surname')
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public string Family
    {
      get { return FamilyElement != null ? FamilyElement.Value : null; }
      set
      {
        if (value == null)
          FamilyElement = null;
        else
          FamilyElement = new Hl7.Fhir.Model.FhirString(value);
        OnPropertyChanged("Family");
      }
    }

    /// <summary>
    /// Given names (not always 'first'). Includes middle names
    /// </summary>
    [FhirElement("given", InSummary=true, Order=60)]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.FhirString> GivenElement
    {
      get { if(_GivenElement==null) _GivenElement = new List<Hl7.Fhir.Model.FhirString>(); return _GivenElement; }
      set { _GivenElement = value; OnPropertyChanged("GivenElement"); }
    }

    private List<Hl7.Fhir.Model.FhirString> _GivenElement;

    /// <summary>
    /// Given names (not always 'first'). Includes middle names
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public IEnumerable<string> Given
    {
      get { return GivenElement != null ? GivenElement.Select(elem => elem.Value) : null; }
      set
      {
        if (value == null)
          GivenElement = null;
        else
          GivenElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
        OnPropertyChanged("Given");
      }
    }

    /// <summary>
    /// Parts that come before the name
    /// </summary>
    [FhirElement("prefix", InSummary=true, Order=70)]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.FhirString> PrefixElement
    {
      get { if(_PrefixElement==null) _PrefixElement = new List<Hl7.Fhir.Model.FhirString>(); return _PrefixElement; }
      set { _PrefixElement = value; OnPropertyChanged("PrefixElement"); }
    }

    private List<Hl7.Fhir.Model.FhirString> _PrefixElement;

    /// <summary>
    /// Parts that come before the name
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public IEnumerable<string> Prefix
    {
      get { return PrefixElement != null ? PrefixElement.Select(elem => elem.Value) : null; }
      set
      {
        if (value == null)
          PrefixElement = null;
        else
          PrefixElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
        OnPropertyChanged("Prefix");
      }
    }

    /// <summary>
    /// Parts that come after the name
    /// </summary>
    [FhirElement("suffix", InSummary=true, Order=80)]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.FhirString> SuffixElement
    {
      get { if(_SuffixElement==null) _SuffixElement = new List<Hl7.Fhir.Model.FhirString>(); return _SuffixElement; }
      set { _SuffixElement = value; OnPropertyChanged("SuffixElement"); }
    }

    private List<Hl7.Fhir.Model.FhirString> _SuffixElement;

    /// <summary>
    /// Parts that come after the name
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public IEnumerable<string> Suffix
    {
      get { return SuffixElement != null ? SuffixElement.Select(elem => elem.Value) : null; }
      set
      {
        if (value == null)
          SuffixElement = null;
        else
          SuffixElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
        OnPropertyChanged("Suffix");
      }
    }

    /// <summary>
    /// Time period when name was/is in use
    /// </summary>
    [FhirElement("period", InSummary=true, Order=90)]
    [DataMember]
    public Hl7.Fhir.Model.Period Period
    {
      get { return _Period; }
      set { _Period = value; OnPropertyChanged("Period"); }
    }

    private Hl7.Fhir.Model.Period _Period;

    public override IDeepCopyable CopyTo(IDeepCopyable other)
    {
      var dest = other as HumanName;

      if (dest == null)
      {
        throw new ArgumentException("Can only copy to an object of the same type", "other");
      }

      base.CopyTo(dest);
      if(UseElement != null) dest.UseElement = (Code<Hl7.Fhir.Model.HumanName.NameUse>)UseElement.DeepCopy();
      if(TextElement != null) dest.TextElement = (Hl7.Fhir.Model.FhirString)TextElement.DeepCopy();
      if(FamilyElement != null) dest.FamilyElement = (Hl7.Fhir.Model.FhirString)FamilyElement.DeepCopy();
      if(GivenElement.Any()) dest.GivenElement = new List<Hl7.Fhir.Model.FhirString>(GivenElement.DeepCopy());
      if(PrefixElement.Any()) dest.PrefixElement = new List<Hl7.Fhir.Model.FhirString>(PrefixElement.DeepCopy());
      if(SuffixElement.Any()) dest.SuffixElement = new List<Hl7.Fhir.Model.FhirString>(SuffixElement.DeepCopy());
      if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
      return dest;
    }

    public override IDeepCopyable DeepCopy()
    {
      return CopyTo(new HumanName());
    }

    ///<inheritdoc />
    public override bool Matches(IDeepComparable other)
    {
      var otherT = other as HumanName;
      if(otherT == null) return false;

      if(!base.Matches(otherT)) return false;
      if( !DeepComparable.Matches(UseElement, otherT.UseElement)) return false;
      if( !DeepComparable.Matches(TextElement, otherT.TextElement)) return false;
      if( !DeepComparable.Matches(FamilyElement, otherT.FamilyElement)) return false;
      if( !DeepComparable.Matches(GivenElement, otherT.GivenElement)) return false;
      if( !DeepComparable.Matches(PrefixElement, otherT.PrefixElement)) return false;
      if( !DeepComparable.Matches(SuffixElement, otherT.SuffixElement)) return false;
      if( !DeepComparable.Matches(Period, otherT.Period)) return false;

      return true;
    }

    public override bool IsExactly(IDeepComparable other)
    {
      var otherT = other as HumanName;
      if(otherT == null) return false;

      if(!base.IsExactly(otherT)) return false;
      if( !DeepComparable.IsExactly(UseElement, otherT.UseElement)) return false;
      if( !DeepComparable.IsExactly(TextElement, otherT.TextElement)) return false;
      if( !DeepComparable.IsExactly(FamilyElement, otherT.FamilyElement)) return false;
      if( !DeepComparable.IsExactly(GivenElement, otherT.GivenElement)) return false;
      if( !DeepComparable.IsExactly(PrefixElement, otherT.PrefixElement)) return false;
      if( !DeepComparable.IsExactly(SuffixElement, otherT.SuffixElement)) return false;
      if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;

      return true;
    }

    [IgnoreDataMember]
    public override IEnumerable<Base> Children
    {
      get
      {
        foreach (var item in base.Children) yield return item;
        if (UseElement != null) yield return UseElement;
        if (TextElement != null) yield return TextElement;
        if (FamilyElement != null) yield return FamilyElement;
        foreach (var elem in GivenElement) { if (elem != null) yield return elem; }
        foreach (var elem in PrefixElement) { if (elem != null) yield return elem; }
        foreach (var elem in SuffixElement) { if (elem != null) yield return elem; }
        if (Period != null) yield return Period;
      }
    }

    [IgnoreDataMember]
    public override IEnumerable<ElementValue> NamedChildren
    {
      get
      {
        foreach (var item in base.NamedChildren) yield return item;
        if (UseElement != null) yield return new ElementValue("use", UseElement);
        if (TextElement != null) yield return new ElementValue("text", TextElement);
        if (FamilyElement != null) yield return new ElementValue("family", FamilyElement);
        foreach (var elem in GivenElement) { if (elem != null) yield return new ElementValue("given", elem); }
        foreach (var elem in PrefixElement) { if (elem != null) yield return new ElementValue("prefix", elem); }
        foreach (var elem in SuffixElement) { if (elem != null) yield return new ElementValue("suffix", elem); }
        if (Period != null) yield return new ElementValue("period", Period);
      }
    }

    protected override bool TryGetValue(string key, out object value)
    {
      switch (key)
      {
        case "use":
          value = UseElement;
          return UseElement is not null;
        case "text":
          value = TextElement;
          return TextElement is not null;
        case "family":
          value = FamilyElement;
          return FamilyElement is not null;
        case "given":
          value = GivenElement;
          return GivenElement?.Any() == true;
        case "prefix":
          value = PrefixElement;
          return PrefixElement?.Any() == true;
        case "suffix":
          value = SuffixElement;
          return SuffixElement?.Any() == true;
        case "period":
          value = Period;
          return Period is not null;
        default:
          return base.TryGetValue(key, out value);
      }

    }

    protected override Base SetValue(string key, object value)
    {
      switch (key)
      {
        case "use":
          UseElement = (Code<Hl7.Fhir.Model.HumanName.NameUse>)value;
          return this;
        case "text":
          TextElement = (Hl7.Fhir.Model.FhirString)value;
          return this;
        case "family":
          FamilyElement = (Hl7.Fhir.Model.FhirString)value;
          return this;
        case "given":
          GivenElement = (List<Hl7.Fhir.Model.FhirString>)value;
          return this;
        case "prefix":
          PrefixElement = (List<Hl7.Fhir.Model.FhirString>)value;
          return this;
        case "suffix":
          SuffixElement = (List<Hl7.Fhir.Model.FhirString>)value;
          return this;
        case "period":
          Period = (Hl7.Fhir.Model.Period)value;
          return this;
        default:
          return base.SetValue(key, value);
      }

    }

    protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
    {
      foreach (var kvp in base.GetElementPairs()) yield return kvp;
      if (UseElement is not null) yield return new KeyValuePair<string,object>("use",UseElement);
      if (TextElement is not null) yield return new KeyValuePair<string,object>("text",TextElement);
      if (FamilyElement is not null) yield return new KeyValuePair<string,object>("family",FamilyElement);
      if (GivenElement?.Any() == true) yield return new KeyValuePair<string,object>("given",GivenElement);
      if (PrefixElement?.Any() == true) yield return new KeyValuePair<string,object>("prefix",PrefixElement);
      if (SuffixElement?.Any() == true) yield return new KeyValuePair<string,object>("suffix",SuffixElement);
      if (Period is not null) yield return new KeyValuePair<string,object>("period",Period);
    }

  }

}

// end of file
