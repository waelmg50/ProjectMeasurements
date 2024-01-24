using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsMeasurements.DBContext
{
    public class DataContextFactory : IDesignTimeDbContextFactory<ProjectsMeasurementsDBContext>
    {
        public ProjectsMeasurementsDBContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
            var optionsBuilder = new DbContextOptionsBuilder<ProjectsMeasurementsDBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DBConnection"));
            return new ProjectsMeasurementsDBContext(optionsBuilder.Options, configuration);
        }
    }
}
