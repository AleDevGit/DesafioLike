using DesafioLike.Dominio.Entidades;
using DesafioLike.Dominio.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DesafioLike.Repositorio.Context
{
    public class DataContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, 
    UserRole, IdentityUserLogin<int>,IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        public DbSet<Categoria> Categorias{get; set;}
        public DbSet<Pergunta> Perguntas{get; set;}
        public DbSet<Resposta> Respostas{get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole =>{
                userRole.HasKey(ur => new{ ur.UserId, ur.RoleId});

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
                
                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

                modelBuilder.Entity<Pergunta>()
                .HasOne<Categoria>(s => s.Categoria)
                .WithMany(g => g.Perguntas)
                .HasForeignKey(s => s.CategoriaId);

        }

    }
}