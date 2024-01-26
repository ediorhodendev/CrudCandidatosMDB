
using CrudCandidatos.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

namespace CrudCandidatos.Infrastructure
{
    public class DatabaseInitializer : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public DatabaseInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var mongoClient = scope.ServiceProvider.GetRequiredService<IMongoClient>();
                var databaseName = scope.ServiceProvider.GetRequiredService<string>();
                var collectionName = "Candidatos"; // Substitua pelo nome da coleção real, se necessário

                var database = mongoClient.GetDatabase(databaseName);
                var candidatosCollection = database.GetCollection<Candidato>(collectionName);

                // Verifica se a coleção de Candidatos já existe
                var candidatosExistem = await candidatosCollection.Find(_ => true).AnyAsync();

                if (!candidatosExistem)
                {
                    // Se a coleção estiver vazia, insere alguns candidatos iniciais
                    var candidatosIniciais = new[]
                    {
                new Candidato { Nome = "Candidato 1", Email = "candidato1@gmail.com", Cpf = "349.425.040-58" },
                new Candidato { Nome = "Candidato 2", Email = "candidato2@gmail.com", Cpf = "875.579.920-59" },
                new Candidato { Nome = "Candidato 3", Email = "candidato3@gmail.com", Cpf = "172.802.270-31" },
                new Candidato { Nome = "Candidato 4", Email = "candidato4@gmail.com", Cpf = "598.964.330-62" },
                new Candidato { Nome = "Candidato 5", Email = "candidato5@gmail.com", Cpf = "926.333.430-74" }
            };

                    await candidatosCollection.InsertManyAsync(candidatosIniciais);
                }

               
            }
        }




        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
