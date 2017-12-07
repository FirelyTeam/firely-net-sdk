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
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Model
{
#if NET45
    [Serializable]
#endif
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
            if (other == null) throw Error.ArgumentNull(nameof(other));
            if(this.GetType() != other.GetType())
                throw Error.Argument(nameof(other), "Can only copy to an object of the same type");

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
            if (other == null) throw Error.ArgumentNull(nameof(other));

            if (this.GetType() != other.GetType()) return false;

            if (!base.IsExactly(other)) return false;

            var otherValue = ((Primitive)other).ObjectValue;

            if (ObjectValue is byte[] bytes && otherValue is byte[] bytesOther)
                return Enumerable.SequenceEqual(bytes, bytesOther);
            else
                return Object.Equals(ObjectValue, ((Primitive)other).ObjectValue);
        }
    }

#if NET45
    [Serializable]
#endif
    public abstract class Primitive<T> : Primitive
    {
        // [WMR 20160615] Cannot provide common generic Value property, as subclasses differ in their implementation
        // e.g. Code<T> exposes T? Value where T : struct
        // T Value { get; set; }
        // => Instead, define and implement a generic interface IValue<T>
    }

}
