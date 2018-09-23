namespace Cart.Api
{
    using AutoMapper;
    using Middleware;
    using Response;
    using Application.Service;
    using Domain.Repository;
    using Infrastructure.Repository;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Cors.Internal;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using StackExchange.Redis;
    using Swashbuckle.AspNetCore.Swagger;
    using System;
    using System.Collections.Generic;
    using System.Text;

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
            services.AddScoped<ICartService, CartService>();

            services.AddScoped<ICartRepository, RedisCartRepository>();

            services.Configure<CartSettings>(Configuration);

            services.AddSingleton<ConnectionMultiplexer>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<CartSettings>>().Value;
                var configuration = ConfigurationOptions.Parse(
                                    Environment.GetEnvironmentVariable("REDIS_CACHE_CONNECTION_STRING") 
                                    ?? settings.ConnectionString, true);
    
                return ConnectionMultiplexer.Connect(configuration);
            });

            services.AddDistributedRedisCache(options => {
                options.Configuration = Environment.GetEnvironmentVariable("REDIS_CACHE_CONNECTION_STRING") ?? Configuration.GetConnectionString("RedisConnection");
                options.InstanceName = Environment.GetEnvironmentVariable("REDIS_CACHE_INSTANCE_NAME") ?? Configuration["RedisCacheInstanceName"];
            });

            var tokenKey = Environment.GetEnvironmentVariable("JWT_TOKEN_KEY") ?? Configuration["Jwt:Key"];

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;

                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? Configuration["Jwt:Issuer"],
                    ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey))
                };
            });

            var allowSpecificOriginUrl = Environment.GetEnvironmentVariable("ALLOW_SPECIFIC_ORIGIN_URL") ?? Configuration["AllowSpecificOriginUrl"];
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder
                        .WithOrigins(allowSpecificOriginUrl)
                        .AllowAnyMethod()
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


                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowSpecificOrigin"));

            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "System API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });

                c.CustomSchemaIds(x => x.FullName);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("index.html");

            app.UseCors("AllowSpecificOrigin")//always berofe "UseMvc"
               .UseMiddleware(typeof(ErrorMiddleware))
               .UseMvc()
               .UseDefaultFiles(options)
               .UseStaticFiles()
               .UseSwagger()
               .UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
        }
    }
}
