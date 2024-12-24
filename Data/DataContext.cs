using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Test_Dev.Data.Configuration;
using Test_Dev.Model;

namespace Test_Dev.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<Employee> employees { get; set; }
        public DbSet<Position> positions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PositionConfiguration).Assembly);
        }
    }
}