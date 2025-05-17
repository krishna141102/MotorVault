using System.ComponentModel.DataAnnotations;

namespace MotorVault.Model.Domain
{
    public class CreateVehicle
    {
        public string BrandName { get; set; }
        public string Country { get; set; }

        public string CarTypeName { get; set; }
        public string Description { get; set; }
        public string CarModelName { get; set; }
        public int ReleaseYear { get; set; }
        public string EngineType { get; set; }
        public int HorsePower { get; set; }
        public string color { get; set; }

        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } = true;
        public string FuelType { get; set; }
        public string TransmissionType { get; set; }
        public IFormFile formFile { get; set; }
    }
}
