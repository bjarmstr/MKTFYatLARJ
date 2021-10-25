using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MKTFY.API.Middleware;
using MKTFY.API.Swashbuckle;
using MKTFY.Repositories;
using MKTFY.Repositories.Repositories;
using MKTFY.Repositories.Repositories.Interfaces;
using MKTFY.Services;
using MKTFY.Services.Interfaces;
using System.IO;

namespace MKTFY.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureDependencyInjection(IServiceCollection services)
        {
            // Configure Dependency Injection
            services.AddScoped<IListingService, ListingService>();
            services.AddScoped<IListingRepository, ListingRepository>();
            services.AddScoped<IFAQService, FAQService>();
            services.AddScoped<IFAQRepository, FAQRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISearchRepository, SearchRepository>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(  // Connect to the Postgres database
                    Configuration.GetConnectionString("DefaultConnection"),
                    builder => {
                        //project where we want Code-First Migrations to reside
                        builder.MigrationsAssembly("MKTFY.Repositories");
                    
                    })
                );
            services.AddControllers();

            // Setup authentication
            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = Configuration.GetSection("Auth0").GetValue<string>("Domain");
                    options.Audience = Configuration.GetSection("Auth0").GetValue<string>("Audience");
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        RoleClaimType = "http://schemas.mktfy.com/roles"
                    };
                });


            ConfigureDependencyInjection(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MKTFY.API", Version = "v1" });
                var apiPath = Path.Combine(System.AppContext.BaseDirectory, "MKTFY.API.xml");
                var modelsPath = Path.Combine(System.AppContext.BaseDirectory, "MKTFY.Models.xml");
                c.IncludeXmlComments(apiPath);
                c.IncludeXmlComments(modelsPath);

                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer"
                });
                c.OperationFilter<AuthHeaderOperationFilter>();

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MKTFY.API v1"));
               
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<GlobalExceptionHandler>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
