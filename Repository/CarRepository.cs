using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotorVault.Data;
using MotorVault.Model.Domain;

namespace MotorVault.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private Brand findedBrand;
        private CarType findedCarType;
        private CarModel findedCarModel;
        public CarRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task AddCar(Brand brand, CarType carType, CarModel carModel, Vehicle vehicle)
        {
            findedBrand = await _dbContext.Brands.FirstOrDefaultAsync(b => b.BrandName == brand.BrandName);
            if (findedBrand == null)
            {
                findedBrand = brand;
                _dbContext.Brands.Add(findedBrand);
            }
            findedCarType = await _dbContext.CarTypes.FirstOrDefaultAsync(ct => ct.BrandName==brand.BrandName & ct.CarTypeName == carType.CarTypeName);
            if (findedCarType == null)
            {
                carType.CarTypeId = Guid.NewGuid();
                findedCarType= carType;
                _dbContext.CarTypes.Add(findedCarType);
            }
            findedCarModel = await _dbContext.CarModels.FirstOrDefaultAsync(cm => cm.CarTypeId == findedCarType.CarTypeId & cm.ModelName == carModel.ModelName);
            if(findedCarModel == null)
            {
                carModel.CarModelId = Guid.NewGuid();
                carModel.CarTypeId = findedCarType.CarTypeId;
                findedCarModel= carModel;
                _dbContext.CarModels.Add(findedCarModel);
            }
            vehicle.VehicleId = Guid.NewGuid();
           vehicle.CarModelId=findedCarModel.CarModelId;
            _dbContext.Vehicles.Add(vehicle);
            await _dbContext.SaveChangesAsync();
        }
    }
}
