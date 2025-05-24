using MotorVault.Model.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MotorVault.Model.DTO
{
    public class VehicleDto
    {
            public string BrandName { get; set; }
            public string CarTypeName { get; set; }
            public string ModelName { get; set; }
            [Required, MaxLength(100)]
            public string Color { get; set; }
            [Required]
            public decimal Price { get; set; }
            public bool IsAvailable { get; set; } = true;
            [Required, MaxLength(100)]
            public string FuelType { get; set; }
            [Required, MaxLength(100)]
            public string TransmissionType { get; set; }
        }
    }

