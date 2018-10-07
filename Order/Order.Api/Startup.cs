namespace Order.Api
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Swashbuckle.AspNetCore.Swagger;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AutoMapper;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Cors.Internal;
    using Microsoft.IdentityModel.Tokens;
    using Middleware;
    using Response;
    using Autofac;
    using MassTransit;
    using RabbitMQ.Client;
    using MassTransit.Util;
    using Autofac.Extensions.DependencyInjection;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
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
                    builderCors => builderCors
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

            var builder = new ContainerBuilder();
            builder.Register(c =>
                {
                    return Bus.Factory.CreateUsingRabbitMq(rmq =>
                    {
                        rmq.Host(new Uri(Environment.GetEnvironmentVariable("RABBITMQ_URL")), h =>
                        {
                            h.Username(Environment.GetEnvironmentVariable("RABBITMQ_USERNAME"));
                            h.Password(Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD"));
                        });
                        rmq.ExchangeType = ExchangeType.Fanout;
                    });

                }).
                As<IBusControl>()
                .As<IBus>()
                .As<IPublishEndpoint>()
                .SingleInstance();

            builder.Populate(services);
            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
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

            var bus = ApplicationContainer.Resolve<IBusControl>();
            var busHandle = TaskUtil.Await(() => bus.StartAsync());
            lifetime.ApplicationStopping.Register(() => busHandle.Stop());
        }
    }
}
