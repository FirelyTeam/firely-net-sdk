/*
 * Copyright (c) 2026, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */
using System.Threading.Tasks;
using Hl7.Fhir.Model;

#nullable enable
namespace Hl7.Fhir.Specification.Source
{
    /// <summary>
    /// Resolves the extension definition behind a name, for logical models that use the
    /// <c>named-elements</c> extension style (see
    /// <see href="https://hl7.org/fhir/tools/StructureDefinition-extension-style.html">extension-style</see>
    /// in the FHIR Tooling Extensions IG), where
    /// extensions appear as plain properties instead of <c>extension</c> entries.
    /// </summary>
    /// <remarks>
    /// The logical model declares the style on the element that may carry extensions:
    /// <code>
    /// { "path": "MyModel", "extension": [{
    ///     "url": "http://hl7.org/fhir/tools/StructureDefinition/extension-style",
    ///     "valueCode": "named-elements" }] }
    /// </code>
    /// The extensions themselves are separate <see cref="StructureDefinition"/>s that state the
    /// name they appear under, via the <c>json-name</c> (or <c>xml-name</c>) extension on their
    /// root element:
    /// <code>
    /// { "url": "http://example.org/StructureDefinition/my-ext", "type": "Extension", ...
    ///   "path": "Extension", "extension": [{
    ///     "url": "http://hl7.org/fhir/tools/StructureDefinition/json-name",
    ///     "valueString": "myExt" }] }
    /// </code>
    /// So an instance spells that extension as a plain property, with no <c>url</c> to resolve by:
    /// <code>
    /// { "resourceType": "MyModel", "myExt": "some value" }
    /// </code>
    /// Validating <c>myExt</c> therefore means going from the name back to the definition:
    /// <c>TryResolveNamedExtensionAsync("myExt")</c> returns the <c>my-ext</c> StructureDefinition.
    /// </remarks>
    public interface INamedExtensionResolver : IAsyncResourceResolver
    {
        /// <summary>Find the extension definition registered under <paramref name="name"/>.</summary>
        /// <param name="name">The json/xml name of the property, as it appears in the instance.</param>
        /// <returns>
        /// A <see cref="ResolverResult"/> carrying the extension's <see cref="StructureDefinition"/>, or the
        /// <see cref="ResolverException"/> explaining why no definition could be produced for that name.
        /// </returns>
        ValueTask<ResolverResult> TryResolveNamedExtensionAsync(string name);
    }
}
