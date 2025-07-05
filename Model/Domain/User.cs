namespace MotorVault.Model.Domain
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Username { get; set; }
        public string Password { get; set; } // Store hashed in real app
        public string Roles { get; set; }
    }
}
