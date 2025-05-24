using MotorVault.Model.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MotorVault.Model.DTO
{
    public class CarTypeDto
    {
        


            [Required(ErrorMessage = "BrandName is required.")]
            public string BrandName { get; set; }
           
            [Required(ErrorMessage ="CarType is required")]
            public string CarTypeName { get; set; }
            

            [MaxLength(100)]
            public string Description { get; set; }

            
        
    }
}
