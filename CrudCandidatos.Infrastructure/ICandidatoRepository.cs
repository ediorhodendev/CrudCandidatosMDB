using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrudCandidatos.Domain.Models;

namespace CrudCandidatos.Infrastructure.Interfaces
{
    public interface ICandidatoRepository
    {
        /// <summary>
        /// Obtém uma lista de todos os Candidatos de forma assíncrona.
        /// </summary>
        /// <returns>Uma coleção de Candidatos.</returns>
        Task<IEnumerable<Candidato>> ListarCandidatosAsync();

        /// <summary>
        /// Obtém um Candidato por ID de forma assíncrona.
        /// </summary>
        /// <param name="id">O ID do Candidato a ser obtido.</param>
        /// <returns>O Candidato correspondente ao ID especificado.</returns>
        Task<Candidato> ObterCandidatoPorIdAsync(string id);

        /// <summary>
        /// Obtém um Candidato por CPF de forma assíncrona.
        /// </summary>
        /// <param name="cpf">O CPF do Candidato a ser obtido.</param>
        /// <returns>O Candidato correspondente ao CPF especificado.</returns>
        Task<bool> VerificarCpfExistenteAsync(string cpf);

        /// <summary>
        /// Cria um novo Candidato de forma assíncrona.
        /// </summary>
        /// <param name="candidato">O Candidato a ser criado.</param>
        /// <returns>O ID do Candidato criado.</returns>
        Task<string> CriarCandidatoAsync(Candidato candidato);

        /// <summary>
        /// Atualiza um Candidato existente de forma assíncrona.
        /// </summary>
        /// <param name="id">O ID do Candidato a ser atualizado.</param>
        /// <param name="candidato">O Candidato atualizado.</param>
        Task AtualizarCandidatoAsync(string id, Candidato candidato);

        /// <summary>
        /// Busca Candidatos por nome de forma assíncrona.
        /// </summary>
        /// <param name="nome">O nome do Candidato a ser buscado.</param>
        /// <returns>Uma coleção de Candidatos correspondentes ao nome especificado.</returns>
        Task<IEnumerable<Candidato>> BuscarCandidatosPorNomeAsync(string nome);

        /// <summary>
        /// Exclui um Candidato de forma assíncrona.
        /// </summary>
        /// <param name="id">O ID do Candidato a ser excluído.</param>
        Task DeletarCandidatoAsync(string id);
    }
}


