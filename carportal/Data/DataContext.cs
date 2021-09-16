using carportal.Models;
using Microsoft.EntityFrameworkCore;

namespace carportal.Data
{
    public class DataContext : DbContext 
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<Car> Cars {get; set;}
        
    }
}