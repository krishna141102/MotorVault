using System.ComponentModel.DataAnnotations;

namespace MotorVault.Model.DTO
{
    public class CreateBrandDTO
    {
        

            [Required, MaxLength(100)]
            public string Name { get; set; }
            public string Country { get; set; }

        }
    
}
