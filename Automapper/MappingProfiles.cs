using AutoMapper;
using MotorVault.Model.Domain;
using MotorVault.Model.DTO;
namespace MotorVault.Automapper
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Brand, CreateBrandDTO>().ReverseMap();
        }

        
    }
    
}
