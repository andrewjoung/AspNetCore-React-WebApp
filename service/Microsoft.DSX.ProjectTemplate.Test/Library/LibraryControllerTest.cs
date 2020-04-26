using FluentAssertions;
using Microsoft.DSX.ProjectTemplate.Data.DTOs;
using Microsoft.DSX.ProjectTemplate.Data.Models;
using Microsoft.DSX.ProjectTemplate.Data.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microsoft.DSX.ProjectTemplate.Test.Library
{
    [TestClass]
    [TestCategory("Library")]
    public class LibraryControllerTest : IntegrationTest
    {
        [TestMethod]
        public async Task GetAll_Valid_Success()
        {
            // Arrange
            var client = Factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/libraries");

            // Assert
            var result = await EnsureObject<IEnumerable<LibraryDto>>(response);
            result.Should().HaveCountGreaterThan(0);
        }

        [DataTestMethod]
        [DataRow(1)]
        public async Task GetById_Valid_Success(int libraryId)
        {
            // Arrange
            var client = Factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/api/libraries/{libraryId}");

            // Assert
            var result = await EnsureObject<LibraryDto>(response);
            result.Should().NotBeNull();
            result.Id.Should().Be(libraryId);
            result.IsValid().Should().BeTrue();
        }

        [TestMethod]
        public async Task Create_Valid_Success()
        {
            // Arrange
            var client = Factory.CreateClient();
            var dto = SetupLibraryDto();

            // Act
            var response = await client.PostAsJsonAsync("/api/libraries", dto);

            // Assert
            var result = await EnsureObject<LibraryDto>(response);
            result.Id.Should().BeGreaterThan(0);
            result.Name.Should().Be(dto.Name);
            //result.Address.Should().Be(dto.Address);
            result.Address.LocationAddressLine1.Should().Be(dto.Address.LocationAddressLine1);
            result.Address.LocationCity.Should().Be(dto.Address.LocationCity);
            result.Address.LocationStateProvince.Should().Be(dto.Address.LocationStateProvince);
            result.Address.LocationZipCode.Should().Be(dto.Address.LocationZipCode);
            result.Address.LocationCountry.Should().Be(dto.Address.LocationCountry);
        }

        [DataTestMethod]
        [DataRow(2)]
        public async Task Delete_Valid_Success(int libraryId)
        {
            // Arrange
            var client = Factory.CreateClient();

            // Act
            var response = await client.DeleteAsync($"/api/libraries/{libraryId}");

            // Assert
            var result = await EnsureObject<bool>(response);
            result.Should().BeTrue();
        }

        private LibraryDto SetupLibraryDto()
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

            return new LibraryDto()
            {
                Name = RandomFactory.GetLibraryName(),
                Address = address
            };
        }
    }
}
