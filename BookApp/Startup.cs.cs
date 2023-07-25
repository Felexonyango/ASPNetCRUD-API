using BookApp.Context;
using BookApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Text;
using BookApp.auth;
using BookApp.Extensions;
using Mapster;
using BookApp.midleware;

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
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:key"]))
                };

            });

            MapsterConfig.Configure(); // Call the method to register the mappings
          


            // Configure DBContext with Postgresql
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(ConnectionString));
            services.AddHttpContextAccessor();
            services.AddTransient<ProductService>();
            services.AddTransient<UserService>();
            services.AddTransient<PostService>();
            services.AddTransient<CommentService>();
            services.AddScoped<JwtUtil>();
// ***** Uncomment the line below only if you are using second approach *****
     // builder.Services.AddTransient<Exceptions>();

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

            app.UseCors(
            options =>
            options.WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
           // This line is common for both approaches
          app.UseMiddleware<Exceptions>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            /*   ECommerceDBInitializer.Seed(app);*/

        }
    }


}
