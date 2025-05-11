using System.ComponentModel.DataAnnotations;

namespace MotorVault.Model.Domain
{
    public class CarType
    {
        public int id { get; set; }

        [Required]
        public string Typename { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }
    }
}
