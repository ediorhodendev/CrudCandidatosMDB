using Microsoft.OpenApi.Models;
using CrudCandidatos.Application.Interfaces;
using CrudCandidatos.Application.Services;
using CrudCandidatos.Infrastructure.Interfaces;
using CrudCandidatos.Infrastructure.Repositories;
using CrudCandidatos.Infrastructure.Data;
using MongoDB.Driver;

using CrudCandidatos.Infrastructure;

namespace CrudCandidatos.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            

            // Configuração do MongoDB
            var mongoConnectionString = Configuration.GetConnectionString("MongoDBConnection");
            services.AddSingleton<IMongoClient>(new MongoClient(mongoConnectionString));

            var databaseName = Configuration.GetConnectionString("MongoDBDatabaseName");
            services.AddSingleton(databaseName);


            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:4200")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });



            // Registra AppDbContext no contêiner de injeção de dependência
            services.AddSingleton(new AppDbContext(mongoConnectionString, databaseName));

            // Configuração de Repositórios e Serviços
            services.AddScoped<ICandidatoRepository, CandidatoRepository>();
            services.AddScoped<ICandidatoService, CandidatoService>();

            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CrudCandidatos API", Version = "v1" });
            });

            services.AddControllers();

            // Inicializa o banco
            services.AddTransient<IHostedService, DatabaseInitializer>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CrudCandidatos API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseCors("AllowLocalhost4200");

            app.UseCors("AllowSpecificOrigin");


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
