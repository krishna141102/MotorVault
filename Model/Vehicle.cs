using Microsoft.VisualBasic.FileIO;

namespace MotorVault.Model
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int CarModelId { get; set; }
        public CarModel CarModel { get; set; }
        public string FuelType { get; set; }
        public string TransmissionType { get; set; }
    }
}
