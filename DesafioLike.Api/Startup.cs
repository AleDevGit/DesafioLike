using AutoMapper;
using DesafioLike.Dominio.Identity;
using DesafioLike.Dominio.IRepositorios;
using DesafioLike.Repositorio.Context;
using DesafioLike.Repositorio.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DesafioLike.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //SqlServer
            //services.AddDbContext<DataContext>(
            //    x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            //);

            //MySQL
            //var connectionString = Configuration.GetConnectionString("QuickBuyDB");
            //services.AddDbContext<QuickBuyContexto>(

            //   x => x.UseLazyLoadingProxies().UseMySql(connectionString, m => m.MigrationsAssembly("QuickBuy.Repositorio")));
            //Sqlite
            services.AddDbContext<DataContext>(
                x => x.UseSqlite(Configuration.GetConnectionString("DesafioLikeConnection"))
            );
            
           //retirar particularidades 
            IdentityBuilder builder = services.AddIdentityCore<User>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            });

            //Configurações de contexto e Usuários
            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<DataContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                        Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //Toda vez que uma rota for chamada ela ira verificar a autorização
            services.AddMvc( options =>{
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser().Build();
		        options.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
            services.AddScoped<IPerguntaRepositorio, PerguntaRepositorio>();
            services.AddScoped<IRespostaRepositorio, RespostaRepositorio>();



            services.AddAutoMapper();
            services.AddControllers().AddNewtonsoftJson(options => 
                options.SerializerSettings.ReferenceLoopHandling = 
                Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddCors();

            services.AddSwaggerGen(options => 
            {
                options.SwaggerDoc("v1",
                new Microsoft.OpenApi.Models.OpenApiInfo{
                    Title ="Swagger Api",
                    Description ="API",
                    Version ="v1"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseCors(x =>x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseStaticFiles();
            app.UseRouting();

           app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json","Swagger API Desafio");
            });
        }
    }
}
