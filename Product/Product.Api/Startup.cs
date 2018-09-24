using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Api.Common.Infrastructure.Persistence.NHibernate;
using Product.Api.Product.Application.Assembler;
using Product.Api.Product.Domain.Repository;
using Product.Api.Product.Infrastructure.Persistence.NHibernate.Repository;

namespace Product.Api
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
            services.AddAutoMapper();
            services.AddMvc();
            services.AddSingleton(new SessionFactory(Environment.GetEnvironmentVariable("MYSQL_CONECTION_STRING")));
            var serviceProvider = services.BuildServiceProvider();
            var mapper = serviceProvider.GetService<IMapper>();
            services.AddSingleton(new ProductCreateAssembler(mapper));
            services.AddScoped<UnitOfWorkNHibernate>();
            services.AddTransient<IProductRepository, ProductNHibernateRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
