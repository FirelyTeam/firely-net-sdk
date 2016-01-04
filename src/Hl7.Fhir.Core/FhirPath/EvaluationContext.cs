/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.FhirPath
{
    // TODO: Need to keep track of related contexts
    // $context - the original context (see below for usage)
    // $resource - the original container resource(e.g.skip contained resources, but do not go past a root resource into a bundle, if it is contained in a bundle)
    // $parent - the element that contains $context
    // $focus - a reference to the current focus.This is useful where you need to refer to the focus in a function parameter (e.g. $focus in criteria)

    // [WMR] Store variables as Memo<T>, i.e. evaluate expression and save result on first call
    // Polymorphic collection - each variable has it's own value type T
    // Important: each Evaluator instance should receive a new EvaluationContext instance - for variable scoping
    // => Copy ctor

    public class EvaluationContext
    {
        //AsInteger etc. implementeren op IFhirValue
        //    Add misschien ook?


        private IEnumerable<IFhirPathValue> _focus;

        public EvaluationContext(params object[] values) : this(null, values)
        {
        }

        public EvaluationContext(EvaluationContext parent, params object[] values)
        {
            if (values == null) throw Error.ArgumentNull("values");

            _focus = values.Select(value => value is IFhirPathValue ? (IFhirPathValue)value : new ConstantFhirPathValue(value));
        }

        public EvaluationContext(EvaluationContext parent, IEnumerable<IFhirPathValue> values)
        {
            if (values == null) throw Error.ArgumentNull("values");

            _focus = values;
        }

        public EvaluationContext(IEnumerable<IFhirPathValue> values) : this(null,values)
        {
        }

        public EvaluationContext Empty()
        {
            return new EvaluationContext(this,!_focus.Any());
        }
        public EvaluationContext Not()
        {
            return new EvaluationContext(this,!_focus.AsBoolean());
        }

        public EvaluationContext ItemAt(int index)
        {
            return new EvaluationContext(this,_focus.Skip(index).Take(1));
        }

        public EvaluationContext First()
        {
            return new EvaluationContext(this, _focus.Take(1));
        }

        public EvaluationContext this[string name]
        {
            get { return Navigate(name); }
        }

        public EvaluationContext Children()
        {
            return new EvaluationContext(_focus.JustFhirPathElements().SelectMany(node => node.Children()));
        }
        public EvaluationContext Navigate(string name)
        {
            return new EvaluationContext(
                _focus.JustFhirPathElements().SelectMany(node => node.Children().Where(child => child.IsMatch(name))));
        }

        public EvaluationContext IsEqualTo(EvaluationContext other)
        {
            return new EvaluationContext(_focus.IsEqualTo(other._focus));
        }

        public EvaluationContext IsEqualTo(object other)
        {
            return IsEqualTo(new EvaluationContext(other));
        }

        public EvaluationContext Where(Func<EvaluationContext,EvaluationContext> predicate)
        {
            return new EvaluationContext(_focus.Where(item => predicate(new EvaluationContext(this, item)).ToBoolean()));
        }

        public EvaluationContext Count()
        {
            return new EvaluationContext(this, _focus.Count());
        }
        public bool ToBoolean()
        {
            return _focus.AsBoolean();
        }

        public IFhirPathValue Single()
        {
            return _focus.Single();
        }
        public object Value()
        {
            return _focus.Single().Value;
        }

        public IEnumerable<object> Values()
        {
            return
                from node in _focus
                where node.Value != null
                select node.Value;
        }

        public IEnumerable<IFhirPathValue> Result()
        {
            return _focus;
        }

        public static implicit operator bool(EvaluationContext ctx)
        {
            return ctx.ToBoolean();
        }

        public static implicit operator int(EvaluationContext ctx)
        {
            return (int)ctx.Value();
        }

        public static implicit operator string(EvaluationContext ctx)
        {
            return (string)ctx.Value();
        }

        public static implicit operator decimal(EvaluationContext ctx)
        {
            return (decimal)ctx.Value();
        }

        public static implicit operator PartialDateTime(EvaluationContext ctx)
        {
            return (PartialDateTime)ctx.Value();
        }
    }
}
