using System;
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

    public abstract class Primitive<T> : Primitive
    {
        // [WMR 20160615] Cannot provide common generic Value property, as subclasses differ in their implementation
        // e.g. Code<T> exposes T? Value where T : struct
        // T Value { get; set; }
        // => Instead, define and implement a generic interface IValue<T>
    }

}
