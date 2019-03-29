/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */


using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using Hl7.Fhir.Specification;

namespace Hl7.Fhir.Serialization
{
    internal class PocoBuilder : IExceptionSource
    {
        public PocoBuilder(PocoBuilderSettings settings = null)
        {
            _settings = settings?.Clone() ?? new PocoBuilderSettings();
        }

        private readonly PocoBuilderSettings _settings;

        public ExceptionNotificationHandler ExceptionHandler { get; set; }
        
        /// <summary>
        /// Build a POCO from an ISourceNode.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dataType">Optional. Type of POCO to build. Should be one of the generated POCO classes.</param>
        /// <returns></returns>
        /// <remarks>If <paramref name="dataType"/> is not supplied, or is <code>Resource</code> or <code>DomainResource</code>, 
        /// the builder will try to determine the actual type to create from the <paramref name="source"/>. </remarks>
        public Base BuildFrom(ISourceNode source, Type dataType = null)
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            string typeFound = null;
            if (dataType != null)
            {
                typeFound = ModelInfo.GetFhirTypeNameForType(dataType);

                if (typeFound == null)
                {
                    ExceptionNotification.Error(
                        new StructuralTypeException($"While building a POCO: The .NET type '{dataType.Name}' does not represent a FHIR type."));

                    return null;
                }
            }
            return BuildFrom(source, typeFound);
        }

        /// <summary>
        /// Build a POCO from an ISourceNode.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dataType">Optional. Type of POCO to build. Should be the name of one of the generated POCO classes.</param>
        /// <returns></returns>
        /// <remarks>If <paramref name="dataType"/> is not supplied, or is <code>Resource</code> or <code>DomainResource</code>, 
        /// the builder will try to determine the actual type to create from the <paramref name="source"/>. </remarks>
        public Base BuildFrom(ISourceNode source, string dataType = null)
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            TypedElementSettings typedSettings = new TypedElementSettings
            {
                ErrorMode = _settings.IgnoreUnknownMembers ?
                    TypedElementSettings.TypeErrorMode.Ignore
                    : TypedElementSettings.TypeErrorMode.Report
            };

            // If dataType is an abstract resource superclass -> ToTypedElement(with type=null) will figure it out;
            if (dataType == FHIRDefinedType.Resource.GetLiteral() || dataType == FHIRDefinedType.DomainResource.GetLiteral())
                dataType = null;

            var typedSource = source.ToTypedElement(new PocoStructureDefinitionSummaryProvider(), dataType, typedSettings);

            return BuildFrom(typedSource);
        }

        /// <summary>
        /// Build a POCO from an ITypedElement.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public Base BuildFrom(ITypedElement source)
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));

            if (source is IExceptionSource)
            {
                using (source.Catch((o, a) => ExceptionHandler.NotifyOrThrow(o, a)))
                {
                    return build();
                }
            }
            else
                return build();

            Base build()
            {
                var typeToBuild = ModelInfo.GetTypeForFhirType(source.InstanceType);

                if (typeToBuild == null)
                {
                    ExceptionHandler.NotifyOrThrow(this,
                        ExceptionNotification.Error(
                            new StructuralTypeException($"While building a POCO: There is no .NET type representing the FHIR type '{source.InstanceType}'.")));

                    return null;
                }

                var settings = new ParserSettings
                {
                    AcceptUnknownMembers = _settings.IgnoreUnknownMembers,
                    AllowUnrecognizedEnums = _settings.AllowUnrecognizedEnums
                };

                return typeToBuild.CanBeTreatedAsType(typeof(Resource))
                    ? new ResourceReader(source, settings).Deserialize()
                    : new ComplexTypeReader(source, settings).Deserialize();
            }
        }
    }
}

