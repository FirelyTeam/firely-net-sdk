/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using System;
using System.Reflection;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Serialization
{
    public class BaseFhirParser
    {
        public readonly ParserSettings Settings;

        public BaseFhirParser(ParserSettings settings = null)
        {
            Settings = settings?.Clone() ?? new ParserSettings();
        }

        private static readonly Lazy<ModelInspector> _inspector = createDefaultModelInspector();

        private static Lazy<ModelInspector> createDefaultModelInspector()
        {
            return new Lazy<ModelInspector>(() =>
            {
                var result = new ModelInspector(ModelInspector.R4_VERSION);

                result.Import(typeof(Resource).GetTypeInfo().Assembly);
                result.Import(typeof(Patient).GetTypeInfo().Assembly);
                return result;
            });
        }

        public static ModelInspector Inspector
        {
            get
            {
                return _inspector.Value;
            }
        }

        private PocoBuilderSettings buildPocoBuilderSettings(ParserSettings ps) =>
            new PocoBuilderSettings
            {
                AllowUnrecognizedEnums = ps.AllowUnrecognizedEnums,
                IgnoreUnknownMembers = ps.AcceptUnknownMembers
            };

        public Base Parse(ITypedElement element) => element.ToPoco(buildPocoBuilderSettings(Settings));

        public T Parse<T>(ITypedElement element) where T : Base => element.ToPoco<T>(buildPocoBuilderSettings(Settings));

        public Base Parse(ISourceNode node, Type type=null) => node.ToPoco(type, buildPocoBuilderSettings(Settings));

        public T Parse<T>(ISourceNode node) where T : Base => node.ToPoco<T>(buildPocoBuilderSettings(Settings));
    }

}
