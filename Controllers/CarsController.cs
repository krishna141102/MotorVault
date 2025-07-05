using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotorVault.Model.Domain;
using MotorVault.Model.DTO;
using MotorVault.Repository;
using MotorVault.Enum;
using AutoMapper;
using Microsoft.OpenApi.Any;
using Microsoft.AspNetCore.Authorization;
namespace MotorVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CarsController : ControllerBase
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        public CarsController(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }
        //[Authorize(Roles = "Admin")]
        [HttpPost("Brand")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize(Roles ="Writer")]
        public async Task<IActionResult> CreateBrand([FromForm] BrandDto brandDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var brand = _mapper.Map<Brand>(brandDto);

            var result = await _carRepository.AddBrand(brand);
            return result switch
            {
                AddResult.Created => Ok("Brand created successfully."),
                AddResult.AlreadyExists => Conflict("Brand already exists."),
                AddResult.Failed => StatusCode(500, "An error occurred while saving the brand."),
                _ => StatusCode(500, "Unexpected result.")
            };
        }

        [HttpPost("Brand/CarType")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateCarType([FromForm] CarTypeDto CarType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var carType = _mapper.Map<CarType>(CarType);

            var result = await _carRepository.AddCarType(carType);
            return result switch
            {
                AddResult.Created => Ok("Car type created successfully."),
                AddResult.AlreadyExists => Conflict("Car type already exists."),
                AddResult.BrandNotFound => NotFound("Brand not found."),
                AddResult.Failed => StatusCode(500, "An error occurred while saving the car type."),

                _ => StatusCode(500, "Unexpected result.")
            };

        }
        [HttpPost("Brand/CarType/CarModel")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateCarModel([FromForm] CarModelDto carModelDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var carModel = _mapper.Map<CarModel>(carModelDto);
            var brand = carModelDto.BrandName.ToLowerInvariant().Trim();
            var carTypeName = carModelDto.CarTypeName.ToLowerInvariant().Trim();
            var result = await _carRepository.AddCarModel(carModel, brand, carTypeName);
            return result switch
            {
                AddResult.Created => Ok("Car model created successfully."),
                AddResult.AlreadyExists => Conflict("Car model already exists."),
                AddResult.BrandNotFound => NotFound("Brand or not found."),
                AddResult.CarTypeNotFound => NotFound("Car type not found."),
                AddResult.Failed => StatusCode(500, "An error occurred while saving the car model."),

                _ => StatusCode(500, "Unexpected result.")
            };
        }

        [HttpPost("Vehicle")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateVehicle([FromForm] VehicleDto vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var addvehicle = _mapper.Map<Vehicle>(vehicle);
            var result = await _carRepository.AddVehicle(addvehicle);
            return result switch
            {
                AddResult.Created => Ok("Vehicle Created Successfully."),
                AddResult.AlreadyExists => Conflict("Car model already exists."),
                AddResult.BrandNotFound => NotFound("Brand or not found."),
                AddResult.CarTypeNotFound => NotFound("Car type not found."),

                AddResult.CarModelNotFound => NotFound("Car model not found."),
                AddResult.Failed => StatusCode(500, "An error occurred while saving the car model."),

                _ => StatusCode(500, "Unexpected result.")
            };
        }
        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAllBrands()
        {
            var brands = await _carRepository.GetAllBrands();

            if (brands == null || !brands.Any())
            {
                return NotFound("No brands found.");
                // Or: return NoContent();
            }

            return Ok(brands);
        }
        [HttpGet("brands/{brandName}/cartypes")]
        [Authorize(Roles = "Reader")]

        public async Task<IActionResult> GetAllCarTypes(string brandName)
        {
            var carTypes = await _carRepository.GetAllCarTypes(brandName);

            if (carTypes == null || !carTypes.Any())
            {
                return NotFound($"No car types found for brand '{brandName}'.");
            }

            return Ok(carTypes);
        }
        [HttpGet("brands/{brandName}/cartypes/{carTypeName}/models")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetModelsByBrandAndCarType(string brandName, string carTypeName)
        {
            var models = await _carRepository.GetCarModels(brandName, carTypeName);

            if (!models.Any())
            {
                return NotFound("No models found for the specified brand and car type.");
            }

            return Ok(models);
        }

        [HttpGet("brands/{brandname}/cartypes/{carTypeName}/models/{carmodel}/vehicle")]
        [Authorize(Roles = "Reader")]

        public async Task<IActionResult> GetVehicles(string brandname, string carTypeName, string carmodel)
        {
            var vehicles = await _carRepository.GetAllVehicles(brandname, carTypeName, carmodel);

            if (!vehicles.Any())
            {
                return NotFound("No vehicles found for the specified brand, car type, and car model.");
            }

            return Ok(vehicles);
        }
        [HttpPut("Brand/{brandName}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> UpdateBrand(string brandName, [FromForm] BrandDto updatedBrand)
        {
            var result = await _carRepository.UpdateBrand(brandName, updatedBrand);
            return result switch
            {
                AddResult.Created => Ok("Brand updated successfully."),
                AddResult.BrandNotFound => NotFound("Brand not found."),
                AddResult.Failed => StatusCode(500, "Failed to update brand."),
                _ => StatusCode(500, "Unexpected result.")
            };
        }

        [HttpPut("Brand/CarType/{carTypeName}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> UpdateCarType(string carTypeName, [FromForm] CarTypeDto dto)
        {
            var result = await _carRepository.UpdateCarType(carTypeName, dto);
            return result switch
            {
                AddResult.Created => Ok("Car type updated successfully."),
                AddResult.CarTypeNotFound => NotFound("Car type not found."),
                AddResult.Failed => StatusCode(500, "Failed to update car type."),
                _ => StatusCode(500, "Unexpected result.")
            };
        }

        [HttpPut("Brand/CarType/CarModel/{modelName}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> UpdateCarModel(string modelName, [FromForm] CarModelDto dto)
        {
            var result = await _carRepository.UpdateCarModel(modelName, dto);
            return result switch
            {
                AddResult.Created => Ok("Car model updated successfully."),
                AddResult.CarModelNotFound => NotFound("Car model not found."),
                AddResult.Failed => StatusCode(500, "Failed to update car model."),
                _ => StatusCode(500, "Unexpected result.")
            };
        }

        [HttpPut("Vehicle/{vehicleId:guid}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> UpdateVehicle(Guid vehicleId, [FromForm] VehicleDto dto)
        {
            var result = await _carRepository.UpdateVehicle(vehicleId, dto);
            return result switch
            {
                AddResult.Created => Ok("Vehicle updated successfully."),
                AddResult.Failed => StatusCode(500, "Failed to update vehicle."),
                _ => NotFound("Vehicle not found.")
            };
        }

        [HttpDelete("Brand/{brand}/CarType/{carTypeName}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> DeleteCarType(string brand,string carTypeName)
        {
            var result = await _carRepository.DeleteCarType(brand,carTypeName);
            return result switch
            {
                AddResult.Created => Ok("Car type deleted successfully."),
                AddResult.CarTypeNotFound => NotFound("Car type not found."),
                AddResult.Failed => StatusCode(500, "Failed to delete car type."),
                _ => StatusCode(500, "Unexpected result.")
            };
        }

        [HttpDelete("Brand/{brand}/CarType/{cartype}/CarModel/{modelName}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> DeleteCarModel(string brand,string cartype,string modelName)
        {
            var result = await _carRepository.DeleteCarModel(brand,cartype,modelName);
            return result switch
            {
                AddResult.Created => Ok("Car model deleted successfully."),
                AddResult.CarModelNotFound => NotFound("Car model not found."),
                AddResult.Failed => StatusCode(500, "Failed to delete car model."),
                _ => StatusCode(500, "Unexpected result.")
            };
        }

        [HttpDelete("Vehicle/{vehicleId:guid}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> DeleteVehicle(Guid vehicleId)
        {
            var result = await _carRepository.DeleteVehicle(vehicleId);
            return result switch
            {
                AddResult.Created => Ok("Vehicle deleted successfully."),
                AddResult.Failed => StatusCode(500, "Failed to delete vehicle."),
                _ => NotFound("Vehicle not found.")
            };
        }




    }
}

