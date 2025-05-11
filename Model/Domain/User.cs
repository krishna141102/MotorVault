using System.ComponentModel.DataAnnotations;

namespace MotorVault.Model.Domain
{
    
    public class User
    {
        public int Id { get; set; }
        [Required] public string Username { get; set; }
        [Required] public string PasswordHash { get; set; }
        [Required] public string Role { get; set; }
    }

}
