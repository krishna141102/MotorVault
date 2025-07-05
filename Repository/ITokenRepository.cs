using Microsoft.AspNetCore.Identity;
using MotorVault.Model.Domain;

namespace MotorVault.Repository
{
    public interface ITokenRepository
    {
        string CreateToken(IdentityUser user,List<string> roles);
    }
}
