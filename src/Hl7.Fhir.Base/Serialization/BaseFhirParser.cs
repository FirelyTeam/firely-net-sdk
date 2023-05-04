/*
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using System;

namespace Hl7.Fhir.Serialization
{
    public class BaseFhirParser
    {
        public readonly ParserSettings Settings;
        private readonly ModelInspector _inspector;

        public BaseFhirParser(ModelInspector inspector, ParserSettings settings = null)
        {
            Settings = settings?.Clone() ?? new ParserSettings();
            _inspector = inspector;
        }

        internal static PocoBuilderSettings BuildPocoBuilderSettings(ParserSettings ps) =>
            new()
            {
                AllowUnrecognizedEnums = ps.AllowUnrecognizedEnums,
                IgnoreUnknownMembers = ps.AcceptUnknownMembers,
                ExceptionHandler = ps.ExceptionHandler,
#pragma warning disable CS0618 // Type or member is obsolete
                TruncateDateTimeToDate = ps.TruncateDateTimeToDate
#pragma warning restore CS0618 // Type or member is obsolete
            };

        internal static FhirXmlParsingSettings BuildXmlParsingSettings(ParserSettings settings) =>
            new()
            {
                DisallowSchemaLocation = settings.DisallowXsiAttributesOnRoot,
                PermissiveParsing = settings.PermissiveParsing,
            };

        internal static FhirJsonParsingSettings BuildJsonParserSettings(ParserSettings settings) =>
            new()
            {
                AllowJsonComments = false,
                PermissiveParsing = settings.PermissiveParsing
            };


        public Base Parse(ITypedElement element) => element.ToPoco(_inspector, BuildPocoBuilderSettings(Settings));

        public T Parse<T>(ITypedElement element) where T : Base => element.ToPoco<T>(_inspector, BuildPocoBuilderSettings(Settings));

        public Base Parse(ISourceNode node, Type type = null) => node.ToPoco(_inspector, type, BuildPocoBuilderSettings(Settings));

        public T Parse<T>(ISourceNode node) where T : Base => node.ToPoco<T>(_inspector, BuildPocoBuilderSettings(Settings));
    }

}
