﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using Hl7.Fhir.Validation;

namespace Hl7.Fhir.Model
{
    [DataContract]
    public abstract class Primitive : Element
    {
        [NotMapped]
        public object ObjectValue { get; set; }

        [NotMapped]
        public override string TypeName
        {
            get { return "Primitive"; }
        }

        public override string ToString()
        {
            // The primitive can exist without a value (when there is an extension present)
            // so we need to be able to handle when there is no extension present
            if (this.ObjectValue == null)
                return null;
            return PrimitiveTypeConverter.ConvertTo<string>(this.ObjectValue);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            if (other == null) throw Error.ArgumentNull("other");
            if(this.GetType() != other.GetType())
                throw Error.Argument("other", "Can only copy to an object of the same type");

            base.CopyTo(other);
            if (ObjectValue != null) ((Primitive)other).ObjectValue = ObjectValue;

            return other;
        }

        public override IDeepCopyable DeepCopy()
        {
            return CopyTo((IDeepCopyable)Activator.CreateInstance(this.GetType()));
        }

        public override bool Matches(IDeepComparable other)
        {
            return IsExactly(other);
        }

        public override bool IsExactly(IDeepComparable other)
        {
            if (other == null) throw Error.ArgumentNull("other");

            if (this.GetType() != other.GetType()) return false;

            if (!base.Matches(other)) return false;

            return Object.Equals(ObjectValue, ((Primitive)other).ObjectValue);
        }
    }

    [KnownType(typeof(Id))]
    [KnownType(typeof(Integer))]
    [KnownType(typeof(FhirString))]
    [KnownType(typeof(FhirUri))]
    [KnownType(typeof(FhirBoolean))]
    [KnownType(typeof(Code))]
    [KnownType(typeof(Date))]
    [KnownType(typeof(FhirDateTime))]
    [KnownType(typeof(FhirDecimal))]
    [KnownType(typeof(Instant))]
    [KnownType(typeof(Markdown))]
    [KnownType(typeof(Oid))]
    [KnownType(typeof(PositiveInt))]
    [KnownType(typeof(Time))]
    [KnownType(typeof(UnsignedInt))]
    [KnownType(typeof(Uuid))]
    [DataContract]
    public abstract class Primitive<T> : Primitive
    {
        // [WMR 20160615] Cannot provide common generic Value property, as subclasses differ in their implementation
        // e.g. Code<T> exposes T? Value where T : struct
        // T Value { get; set; }
        // => Instead, define and implement a generic interface IValue<T>
    }

}
