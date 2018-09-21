/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.FhirPath;
using System;

namespace Hl7.Fhir.FhirPath
{
    public class FhirEvaluationContext : EvaluationContext
    {
        [Obsolete("Please use CreateDefault() instead of this member, which may cause raise conditions.")]
        new public static readonly FhirEvaluationContext Default = new FhirEvaluationContext();

        public static FhirEvaluationContext CreateDefault() => new FhirEvaluationContext();

        public FhirEvaluationContext() : base()
        {
        }

        public FhirEvaluationContext(Resource context) : base(context?.ToElementNavigator())
        {
        }

        [Obsolete("Use FhirEvaluationContext(ITypedElement context) instead")]
        public FhirEvaluationContext(IElementNavigator context) : base(context)
        {
        }

        public FhirEvaluationContext(ITypedElement context) : base(context.ToElementNavigator())
        {
        }

        private Func<string, IElementNavigator> _resolver;

        [Obsolete("Use property ElementResolver instead")]
        public Func<string, IElementNavigator> Resolver
        {
            get { return _resolver; }
            set
            {
                _resolver = value;
                if (value == null)
                    _elementResolver = null;
                else
                    _elementResolver = (s) => value(s).ToTypedElement();
            }
        }

        private Func<string, ITypedElement> _elementResolver;

        public Func<string, ITypedElement> ElementResolver
        {
            get { return _elementResolver; }
            set
            {
                _elementResolver = value;
                if (value == null)
                    _resolver = null;
                else
                    _resolver = (s) => value(s).ToElementNavigator();
            }
        }
    }
}
