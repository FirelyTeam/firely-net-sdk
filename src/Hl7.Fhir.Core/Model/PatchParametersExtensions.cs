using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Hl7.Fhir.Rest
{
    public static class PatchParameterExtensions
    {
        private enum PatchType
        {
            [EnumLiteral("add")]
            Add,
            [EnumLiteral("insert")]
            Insert,
            [EnumLiteral("delete")]
            Delete,
            [EnumLiteral("replace")]
            Replace,
            [EnumLiteral("move")]
            Move
        }

        private const string OPERATION = "operation";

        /// <summary>
        /// Add an "add" patch operation as a parameter
        /// </summary>
        /// <param name="parameters">Parameters resource this operation should be added to</param>
        /// <param name="path">Path at which to add the content</param>
        /// <param name="name">Name of the property to add</param>
        /// <param name="value">Value to add at nominated place</param>
        public static Parameters AddAddPatchParameter(this Parameters parameters, string path, string name, DataType value)
        {
            var parts = new List<Parameters.ParameterComponent>()
                .addTypePart(PatchType.Add)
                .addPathPart(path)
                .addNamePart(name)
                .addValuePart(value);               

            parameters.addPatchOperation(parts);

            return parameters;
        }

        /// <summary>
        /// Add an "insert" patch operation as a parameter
        /// </summary>
        /// <param name="parameters">Parameters resource this operation should be added to</param>
        /// <param name="path">Path of the collection in which to insert the content</param>
        /// <param name="value">Value to add at nominated place</param>
        /// <param name="index">Index at which to insert</param>
        public static Parameters AddInsertPatchParameter(this Parameters parameters, string path, DataType value, int index)
        {

            var parts = new List<Parameters.ParameterComponent>()
                .addTypePart(PatchType.Insert)
                .addPathPart(path)
                .addValuePart(value)
                .addIndexPart(index);

            parameters.addPatchOperation(parts);

            return parameters;
        }

        /// <summary>
        /// Add a "delete" patch operation as a parameter
        /// </summary>
        /// <param name="parameters">Parameters resource this operation should be added to</param>
        /// <param name="path">Path of the content to delete (if found)</param>
        public static Parameters AddDeletePatchParameter(this Parameters parameters, string path)
        {
            var parts = new List<Parameters.ParameterComponent>()
                .addTypePart(PatchType.Delete)
                .addPathPart(path);           

            parameters.addPatchOperation(parts);

            return parameters;
        }

        /// <summary>
        /// Add a "replace" patch operation as a parameter
        /// </summary>
        /// <param name="parameters">Parameters resource this operation should be added to</param>      
        /// <param name="path">Path of the content to replace</param>
        /// <param name="value">value to replace it with</param>
        public static Parameters AddReplacePatchParameter(this Parameters parameters, string path, DataType value)
        {
            var parts = new List<Parameters.ParameterComponent>()
                .addTypePart(PatchType.Replace)
                .addPathPart(path)
                .addValuePart(value);

            parameters.addPatchOperation(parts);

            return parameters;
        }


        /// <summary>
        /// Add a "move" patch operation as a parameter
        /// </summary>
        /// <param name="parameters">Parameters resource this operation should be added to</param>
        /// <param name="path">Path of the collection in which to move the content</param>
        /// <param name="source">List index to move from</param>
        /// <param name="destination">List index to move to</param>
        public static Parameters AddMovePatchParameter(this Parameters parameters, string path, int source, int destination)
        {
            var parts = new List<Parameters.ParameterComponent>()
                .addTypePart(PatchType.Move)
                .addPathPart(path)
                .addSourcePart(source)
                .addDestinationPart(destination);

            parameters.addPatchOperation(parts);

            return parameters;
        }

        private static Parameters addPatchOperation(this Parameters parameters, List<Parameters.ParameterComponent> parts)
        {
           parameters.Parameter.Add(
                  new Parameters.ParameterComponent
                  {
                      Name = OPERATION,
                      Part = parts
                  }
            );

            return parameters;
        }     

        private static List<Parameters.ParameterComponent> addPatchPart(this List<Parameters.ParameterComponent> parts, string name, DataType value)
        {
            parts.Add(
                new Parameters.ParameterComponent
                {
                    Name = name,
                    Value = value
                }
            );           
            return parts;
        }

        private static List<Parameters.ParameterComponent> addTypePart(this List<Parameters.ParameterComponent> parameters, PatchType type)
        {
            return parameters.addPatchPart("type", new Code(type.GetLiteral()));
        }

        private static List<Parameters.ParameterComponent> addPathPart(this List<Parameters.ParameterComponent> parameters, string path)
        {
            return parameters.addPatchPart("path", new FhirString(path));
        }

        private static List<Parameters.ParameterComponent> addNamePart(this List<Parameters.ParameterComponent> parameters, string name)
        {
            return parameters.addPatchPart("name", new FhirString(name));
        }

        private static List<Parameters.ParameterComponent> addValuePart(this List<Parameters.ParameterComponent> parameters, DataType value)
        {
            return parameters.addPatchPart("value", value);
        }

        private static List<Parameters.ParameterComponent> addIndexPart(this List<Parameters.ParameterComponent> parameters, int index)
        {
            return parameters.addPatchPart("index", new Integer(index));
        }

        private static List<Parameters.ParameterComponent> addSourcePart(this List<Parameters.ParameterComponent> parameters, int source)
        {
            return parameters.addPatchPart("source", new Integer(source));
        }

        private static List<Parameters.ParameterComponent> addDestinationPart(this List<Parameters.ParameterComponent> parameters, int destination)
        {
            return parameters.addPatchPart("destination", new Integer(destination));
        }
    }
}