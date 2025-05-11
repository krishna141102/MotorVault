namespace MotorVault.Model
{
    public class CarModel
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int CarTypeId { get; set; }
        public CarType CarType { get; set; }
        public int ReleaseYear { get; set; }
        public string EngineType { get; set; }
        public int HorsePower { get; set; }
    }
}
