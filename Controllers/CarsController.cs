using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotorVault.Model.Domain;
using MotorVault.Model.DTO;
using MotorVault.Repository;

namespace MotorVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarRepository _carRepository;
        //private readonly IMapper _mapper;
        public CarsController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
            //_mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar([FromForm] BrandDto brandDto)
        {
            if (ModelState.IsValid)
            {
                // Logic to create a car
                var brand = new Brand
                {
                    BrandName = brandDto.BrandName,
                    Country = brandDto.Country
                };
                var carType = new CarType
                {
                    BrandName = brandDto.BrandName,
                    CarTypeName = brandDto.CarTypeName,
                    Description = brandDto.Description
                };
                var carModel = new CarModel
                {

                    ModelName = brandDto.CarModelName,
                    ReleaseYear = brandDto.ReleaseYear,
                    EngineType = brandDto.EngineType,
                    HorsePower = brandDto.HorsePower,

                };
                var memory = new MemoryStream();
                await brandDto.formFile.CopyToAsync(memory);


                var vehicle = new Vehicle
                {
                    CarModelId = carModel.CarModelId,
                    Color = brandDto.color,
                    Price = brandDto.Price,
                    IsAvailable = brandDto.IsAvailable,
                    FuelType = brandDto.FuelType,
                    TransmissionType = brandDto.TransmissionType,
                    Data = memory.ToArray(),
                    ContentType = brandDto.formFile.ContentType,

                };
                await _carRepository.AddCar(brand, carType, carModel, vehicle);
                return Ok();
            }
            return BadRequest(ModelState);
        }

    }
}
