using System.ComponentModel.DataAnnotations;

namespace MotorVault.Model
{
    
        public class Brand
        {
            public int Id { get; set; }

            [Required, MaxLength(100)] 
            public string Name { get; set; }
            public string Country { get; set; }
            
        }
    
}
