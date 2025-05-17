using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        //[HttpPost]
        //public async Task<IActionResult> CreateCar([FromBody] CreateCarDTO createCarDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Logic to create a car
        //        var car = _mapper.Map<Car>(createCarDTO);
        //        await _carRepository.AddCar(car);
        //        return Ok();
        //    }
        //    return BadRequest(ModelState);
        //}

    }
}
