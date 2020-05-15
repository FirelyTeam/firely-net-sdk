using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using Hl7.Fhir.Utility;

/*
    Copyright (c) 2011+, HL7, Inc.
    All rights reserved.

    Redistribution and use in source and binary forms, with or without modification, 
    are permitted provided that the following conditions are met:

    * Redistributions of source code must retain the above copyright notice, this 
        list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright notice, 
        this list of conditions and the following disclaimer in the documentation 
        and/or other materials provided with the distribution.
    * Neither the name of HL7 nor the names of its contributors may be used to 
        endorse or promote products derived from this software without specific 
        prior written permission.

    THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
    ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
    WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
    IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
    INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
    NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
    PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
    WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
    ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
    POSSIBILITY OF SUCH DAMAGE.


*/
#pragma warning disable 1591 // suppress XML summary warnings

//
// Generated for FHIR v1.0.2, v4.0.1, v3.0.1
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Describes a set of tests
    /// </summary>
    public partial interface ITestScript : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Absolute URL used to reference this TestScript
        /// </summary>
        Hl7.Fhir.Model.FhirUri UrlElement { get; set; }
        
        /// <summary>
        /// Absolute URL used to reference this TestScript
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Url { get; set; }
    
        /// <summary>
        /// External identifier
        /// </summary>
        Hl7.Fhir.Model.Identifier Identifier { get; set; }
    
        /// <summary>
        /// Logical id for this version of the TestScript
        /// </summary>
        Hl7.Fhir.Model.FhirString VersionElement { get; set; }
        
        /// <summary>
        /// Logical id for this version of the TestScript
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Version { get; set; }
    
        /// <summary>
        /// Informal name for this TestScript
        /// </summary>
        Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
        /// <summary>
        /// Informal name for this TestScript
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Name { get; set; }
    
        /// <summary>
        /// If for testing purposes, not real usage
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean ExperimentalElement { get; set; }
        
        /// <summary>
        /// If for testing purposes, not real usage
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? Experimental { get; set; }
    
        /// <summary>
        /// Date for this version of the TestScript
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime DateElement { get; set; }
        
        /// <summary>
        /// Date for this version of the TestScript
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Date { get; set; }
    
        /// <summary>
        /// Name of the publisher (Organization or individual)
        /// </summary>
        Hl7.Fhir.Model.FhirString PublisherElement { get; set; }
        
        /// <summary>
        /// Name of the publisher (Organization or individual)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Publisher { get; set; }
    
        /// <summary>
        /// Required capability that is assumed to function correctly on the FHIR server being tested
        /// </summary>
        Hl7.Fhir.Model.ITestScriptMetadataComponent Metadata { get; }
    
        /// <summary>
        /// Fixture in the test script - by reference (uri)
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ITestScriptFixtureComponent> Fixture { get; }
    
        /// <summary>
        /// Reference of the validation profile
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Profile { get; set; }
    
        /// <summary>
        /// Placeholder for evaluated elements
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ITestScriptVariableComponent> Variable { get; }
    
        /// <summary>
        /// A series of required setup operations before tests are executed
        /// </summary>
        Hl7.Fhir.Model.ITestScriptSetupComponent Setup { get; }
    
        /// <summary>
        /// A test in this script
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ITestScriptTestComponent> Test { get; }
    
        /// <summary>
        /// A series of required clean up steps
        /// </summary>
        Hl7.Fhir.Model.ITestScriptTeardownComponent Teardown { get; }
    
    }
    
    public partial interface ITestScriptMetadataComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Links to the FHIR specification
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ITestScriptLinkComponent> Link { get; }
    
        /// <summary>
        /// Capabilities  that are assumed to function correctly on the FHIR server being tested
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ITestScriptCapabilityComponent> Capability { get; }
    
    }
    
    public partial interface ITestScriptLinkComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// URL to the specification
        /// </summary>
        Hl7.Fhir.Model.FhirUri UrlElement { get; set; }
        
        /// <summary>
        /// URL to the specification
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Url { get; set; }
    
        /// <summary>
        /// Short description
        /// </summary>
        Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
        /// <summary>
        /// Short description
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
    }
    
    public partial interface ITestScriptCapabilityComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Are the capabilities required?
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean RequiredElement { get; set; }
        
        /// <summary>
        /// Are the capabilities required?
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? Required { get; set; }
    
        /// <summary>
        /// Are the capabilities validated?
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean ValidatedElement { get; set; }
        
        /// <summary>
        /// Are the capabilities validated?
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? Validated { get; set; }
    
        /// <summary>
        /// The expected capabilities of the server
        /// </summary>
        Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
        /// <summary>
        /// The expected capabilities of the server
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
        /// <summary>
        /// Which server these requirements apply to
        /// </summary>
        Hl7.Fhir.Model.Integer DestinationElement { get; set; }
        
        /// <summary>
        /// Which server these requirements apply to
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? Destination { get; set; }
    
        /// <summary>
        /// Links to the FHIR specification
        /// </summary>
        List<Hl7.Fhir.Model.FhirUri> LinkElement { get; set; }
        
        /// <summary>
        /// Links to the FHIR specification
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        IEnumerable<string> Link { get; set; }
    
    }
    
    public partial interface ITestScriptFixtureComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Whether or not to implicitly create the fixture during setup
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean AutocreateElement { get; set; }
        
        /// <summary>
        /// Whether or not to implicitly create the fixture during setup
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? Autocreate { get; set; }
    
        /// <summary>
        /// Whether or not to implicitly delete the fixture during teardown
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean AutodeleteElement { get; set; }
        
        /// <summary>
        /// Whether or not to implicitly delete the fixture during teardown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? Autodelete { get; set; }
    
        /// <summary>
        /// Reference of the resource
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Resource { get; set; }
    
    }
    
    public partial interface ITestScriptVariableComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Descriptive name for this variable
        /// </summary>
        Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
        /// <summary>
        /// Descriptive name for this variable
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Name { get; set; }
    
        /// <summary>
        /// HTTP header field name for source
        /// </summary>
        Hl7.Fhir.Model.FhirString HeaderFieldElement { get; set; }
        
        /// <summary>
        /// HTTP header field name for source
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string HeaderField { get; set; }
    
        /// <summary>
        /// XPath or JSONPath against the fixture body
        /// </summary>
        Hl7.Fhir.Model.FhirString PathElement { get; set; }
        
        /// <summary>
        /// XPath or JSONPath against the fixture body
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Path { get; set; }
    
        /// <summary>
        /// Fixture Id of source expression or headerField within this variable
        /// </summary>
        Hl7.Fhir.Model.Id SourceIdElement { get; set; }
        
        /// <summary>
        /// Fixture Id of source expression or headerField within this variable
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string SourceId { get; set; }
    
    }
    
    public partial interface ITestScriptSetupComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// A setup operation or assert to perform
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ITestScriptSetupActionComponent> Action { get; }
    
    }
    
    public partial interface ITestScriptSetupActionComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// The setup operation to perform
        /// </summary>
        Hl7.Fhir.Model.ITestScriptOperationComponent Operation { get; }
    
        /// <summary>
        /// The assertion to perform
        /// </summary>
        Hl7.Fhir.Model.ITestScriptAssertComponent Assert { get; }
    
    }
    
    public partial interface ITestScriptOperationComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// The setup operation type that will be executed
        /// </summary>
        Hl7.Fhir.Model.Coding Type { get; set; }
    
        /// <summary>
        /// Tracking/logging operation label
        /// </summary>
        Hl7.Fhir.Model.FhirString LabelElement { get; set; }
        
        /// <summary>
        /// Tracking/logging operation label
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Label { get; set; }
    
        /// <summary>
        /// Tracking/reporting operation description
        /// </summary>
        Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
        /// <summary>
        /// Tracking/reporting operation description
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
        /// <summary>
        /// Which server to perform the operation on
        /// </summary>
        Hl7.Fhir.Model.Integer DestinationElement { get; set; }
        
        /// <summary>
        /// Which server to perform the operation on
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? Destination { get; set; }
    
        /// <summary>
        /// Whether or not to send the request url in encoded format
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean EncodeRequestUrlElement { get; set; }
        
        /// <summary>
        /// Whether or not to send the request url in encoded format
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? EncodeRequestUrl { get; set; }
    
        /// <summary>
        /// Explicitly defined path parameters
        /// </summary>
        Hl7.Fhir.Model.FhirString ParamsElement { get; set; }
        
        /// <summary>
        /// Explicitly defined path parameters
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Params { get; set; }
    
        /// <summary>
        /// Each operation can have one ore more header elements
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ITestScriptRequestHeaderComponent> RequestHeader { get; }
    
        /// <summary>
        /// Fixture Id of mapped response
        /// </summary>
        Hl7.Fhir.Model.Id ResponseIdElement { get; set; }
        
        /// <summary>
        /// Fixture Id of mapped response
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string ResponseId { get; set; }
    
        /// <summary>
        /// Fixture Id of body for PUT and POST requests
        /// </summary>
        Hl7.Fhir.Model.Id SourceIdElement { get; set; }
        
        /// <summary>
        /// Fixture Id of body for PUT and POST requests
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string SourceId { get; set; }
    
        /// <summary>
        /// Id of fixture used for extracting the [id],  [type], and [vid] for GET requests
        /// </summary>
        Hl7.Fhir.Model.Id TargetIdElement { get; set; }
        
        /// <summary>
        /// Id of fixture used for extracting the [id],  [type], and [vid] for GET requests
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string TargetId { get; set; }
    
        /// <summary>
        /// Request URL
        /// </summary>
        Hl7.Fhir.Model.FhirString UrlElement { get; set; }
        
        /// <summary>
        /// Request URL
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Url { get; set; }
    
    }
    
    public partial interface ITestScriptRequestHeaderComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// HTTP header field name
        /// </summary>
        Hl7.Fhir.Model.FhirString FieldElement { get; set; }
        
        /// <summary>
        /// HTTP header field name
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Field { get; set; }
    
        /// <summary>
        /// HTTP headerfield value
        /// </summary>
        Hl7.Fhir.Model.FhirString ValueElement { get; set; }
        
        /// <summary>
        /// HTTP headerfield value
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Value { get; set; }
    
    }
    
    public partial interface ITestScriptAssertComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Tracking/logging assertion label
        /// </summary>
        Hl7.Fhir.Model.FhirString LabelElement { get; set; }
        
        /// <summary>
        /// Tracking/logging assertion label
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Label { get; set; }
    
        /// <summary>
        /// Tracking/reporting assertion description
        /// </summary>
        Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
        /// <summary>
        /// Tracking/reporting assertion description
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
        /// <summary>
        /// response | request
        /// </summary>
        Code<Hl7.Fhir.Model.AssertionDirectionType> DirectionElement { get; set; }
        
        /// <summary>
        /// response | request
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.AssertionDirectionType? Direction { get; set; }
    
        /// <summary>
        /// Id of fixture used to compare the "sourceId/path" evaluations to
        /// </summary>
        Hl7.Fhir.Model.FhirString CompareToSourceIdElement { get; set; }
        
        /// <summary>
        /// Id of fixture used to compare the "sourceId/path" evaluations to
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string CompareToSourceId { get; set; }
    
        /// <summary>
        /// XPath or JSONPath expression against fixture used to compare the "sourceId/path" evaluations to
        /// </summary>
        Hl7.Fhir.Model.FhirString CompareToSourcePathElement { get; set; }
        
        /// <summary>
        /// XPath or JSONPath expression against fixture used to compare the "sourceId/path" evaluations to
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string CompareToSourcePath { get; set; }
    
        /// <summary>
        /// HTTP header field name
        /// </summary>
        Hl7.Fhir.Model.FhirString HeaderFieldElement { get; set; }
        
        /// <summary>
        /// HTTP header field name
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string HeaderField { get; set; }
    
        /// <summary>
        /// Fixture Id of minimum content resource
        /// </summary>
        Hl7.Fhir.Model.FhirString MinimumIdElement { get; set; }
        
        /// <summary>
        /// Fixture Id of minimum content resource
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string MinimumId { get; set; }
    
        /// <summary>
        /// Perform validation on navigation links?
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean NavigationLinksElement { get; set; }
        
        /// <summary>
        /// Perform validation on navigation links?
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? NavigationLinks { get; set; }
    
        /// <summary>
        /// XPath or JSONPath expression
        /// </summary>
        Hl7.Fhir.Model.FhirString PathElement { get; set; }
        
        /// <summary>
        /// XPath or JSONPath expression
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Path { get; set; }
    
        /// <summary>
        /// okay | created | noContent | notModified | bad | forbidden | notFound | methodNotAllowed | conflict | gone | preconditionFailed | unprocessable
        /// </summary>
        Code<Hl7.Fhir.Model.AssertionResponseTypes> ResponseElement { get; set; }
        
        /// <summary>
        /// okay | created | noContent | notModified | bad | forbidden | notFound | methodNotAllowed | conflict | gone | preconditionFailed | unprocessable
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.AssertionResponseTypes? Response { get; set; }
    
        /// <summary>
        /// HTTP response code to test
        /// </summary>
        Hl7.Fhir.Model.FhirString ResponseCodeElement { get; set; }
        
        /// <summary>
        /// HTTP response code to test
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string ResponseCode { get; set; }
    
        /// <summary>
        /// Fixture Id of source expression or headerField
        /// </summary>
        Hl7.Fhir.Model.Id SourceIdElement { get; set; }
        
        /// <summary>
        /// Fixture Id of source expression or headerField
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string SourceId { get; set; }
    
        /// <summary>
        /// Profile Id of validation profile reference
        /// </summary>
        Hl7.Fhir.Model.Id ValidateProfileIdElement { get; set; }
        
        /// <summary>
        /// Profile Id of validation profile reference
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string ValidateProfileId { get; set; }
    
        /// <summary>
        /// The value to compare to
        /// </summary>
        Hl7.Fhir.Model.FhirString ValueElement { get; set; }
        
        /// <summary>
        /// The value to compare to
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Value { get; set; }
    
        /// <summary>
        /// Will this assert produce a warning only on error?
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean WarningOnlyElement { get; set; }
        
        /// <summary>
        /// Will this assert produce a warning only on error?
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? WarningOnly { get; set; }
    
    }
    
    public partial interface ITestScriptTestComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Tracking/logging name of this test
        /// </summary>
        Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
        /// <summary>
        /// Tracking/logging name of this test
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Name { get; set; }
    
        /// <summary>
        /// Tracking/reporting short description of the test
        /// </summary>
        Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
        /// <summary>
        /// Tracking/reporting short description of the test
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
        /// <summary>
        /// A test operation or assert to perform
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ITestScriptTestActionComponent> Action { get; }
    
    }
    
    public partial interface ITestScriptTestActionComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// The setup operation to perform
        /// </summary>
        Hl7.Fhir.Model.ITestScriptOperationComponent Operation { get; }
    
        /// <summary>
        /// The setup assertion to perform
        /// </summary>
        Hl7.Fhir.Model.ITestScriptAssertComponent Assert { get; }
    
    }
    
    public partial interface ITestScriptTeardownComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
    }

}
