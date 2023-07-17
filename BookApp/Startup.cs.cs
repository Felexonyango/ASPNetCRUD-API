using BookApp.Context;
using BookApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

using Microsoft.OpenApi.Models;
using System.Text;
using BookApp.auth;
using BookApp.Extensions;

namespace BookApp
{
    public class Startup
    {


        public string ConnectionString { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("DefaultConnection");
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
           //JWT Token Authentication
           services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer( options =>{
            options.TokenValidationParameters=new TokenValidationParameters{
                ValidateIssuer=false,
                ValidateAudience=false,
                ValidateLifetime=true,
                ValidIssuer=Configuration["Jwt:Issuer"],
                ValidAudience=Configuration["Jwt:Audience"],
                IssuerSigningKey =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:key"]))
            };
            
            });


            // Configure DBContext with Postgresql
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(ConnectionString));

            services.AddTransient<ProductService>();
            services.AddTransient<UserService>();
            services.AddScoped<JwtUtil>();
 

            services.AddControllers();
            services.AddSwaggerDocumentation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            
                  app.UseSwaggerDocumentation();
            }

            app.UseCors(options => options.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            /*   ECommerceDBInitializer.Seed(app);*/

        }
    }


}
