using MotorVault.Model.DTO;

namespace MotorVault.Repository
{
    public interface ICarRepository
    {
        public Task Create(BrandDto brandDto);
    }
}
