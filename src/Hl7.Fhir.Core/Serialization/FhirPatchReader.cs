/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */


using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Patch;
using Hl7.Fhir.Patch.Operations;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using Hl7.FhirPath;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Serialization
{
    public static class FhirPatchReader
    {
        enum ParameterName
        {
            Type,
            Path,
            Name,
            Value,
            Index,
            Source,
            Destination
        }

        public static PatchDocument Read (Parameters fhirPatch)
        {
            var patchDocument = new PatchDocument(new List<Operation>(), new PocoStructureDefinitionSummaryProvider());

            foreach ( var parameter in fhirPatch.Parameter )
            {
                if ( parameter.Name != "operation" ) 
                    throw Error.Argument(nameof(parameter.Name), "Parameter Name must be 'operation'");
                
                if( parameter.Part.IsNullOrEmpty()) 
                    throw Error.ArgumentNullOrEmpty(nameof(parameter.Part));

                var operationParams = new Dictionary<ParameterName, Parameters.ParameterComponent>();
                foreach ( var part in parameter.Part )
                {
                    if ( !Enum.TryParse(part.Name, ignoreCase: true, result: out ParameterName paramName) )
                        throw Error.Argument("parameter.part.Name", $"Operation parameter name '${part.Name}' is not a valid parameter name");

                    operationParams.Add(paramName, part);
                }

                // Verify Operation type is present and valid
                if ( !operationParams.TryGetValue(ParameterName.Type, out var operationTypeComponent))
                    throw Error.Argument($"parameter.part['type']", "Operation type must be specified");

                var operationTypeStr = (operationTypeComponent.Value as Code)?.Value;
                if ( !Enum.TryParse(operationTypeStr, ignoreCase: true, result: out OperationType operationType) )
                    throw Error.Argument("parameter.part['type']", $"Operation type '{operationTypeStr}' is not a valid operation type");

                // Verify path is present and parse it
                if ( !operationParams.TryGetValue(ParameterName.Path, out var pathComponent) )
                    throw Error.Argument($"parameter.part['path']", "Path must be specified");

                var path = (pathComponent.Value as FhirString)?.Value;
                if ( string.IsNullOrEmpty(path) )
                    throw Error.ArgumentNullOrEmpty("parameter.part['path']");

                CompiledExpression fhirPath = null;
                try
                {
                    var fhirPathCompiler = new FhirPathCompiler();
                    fhirPath = fhirPathCompiler.Compile(path);
                }
                catch (Exception ex)
                {
                    throw Error.InvalidOperation(ex, $"Specified path '{path}' is not valid");
                }

                // Parse value parameter if it is required
                ITypedElement value = null;
                switch ( operationType )
                {
                    // Value is required
                    case OperationType.Add:
                    case OperationType.Insert:
                    case OperationType.Replace:
                        // Verify value is present and parse it
                        if ( !operationParams.TryGetValue(ParameterName.Value, out var valueComponent) )
                            throw Error.Argument($"parameter.part['value']", "Value must be specified");

                        if ( valueComponent.Value == null )
                            throw Error.ArgumentNull("parameter.part['value']");

                        value = valueComponent.Value.ToTypedElement();
                        
                        break;
                    // Value is not required
                    case OperationType.Delete:
                    case OperationType.Move:
                    case OperationType.Invalid:
                    default:
                        break;
                }

                // Parse operation specific parameters and add operation to the patch document
                switch ( operationType )
                {
                    case OperationType.Add:
                        // Verify name is present and parse it
                        if ( !operationParams.TryGetValue(ParameterName.Name, out var nameComponent) )
                            throw Error.Argument($"parameter.part['name']", "Name must be specified");

                        var name = (nameComponent.Value as FhirString)?.Value;
                        if ( string.IsNullOrEmpty(name) )
                            throw Error.ArgumentNullOrEmpty("parameter.part['name']");

                        patchDocument.Add(fhirPath, name, value);
                        break;
                    case OperationType.Insert:
                        // Verify index is present and parse it
                        if ( !operationParams.TryGetValue(ParameterName.Index, out var indexComponent) )
                            throw Error.Argument($"parameter.part['index']", "Index must be specified");

                        var index = (indexComponent.Value as Integer)?.Value;
                        if ( index == null )
                            throw Error.ArgumentNull("parameter.part['index']");

                        patchDocument.Insert(fhirPath, index.Value, value);
                        break;
                    case OperationType.Delete:
                        patchDocument.Delete(fhirPath);
                        break;
                    case OperationType.Replace:
                        patchDocument.Replace(fhirPath, value);
                        break;
                    case OperationType.Move:
                        // Verify source index is present and parse it
                        if ( !operationParams.TryGetValue(ParameterName.Source, out var sourceComponent) )
                            throw Error.Argument($"parameter.part['source']", "Source index must be specified");

                        var source = (sourceComponent.Value as Integer)?.Value;
                        if ( source == null )
                            throw Error.ArgumentNull("parameter.part['source']");

                        // Verify destination index is present and parse it
                        if ( !operationParams.TryGetValue(ParameterName.Destination, out var destinationComponent) )
                            throw Error.Argument($"parameter.part['destination']", "Destination index must be specified");

                        var destination = (destinationComponent.Value as Integer)?.Value;
                        if ( destination == null )
                            throw Error.ArgumentNull("parameter.part['destination']");

                        patchDocument.Move(fhirPath, source.Value, destination.Value);
                        break;
                    case OperationType.Invalid:
                    default:
                        throw Error.InvalidOperation($"Operation type '{operationTypeStr}' is not a valid operation type");
                }
            }

            return patchDocument;
        }
    }
}
