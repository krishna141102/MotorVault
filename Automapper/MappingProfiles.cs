using AutoMapper;
using MotorVault.Model.Domain;
using MotorVault.Model.DTO;
namespace MotorVault.Automapper
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<BrandDto,Brand>().ForMember(dest=>dest.BrandName,op=>op.MapFrom(src=> src.BrandName.ToLowerInvariant().Trim()))
                .ForMember(dest => dest.Country,op=>op.MapFrom(src=>src.Country.ToLowerInvariant().Trim()));
           
            CreateMap<CarTypeDto, CarType>()
                .ForMember(dest => dest.BrandName, op => op.MapFrom(src => src.BrandName.ToLowerInvariant().Trim()))
                .ForMember(dest => dest.CarTypeName, op => op.MapFrom(src => src.CarTypeName.ToLowerInvariant().Trim()))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description != null ? src.Description : null));
            
            CreateMap<CarModelDto, CarModel>()
                .ForMember(dest => dest.ModelName, op => op.MapFrom(src => src.ModelName.ToLowerInvariant().Trim()))
                .ForMember(dest => dest.ReleaseYear, opt => opt.MapFrom(src => src.ReleaseYear))
                .ForMember(dest => dest.EngineType, opt => opt.MapFrom(src => src.EngineType.ToLowerInvariant().Trim()))
                .ForMember(dest=> dest.HorsePower,opt=> opt.MapFrom(src=>src.HorsePower));


            CreateMap<VehicleDto, Vehicle>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.BrandName.ToLowerInvariant().Trim()))
                .ForMember(dest => dest.CarTypeName, opt => opt.MapFrom(src => src.CarTypeName.ToLowerInvariant().Trim()))
                .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.ModelName.ToLowerInvariant().Trim()))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color.ToLowerInvariant().Trim()))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.IsAvailable))
                .ForMember(dest => dest.FuelType, opt => opt.MapFrom(src => src.FuelType.ToLowerInvariant().Trim()))
                .ForMember(dest => dest.TransmissionType, opt => opt.MapFrom(src => src.TransmissionType.ToLowerInvariant().Trim()));

        }


    }
    
}
