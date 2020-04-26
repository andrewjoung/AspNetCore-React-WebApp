using FluentAssertions.Execution;
using MediatR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.DSX.ProjectTemplate.Command.Group;
using Microsoft.DSX.ProjectTemplate.Command.Library;
using Microsoft.DSX.ProjectTemplate.Data.DTOs;
using Microsoft.DSX.ProjectTemplate.Data.Events;
using Microsoft.DSX.ProjectTemplate.Data.Exceptions;
using Microsoft.DSX.ProjectTemplate.Data.Models;
using Microsoft.DSX.ProjectTemplate.Data.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.DSX.ProjectTemplate.Test.Library
{
    [TestClass]
    [TestCategory("Library")]
    public class LibraryCreateCommandHandlerTests : UnitTest
    {
        [DataTestMethod]
        [ExpectedException(typeof(BadRequestException))]
        [DataRow(null)]
        [DataRow("")]
        public async Task Create_MissingName_BadRequestException(string name)
        {
            await ExecuteWithDb((db) =>
            {
                var handler = new CreateLibraryCommandHandler(
                    MockMediator.Object,
                    db,
                    Mapper,
                    MockAuthorizationService.Object);

                var dto = SetupLibraryDto();

                return handler.Handle(new CreateLibraryCommand() { Library = dto }, default);
            });
        }

        [DataTestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public async Task Create_MissingAddress_NullReferenceException()
        {
            await ExecuteWithDb((db) =>
            {
                var handler = new CreateLibraryCommandHandler(
                    MockMediator.Object,
                    db,
                    Mapper,
                    MockAuthorizationService.Object);

                var dto = new LibraryDto()
                {
                    Name = RandomFactory.GetLibraryName(),
                    Address = null
                };

                return handler.Handle(new CreateLibraryCommand() { Library = dto }, default);
            });
        }

        [TestMethod]
        public async Task Create_Valid_PublishesLibraryCreatedDomainEvent()
        {
            await ExecuteWithDb((db) =>
            {
                var handler = new CreateLibraryCommandHandler(
                    MockMediator.Object,
                    db,
                    Mapper,
                    MockAuthorizationService.Object);

                var dto = SetupLibraryDto(RandomFactory.GetLibraryName());

                return handler.Handle(new CreateLibraryCommand() { Library = dto }, default);
            }, (result, db) =>
            {
                MockMediator.Verify(x => x.Publish(It.IsAny<LibraryCreatedDomainEvent>(), It.IsAny<CancellationToken>()), Times.Once);

            });
        }

        private LibraryDto SetupLibraryDto(string name = "")
        {
            var address = new Address()
            {
                LocationAddressLine1 = RandomFactory.GetStreetAddress(),
                LocationAddressLine2 = "",
                LocationCity = RandomFactory.GetCity(),
                LocationStateProvince = RandomFactory.GetState(),
                LocationZipCode = RandomFactory.GetZip(),
                LocationCountry = "US"
            };

            var dto = new LibraryDto()
            {
                Name = name,
                Address = address
            };

            return dto;
        }
    }
}
