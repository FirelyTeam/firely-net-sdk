using System;
using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.Support;
using HL7.Fhir.FluentPath.FluentPath;
using HL7.Fhir.FluentPath.FluentPath.Expressions;
using Sprache;
using System.Text;
using System.Threading.Tasks;
using HL7.Fhir.FluentPath;

namespace Hl7.Fhir.FluentPath.Binding
{
    public class ParamBinding
    {
        public ParamBinding(string name, bool optional, Type nativeType)
        {
            Name = name;
            IsOptional = optional;
            NativeType = nativeType;
        }

        public string Name { get; private set; }
        public bool IsOptional { get; private set; }

        public Type NativeType { get; private set; }

        public bool StaticMatches(Expression parameterExpression)
        {
            return true;
        }

        public T Bind<T>(object source)
        {
            try
            {
                return CastToSingleValue<T>(source);
            }
            catch (InvalidCastException ice)
            {
                throw Error.Argument(Name, ice.Message);
            }
        }

        public static T CastToSingleValue<T>(object source)
        {
            if (source is T)
                return (T)source;

            if (source is IEnumerable<IFluentPathValue>)
            {
                var ifp = (IEnumerable<IFluentPathValue>)source;
                if (ifp.Any())
                {
                    if (ifp.Skip(1).Any())
                        throw new InvalidCastException("expecting only a single value");
                    source = ifp.Single();
                }
                else
                    return default(T);
            }

            if (source is T)
                return (T)source;

            if (source is IFluentPathValue)
            {
                if (source == null)
                    return default(T);

                var val = (IFluentPathValue)source;
                source = val.Value;
            }

            if (source is T)
                return (T)source;

            throw new ArgumentException("cannot cast argument of type '{0}' to a '{1}'".FormatWith(source.GetType().Name, typeof(T).Name));
        }
    }
}