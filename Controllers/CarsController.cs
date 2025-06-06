﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotorVault.Model.Domain;
using MotorVault.Model.DTO;
using MotorVault.Repository;
using MotorVault.Enum;
using AutoMapper;
namespace MotorVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        public CarsController(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        [HttpPost("Brand")]
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
        public async Task<IActionResult> CreateVehicle([FromForm]VehicleDto vehicle)
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
                AddResult.Failed => StatusCode(500, "An error ccurred while saving the car model."),

                _ => StatusCode(500, "Unexpected result.")
            };

            //[HttpGet]
            //public async Task<IActionResult> GetAllBrands()
            //{
            //    var brands = await _carRepository.GetAllBrands();

            //return Ok(brands);
            //}
            //[HttpGet("CarTypes")]
            //public async Task<IActionResult> GetAllCarTypes([FromQuery] string brand)
            //{
            //    var brands = await _carRepository.GetAllCarTypes(brand);
            //    return Ok(brand);
            //}
            //[HttpGet("brand-cartype")]

            //public async Task<IActionResult> GetByCarType([FromQuery] string BrandName,[FromQuery]string CarTypeName)
            //{
            //    var brand = await _carRepository.GetByCarType(BrandName, CarTypeName);
            //    return Ok(brand);
            //}


        }
    }
}
