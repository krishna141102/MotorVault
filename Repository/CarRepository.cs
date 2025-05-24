using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotorVault.Data;
using MotorVault.Model.Domain;
using MotorVault.Enum;

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

                // 2. Validate CarType
                var existingCarType = await _dbContext.CarTypes
                    .FirstOrDefaultAsync(ct =>
                        ct.CarTypeName == vehicle.CarTypeName &&
                        ct.BrandName == vehicle.BrandName);

                if (existingCarType == null)
                    return AddResult.CarTypeNotFound;

                // 3. Validate CarModel
                var existingCarModel = await _dbContext.CarModels
                    .FirstOrDefaultAsync(cm =>
                        cm.ModelName == vehicle.ModelName &&
                        cm.CarTypeId == existingCarType.CarTypeId);

                if (existingCarModel == null)
                    return AddResult.CarModelNotFound;

                // 4. Assign CarModelId and other navigation values
                vehicle.CarModelId = existingCarModel.CarModelId;

                // Optional: Prevent duplicates (depends on your business logic)
                var duplicate = await _dbContext.Vehicles
                    .FirstOrDefaultAsync(v =>
                        v.ModelName == vehicle.ModelName &&
                        v.Color== vehicle.Color &&
                        v.Price == vehicle.Price && 
                        v.FuelType==vehicle.FuelType && 
                        v.TransmissionType==vehicle.TransmissionType);

                if (duplicate != null)
                    return AddResult.AlreadyExists;

                // 5. Add to DB
                _dbContext.Vehicles.Add(vehicle);
                await _dbContext.SaveChangesAsync();
                return AddResult.Created;

            }
            catch (Exception)
            {
                return AddResult.Failed;
            }
        }

        //public async Task<IEnumerable<Brand>> GetAllBrands()
        //{
        //    var brands= await _dbContext.Brands.ToListAsync();
        //    return brands;
        //}
        //public async Task<IEnumerable<string>> GetAllCarTypes(string brand)
        //{
        //    return await _dbContext.CarTypes.Where(ct => ct.BrandName == brand).Select(ct=> ct.CarTypeName).ToListAsync();

        //}



        //public async Task<IEnumerable<CarType>> GetByCarType(string brand, string carType)
        //{
        //    var findedCarType = await _dbContext.CarTypes.Where(ct => ct.BrandName == brand & ct.CarTypeName == carType).ToListAsync();

        //    return findedCarType;
        //}
    }
}

