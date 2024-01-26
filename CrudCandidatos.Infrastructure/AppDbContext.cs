using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using CrudCandidatos.Domain.Models;

namespace CrudCandidatos.Infrastructure.Data
{
    public class AppDbContext
    {
        private readonly IMongoDatabase _database;

        public AppDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);

            RegisterClassMaps(); 
        }

        public IMongoCollection<Candidato> Candidatos => _database.GetCollection<Candidato>("Candidatos");

        private void RegisterClassMaps()
        {
            // Registre aqui quaisquer mapeamentos personalizados para suas classes de modelo, se necessário.
            BsonClassMap.RegisterClassMap<Candidato>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
            });
        }
    }
}
