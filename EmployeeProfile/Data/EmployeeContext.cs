using EmployeeProfile.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeProfile.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<EmployeeHobbies> EmployeeHobbies { get; set; }

        public DbSet<Hobby> Hobbies { get; set; }

    }
}
