/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;

namespace Hl7.FhirPath.Expressions;

internal static class ClosureExtensions
{
    extension(Closure ctx)
    {
        public IEnumerable<PocoNode> GetThis() => ctx.ResolveValue("builtin.this") ?? [];

        public void SetThis(IEnumerable<PocoNode> value)
        {
            ctx.SetValue("builtin.this", value);
        }

        public IEnumerable<PocoNode> GetTotal() => ctx.ResolveValue("builtin.total") ?? [];

        public void SetTotal(IEnumerable<PocoNode> value)
        {
            ctx.SetValue("builtin.total", value);
        }

        public IEnumerable<PocoNode> GetThat() => ctx.ResolveValue("builtin.that") ?? [];

        public void SetThat(IEnumerable<PocoNode> value)
        {
            ctx.SetValue("builtin.that", value);
        }

        /// <summary>
        /// The original node that was passed to the evaluation engine before starting evaluation.
        /// </summary>
        public void SetOriginalContext(IEnumerable<PocoNode> value)
        {
            ctx.SetValue("context", value);
        }

        /// <summary>
        /// The original resource current context is part of. When evaluating a datatype, this would be the
        /// resource the element is part of. Do not go past a root resource into a bundle, if it is contained
        /// in a bundle.
        /// </summary>
        public void SetResource(IEnumerable<PocoNode> value)
        {
            ctx.SetValue("resource", value);
        }

        /// <summary>
        /// When a DomainResource contains another resource, and that contained resource is the focus (%resource)
        /// then %rootResource refers to the container resource.
        /// </summary>
        public void SetRootResource(IEnumerable<PocoNode> value)
        {
            ctx.SetValue("rootResource", value);
        }

        public IEnumerable<PocoNode> GetOriginalContext() => ctx.ResolveValue("context") ?? [];

        public IEnumerable<PocoNode> GetResource() => ctx.ResolveValue("resource") ?? [];

        public IEnumerable<PocoNode> GetRootResource() => ctx.ResolveValue("rootResource") ?? [];

        public Closure Nest(IEnumerable<PocoNode> input)
        {
            var nested = ctx.Nest();
            nested.SetThat(input);

            return nested;
        }

        public void SetIndex(IEnumerable<PocoNode> value)
        {
            ctx.SetValue("builtin.index", value);
        }

        public IEnumerable<PocoNode> GetIndex() => ctx.ResolveValue("builtin.index") ?? [];
    }
}