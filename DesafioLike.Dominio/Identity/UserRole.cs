using Microsoft.AspNetCore.Identity;

namespace DesafioLike.Dominio.Identity
{
    public class UserRole : IdentityUserRole<int>
    {
        public Role Role { get; set; }
        public User User { get; set; }

    }
}