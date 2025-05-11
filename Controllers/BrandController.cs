using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotorVault.Model.Domain;
using MotorVault.Model.DTO;
using MotorVault.Repository;

namespace MotorVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        public BrandController(IBrandRepository brandRepository,IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;

        }
        [HttpPost]

        public async Task<IActionResult> CreateBrand([FromBody] CreateBrandDTO
            createBrandDTO)
        {
            if (ModelState.IsValid)
            {
                // Logic to create a brand
                var brand= _mapper.Map<Brand>(createBrandDTO);
                await _brandRepository.AddBrand(brand);
                return Ok();
            }
            return BadRequest(ModelState);
        }
    }
}
