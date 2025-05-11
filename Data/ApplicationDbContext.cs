//using MotorVault.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using MotorVault.Model.Domain;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace MotorVault.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<User> users { get; set; }

        //Entity Relationships
        //One Brand → Many CarModels

        //One CarType → Many CarModels

        //One CarModel → Many Vehicles

    }
}
