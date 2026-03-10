/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


using Hl7.Fhir.Introspection;

#nullable enable

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// Contains contextual information for the property that is currently being deserialized and is passed 
    /// to delegate methods implementing parts of user-definable deserialization and validation logic.
    /// </summary>
    public readonly struct PropertyDeserializationContext
    {
        internal PropertyDeserializationContext(
            PathStack path,
            string propertyName,
            long lineNumber,
            long linePosition,
            PropertyMapping propMapping)
        {
            _path = path;
            PropertyName = propertyName;
            LineNumber = lineNumber;
            LinePosition = linePosition;
            ElementMapping = propMapping;
        }

        private readonly PathStack _path;

        internal PathStack PathStack { get { return _path; } }

        /// <summary>
        /// The dotted path leading to this element from the root (has no indexers and includes the value virtual property on primitives)
        /// </summary>
        public string Path { get { return _path.GetPath(); } }

        /// <summary>
        /// The dotted FhirPath path leading to this element from the root with indexers (and no value property on primitives).
        /// </summary>
        public string InstancePath { get { return _path.GetInstancePath(); } }

        /// <summary>
        /// The property name for which an instance is currently being deserialized.
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// The approximate line number in the source data that is being deserialized.
        /// </summary>
        public long LineNumber { get; }

        /// <summary>
        /// The approximate line position in the source data that is being deserialized.
        /// </summary>
        public long LinePosition { get; }

        /// <summary>
        /// The metadata for the element that is currently being deserialized.
        /// </summary>
        public PropertyMapping ElementMapping { get; }
    }

    /// <summary>
    /// Contains contextual information for the instance that is currently being deserialized and is passed 
    /// to delegate methods implementing parts of user-definable deserialization and validation logic.
    /// </summary>
    public readonly struct InstanceDeserializationContext
    {
        internal InstanceDeserializationContext(
            PathStack path,
            long lineNumber,
            long linePosition,
            ClassMapping instanceMapping)
        {
            _path = path;
            LineNumber = lineNumber;
            LinePosition = linePosition;
            InstanceMapping = instanceMapping;
        }

        private readonly PathStack _path;

        internal PathStack PathStack { get { return _path; } }

        /// <summary>
        /// The dotted FhirPath path leading to this element from the root.
        /// </summary>
        public string Path { get { return _path.GetInstancePath(); } }

        /// <summary>
        /// The approximate line number in the source data that is being deserialized.
        /// </summary>
        public long LineNumber { get; }

        /// <summary>
        /// The approximate line position in the source data that is being deserialized.
        /// </summary>
        public long LinePosition { get; }

        /// <summary>
        /// The metadata for the type of which the current property is part of.
        /// </summary>
        public ClassMapping InstanceMapping { get; }
    }
}

#nullable restore
