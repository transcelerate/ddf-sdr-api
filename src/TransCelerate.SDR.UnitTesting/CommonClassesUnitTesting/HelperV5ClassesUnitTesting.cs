using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TransCelerate.SDR.Core.DTO.StudyV5;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.StudyV5;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV5;
using TransCelerate.SDR.DataAccess.Filters;
using TransCelerate.SDR.RuleEngineV5;

namespace TransCelerate.SDR.UnitTesting
{
    [TestFixture]
    public class HelperV5ClassesUnitTesting
    {
        [TestFixture]
        public class ReferenceIntegrityValidationForDocument_Method : HelperV5ClassesUnitTesting
        {
            #region Positive Scenarios: No Errors Expected

            [Test]
            public void Should_Return_NoErrors_When_All_ChildIds_Are_Valid()
            {
                // Arrange
                var allDocumentIds = new List<string> { "doc1", "doc2", "doc3" };
                var documentToTest = new StudyDefinitionDocumentDto
                {
                    Id = "doc1",
                    ChildIds = new List<string> { "doc2", "doc3" },
                    Versions = null
                };

                // Act
                var errors = HelperV5.ReferenceIntegrityValidationForDocument(documentToTest, 0, allDocumentIds);

                // Assert
                Assert.That(errors, Is.Empty, "Expected no errors for valid child IDs.");
            }

            [Test]
            public void Should_Return_NoErrors_When_ChildIds_Property_Is_Null()
            {
                // Arrange
                var allDocumentIds = new List<string> { "doc1", "doc2" };
                var documentToTest = new StudyDefinitionDocumentDto
                {
                    Id = "doc1",
                    ChildIds = null, // ChildIds property is null
                    Versions = null
                };

                // Act
                var errors = HelperV5.ReferenceIntegrityValidationForDocument(documentToTest, 0, allDocumentIds);

                // Assert
                Assert.That(errors, Is.Empty, "Expected no errors when ChildIds list is null.");
            }

            [Test]
            public void Should_Return_NoErrors_When_ChildIds_List_Is_Empty()
            {
                // Arrange
                var allDocumentIds = new List<string> { "doc1", "doc2" };
                var documentToTest = new StudyDefinitionDocumentDto
                {
                    Id = "doc1",
                    ChildIds = new List<string>(), // Test case: ChildIds list is empty
                    Versions = null
                };

                // Act
                var errors = HelperV5.ReferenceIntegrityValidationForDocument(documentToTest, 0, allDocumentIds);

                // Assert
                Assert.That(errors, Is.Empty, "Expected no errors when ChildIds list is empty.");
            }

            [TestCase(null)]
            [TestCase("")]
            [TestCase("   ")]
            public void Should_Ignore_NullOrWhitespace_ChildId_Entries(string invalidEntry)
            {
                // Arrange
                var allDocumentIds = new List<string> { "doc1", "doc2" };
                var documentToTest = new StudyDefinitionDocumentDto
                {
                    Id = "doc1",
                    ChildIds = new List<string> { "doc2", invalidEntry },
                    Versions = null
                };

                // Act
                var errors = HelperV5.ReferenceIntegrityValidationForDocument(documentToTest, 0, allDocumentIds);

                // Assert
                Assert.That(errors, Is.Empty, "Should ignore null or whitespace child IDs and produce no errors.");
            }

            #endregion

            #region Negative Scenarios: Errors Expected

            [Test]
            public void Should_Return_Error_When_A_ChildId_Is_Not_In_The_Valid_List()
            {
                // Arrange
                var allDocumentIds = new List<string> { "doc1", "doc2" };
                var documentIndex = 1;
                var documentToTest = new StudyDefinitionDocumentDto
                {
                    Id = "doc1",
                    ChildIds = new List<string> { "invalid_id" },
                    Versions = null
                };
                var expectedError = $"Study.DocumentedBy[{documentIndex}].ChildIds[0]";

                // Act
                var errors = HelperV5.ReferenceIntegrityValidationForDocument(documentToTest, documentIndex, allDocumentIds);

                // Assert
                Assert.That(errors, Has.Count.EqualTo(1), "Expected exactly one validation error.");
                Assert.That(errors.First(), Is.EqualTo(expectedError));
            }

            [Test]
            public void Should_Return_Error_Only_For_The_Invalid_ChildId_In_A_Mixed_List()
            {
                // Arrange
                var allDocumentIds = new List<string> { "doc1", "doc2" };
                var documentToTest = new StudyDefinitionDocumentDto
                {
                    Id = "doc1",
                    ChildIds = new List<string> { "doc2", "invalid_id_number_2" },
                    Versions = null
                };
                var expectedError = $"Study.DocumentedBy[0].ChildIds[1]";

                // Act
                var errors = HelperV5.ReferenceIntegrityValidationForDocument(documentToTest, 0, allDocumentIds);

                // Assert
                Assert.That(errors, Has.Count.EqualTo(1));
                Assert.That(errors.First(), Is.EqualTo(expectedError));
            }

            [Test]
            public void Should_Return_Error_When_A_Document_References_Itself_As_A_Child()
            {
                // Arrange
                var allDocumentIds = new List<string> { "doc1", "doc2" };
                var documentToTest = new StudyDefinitionDocumentDto
                {
                    Id = "doc1",
                    ChildIds = new List<string> { "doc1" }, // Document references itself
                    Versions = null
                };
                var expectedError = $"Study.DocumentedBy[0].ChildIds[0]";

                // Act
                var errors = HelperV5.ReferenceIntegrityValidationForDocument(documentToTest, 0, allDocumentIds);

                // Assert
                Assert.That(errors, Has.Count.EqualTo(1), "Expected an error for a self-referencing child ID.");
                Assert.That(errors.First(), Is.EqualTo(expectedError));
            }

            #endregion
        }
    }
}