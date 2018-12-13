/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
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
        
        //dataType can also be Resourse or DomainResourse
        //the builder will figure the type by itself when it is the case
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
                        new StructuralTypeException($"The .NET type '{dataType.Name}' does not represent a FHIR type."));

                    return null;
                }
            }
            return BuildFrom(source, typeFound);
        }
        
        //dataType can also be Resourse or DomainResourse
        //the builder will figure the type by itself when it is the case
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
                var settings = new ParserSettings
                {
                    AcceptUnknownMembers = _settings.IgnoreUnknownMembers,
                    AllowUnrecognizedEnums = _settings.AllowUnrecognizedEnums
                };

                var typeToBuild = ModelInfo.GetTypeForFhirType(source.InstanceType);

                if (typeToBuild == null)
                {
                    ExceptionNotification.Error(
                        new StructuralTypeException($"There is no .NET type representing the FHIR type '{source.InstanceType}'."));

                    return null;
                }

                return typeToBuild.CanBeTreatedAsType(typeof(Resource))
                    ? new ResourceReader(source, settings).Deserialize()
                    : new ComplexTypeReader(source, settings).Deserialize();
            }
        }
    }
}

