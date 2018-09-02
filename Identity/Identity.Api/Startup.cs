namespace Identity.Api
{
    using System;
    using System.Security.Claims;
    using System.Text;
    using AutoMapper;
    using Middleware;
    using Response;
    using Domain.Entity;
    using Domain.Repository;
    using Infrastructure.Repositry;
    using Infrastructure.Initializer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Identity.Application.Service.Interfaces;
    using Identity.Application.Service.Implementations;

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
            var connectionString = Environment.GetEnvironmentVariable("SHOPPING_CART_CONECTION_STRING") ?? Configuration.GetConnectionString("SqlConnection");

            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(connectionString));
            var lockoutOptions = new LockoutOptions
            {
                AllowedForNewUsers = Convert.ToBoolean(Configuration["Account:UserLockoutEnabledByDefault"]),
                DefaultLockoutTimeSpan = TimeSpan.FromMinutes(double.Parse(Configuration["Account:DefaultAccountLockoutTimeSpan"])),
                MaxFailedAccessAttempts = Convert.ToInt32(Configuration["Account:MaxFailedAccessAttemptsBeforeLockout"])
            };

            services.AddIdentity<User, Role>(options =>
            {
                options.Lockout = lockoutOptions;
                options.SignIn.RequireConfirmedEmail = Convert.ToBoolean(Configuration["Account:RequireConfirmedEmail"]);
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = Convert.ToInt32(Configuration["Account:PasswordRequiredLength"]);

            }).AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(Convert.ToInt32(Configuration["Account:TokenExpirationTimeSpan"]));
            });

            services.AddScoped<IAuthApplicationService, AuthApplicationService>();
            services.AddScoped<IRoleApplicationService, RoleApplicationService>();
            services.AddScoped<IUserApplicationService, UserApplicationService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<DbInitializer>();

            services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy("Administrator", p => p.RequireClaim(ClaimTypes.Role, Roles.Admin));
                cfg.AddPolicy("Member", p => p.RequireClaim(ClaimTypes.Role, Roles.Member));
            });

            var tokenKey = Environment.GetEnvironmentVariable("TOKEN_KEY") ?? Configuration["Jwt:Key"];

            services.AddAuthentication().AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;

                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey))
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowFromAll",
                    builder => builder
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                        .AllowCredentials()
                        .AllowAnyHeader());
            });

            services.AddAutoMapper();
            services.AddMvc(options =>
            {
                options.Filters.Add(
                    new ProducesResponseTypeAttribute(typeof(ErrorResponse), 400));
                options.Filters.Add(
                    new ProducesResponseTypeAttribute(typeof(ErrorResponse), 401));
                options.Filters.Add(
                    new ProducesResponseTypeAttribute(typeof(ErrorResponse), 403));
                options.Filters.Add(
                    new ProducesResponseTypeAttribute(typeof(ErrorResponse), 404));
                options.Filters.Add(
                    new ProducesResponseTypeAttribute(typeof(ErrorResponse), 415));
                options.Filters.Add(
                    new ProducesResponseTypeAttribute(typeof(ErrorResponse), 500));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DbInitializer seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("index.html");

            app.UseCors("AllowFromAll")//always berofe "UseMvc"
                .UseMiddleware(typeof(ErrorMiddleware))
                .UseMvc()
                .UseDefaultFiles(options)
                .UseStaticFiles();

            if (env.IsDevelopment())
            {
                seeder.Seed().Wait();
            }
        }
    }
}
