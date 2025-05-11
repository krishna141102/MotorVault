using MotorVault.Model.Domain;

namespace MotorVault.Repository
{
    public interface IBrandRepository
    {
        Task AddBrand(Brand brand);
     
        Task<IEnumerable<Brand>> GetAllBrands();

    }
}
