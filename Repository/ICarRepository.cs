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

        //Task<IEnumerable<Brand>> GetAllBrands();
        //Task<IEnumerable<String>> GetByCarType(string brand, string carType);

        //Task<IEnumerable<CarType>> GetAllCarTypes(string brand);

    }   
}
