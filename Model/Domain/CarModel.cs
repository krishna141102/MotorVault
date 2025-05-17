using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorVault.Model.Domain
{
    public class CarModel
    {
        [Key]
        public Guid CarModelId { get; set; }
        [Required]
        [MaxLength(100)]
        public string ModelName { get; set; }
        public Guid CarTypeId { get; set; }

        [Required, MaxLength(4)]
        public int ReleaseYear { get; set; }
        [Required,MaxLength(100)]
        public string EngineType { get; set; }
        [Required, MaxLength(4)]
        public int HorsePower { get; set; }

        [ForeignKey("CarTypeId")]
        public CarType CarType { get; set; }

        public ICollection<CarModel> CarModels { get; set; }
    }
}
