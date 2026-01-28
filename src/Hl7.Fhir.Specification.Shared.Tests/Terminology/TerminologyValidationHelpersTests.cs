/*
 * Copyright (c) 2025, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable

using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Specification.Terminology;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Hl7.Fhir.Specification.Tests;

public class TerminologyValidationHelpersTests
{
    [Fact]
    public void ValidateExactlyOneCodeParameter_WithSingleCode_ReturnsTrue()
    {
        // Arrange
        var code = new Code("test");
        var coding = (Coding?)null;
        var codeableConcept = (CodeableConcept?)null;

        // Act
        var exception = Record.Exception(() => 
            TerminologyValidationHelpers.ValidateExactlyOneCodeParameter(code, coding, codeableConcept));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ValidateExactlyOneCodeParameter_WithMultipleParameters_ThrowsException()
    {
        // Arrange
        var code = new Code("test");
        var coding = new Coding { System = "http://test.org", Code = "test" };
        var codeableConcept = (CodeableConcept?)null;

        // Act & Assert
        var exception = Assert.Throws<FhirOperationException>(() => 
            TerminologyValidationHelpers.ValidateExactlyOneCodeParameter(code, coding, codeableConcept));

        Assert.Equal("One (and only one) of 'code', 'coding' or 'codeableConcept' must be provided.", exception.Message);
    }

    [Fact]
    public void ValidateSystemForCode_WithCodeAndSystem_DoesNotThrow()
    {
        // Arrange
        var code = new Code("test");
        var system = new FhirUri("http://test.org");
        var inferSystem = (FhirBoolean?)null;

        // Act
        var exception = Record.Exception(() => 
            TerminologyValidationHelpers.ValidateSystemForCode(code, system, inferSystem));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ValidateSystemForCode_WithCodeWithoutSystem_ThrowsException()
    {
        // Arrange
        var code = new Code("test");
        var system = (FhirUri?)null;
        var inferSystem = (FhirBoolean?)null;

        // Act & Assert
        var exception = Assert.Throws<FhirOperationException>(() => 
            TerminologyValidationHelpers.ValidateSystemForCode(code, system, inferSystem));

        Assert.Equal("If 'code' is provided, either 'system' must be provided, or 'inferSystem' must be true", exception.Message);
    }

    [Fact]
    public void ValidateCodedParameter_WithValidCoding_DoesNotThrow()
    {
        // Arrange
        var coding = new Coding { System = "http://test.org", Code = "test" };

        // Act
        var exception = Record.Exception(() => TerminologyValidationHelpers.ValidateCoding(coding));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ValidateCodedParameter_WithNullCoding_DoesNotThrow()
    {
        // Arrange
        var coding = (Coding?)null;

        // Act
        var exception = Record.Exception(() => TerminologyValidationHelpers.ValidateCoding(coding));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ValidateCodedParameter_WithEmptyCode_ThrowsException()
    {
        // Arrange
        var coding = new Coding { System = "http://test.org", Code = "" };

        // Act & Assert
        var exception = Assert.Throws<FhirOperationException>(() => TerminologyValidationHelpers.ValidateCoding(coding));

        Assert.Equal("Must have a coding with both code and system to be validated.", exception.Message);
    }

    [Fact]
    public void ValidateValueSetReference_WithUrlOnly_DoesNotThrow()
    {
        // Arrange
        var url = new FhirUri("http://test.org");
        var valueSet = (Resource?)null;
        var context = (FhirUri?)null;

        // Act
        var exception = Record.Exception(() => 
            TerminologyValidationHelpers.ValidateValueSetReference(url, valueSet, context));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ValidateValueSetReference_WithValueSetOnly_DoesNotThrow()
    {
        // Arrange
        var url = (FhirUri?)null;
        var valueSet = new ValueSet();
        var context = (FhirUri?)null;

        // Act
        var exception = Record.Exception(() => 
            TerminologyValidationHelpers.ValidateValueSetReference(url, valueSet, context));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ValidateValueSetReference_WithContextOnly_DoesNotThrow()
    {
        // Arrange
        var url = (FhirUri?)null;
        var valueSet = (Resource?)null;
        var context = new FhirUri("http://test.org");

        // Act
        var exception = Record.Exception(() => 
            TerminologyValidationHelpers.ValidateValueSetReference(url, valueSet, context));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ValidateValueSetReference_WithNoReferences_ThrowsException()
    {
        // Arrange
        var url = (FhirUri?)null;
        var valueSet = (Resource?)null;
        var context = (FhirUri?)null;

        // Act & Assert
        var exception = Assert.Throws<FhirOperationException>(() => 
            TerminologyValidationHelpers.ValidateValueSetReference(url, valueSet, context));

        Assert.Equal("At least one of 'url', 'context' or a 'valueSet' must be provided.", exception.Message);
    }

    [Fact]
    public void ValidateExpandValueSetReference_WithUrl_DoesNotThrow()
    {
        // Arrange
        var url = new FhirUri("http://test.org");
        var valueSet = (Resource?)null;
        var context = (FhirUri?)null;

        // Act
        var exception = Record.Exception(() => 
            TerminologyValidationHelpers.ValidateExpandParameters(url, valueSet, context, null, null, null));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ValidateExpandValueSetReference_WithNoReferences_ThrowsException()
    {
        // Arrange
        var url = (FhirUri?)null;
        var valueSet = (Resource?)null;
        var context = (FhirUri?)null;

        // Act & Assert
        var exception = Assert.Throws<FhirOperationException>(() => 
            TerminologyValidationHelpers.ValidateExpandParameters(url, valueSet, context, null, null, null));

        Assert.Equal("At least one of 'url', 'context' or a 'valueSet' must be provided.", exception.Message);
    }

    [Fact]
    public void ValidateExpandParameters_WithNegativeOffset_ThrowsException()
    {
        // Arrange
        var offset = new Integer(-1);

        // Act & Assert
        var exception = Assert.Throws<FhirOperationException>(() => 
            TerminologyValidationHelpers.ValidateExpandParameters(
                new FhirUri("http://test.org"), null, null, null, offset, null));

        Assert.Equal("'offset' must be non-negative.", exception.Message);
    }

    [Fact]
    public void ValidateExpandParameters_WithNegativeCount_ThrowsException()
    {
        // Arrange
        var count = new Integer(-1);

        // Act & Assert
        var exception = Assert.Throws<FhirOperationException>(() => 
            TerminologyValidationHelpers.ValidateExpandParameters(
                new FhirUri("http://test.org"), null, null, null, null, count));

        Assert.Equal("'count' must be non-negative.", exception.Message);
    }

    [Fact]
    public void ValidateNoDuplicateParameters_WithoutDuplicates_DoesNotThrow()
    {
        // Arrange
        var parameters = new Parameters();

        // Act
        var exception = Record.Exception(() => parameters.NoDuplicates());

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ValidateNoDuplicateParameters_WithDuplicates_ThrowsException()
    {
        // Arrange
        var parameters = new Parameters();
        parameters.Add("param1", new Code("value1"));
        parameters.Add("param1", new Code("value2")); // duplicate

        // Act & Assert
        var exception = Assert.Throws<FhirOperationException>(() => parameters.NoDuplicates());

        Assert.Equal("List of input parameters contains the following duplicates: param1", exception.Message);
    }

    [Fact]
    public void ValidateSubsumesParameters_WithCodesOnly_ValidatesCorrectly()
    {
        // Arrange
        var codeA = new Code("codeA");
        var codeB = new Code("codeB");
        var system = new FhirUri("http://test.org");

        // Act
        var exception = Record.Exception(() => 
            TerminologyValidationHelpers.ValidateSubsumesParameters(codeA, codeB, null, null, system, null));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ValidateSubsumesParameters_WithCodingsOnly_ValidatesCorrectly()
    {
        // Arrange
        var codingA = new Coding { System = "http://test.org", Code = "codeA" };
        var codingB = new Coding { System = "http://test.org", Code = "codeB" };

        // Act
        var exception = Record.Exception(() => 
            TerminologyValidationHelpers.ValidateSubsumesParameters(null, null, codingA, codingB, new FhirUri("http://test.org"), null));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ValidateSubsumesParameters_WithCodeAAndCodingB_ThrowsException()
    {
        // Arrange
        var codeA = new Code("codeA");
        var codingA = new Coding { System = "http://test.org", Code = "codeA" };
        var system = new FhirUri("http://test.org");

        // Act & Assert
        var exception = Assert.Throws<FhirOperationException>(() => 
            TerminologyValidationHelpers.ValidateSubsumesParameters(codeA, null, codingA, null, system, null));

        Assert.Equal("One (and only one) of 'codeA' or 'codingA' must be provided.", exception.Message);
    }

    [Fact]
    public void ValidateSubsumesParameters_WithCodesWithoutSystem_ThrowsException()
    {
        // Arrange
        var codeA = new Code("codeA");
        var codeB = new Code("codeB");
        var system = (FhirUri?)null;

        // Act & Assert
        var exception = Assert.Throws<FhirOperationException>(() => 
            TerminologyValidationHelpers.ValidateSubsumesParameters(codeA, codeB, null, null, system, null));

        Assert.Equal("If 'codeA' or 'codeB' is provided, 'system' must be provided.", exception.Message);
    }

    [Fact]
    public void ValidateLookupParameters_WithCodeOnly_ValidatesCorrectly()
    {
        // Arrange
        var code = new Code("test");
        var system = new FhirUri("http://test.org");
        var coding = (Coding?)null;

        // Act
        var exception = Record.Exception(() => 
            TerminologyValidationHelpers.ValidateLookupParameters(code, coding, system));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ValidateLookupParameters_WithCodingOnly_ValidatesCorrectly()
    {
        // Arrange
        var coding = new Coding { System = "http://test.org", Code = "test" };
        var code = (Code?)null;
        var system = (FhirUri?)null;

        // Act
        var exception = Record.Exception(() => 
            TerminologyValidationHelpers.ValidateLookupParameters(code, coding, system));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ValidateLookupParameters_WithInvalidCoding_ThrowsException()
    {
        // Arrange
        var coding = new Coding { System = "", Code = "test" }; // missing system
        var code = (Code?)null;
        var system = (FhirUri?)null;

        // Act & Assert
        var exception = Assert.Throws<FhirOperationException>(() => 
            TerminologyValidationHelpers.ValidateLookupParameters(code, coding, system));

        Assert.Equal("Must have a coding with both code and system to be validated.", exception.Message);
    }

    [Fact]
    public void ValidateTranslateParameters_WithValidParameters_DoesNotThrow()
    {
        // Arrange
        var code = new Code("test");
        var system = new FhirUri("http://test.org");
        var url = new FhirUri("http://test.org");
        var conceptMap = (Resource?)null;
        var coding = (Coding?)null;
        var codeableConcept = (CodeableConcept?)null;

        // Act
        var exception = Record.Exception(() => 
            TerminologyValidationHelpers.ValidateTranslateParameters(code, coding, codeableConcept, url, conceptMap, system));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ValidateTranslateParameters_WithNoConceptMapOrUrl_ThrowsException()
    {
        // Arrange
        var code = new Code("test");
        var system = new FhirUri("http://test.org");
        var url = (FhirUri?)null;
        var conceptMap = (Resource?)null;
        var coding = (Coding?)null;
        var codeableConcept = (CodeableConcept?)null;

        // Act & Assert
        var exception = Assert.Throws<FhirOperationException>(() => 
            TerminologyValidationHelpers.ValidateTranslateParameters(code, coding, codeableConcept, url, conceptMap, system));

        Assert.Equal("One of 'url' or 'conceptMap' must be provided.", exception.Message);
    }

    [Fact]
    public void ValidateClosureParameters_WithNameOnly_DoesNotThrow()
    {
        // Arrange
        var name = new FhirString("test");
        var concepts = (IEnumerable<Coding>?)null;
        var version = (FhirString?)null;

        // Act
        var exception = Record.Exception(() => 
            TerminologyValidationHelpers.ValidateClosureParameters(name, concepts, version));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ValidateClosureParameters_WithConcepts_DoesNotThrow()
    {
        // Arrange
        var name = new FhirString("test");
        var concepts = new List<Coding>
        {
            new Coding { System = "http://test.org", Code = "test1" },
            new Coding { System = "http://test.org", Code = "test2" }
        };
        var version = (FhirString?)null;

        // Act
        var exception = Record.Exception(() => 
            TerminologyValidationHelpers.ValidateClosureParameters(name, concepts, version));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ValidateClosureParameters_WithInvalidConcept_ThrowsException()
    {
        // Arrange
        var name = new FhirString("test");
        var concepts = new List<Coding>
        {
            new Coding { System = "", Code = "test" } // missing system
        };
        var version = (FhirString?)null;

        // Act & Assert
        var exception = Assert.Throws<FhirOperationException>(() => 
            TerminologyValidationHelpers.ValidateClosureParameters(name, concepts, version));

        Assert.Equal("Must have a concept[0] with both code and system to be validated.", exception.Message);
    }

    [Fact]
    public void ValidateCodeSystemValidateCodeParameters_WithValidParameters_DoesNotThrow()
    {
        // Arrange
        var code = new Code("test");
        var system = new FhirUri("http://test.org");
        var coding = (Coding?)null;
        var codeableConcept = (CodeableConcept?)null;

        // Act
        var exception = Record.Exception(() => 
            TerminologyValidationHelpers.ValidateCodeSystemValidateCodeParameters(code, coding, codeableConcept, system));

        // Assert
        Assert.Null(exception);
    }
}