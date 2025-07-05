using MotorVault.Model.Domain;
using MotorVault.Model.DTO;
using MotorVault.Enum;

namespace MotorVault.Repository
{
    public interface ICarRepository
    {
        Task<AddResult> AddBrand(Brand brand);
        Task<AddResult> AddCarType(CarType carType);

        Task<AddResult> AddCarModel(CarModel carModel,string brand,string cartype);
        Task<AddResult> AddVehicle(Vehicle vehicle);

        Task<IEnumerable<BrandDto>> GetAllBrands();

        Task<IEnumerable<string>> GetAllCarTypes(string brand);


        Task<IEnumerable<CarModelDto>> GetCarModels(string brand, string carType);

        Task<IEnumerable<VehicleDto>> GetAllVehicles(string brand, string carType, string carmodel);

        Task<AddResult> UpdateBrand(string brandName, BrandDto updatedBrand);

        Task<AddResult> UpdateCarType(string carTypeName, CarTypeDto dto);

        Task<AddResult> UpdateCarModel(string modelName, CarModelDto dto);

        Task<AddResult> UpdateVehicle(Guid vehicleId, VehicleDto dto);

        Task<AddResult> DeleteCarType(string brand,string carTypeName);
        Task<AddResult> DeleteCarModel(string brand,string cartype,string modelName);
        Task<AddResult> DeleteVehicle(Guid vehicleId);

    }   
}
