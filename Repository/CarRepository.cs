using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotorVault.Data;
using MotorVault.Model.Domain;
using MotorVault.Enum;
using MotorVault.Model.DTO;
using System.Diagnostics.Metrics;

namespace MotorVault.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CarRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<AddResult> AddBrand(Brand brand)
        {
            try
            {
                var findedBrand = await _dbContext.Brands.FirstOrDefaultAsync(b => b.BrandName == brand.BrandName);
                if (findedBrand == null)
                {
                    _dbContext.Brands.Add(brand);
                    await _dbContext.SaveChangesAsync();
                    return AddResult.Created;
                }
                return AddResult.AlreadyExists;
            }
            catch (Exception)
            {
                return AddResult.Failed;


            }
        }

        public async Task<AddResult> AddCarType(CarType carType)
        {
            try
            {
                var brand = await _dbContext.Brands.FirstOrDefaultAsync(b => b.BrandName == carType.BrandName);
                if (brand == null)
                {
                    return AddResult.BrandNotFound;
                }
                var findedCarType = await _dbContext.CarTypes.FirstOrDefaultAsync(ct => ct.CarTypeName == carType.CarTypeName && ct.BrandName == carType.BrandName);
                if (findedCarType == null)
                {
                    _dbContext.CarTypes.Add(carType);
                    await _dbContext.SaveChangesAsync();
                    return AddResult.Created;
                }
                return AddResult.AlreadyExists;
            }
            catch (Exception)
            {
                return AddResult.Failed;
            }
        }
        public async Task<AddResult> AddCarModel(CarModel carModel, string brand, string cartype)
        {
            try
            {
                var findedBrand = await _dbContext.Brands.FirstOrDefaultAsync(b => b.BrandName == brand);
                if (findedBrand == null)
                {
                    return AddResult.BrandNotFound;
                }
                var findedCarType = await _dbContext.CarTypes.FirstOrDefaultAsync(ct => ct.CarTypeName == cartype && ct.BrandName == brand);
                if (findedCarType == null)
                {
                    return AddResult.CarTypeNotFound;
                }
                carModel.CarTypeId = findedCarType.CarTypeId;

                var findedCarModel = await _dbContext.CarModels
                    .FirstOrDefaultAsync(cm =>
                        cm.ModelName == carModel.ModelName &&
                        cm.CarTypeId == findedCarType.CarTypeId);
                if (findedCarModel == null)
                {
                    _dbContext.CarModels.Add(carModel);
                    await _dbContext.SaveChangesAsync();
                    return AddResult.Created;
                }
                return AddResult.AlreadyExists;
            }
            catch (Exception)
            {
                return AddResult.Failed;
            }
        }

        public async Task<AddResult> AddVehicle(Vehicle vehicle)
        {
            try
            {
                var existingBrand = await _dbContext.Brands
            .FirstOrDefaultAsync(b => b.BrandName == vehicle.BrandName);

                if (existingBrand == null)
                    return AddResult.BrandNotFound;

                var existingCarType = await _dbContext.CarTypes
                    .FirstOrDefaultAsync(ct =>
                        ct.CarTypeName == vehicle.CarTypeName &&
                        ct.BrandName == vehicle.BrandName);

                if (existingCarType == null)
                    return AddResult.CarTypeNotFound;

                var existingCarModel = await _dbContext.CarModels
                    .FirstOrDefaultAsync(cm =>
                        cm.ModelName == vehicle.ModelName &&
                        cm.CarTypeId == existingCarType.CarTypeId);

                if (existingCarModel == null)
                    return AddResult.CarModelNotFound;

                vehicle.CarModelId = existingCarModel.CarModelId;

                var duplicate = await _dbContext.Vehicles
                    .FirstOrDefaultAsync(v =>
                        v.ModelName == vehicle.ModelName &&
                        v.Color == vehicle.Color &&
                        v.Price == vehicle.Price &&
                        v.FuelType == vehicle.FuelType &&
                        v.TransmissionType == vehicle.TransmissionType);

                if (duplicate != null)
                    return AddResult.AlreadyExists;

                _dbContext.Vehicles.Add(vehicle);
                await _dbContext.SaveChangesAsync();
                return AddResult.Created;

            }
            catch (Exception)
            {
                return AddResult.Failed;
            }
        }

        public async Task<IEnumerable<BrandDto>> GetAllBrands()
        {
            try
            {
                var brands = await _dbContext.Brands.Select(
                    b => new BrandDto { BrandName = b.BrandName, Country = b.Country }).ToListAsync();
                return brands;
            }
            catch (Exception)
            {
                return Enumerable.Empty<BrandDto>();
            }
        }
        public async Task<IEnumerable<string>> GetAllCarTypes(string brand)
        {

            try
            {
                return await _dbContext.CarTypes
                    .Where(ct => ct.BrandName == brand)
                    .Select(ct => ct.CarTypeName)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return Enumerable.Empty<string>();
            }

        }



        public async Task<IEnumerable<CarModelDto>> GetCarModels(string brand, string carType)
        {
            try
            {
                var cartype= await _dbContext.CarTypes
                    .FirstOrDefaultAsync(ct => ct.CarTypeName == carType && ct.BrandName == brand);
                return await _dbContext.CarModels.Where(ct => ct.CarType.BrandName == brand && ct.CarTypeId == cartype.CarTypeId).Select(c => new CarModelDto
                {
                    BrandName=c.CarType.BrandName,
                    CarTypeName=c.CarType.CarTypeName,
                    ModelName = c.ModelName,
                    ReleaseYear = c.ReleaseYear,
                    EngineType = c.EngineType,
                    HorsePower = c.HorsePower

                }).ToListAsync();

            }
            catch (Exception)
            {
                return Enumerable.Empty<CarModelDto>();
            }
        }
        public async Task<IEnumerable<VehicleDto>> GetAllVehicles(string brand, string carType, string carmodel)
        {
            try
            {
                return await _dbContext.Vehicles
                    .Where(v =>
                        v.BrandName == brand &&
                        v.CarTypeName == carType &&
                        v.ModelName == carmodel)
                    .Select(v => new VehicleDto
                    {
                        BrandName = v.BrandName,
                        CarTypeName = v.CarTypeName,
                        ModelName = v.ModelName,
                        Color = v.Color,
                        Price = v.Price,
                        IsAvailable = v.IsAvailable,
                        FuelType = v.FuelType,
                        TransmissionType = v.TransmissionType
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] {ex.Message}");
                return Enumerable.Empty<VehicleDto>();
            }
        }

    }
}


