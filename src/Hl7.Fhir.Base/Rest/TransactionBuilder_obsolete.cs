/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using System;

namespace Hl7.Fhir.Rest;

/// <summary>
/// Builder to describe a FHIR transaction Bundle
/// </summary>
public partial class TransactionBuilder
{
    /// <summary>
    /// Add a "conditional update" entry to the transaction/batch
    /// </summary>
    /// <param name="condition">conditions on which a resource is supposed to be updated or not</param>
    /// <param name="body">the new version of the resource to be updated</param>
    /// <param name="versionId">optional version id of the resource</param>
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <returns></returns>
    [Obsolete("This overload will be replaced by ConditionalUpdate(). Using the new method is recommended")]
    public TransactionBuilder Update(SearchParams condition, Resource body, string? versionId = null, string? bundleEntryFullUrl = null)
    {
        return ConditionalUpdate(condition, body, versionId, bundleEntryFullUrl);
    }
        
    /// <summary>
    /// Add a "conditional patch" entry to the transaction/batch
    /// </summary>
    /// <param name="resourceType">type of the resource to be patched</param>
    /// <param name="condition">conditions on which the a resource is supposed to be patched</param>
    /// <param name="body">parameters resource that describes the patch operation</param>
    /// <param name="versionId">optional version id of the resource</param>
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <returns></returns>
    [Obsolete("This overload will be replaced by ConditionalPatch(). Using the new method is recommended")]
    public TransactionBuilder Patch(string resourceType, SearchParams condition, Parameters body, string? versionId = null, string? bundleEntryFullUrl = null)
    {
        return ConditionalPatch(resourceType, condition, body, versionId, bundleEntryFullUrl);
    }
        
    /// <summary>
    /// Add a "conditional delete" entry to the transaction/batch
    /// </summary>
    /// <param name="resourceType">type of the resource to be deleted</param>
    /// <param name="condition">conditions on which the resource should be deleted</param>
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <returns></returns>
    [Obsolete("This overload will be replaced by ConditionalDeleteSingle() and ConditionalDeleteMultiple(). Using the new methods is recommended")]
    public TransactionBuilder Delete(string resourceType, SearchParams condition, string? bundleEntryFullUrl = null)
    {
        return ConditionalDeleteSingle(condition, resourceType, bundleEntryFullUrl);
    }
        
    /// <summary>
    /// Add a "conditional create" entry to the transaction/batch
    /// </summary>
    /// <param name="body">the resource that is to be created</param>
    /// <param name="condition">conditions on which the resource is supposed to be created</param>
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <returns></returns>
    [Obsolete("This overload will be replaced by ConditionalCreate(). Using the new method is recommended")]
    public TransactionBuilder Create(Resource body, SearchParams condition, string? bundleEntryFullUrl = null)
    {
        return ConditionalCreate(body, condition, bundleEntryFullUrl);
    }
}
#nullable restore