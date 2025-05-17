using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic.FileIO;

namespace MotorVault.Model.Domain
{
    public class Vehicle
    {
        [Key]
        public Guid VehicleId { get; set; }
        public Guid CarModelId { get; set; }
        [Required, MaxLength(100)]
        public string Color { get; set; }
        [Required]
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } = true;
        [Required, MaxLength(100)]
        public string FuelType { get; set; }
        [Required, MaxLength(100)]
        public string TransmissionType { get; set; }

      public byte[] Data { get; set; }
        [Required]
        public string ContentType { get; set; }

        [ForeignKey("CarModelId")]
        public CarModel CarModel { get; set; }
    }
}
