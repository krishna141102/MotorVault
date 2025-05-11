using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotorVault.Data;
using MotorVault.Model;
using MotorVault.Model.DTO;

namespace MotorVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private ApplicationDbContext _dbContext;
        public BrandController(ApplicationDbContext dbcontext)
        {
            _dbContext = dbcontext;

        }
        [HttpPost]

        public IActionResult CreateBrand([FromBody] CreateBrandDTO
            createBrandDTO)
        {
            if (ModelState.IsValid)
            {
                // Logic to create a brand
                _dbContext.Brands.Add(new Brand
                {
                    Name = createBrandDTO.Name,
                    Country = createBrandDTO.Country
                });
                _dbContext.SaveChanges();
                return Ok();
            }
            return BadRequest(ModelState);
        }
    }
}
