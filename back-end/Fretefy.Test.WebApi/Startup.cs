using Fretefy.Test.Domain.Interfaces;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Fretefy.Test.Domain.Services;
using Fretefy.Test.Infra.EntityFramework;
using Fretefy.Test.Infra.EntityFramework.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Fretefy.Test.WebApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<DbContext, TestDbContext>();
            services.AddDbContext<TestDbContext>((provider, options) =>
            {
                options.UseSqlite("Data Source=Data\\Test.db");
            });

            ConfigureInfraService(services);
            ConfigureDomainService(services);

            services.AddMvc()
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest);

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Fretefy Teste API",
                    Version = "v1",
                    Description = "Swagger",
                    Contact = new OpenApiContact
                    {
                        Name = "Thales Gomes",
                        Email = "thales3238junior@gmail.com",
                    }
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });
        }

        private void ConfigureDomainService(IServiceCollection services)
        {
            services.AddScoped<ICidadeService, CidadeService>();
            services.AddScoped<IRegiaoService, RegiaoService>();
        }

        private void ConfigureInfraService(IServiceCollection services)
        {
            services.AddScoped<ICidadeRepository, CidadeRepository>();
            services.AddScoped<IRegiaoRepository, RegiaoRepository>();
            services.AddScoped<IRegiaoCidadeRepository, RegiaoCidadeRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
               // Ativar Swagger apenas no ambiente de desenvolvimento
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fretefy API v1");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseRouting();
            
            app.UseCors("AllowAll");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
