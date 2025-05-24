using System.ComponentModel.DataAnnotations;

namespace MotorVault.Model.Domain
{
    
        public class Brand
        {
            [Key]
            [Required]
            public string BrandName { get; set; }


            [Required, MaxLength(100)] 
            public string Country { get; set; }

        public ICollection<CarType> CarTypes { get; set; }

        
    }
    
}
