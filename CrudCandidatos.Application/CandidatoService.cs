using System.Text.RegularExpressions;
using CrudCandidatos.Application.Interfaces;
using CrudCandidatos.Domain.Models;
using CrudCandidatos.Infrastructure.Interfaces;

namespace CrudCandidatos.Application.Services
{
    public class CandidatoService : ICandidatoService
    {
        private readonly ICandidatoRepository _candidatoRepository;

        public CandidatoService(ICandidatoRepository candidatoRepository)
        {
            _candidatoRepository = candidatoRepository;
        }

        public async Task<IEnumerable<Candidato>> ListarCandidatosAsync()
        {
            return await _candidatoRepository.ListarCandidatosAsync();
        }

        public async Task<Candidato> ObterCandidatoPorIdAsync(string id)
        {
            return await _candidatoRepository.ObterCandidatoPorIdAsync(id);
        }

        public async Task<bool> VerificarCpfExisteAsync(string cpf)
        {
            return await _candidatoRepository.VerificarCpfExistenteAsync(cpf);
        }

        public async Task<string> CriarCandidatoAsync(Candidato candidato)
        {
            if (!IsValidCPF(candidato.Cpf))
            {
                throw new ArgumentException("CPF inválido.");
            }

            if (!IsValidEmail(candidato.Email))
            {
                throw new ArgumentException("Email inválido.");
            }

            return await _candidatoRepository.CriarCandidatoAsync(candidato);
        }
        public async Task<Candidato> AtualizarCandidatoAsync(string id, Candidato candidato)
        {
            var candidatoExistente = await ObterCandidatoExistente(id);

            candidatoExistente.Nome = candidato.Nome;
            candidatoExistente.Email = candidato.Email;
            candidatoExistente.Cpf = candidato.Cpf;

            await _candidatoRepository.AtualizarCandidatoAsync(id, candidatoExistente);

            
            return candidatoExistente;
        }


        public async Task<IEnumerable<Candidato>> BuscarCandidatosPorNomeAsync(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("O nome do Candidato não pode ser vazio ou nulo.");
            }

            return await _candidatoRepository.BuscarCandidatosPorNomeAsync(nome);
        }

        public async Task DeletarCandidatoAsync(string id)
        {
            var candidatoExistente = await ObterCandidatoExistente(id);
            await _candidatoRepository.DeletarCandidatoAsync(id);
        }

  



        private async Task<Candidato> ObterCandidatoExistente(string id)
        {
            var candidatoExistente = await _candidatoRepository.ObterCandidatoPorIdAsync(id);

            if (candidatoExistente == null)
            {
                throw new ArgumentException("Candidato não encontrado.");
            }

            return candidatoExistente;
        }

        private bool IsValidCPF(string cpf)
        {
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
            {
                return false;
            }

            int[] weights = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(cpf[i].ToString()) * weights[i];
            }
            int remainder = sum % 11;
            if ((remainder < 2 && int.Parse(cpf[9].ToString()) != 0) || (remainder >= 2 && int.Parse(cpf[9].ToString()) != 11 - remainder))
            {
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}
