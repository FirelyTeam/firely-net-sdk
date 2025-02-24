/* 
 * Copyright (c) 2023, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable


using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Marks a resource that can be identified by some assigned identifier.
    /// </summary>
    public interface IIdentifiable
    {
        // Empty marker interface
    }

    /// <summary>
    /// Represents a resource that can be identified by some assigned identifier.
    /// </summary>
    /// <typeparam name="T">The type that is used to identify the resource, usually a (list of) <see cref="Identifier"/>.</typeparam>
    public interface IIdentifiable<T> : IIdentifiable
    {
        T Identifier { get; set; }
    }


    public static class IdentifiableExtensions
    {
        /// <summary>
        /// Gets the first identifier having the specified system. If none is being found, <c>null</c> will be returned.
        /// </summary>
        /// <param name="identifiable">The identifiable containing the identifiers to search.</param>
        /// <param name="system">The system to search for (case sensitive).</param>
        /// <returns>The first identifier having the specified system or <c>null</c>.</returns>
        public static Identifier? GetIdentifier(this IIdentifiable<List<Identifier>> identifiable, string system)
        {
            if (system is null) throw new ArgumentNullException(nameof(system));
            return identifiable.Identifier?.FirstOrDefault(identifier => identifier.System == system);
        }

        /// <summary>
        /// Tries to get the identifier having the specified system.
        /// </summary>
        /// <param name="identifiable">The identifiable containing the identifiers to search.</param>
        /// <param name="system">The system to search for (case sensitive).</param>
        /// <param name="identifier">The first identifier having the specified system, if it could be found; otherwise <c>null</c>.</param>
        /// <returns><c>true</c> if the identifier was found; otherwise <c>false</c>.</returns>
        public static bool TryGetIdentifier(this IIdentifiable<List<Identifier>> identifiable, string system, out Identifier? identifier)
        {
            identifier = GetIdentifier(identifiable, system);
            return identifier is not null;
        }

        /// <summary>
        /// Matches the identifier with the specified system. If the systems differ, <c>null</c> will be returned.
        /// </summary>
        /// <param name="identifiable">The identifiable containing an identifier to match.</param>
        /// <param name="system">The system to search for (case sensitive).</param>
        public static Identifier? GetIdentifier(this IIdentifiable<Identifier> identifiable, string system)
        {
            if (system is null) throw new ArgumentNullException(nameof(system));
            return identifiable.Identifier?.System == system ? identifiable.Identifier : null;
        }

        /// <summary>
        /// Tries to match the identifier with the specified system.
        /// </summary>
        /// <param name="identifiable">The identifiable containing an identifier to match.</param>
        /// <param name="system">The system to search for (case sensitive).</param>
        /// <param name="identifier">The identifier if it matches the specified system, otherwise <c>null</c>.</param>
        public static bool TryGetIdentifier(this IIdentifiable<Identifier> identifiable, string system, [NotNullWhen(true)] out Identifier? identifier)
        {
            identifier = GetIdentifier(identifiable, system);
            return identifier is not null;
        }

        /// <summary>
        /// Catches the unsupported invocations of GetIdentifier.
        /// </summary>
#pragma warning disable IDE0060 // Remove unused parameter
        public static Identifier? GetIdentifier<X>(this IIdentifiable<X> identifiable, string system) => null;
#pragma warning restore IDE0060 // Remove unused parameter

        /// <summary>
        /// Catches the unsupported invocations of TryGetIdentifier.
        /// </summary>
        public static bool TryGetIdentifier<X>(this IIdentifiable<X> identifiable, string system, out Identifier? identifier)
        {
            identifier = GetIdentifier(identifiable, system);
            return identifier is not null;
        }

        /// <summary>
        /// Gets the first identifier having the specified system. If none is being found, <c>null</c> will be returned.
        /// </summary>
        /// <param name="identifiable">The identifiable containing the identifiers to search.</param>
        /// <param name="system">The system to search for (case sensitive).</param>
        /// <returns>The first identifier having the specified system or <c>null</c>.</returns>
        public static Identifier? GetIdentifier(this IIdentifiable identifiable, string system)
        {
            // Try all interfaces
            if (identifiable is IIdentifiable<Identifier> single && single.TryGetIdentifier(system, out var identifier)) return identifier;
            if (identifiable is IIdentifiable<List<Identifier>> list && list.TryGetIdentifier(system, out identifier)) return identifier;

            return null;
        }

        /// <summary>
        /// Tries to match the identifier(s) with the specified system.
        /// </summary>
        /// <param name="identifiable">The identifiable containing an identifier(s) to match.</param>
        /// <param name="system">The system to search for (case sensitive).</param>
        /// <param name="identifier">The identifier if it matches the specified system, otherwise <c>null</c>.</param>
        public static bool TryGetIdentifier(this IIdentifiable identifiable, string system, out Identifier? identifier)
        {
            identifier = GetIdentifier(identifiable, system);
            return identifier is not null;
        }
    }
}

#nullable restore