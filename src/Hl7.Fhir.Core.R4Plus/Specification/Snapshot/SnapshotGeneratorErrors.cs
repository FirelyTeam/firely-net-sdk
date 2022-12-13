using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Utility;
using System;

namespace Hl7.Fhir.Specification.Snapshot
{
    internal static class SnapshotGeneratorErrors
    {
        public const string Error_Navigator_Position = "Error! The specified element definition navigator is not positioned on an element.";
        public const string Error_ElementDefinition_Path = "Error! The specified element definition has no path.";
        public const string Error_StructureDefinition_Snapshot = "Error! The specified structure definition has no snapshot.";

        /// <summary>
        /// Throw a runtime exception if the specified <see cref="ElementDefinitionNavigator"/> instance
        /// is <c>null</c> or if it is not positioned on an element.
        /// </summary>
        public static void ThrowIfNullOrNotPositioned(this ElementDefinitionNavigator nav, string paramName)
        {
            if (nav == null) { throw Error.ArgumentNull(nameof(nav)); }
            if (nav.AtRoot) { throw Error.Argument(nameof(nav), Error_Navigator_Position); }
        }

        /// <summary>
        /// Throw a runtime exception if the specified <see cref="ElementDefinition"/> instance is <c>null</c>,
        /// or if the <see cref="ElementDefinition.Path"/> property is <c>null</c> or empty.
        /// </summary>
        public static void ThrowIfNullOrEmptyPath(this ElementDefinition elemDef, string paramName)
        {
            if (elemDef == null) { throw Error.ArgumentNull(paramName); }
            if (string.IsNullOrEmpty(elemDef.Path)) { throw Error.Argument(paramName, Error_ElementDefinition_Path); }
        }

        /// <summary>
        /// Throw a runtime exception if the specified <see cref="StructureDefinition"/> instance is <c>null</c>,
        /// or if the <see cref="StructureDefinition.Snapshot"/> component is <c>null</c> or empty.
        /// </summary>
        public static void ThrowIfNullOrNoSnapshot(this StructureDefinition structDef, string paramName)
        {
            if (structDef == null) { throw Error.ArgumentNull(paramName); }
            if (!structDef.HasSnapshot) { throw Error.Argument(paramName, Error_StructureDefinition_Snapshot); }
        }

    }
}
