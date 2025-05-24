using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorVault.Model.Domain
{
    public class CarType
    {
        [Key]
        public Guid CarTypeId { get; set; }
        [Required]
        public string BrandName { get; set; }


        [Required]
        public string CarTypeName { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [ForeignKey("BrandName")]
        public Brand Brand { get; set; }

        public ICollection<CarModel> CarModels { get; set; }
    }
}
