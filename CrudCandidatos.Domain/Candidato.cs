using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CrudCandidatos.Domain.Models
{
    public class Candidato
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Nome")]
        public string Nome { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("Cpf")]
        public string Cpf { get; set; }
    }
}
