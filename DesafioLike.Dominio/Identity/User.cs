using System.Collections.Generic;
using DesafioLike.Dominio.Enum;
using Microsoft.AspNetCore.Identity;

namespace DesafioLike.Dominio.Identity
{
    public class User : IdentityUser<int>
    {
        public string CPF { get; set; }
        public int AnoNasc { get; set; } 
        public Sexo Sexo { get; set; } 
        public string FullName {get; set;}
        public List<UserRole> UserRoles { get; set; }
    }
}