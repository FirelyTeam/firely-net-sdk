using Hl7.Fhir.Generation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Model
{
    [ApplyInterfaceToGeneratedClasses]
    public interface IIdentifiable
    {
        List<Identifier> Identifier { get; set; }
    }

    public static class IdentifiableExtensions
    {
        /// <summary>
        /// Gets the first identifier having the specified system. If none is being found, <c>null</c> will be returned.
        /// </summary>
        /// <param name="identifiable">The identifiable containing the identifiers to search.</param>
        /// <param name="system">The system to search for (case sensitive).</param>
        /// <returns>The first identifier having the specified system or <c>null</c>.</returns>
        public static Identifier GetIdentifier(this IIdentifiable identifiable, string system)
        {
            if (system == null)
            {
                throw new ArgumentNullException(nameof(system));
            }

            return identifiable?.Identifier?.FirstOrDefault(identifier => identifier.System == system);
        }

        /// <summary>
        /// Tries to get the identifier having the specified system.
        /// </summary>
        /// <param name="identifiable">The identifiable containing the identifiers to search.</param>
        /// <param name="system">The system to search for (case sensitive).</param>
        /// <param name="identifier">The first identifier having the specified system, if it could be found; otherwise <c>null</c>.</param>
        /// <returns><c>true</c> if the identifier was found; otherwise <c>false</c>.</returns>
        public static bool TryGetIdentifier(this IIdentifiable identifiable, string system, out Identifier identifier)
        {
            identifier = GetIdentifier(identifiable, system);
            return identifier != null;
        }

        /// <summary>
        /// Gets the value of the first identifier having the specified system. If none is being found, <c>null</c> will be returned.
        /// </summary>
        /// <param name="identifiable">The identifiable containing the identifiers to search.</param>
        /// <param name="system">The system to search for (case sensitive).</param>
        /// <returns>The value of the first identifier having the specified system or <c>null</c>.</returns>
        public static string GetIdentifierValue(this IIdentifiable identifiable, string system)
        {
            if (system == null)
            {
                throw new ArgumentNullException(nameof(system));
            }

            return identifiable?.Identifier?.FirstOrDefault(identifier => identifier.System == system)?.Value;
        }

        /// <summary>
        /// Tries to get the value of the first identifier having the specified system.
        /// </summary>
        /// <param name="identifiable">The identifiable containing the identifiers to search.</param>
        /// <param name="system">The system to search for (case sensitive).</param>
        /// <param name="identifierValue">The value of the first identifier having the specified system, if it could be found; otherwise <c>null</c>.</param>
        /// <returns><c>true</c> if the value was found; otherwise <c>false</c>.</returns>
        public static bool TryGetIdentifierValue(this IIdentifiable identifiable, string system, out string identifierValue)
        {
            identifierValue = GetIdentifierValue(identifiable, system);
            return identifierValue != null;
        }
    }
}