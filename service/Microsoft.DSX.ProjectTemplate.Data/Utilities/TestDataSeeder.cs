using Microsoft.Extensions.Logging;

namespace Microsoft.DSX.ProjectTemplate.Data.Utilities
{
    public class TestDataSeeder
    {
        private readonly ProjectTemplateDbContext _dbContext;
        private readonly ILogger<TestDataSeeder> _logger;

        public TestDataSeeder(ProjectTemplateDbContext context, ILogger<TestDataSeeder> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public void SeedTestData()
        {
            _logger.LogInformation("Database seeding started");

            SeedGroups(10);

            SeedProjects(10);

            // Seed 5 new libraries into the database
            SeedLibraries(5);

            _logger.LogInformation("Database seeding completed");
        }

        private void SeedGroups(int entityCount)
        {
            for (int i = 0; i < entityCount; i++)
            {
                var newProject = SeedHelper.CreateValidNewGroup(_dbContext);
                _dbContext.Groups.Add(newProject);
            }

            _dbContext.SaveChanges();
        }

        private void SeedProjects(int entityCount)
        {
            for (int i = 0; i < entityCount; i++)
            {
                var newProject = SeedHelper.CreateValidNewProject(_dbContext);
                _dbContext.Projects.Add(newProject);
            }

            _dbContext.SaveChanges();
        }

        // Method for seeding library data
        private void SeedLibraries(int entityCount)
        {
            for(int i = 0; i < entityCount; i++)
            {
                var newLibrary = SeedHelper.CreateValidNewLibrary(_dbContext);
                _dbContext.Libraries.Add(newLibrary);
            }

            _dbContext.SaveChanges();
        }
    }
}
