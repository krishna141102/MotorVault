using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MotorVault.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "4de69ed5-b990-4582-9bb6-d37386efc30e";
            var writerRoleId = "47f04e0f-fa77-429c-839a-6a7cd4e74028";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id=readerRoleId,
                    ConcurrencyStamp= readerRoleId,
                    Name="Reader",
                    NormalizedName="READER"
                },
                new IdentityRole
                {
                    Id=writerRoleId,
                    ConcurrencyStamp= writerRoleId,
                    Name="writer",
                    NormalizedName="WRITER"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
