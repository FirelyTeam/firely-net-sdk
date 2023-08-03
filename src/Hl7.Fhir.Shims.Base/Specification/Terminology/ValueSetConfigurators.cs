#nullable enable

/* 
 * Copyright (c) 2023, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Tests
{
    /// <summary>
    /// Extension methods to help constructing a <see cref="ValueSet"/> in code.
    /// </summary>
    public static class ValueSetConfigurators
    {
        /// <summary>
        /// Adds a new <see cref="ValueSet.ConceptSetComponent"/> to the includes of a <see cref="ValueSet.Compose"/> and returns
        /// it to enable fluent construction of valuesets.
        /// </summary>
        public static ValueSet Includes(this ValueSet vs, Action<ValueSet.ConceptSetComponent> a)
        {
            var csc = new ValueSet.ConceptSetComponent();
            a(csc);
            vs.Compose ??= new();
            vs.Compose.Include.Add(csc);
            return vs;
        }

        /// <summary>
        /// Adds a new <see cref="ValueSet.ConceptSetComponent"/> to the excludes of a <see cref="ValueSet.Compose"/> and returns
        /// it to enable fluent construction of valuesets.
        /// </summary>
        public static ValueSet Excludes(this ValueSet vs, Action<ValueSet.ConceptSetComponent> a)
        {
            var csc = new ValueSet.ConceptSetComponent();
            a(csc);
            vs.Compose ??= new();
            vs.Compose.Exclude.Add(csc);
            return vs;
        }

        /// <summary>
        /// Sets the system of a <see cref="ValueSet.ConceptSetComponent"/> to enable fluent construction of an include or exclude.
        /// </summary>
        public static ValueSet.ConceptSetComponent System(this ValueSet.ConceptSetComponent component, string system)
        {
            component.System = system;
            return component;
        }

        /// <summary>
        /// Adds to the concepts of a <see cref="ValueSet.ConceptSetComponent"/> to enable fluent construction of an include or exclude.
        /// </summary>
        public static ValueSet.ConceptSetComponent Concepts(this ValueSet.ConceptSetComponent component, IEnumerable<ValueSet.ConceptReferenceComponent> refcomponents)
        {
            component.Concept.AddRange(refcomponents);
            return component;
        }

        /// <inheritdoc cref="Concepts(ValueSet.ConceptSetComponent, IEnumerable{ValueSet.ConceptReferenceComponent})" />
        public static ValueSet.ConceptSetComponent Concepts(this ValueSet.ConceptSetComponent component, params ValueSet.ConceptReferenceComponent[] refcomponents) => component.Concepts(refcomponents.AsEnumerable());

        /// <inheritdoc cref="Concepts(ValueSet.ConceptSetComponent, IEnumerable{ValueSet.ConceptReferenceComponent})" />
        public static ValueSet.ConceptSetComponent Concepts(this ValueSet.ConceptSetComponent component, params string[] code) =>
            component.Concepts(code.Select(c => new ValueSet.ConceptReferenceComponent() { Code = c }));

        /// <inheritdoc cref="Concepts(ValueSet.ConceptSetComponent, IEnumerable{ValueSet.ConceptReferenceComponent})" />
        public static ValueSet.ConceptSetComponent Concepts(this ValueSet.ConceptSetComponent component, string code) => component.Concepts(new ValueSet.ConceptReferenceComponent() { Code = code });

        /// <summary>
        /// Adds to the valuesets of a <see cref="ValueSet.ConceptSetComponent"/> to enable fluent construction of an include or exclude.
        /// </summary>
        public static ValueSet.ConceptSetComponent ValueSets(this ValueSet.ConceptSetComponent component, IEnumerable<string> canonicals)
        {
#if STU3
            component.ValueSetElement.AddRange(canonicals.Select(c => new FhirUri(c)));
#else
            component.ValueSetElement.AddRange(canonicals.Select(c => new Canonical(c)));
#endif
            return component;
        }

        /// <summary>
        /// Adds to the valuesets of a <see cref="ValueSet.ConceptSetComponent"/> to enable fluent construction of an include or exclude.
        /// </summary>
        public static ValueSet.ConceptSetComponent ValueSets(this ValueSet.ConceptSetComponent component, params string[] canonicals) => component.ValueSets(canonicals.AsEnumerable());
    }
}

#nullable restore