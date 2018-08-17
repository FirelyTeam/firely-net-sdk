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

namespace Hl7.Fhir.Serialization
{
    public class PocoBuilder : IExceptionSource
    {
        public PocoBuilder(PocoBuilderSettings settings = null)
        {
            _settings = settings?.Clone() ?? new PocoBuilderSettings();
        }

        private readonly PocoBuilderSettings _settings;

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        public Base BuildFrom(ISourceNode source, Type dataType = null)
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));

            if (dataType != null)
                return buildInternal(source, dataType);
            else
            {
                var rti = source.GetResourceTypeIndicator();
                if (rti == null)
                {
                    ExceptionHandler.NotifyOrThrow(this,
                     ExceptionNotification.Error(new StructuralTypeException($"No type indication on source to build POCO's for.")));
                    return null;
                }
                else
                    return BuildFrom(source, rti);
            }
        }

        public Base BuildFrom(ISourceNode source, string dataType)
        {
            if (dataType == null) throw Error.ArgumentNull(nameof(dataType));

            var typeFound = ModelInfo.GetTypeForFhirType(dataType);

            if (typeFound == null)
            {
                ExceptionNotification.Error(
                    new StructuralTypeException($"There is no .NET type representing the FHIR type '{dataType}'."));

                return null;
            }
            else
                return buildInternal(source, typeFound);
        }

        public Base BuildFrom(ITypedElement source)
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));

            return BuildFrom(source.ToSourceNode(), source.InstanceType);
        }

        private Base buildInternal(ISourceNode source, Type typeToBuild)
        {
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

                return typeToBuild.CanBeTreatedAsType(typeof(Resource))
                    ? new ResourceReader(source, settings).Deserialize()
                    : new ComplexTypeReader(source, settings).Deserialize(typeToBuild);
            }
        }
    }
}

