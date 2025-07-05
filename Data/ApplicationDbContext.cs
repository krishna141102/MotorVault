using Microsoft.EntityFrameworkCore;
using MotorVault.Model.Domain;
using System.Security.Cryptography;
using System.Text;

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
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // === Seed Brands ===
            var brands = new[]
            {
                new Brand { BrandName = "BMW", Country = "Germany" },
                new Brand { BrandName = "Audi", Country = "Germany" },
                new Brand { BrandName = "Toyota", Country = "Japan" },
                new Brand { BrandName = "Ford", Country = "USA" },
                new Brand { BrandName = "Mercedes", Country = "Germany" },
                new Brand { BrandName = "Honda", Country = "Japan" },
                new Brand { BrandName = "Chevrolet", Country = "USA" },
                new Brand { BrandName = "Nissan", Country = "Japan" },
                new Brand { BrandName = "Volkswagen", Country = "Germany" },
                new Brand { BrandName = "Hyundai", Country = "South Korea" }
            };
            modelBuilder.Entity<Brand>().HasData(brands);

            // === Helper: Deterministic GUID Generator ===
            Guid GuidFrom(string input)
            {
                using var md5 = MD5.Create();
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                return new Guid(hash.Take(16).ToArray());
            }

            // === Seed CarTypes (3 per brand) ===
            var carTypes = new List<CarType>();
            string[] carTypeNames = { "Sedan", "SUV", "Coupe" };

            foreach (var brand in brands)
            {
                foreach (var type in carTypeNames)
                {
                    carTypes.Add(new CarType
                    {
                        CarTypeId = GuidFrom($"{brand.BrandName}_{type}"),
                        BrandName = brand.BrandName,
                        CarTypeName = type,
                        Description = $"{type} for {brand.BrandName}"
                    });
                }
            }
            modelBuilder.Entity<CarType>().HasData(carTypes);

            // === Seed CarModels (5 per car type) ===
            var carModels = new List<CarModel>();
            foreach (var carType in carTypes)
            {
                for (int i = 1; i <= 5; i++)
                {
                    string modelName = $"{carType.BrandName}_{carType.CarTypeName}_Model{i}";
                    carModels.Add(new CarModel
                    {
                        CarModelId = GuidFrom(modelName),
                        ModelName = modelName,
                        CarTypeId = carType.CarTypeId,
                        ReleaseYear = 2017 + i,
                        EngineType = i % 2 == 0 ? "Petrol" : "Diesel",
                        HorsePower = 140 + i * 15
                    });
                }
            }
            modelBuilder.Entity<CarModel>().HasData(carModels);

            // === Seed Vehicles (5 per car model) ===
            var vehicles = new List<Vehicle>();
            var colors = new[] { "Black", "White", "Blue", "Red", "Silver" };
            var transmissions = new[] { "Automatic", "Manual" };
            var fuelTypes = new[] { "Petrol", "Diesel" };

            foreach (var carModel in carModels)
            {
                for (int i = 0; i < 5; i++)
                {
                    string vehicleKey = $"{carModel.ModelName}_Vehicle_{i}";
                    vehicles.Add(new Vehicle
                    {
                        VehicleId = GuidFrom(vehicleKey),
                        BrandName = carModel.ModelName.Split('_')[0],
                        CarTypeName = carModel.ModelName.Split('_')[1],
                        ModelName = carModel.ModelName,
                        CarModelId = carModel.CarModelId,
                        Color = colors[i % colors.Length],
                        Price = 28000 + (i * 1500),
                        IsAvailable = true,
                        FuelType = fuelTypes[i % fuelTypes.Length],
                        TransmissionType = transmissions[i % transmissions.Length]
                    });
                }
            }
            modelBuilder.Entity<Vehicle>().HasData(vehicles);
        }
    }
}
