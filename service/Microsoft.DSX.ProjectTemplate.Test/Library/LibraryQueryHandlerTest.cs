using FluentAssertions;
using Microsoft.DSX.ProjectTemplate.Command.Group;
using Microsoft.DSX.ProjectTemplate.Command.Library;
using Microsoft.DSX.ProjectTemplate.Data.DTOs;
using Microsoft.DSX.ProjectTemplate.Data.Exceptions;
using Microsoft.DSX.ProjectTemplate.Data.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.DSX.ProjectTemplate.Test.Library
{
    [TestClass]
    [TestCategory("Library")]
    public class LibraryQueryHandlerTest : UnitTest
    {
        [TestMethod]
        public async Task GetAll_Valid_Success()
        {
            DateTime dtStart = DateTime.Now;

            await ExecuteWithDb((db) =>
            {
                var handler = new LibraryQueryHandler(
                    MockMediator.Object,
                    db,
                    Mapper,
                    MockAuthorizationService.Object);

                return handler.Handle(new GetAllLibraryQuery(), default(CancellationToken));
            }, (result, db) => {
                result.Should().NotBeNull();
                result.Should().BeAssignableTo<IEnumerable<LibraryDto>>();
                result.Should().HaveCountGreaterThan(0);
                foreach (var library in result)
                {
                    library.Id.Should().BeGreaterThan(0);
                    library.Name.Should().NotBeNullOrWhiteSpace();
                    library.Address.Should().NotBeNull();
                    library.CreatedDate.Should().BeOnOrAfter(dtStart);
                    library.UpdatedDate.Should().BeOnOrAfter(dtStart);
                }
            });
        }

        [DataTestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        [DataRow(int.MaxValue)]
        public async Task GetById_IdNotFound_entityNotFoundException(int id)
        {
            await ExecuteWithDb((db) =>
            {
                var handler = new LibraryQueryHandler(
                    MockMediator.Object,
                    db,
                    Mapper,
                    MockAuthorizationService.Object);
                return handler.Handle(new GetLibraryByIdQuery() { LibraryId = id }, default(CancellationToken));
            });
        }

        [DataTestMethod]
        public async Task GetById_Valid_Success()
        {


            await ExecuteWithDb((db) =>
            {
                var dto = SeedHelper.GetRandomLibrary(db);

                var handler = new LibraryQueryHandler(
                    MockMediator.Object,
                    db,
                    Mapper,
                    MockAuthorizationService.Object);

                return handler.Handle(new GetLibraryByIdQuery() { LibraryId = dto.Id }, default(CancellationToken));
            }, (result, db) => {
                result.Should().NotBeNull();
                result.Should().BeAssignableTo<LibraryDto>();
                result.Id.Should().BeGreaterThan(0);
                result.Name.Should().NotBeNullOrWhiteSpace();
                result.Address.Should().NotBeNull();
            });
        }
    }
}
