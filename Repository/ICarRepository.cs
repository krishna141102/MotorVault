using MotorVault.Model.Domain;
using MotorVault.Model.DTO;


namespace MotorVault.Repository
{
    public interface ICarRepository
    {
        Task AddCar(Brand brand, CarType carType, CarModel carModel, Vehicle vehicle);

    }   
}
