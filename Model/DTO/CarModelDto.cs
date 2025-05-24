using System.ComponentModel.DataAnnotations;

namespace MotorVault.Model.DTO
{
    public class CarModelDto
    {
        [Required(ErrorMessage = "Brand is required.")]
        public string BrandName { get; set; }
        [Required(ErrorMessage = "Car type is required.")]
        public string CarTypeName { get; set; }
        [Required(ErrorMessage="Model Name is required.")]
        [MaxLength(100)]
        public string ModelName { get; set; }

        [Required]
        public int ReleaseYear { get; set; }
        [Required, MaxLength(100)]
        public string EngineType { get; set; }
        [Required]
        public int HorsePower { get; set; }

    }
}
