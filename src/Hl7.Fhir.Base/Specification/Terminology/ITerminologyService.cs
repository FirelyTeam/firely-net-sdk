/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Terminology
{
    public interface ICodeValidationTerminologyService
    {
        /// <summary>
        /// Validate that a coded value is in the set of codes allowed by a value set.
        /// </summary>
        /// <param name="parameters">Input parameters for the operation</param>
        /// <param name="id">Id of a specific ValueSet which is used to validate against</param>
        /// <param name="useGet"> Use the GET instead of POST Http method</param>
        /// <returns>Output parameters containing the result of the operation</returns>
        /// <exception cref="FhirOperationException">Thrown when the terminology service encounters an error</exception>
        /// <remarks>See  for more information</remarks>
        /// <remarks>This function corresponds to the <seealso href="http://hl7.org/fhir/valueset-operation-validate-code.html">$validate-codes</seealso> operation.</remarks>
        Task<Parameters> ValueSetValidateCode(Parameters parameters, string id = null, bool useGet = false);

        /// <summary>
        /// Test the subsumption relationship between code/Coding A and code/Coding B given the semantics of subsumption in the underlying code system
        /// </summary>
        /// <param name="parameters">Input parameters for the operation</param>
        /// <param name="id">Id of the code system in which subsumption testing is to be performed.</param>
        /// <param name="useGet">Use the GET instead of POST Http method</param>
        /// <returns>Output parameters containing the subsumption relationship between code/Coding "A" and code/Coding "B".</returns>
        /// <exception cref="FhirOperationException">Thrown when the terminology service encounters an error</exception>
        /// <remarks>This function corresponds to the <seealso href="http://hl7.org/fhir/codesystem-operation-subsumes.html">$subsumes</seealso> operation.</remarks>
        Task<Parameters> Subsumes(Parameters parameters, string id = null, bool useGet = false);
    }

    /// <summary>
    /// Subset of the FHIR terminology service (http://hl7.org/fhir/terminology-service.html) that concerns lookup and validation of single codes.
    /// </summary>
    public interface ICodeSystemTerminologyService
    {
        /// <summary>
        /// Validate that a coded value is in the code system.
        /// </summary>
        /// <param name="parameters">Input parameters for the operation</param>
        /// <param name="id">Id of a specific CodeSystem which is used to validate against</param>
        /// <param name="useGet">Use the GET instead of POST Http method</param>
        /// <returns>Output parameters containing the result of the operation</returns>
        /// <exception cref="FhirOperationException">Thrown when the terminology service encounters an error</exception>
        /// <remarks>This function corresponds to the <seealso href="http://hl7.org/fhir/codesystem-operation-validate-code.html">$validate-code</seealso> operation.</remarks>
        Task<Parameters> CodeSystemValidateCode(Parameters parameters, string id = null, bool useGet = false);

        /// <summary>
        /// Given a code/system, or a Coding, get additional details about the concept, including definition, status, designations, and properties.
        /// </summary>
        /// <param name="parameters">Input parameters for the operation</param>
        /// <param name="useGet">Use the GET instead of POST Http method</param>
        /// <returns>Output parameters containing the result of the operation</returns>
        /// <exception cref="FhirOperationException">Thrown when the terminology service encounters an error</exception>
        /// <remarks>This function corresponds to the <seealso href="http://hl7.org/fhir/codesystem-operation-lookup.html">$lookup</seealso> operation.</remarks>
        Task<Parameters> Lookup(Parameters parameters, bool useGet = false);
    }


    /// <summary>
    /// Subset of the FHIR terminology service (http://hl7.org/fhir/terminology-service.html) that concerns mapping between valuesets.
    /// </summary>
    public interface IMappingTerminologyService
    {
        /// <summary>
        /// Translates a code from one value set to another, based on the existing value set and concept maps resources, and/or other additional knowledge of the processor.
        /// </summary>
        /// <param name="parameters">Input parameters for the operation</param>
        /// <param name="id">Id of the ConceptMap used for the translation</param>
        /// <param name="useGet">Use the GET instead of POST Http method</param>
        /// <returns>Output parameter containing the result of the translation</returns>
        /// <exception cref="FhirOperationException">Thrown when the terminology service encounters an error</exception>
        /// <remarks>This function corresponds to the <seealso href="http://hl7.org/fhir/conceptmap-operation-translate.html">$translate</seealso> operation.</remarks>
        Task<Parameters> Translate(Parameters parameters, string id = null, bool useGet = false);
    }

    /// <summary>
    /// Subset of the FHIR terminology service (http://hl7.org/fhir/terminology-service.html) that concerns closure maintenance.
    /// </summary>
    public interface ITerminologyServiceWithClosure
    {
        /// <summary>
        /// Provides support for ongoing maintenance of a client-side transitive closure table based on server-side terminological logic. 
        /// </summary>
        /// <param name="parameters">Input parameters for the operation</param>
        /// <param name="useGet">Use the GET instead of POST Http method</param>
        /// <returns>Output parameters containing a ConceptMap with a list of new entries (code / system --> code/system) that the client should add to its closure table.</returns>
        /// <exception cref="FhirOperationException">Thrown when the terminology service encounters an error</exception>
        /// <remarks>This function corresponds to the <seealso href="http://hl7.org/fhir/conceptmap-operation-closure.html">$closure</seealso> operation.</remarks>
        Task<Resource> Closure(Parameters parameters, bool useGet = false);
    }

    /// <summary>
    /// Subset of the FHIR terminology service (http://hl7.org/fhir/terminology-service.html) that concerns calculation of value set expansions.
    /// </summary>
    public interface IExpandingTerminologyService
    {

        /// <summary>
        /// The definition of a value set is used to create a simple collection of codes suitable for use for data entry or validation.
        /// </summary>
        /// <param name="parameters">Input parameters for the operation</param>
        /// <param name="id">Id of a specific ValueSet to expand</param>
        /// <param name="useGet">Use the GET instead of POST Http method</param>
        /// <returns>Output parameters containing the expanded ValueSet</returns>
        /// <exception cref="FhirOperationException">Thrown when the terminology service encounters an error</exception>
        /// <remarks>This function corresponds to the <seealso href="http://hl7.org/fhir/valueset-operation-expand.html">$expand</seealso> operation.</remarks>
        Task<Resource> Expand(Parameters parameters, string id = null, bool useGet = false);
    }

    public interface ITerminologyService : ICodeValidationTerminologyService, ICodeSystemTerminologyService, IMappingTerminologyService, ITerminologyServiceWithClosure, IExpandingTerminologyService
    {
    }
}
