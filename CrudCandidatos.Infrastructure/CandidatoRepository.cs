
using CrudCandidatos.Domain.Models;
using CrudCandidatos.Infrastructure.Data;
using CrudCandidatos.Infrastructure.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CrudCandidatos.Infrastructure.Repositories
{
    public class CandidatoRepository : ICandidatoRepository
    {
        private readonly IMongoCollection<Candidato> _candidatosCollection;

        public CandidatoRepository(AppDbContext context)
        {
            _candidatosCollection = context.Candidatos;
        }

        public async Task<IEnumerable<Candidato>> ListarCandidatosAsync()
        {
            return await _candidatosCollection.Find(_ => true).ToListAsync();
        }


        public async Task<Candidato> ObterCandidatoPorIdAsync(string id)
        {
            return await _candidatosCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> VerificarCpfExistenteAsync(string cpf)
        {
            var candidato = await _candidatosCollection.Find(c => c.Cpf == cpf).FirstOrDefaultAsync();
            return candidato != null;
        }

        public async Task<IEnumerable<Candidato>> BuscarCandidatosPorNomeAsync(string nome)
        {
            var filter = Builders<Candidato>.Filter.Regex(c => c.Nome, new BsonRegularExpression(nome, "i"));
            return await _candidatosCollection.Find(filter).ToListAsync();
        }

        public async Task<string> CriarCandidatoAsync(Candidato candidato)
        {
            if (candidato == null)
            {
                throw new ArgumentNullException(nameof(candidato));
            }

            await _candidatosCollection.InsertOneAsync(candidato);
            return candidato.Id;
        }

        public async Task AtualizarCandidatoAsync(string id, Candidato candidato)
        {
            if (candidato == null)
            {
                throw new ArgumentNullException(nameof(candidato));
            }

            var filter = Builders<Candidato>.Filter.Eq(c => c.Id, id);
            await _candidatosCollection.ReplaceOneAsync(filter, candidato);
        }

        public async Task DeletarCandidatoAsync(string id)
        {
            var filter = Builders<Candidato>.Filter.Eq(c => c.Id, id);
            var result = await _candidatosCollection.DeleteOneAsync(filter);

            if (result.DeletedCount == 0)
            {
                throw new KeyNotFoundException("Candidato não encontrado.");
            }
        }
    }
}
