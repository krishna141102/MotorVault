using System.ComponentModel.DataAnnotations;

namespace MotorVault.Model.DTO
{
    public class BrandDto
    {
        //public int Id { get; set; }


        [Required(ErrorMessage = "Brand name is required.")]
        public string BrandName { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }


    }
}
