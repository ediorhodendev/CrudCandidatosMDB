using System;
using System.Threading.Tasks;
using CrudCandidatos.Domain.Models;
using CrudCandidatos.Infrastructure.Data;
using CrudCandidatos.Infrastructure.Interfaces;
using CrudCandidatos.Infrastructure.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using Xunit;

namespace CrudCandidatos.Tests.Repositories
{
    public class CandidatoRepositoryTests : IDisposable
    {
        private readonly ICandidatoRepository _candidatoRepository;
        private readonly IMongoCollection<Candidato> _candidatosCollection;

        public CandidatoRepositoryTests()
        {
            var connectionString = "mongodb://root:totus123@localhost:27017"; 
            var databaseName = "candidato-api-mdb"; 

            var context = new AppDbContext(connectionString, databaseName);
            _candidatoRepository = new CandidatoRepository(context);
            _candidatosCollection = context.Candidatos;
        }

        [Fact]
        public async Task InserirCandidato_DeveInserirComSucesso()
        {
            // Arrange
            var novoCandidato = new Candidato
            {
                Nome = "João",
                Email = "joao@example.com",
                Cpf = "123.456.789-00"
            };

            // Act
            var idInserido = await _candidatoRepository.CriarCandidatoAsync(novoCandidato);
            var candidatoInserido = await _candidatoRepository.ObterCandidatoPorIdAsync(idInserido);

            // Assert
            Assert.NotNull(candidatoInserido);
            Assert.Equal(novoCandidato.Nome, candidatoInserido.Nome);
            Assert.Equal(novoCandidato.Email, candidatoInserido.Email);
            Assert.Equal(novoCandidato.Cpf, candidatoInserido.Cpf);

            // Limpar os dados de teste após o teste
            await _candidatoRepository.DeletarCandidatoAsync(idInserido);
        }

        [Fact]
        public async Task EditarCandidato_DeveEditarComSucesso()
        {
            // Arrange
            var candidatoExistente = new Candidato
            {
                Nome = "Carlos",
                Email = "carlos@example.com",
                Cpf = "555.555.555-55"
            };

            // Inserir um candidato de teste
            var idInserido = await _candidatoRepository.CriarCandidatoAsync(candidatoExistente);

            // Modificar os dados do candidato
            candidatoExistente.Nome = "NovoNome";

            // Act
            await _candidatoRepository.AtualizarCandidatoAsync(idInserido, candidatoExistente);
            var candidatoAtualizado = await _candidatoRepository.ObterCandidatoPorIdAsync(idInserido);

            // Assert
            Assert.NotNull(candidatoAtualizado);
            Assert.Equal(candidatoExistente.Nome, candidatoAtualizado.Nome);

            // Limpar os dados de teste após o teste
            await _candidatoRepository.DeletarCandidatoAsync(idInserido);
        }

        [Fact]
        public async Task ExcluirCandidato_DeveExcluirComSucesso()
        {
            // Arrange
            var candidatoParaExcluir = new Candidato
            {
                Nome = "ParaExcluir",
                Email = "excluir@example.com",
                Cpf = "987.654.321-00"
            };

            // Inserir um candidato de teste
            var idInserido = await _candidatoRepository.CriarCandidatoAsync(candidatoParaExcluir);

            // Act
            await _candidatoRepository.DeletarCandidatoAsync(idInserido);
            var candidatoExcluido = await _candidatoRepository.ObterCandidatoPorIdAsync(idInserido);

            // Assert
            Assert.Null(candidatoExcluido);
        }

        public void Dispose()
        {
            // Limpar quaisquer dados de teste adicionais, se necessário
            var filter = Builders<Candidato>.Filter.Regex(c => c.Nome, new BsonRegularExpression("Teste.*", "i")); // Filtrar candidatos de teste
            _candidatosCollection.DeleteMany(filter);
        }
    }
}
