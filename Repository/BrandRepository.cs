using MotorVault.Data;
using MotorVault.Model.Domain;

namespace MotorVault.Repository
{
    public class BrandRepository: IBrandRepository
    {
        public readonly ApplicationDbContext _dbcontext;
        public BrandRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }   
        public async Task AddBrand(Brand brand)
        {
            _dbcontext.Brands.Add(brand);
            await _dbcontext.SaveChangesAsync();
        }
    }
}
