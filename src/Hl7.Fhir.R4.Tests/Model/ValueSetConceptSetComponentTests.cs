/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Tests.Model
{
    /// <summary>
    /// Tests for Issue #3494 - Wrong TypeName for ValueSet.ConceptSetComponent
    /// </summary>
    [TestClass]
    public class ValueSetConceptSetComponentTests
    {
        [TestMethod]
        public void ConceptSetComponent_InInclude_ShouldHaveIncludeTypeName()
        {
            // Arrange
            var valueSet = new ValueSet();
            valueSet.Compose = new ValueSet.ComposeComponent();
            
            // Act
            var includeComponent = new ValueSet.ConceptSetComponent
            {
                System = "http://example.com"
            };
            valueSet.Compose.Include.Add(includeComponent);
            
            // Access the Include property to trigger TypeName setting
            var _ = valueSet.Compose.Include;
            
            // Assert
            includeComponent.TypeName.Should().Be("ValueSet.compose.include",
                "ConceptSetComponent used in Include should have TypeName 'ValueSet.compose.include'");
        }

        [TestMethod]
        public void ConceptSetComponent_InExclude_ShouldHaveExcludeTypeName()
        {
            // Arrange
            var valueSet = new ValueSet();
            valueSet.Compose = new ValueSet.ComposeComponent();
            
            // Act
            var excludeComponent = new ValueSet.ConceptSetComponent
            {
                System = "http://example.com"
            };
            valueSet.Compose.Exclude.Add(excludeComponent);
            
            // Access the Exclude property to trigger TypeName setting
            var _ = valueSet.Compose.Exclude;
            
            // Assert
            excludeComponent.TypeName.Should().Be("ValueSet.compose.exclude",
                "ConceptSetComponent used in Exclude should have TypeName 'ValueSet.compose.exclude'");
        }

        [TestMethod]
        public void ConceptSetComponent_DefaultTypeName_ShouldBeInclude()
        {
            // Arrange & Act
            var component = new ValueSet.ConceptSetComponent
            {
                System = "http://example.com"
            };
            
            // Assert - default should be include
            component.TypeName.Should().Be("ValueSet.compose.include",
                "Default TypeName should be 'ValueSet.compose.include'");
        }

        [TestMethod]
        public void ConceptSetComponent_MultipleIncludes_ShouldAllHaveIncludeTypeName()
        {
            // Arrange
            var valueSet = new ValueSet();
            valueSet.Compose = new ValueSet.ComposeComponent();
            
            // Act
            var component1 = new ValueSet.ConceptSetComponent { System = "http://example1.com" };
            var component2 = new ValueSet.ConceptSetComponent { System = "http://example2.com" };
            var component3 = new ValueSet.ConceptSetComponent { System = "http://example3.com" };
            
            valueSet.Compose.Include.Add(component1);
            valueSet.Compose.Include.Add(component2);
            valueSet.Compose.Include.Add(component3);
            
            // Access the Include property to trigger TypeName setting
            var _ = valueSet.Compose.Include;
            
            // Assert
            component1.TypeName.Should().Be("ValueSet.compose.include");
            component2.TypeName.Should().Be("ValueSet.compose.include");
            component3.TypeName.Should().Be("ValueSet.compose.include");
        }

        [TestMethod]
        public void ConceptSetComponent_MultipleExcludes_ShouldAllHaveExcludeTypeName()
        {
            // Arrange
            var valueSet = new ValueSet();
            valueSet.Compose = new ValueSet.ComposeComponent();
            
            // Act
            var component1 = new ValueSet.ConceptSetComponent { System = "http://example1.com" };
            var component2 = new ValueSet.ConceptSetComponent { System = "http://example2.com" };
            
            valueSet.Compose.Exclude.Add(component1);
            valueSet.Compose.Exclude.Add(component2);
            
            // Access the Exclude property to trigger TypeName setting
            var _ = valueSet.Compose.Exclude;
            
            // Assert
            component1.TypeName.Should().Be("ValueSet.compose.exclude");
            component2.TypeName.Should().Be("ValueSet.compose.exclude");
        }

        [TestMethod]
        public void ConceptSetComponent_SetIncludeThenExclude_ShouldUpdateTypeName()
        {
            // Arrange
            var valueSet = new ValueSet();
            valueSet.Compose = new ValueSet.ComposeComponent();
            
            // Act - add to include first
            var component = new ValueSet.ConceptSetComponent { System = "http://example.com" };
            valueSet.Compose.Include.Add(component);
            var _ = valueSet.Compose.Include; // Trigger TypeName setting
            component.TypeName.Should().Be("ValueSet.compose.include");
            
            // Act - remove from include and add to exclude
            valueSet.Compose.Include.Clear();
            valueSet.Compose.Exclude.Add(component);
            var __ = valueSet.Compose.Exclude; // Trigger TypeName setting
            
            // Assert - TypeName should now be exclude
            component.TypeName.Should().Be("ValueSet.compose.exclude",
                "When moved from Include to Exclude, TypeName should update to 'ValueSet.compose.exclude'");
        }

        [TestMethod]
        public void ConceptSetComponent_SetExcludeThenInclude_ShouldUpdateTypeName()
        {
            // Arrange
            var valueSet = new ValueSet();
            valueSet.Compose = new ValueSet.ComposeComponent();
            
            // Act - add to exclude first
            var component = new ValueSet.ConceptSetComponent { System = "http://example.com" };
            valueSet.Compose.Exclude.Add(component);
            var _ = valueSet.Compose.Exclude; // Trigger TypeName setting
            component.TypeName.Should().Be("ValueSet.compose.exclude");
            
            // Act - remove from exclude and add to include
            valueSet.Compose.Exclude.Clear();
            valueSet.Compose.Include.Add(component);
            var __ = valueSet.Compose.Include; // Trigger TypeName setting
            
            // Assert - TypeName should now be include
            component.TypeName.Should().Be("ValueSet.compose.include",
                "When moved from Exclude to Include, TypeName should update to 'ValueSet.compose.include'");
        }
    }
}
