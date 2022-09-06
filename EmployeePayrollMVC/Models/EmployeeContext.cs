using Microsoft.EntityFrameworkCore;

namespace EmployeePayrollMVC.Models
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions options)
           : base(options)
        {
        }
        public DbSet<EmployeeModel> EmployeePayrollTable { get; set; }
    }
}
