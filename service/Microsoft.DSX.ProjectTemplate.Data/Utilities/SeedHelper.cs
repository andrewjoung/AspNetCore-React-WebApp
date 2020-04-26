using AutoMapper;
using Microsoft.DSX.ProjectTemplate.Data.DTOs;
using Microsoft.DSX.ProjectTemplate.Data.Models;
using System;
using System.Linq;

namespace Microsoft.DSX.ProjectTemplate.Data.Utilities
{
    public static class SeedHelper
    {
        public static Group GetRandomGroup(ProjectTemplateDbContext database)
        {
            return database.Groups.OrderBy(x => Guid.NewGuid()).First();
        }

        public static Group CreateValidNewGroup(ProjectTemplateDbContext database, string name = "")
        {
            var group = new Group()
            {
                Name = name.Length == 0 ? RandomFactory.GetAlphanumericString(8) : name,
                IsActive = RandomFactory.GetBoolean()
            };

            return group;
        }

        public static GroupDto CreateValidNewGroupDto(ProjectTemplateDbContext database, IMapper mapper, string name = "")
        {
            var group = CreateValidNewGroup(database, name);

            return mapper.Map<GroupDto>(group);
        }

        public static Project CreateValidNewProject(ProjectTemplateDbContext database, Group group = null)
        {
            var project = new Project()
            {
                Name = RandomFactory.GetCodeName(),
                Group = group ?? GetRandomGroup(database)
            };

            return project;
        }

        public static Library GetRandomLibrary(ProjectTemplateDbContext database)
        {
            return database.Libraries.OrderBy(x => Guid.NewGuid()).First();
        }

        public static Library CreateValidNewLibrary(ProjectTemplateDbContext database, string name ="")
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

            var library = new Library()
            {
                Name = RandomFactory.GetLibraryName(),
                Address = address
            };

            return library;
        }

        /*
        public static Library CreateValidNewLibraryDto(ProjectTemplateDbContext database, IMapper mapper, string name = "")
        {
            var library = CreateValidNewLibrary(database, name);

            return mapper.Map<LibraryDto>(library);
        }
        */
    }
}
