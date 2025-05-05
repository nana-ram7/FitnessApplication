using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using FitnessApplication.Models;

namespace FitnessApplication
{
    public class FitnessContextFactory : IDesignTimeDbContextFactory<FitnessContext>
    {
        public FitnessContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FitnessContext>();

            // Use full connection string in correct format (NOT postgresql://...)
            var connectionString = "Host=dpg-d0c1qpbuibrs73doev0g-a.oregon-postgres.render.com;Port=5432;Database=fitness_db_6ulj;Username=fitness_db_6ulj_user;Password=dypQWfTjXbuzy0WV7vL2jquXnbFHKCm7;SSL Mode=Require;Trust Server Certificate=true";

            optionsBuilder.UseNpgsql(connectionString);

            return new FitnessContext(optionsBuilder.Options);
        }
    }
}
