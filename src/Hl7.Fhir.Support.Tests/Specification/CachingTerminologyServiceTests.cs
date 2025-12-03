using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Specification.Terminology;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Tests;

[TestClass]
public class CachingTerminologyServiceTests
{
    private ITerminologyService _mockService;
    private CachingTerminologyService _cachingService;
    private Parameters _testParameters;
    private Parameters _expectedResult;

    [TestInitialize]
    public void Setup()
    {
        _mockService = Substitute.For<ITerminologyService>();
        _cachingService = new CachingTerminologyService(_mockService);

        _testParameters = new Parameters();
        _testParameters.Parameter.Add(new Parameters.ParameterComponent
        {
            Name = "code",
            Value = new Code("test-code")
        });

        _expectedResult = new Parameters();
        _expectedResult.Parameter.Add(new Parameters.ParameterComponent
        {
            Name = "result",
            Value = new FhirBoolean(true)
        });
    }

    [TestMethod]
    public async Task ValueSetValidateCode_SecondCallWithSameParameters_ReturnsCachedResult()
    {
        // Arrange
        _mockService.ValueSetValidateCode(_testParameters, "id1", false)
            .Returns(Task.FromResult(_expectedResult));

        // Act - Call with different id and useGet values but same parameters
        await _cachingService.ValueSetValidateCode(_testParameters, "id1", false);
        var result = await _cachingService.ValueSetValidateCode(_testParameters, "id2", true);

        // Assert - Should return cached result and only call underlying service once
        Assert.AreEqual(_expectedResult, result);
        await _mockService.Received(1).ValueSetValidateCode(_testParameters, "id1", false);
        await _mockService.DidNotReceive().ValueSetValidateCode(_testParameters, "id2", true);
    }

    [TestMethod]
    public async Task Translate_SecondCallWithSameParameters_ReturnsCachedResult()
    {
        // Arrange
        _mockService.Translate(_testParameters, "original-id", false)
            .Returns(Task.FromResult(_expectedResult));

        // Act - Call with different id and useGet values but same parameters
        await _cachingService.Translate(_testParameters, "original-id", false);
        var result = await _cachingService.Translate(_testParameters, "different-id", true);

        // Assert - Should return cached result and only call underlying service once
        Assert.AreEqual(_expectedResult, result);
        await _mockService.Received(1).Translate(_testParameters, "original-id", false);
        await _mockService.DidNotReceive().Translate(_testParameters, "different-id", true);
    }

    [TestMethod]
    public async Task ParametersWithResource_DoesNotCache()
    {
        // Arrange
        var parametersWithResource = new Parameters();
        parametersWithResource.Parameter.Add(new Parameters.ParameterComponent
        {
            Name = "resource",
            Resource = new Bundle()
        });

        _mockService.ValueSetValidateCode(parametersWithResource, null, false)
            .Returns(Task.FromResult(_expectedResult));

        // Act
        await _cachingService.ValueSetValidateCode(parametersWithResource);
        await _cachingService.ValueSetValidateCode(parametersWithResource);

        // Assert - Should call underlying service twice since resource parameters aren't cached
        await _mockService.Received(2).ValueSetValidateCode(parametersWithResource, null, false);
    }

    [TestMethod]
    public async Task DifferentParameters_CallsServiceTwice()
    {
        // Arrange
        var parameters2 = new Parameters();
        parameters2.Parameter.Add(new Parameters.ParameterComponent
        {
            Name = "code",
            Value = new Code("different-code")
        });

        _mockService.ValueSetValidateCode(Arg.Any<Parameters>(), Arg.Any<string>(), Arg.Any<bool>())
            .Returns(Task.FromResult(_expectedResult));

        // Act
        await _cachingService.ValueSetValidateCode(_testParameters);
        await _cachingService.ValueSetValidateCode(parameters2);

        // Assert - Should call underlying service twice for different parameters
        await _mockService.Received(2).ValueSetValidateCode(Arg.Any<Parameters>(), Arg.Any<string>(), Arg.Any<bool>());
    }
    
    [TestMethod]
    public async Task Translate_WhenServiceThrowsException_DoesNotCacheException()
    {
        // Arrange
        var exception = new FhirOperationException("Service error", HttpStatusCode.InternalServerError);
        _mockService.Translate(_testParameters, Arg.Any<string>(), Arg.Any<bool>())
            .Returns(Task.FromException<Parameters>(exception));
    
        // Act & Assert - First call should throw
        var firstException = await Assert.ThrowsAsync<FhirOperationException>(
            () => _cachingService.Translate(_testParameters, "id1", false));
        Assert.AreEqual("Service error", firstException.Message);
    
        // Act & Assert - Second call should also throw (exception cached)
        var secondException = await Assert.ThrowsAsync<FhirOperationException>(
            () => _cachingService.Translate(_testParameters, "id2", true));
        Assert.AreEqual("Service error", secondException.Message);
    
        // Assert - Service should be called once since exceptions are cached
        await _mockService.Received(1).Translate(_testParameters, Arg.Any<string>(), Arg.Any<bool>());
    }
    
}
