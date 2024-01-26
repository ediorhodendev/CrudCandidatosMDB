
using CrudCandidatos.Domain.Models;

namespace CrudCandidatos.Application.Interfaces
{
    public interface ICandidatoService
    {
        Task<IEnumerable<Candidato>> ListarCandidatosAsync();
        Task<Candidato> ObterCandidatoPorIdAsync(string id);
        Task<bool> VerificarCpfExisteAsync(string cpf);

        Task<string> CriarCandidatoAsync(Candidato Candidato);
       
        Task<Candidato> AtualizarCandidatoAsync(string id, Candidato candidato);

        Task<IEnumerable<Candidato>> BuscarCandidatosPorNomeAsync(string nome); 
        Task DeletarCandidatoAsync(string id);


    }
}
