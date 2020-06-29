/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

using System;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Patch.Adapters;
using Hl7.Fhir.Patch.Internal;
using Hl7.Fhir.Specification;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Patch.Tests
{
    [TestClass]
    public class AdapterFactoryTests
    {
        [TestMethod]
        public void GetCollectionAdapterForCollectionTargets ()
        {
            // Arrange
            AdapterFactory factory = new AdapterFactory();

            //Act:
            IAdapter adapter = factory.Create(ElementNode.FromElement(new Patient().ToTypedElement()).Children(), new PocoStructureDefinitionSummaryProvider());

            // Assert
            Assert.IsInstanceOfType(adapter, typeof(CollectionAdapter));
        }

        [TestMethod]
        public void GetElementNodeAdapterForUnsupportedObjects_ThrowsException ()
        {
            // Arrange
            AdapterFactory factory = new AdapterFactory();

            //Act & Assert:
            Assert.ThrowsException<NotSupportedException>(() => factory.Create(ElementNode.ForPrimitive(3), new PocoStructureDefinitionSummaryProvider()));
        }

        [TestMethod]
        public void GetElementNodeAdapterForElementNodeObjects ()
        {
            // Arrange
            AdapterFactory factory = new AdapterFactory();

            //Act:
            IAdapter adapter = factory.Create(ElementNode.FromElement(new Patient().ToTypedElement()), new PocoStructureDefinitionSummaryProvider());

            // Assert
            Assert.IsInstanceOfType(adapter, typeof(ElementNodeAdapter));
        }
    }
}
