using AutoMapper;
using Financas.Api.Contract.NaturezaDeLancamento;
using Financas.Api.Domain.Models;
using Financas.Api.Domain.Repository.Interfaces;
using Financas.Api.Domain.Services.Interfaces;
using Financas.Api.Exceptions;

namespace Financas.Api.Domain.Services.Classes
{
    public class NaturezaDeLancamentoService : IService<NaturezaDeLancamentoRequestContract, NaturezaDeLancamentoResponseContract, long>
    {
        private readonly INaturezaDeLancamentoRepository _naturezaDeLancamentoRepository;

        private readonly IMapper _mapper;

        public NaturezaDeLancamentoService(
            INaturezaDeLancamentoRepository naturezaDeLancamentoRepository,
            IMapper mapper
            )
        {
            _naturezaDeLancamentoRepository = naturezaDeLancamentoRepository;
            _mapper = mapper;
        }

        public async Task<NaturezaDeLancamentoResponseContract> Adicionar(NaturezaDeLancamentoRequestContract entidade, long idUsuario)
        {
            var naturezaDeLancamento = _mapper.Map<NaturezaDeLancamento>(entidade);

            naturezaDeLancamento.DataCadastro = DateTime.Now;

            naturezaDeLancamento.IdUsuario = idUsuario;

            naturezaDeLancamento = await _naturezaDeLancamentoRepository.Adicionar(naturezaDeLancamento);

            return _mapper.Map<NaturezaDeLancamentoResponseContract>(naturezaDeLancamento);
        }

        public async Task<NaturezaDeLancamentoResponseContract> Atualizar(long id, NaturezaDeLancamentoRequestContract entidade, long idUsuario)
        {
            NaturezaDeLancamento naturezaDeLancamento = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            naturezaDeLancamento.Descricao = entidade.Descricao;
            naturezaDeLancamento.Observacao = entidade.Observacao;

            naturezaDeLancamento = await _naturezaDeLancamentoRepository.Atualizar(naturezaDeLancamento);

            return _mapper.Map<NaturezaDeLancamentoResponseContract>(naturezaDeLancamento);
        }

        public async Task Inativar(long id, long idUsuario)
        {
            NaturezaDeLancamento naturezaDeLancamento = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            await _naturezaDeLancamentoRepository.Deletar(naturezaDeLancamento);
        }

        public async Task<IEnumerable<NaturezaDeLancamentoResponseContract>> Obter(long idUsuario)
        {
            var naturezasDeLancamento = await _naturezaDeLancamentoRepository.ObterPeloIdUsuario(idUsuario);
            return naturezasDeLancamento.Select(n => _mapper.Map<NaturezaDeLancamentoResponseContract>(n));
        }

        public async Task<NaturezaDeLancamentoResponseContract> Obter(long id, long idUsuario)
        {
            NaturezaDeLancamento naturezaDeLancamento = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            return _mapper.Map<NaturezaDeLancamentoResponseContract>(naturezaDeLancamento);
        }

        private async Task<NaturezaDeLancamento> ObterPorIdVinculadoAoIdUsuario(long id, long idUsuario)
        {
            var naturezaDeLancamento = await _naturezaDeLancamentoRepository.Obter(id);

            if (naturezaDeLancamento is null)
            {
                throw new NotFoundException($"A natureza de lançamento com ID {id} não existe.");
            }

            if (naturezaDeLancamento.IdUsuario != idUsuario)
            {
                throw new UnauthorizedException("Você não tem permissão para acessar esta natureza de lançamento.");
            }

            return naturezaDeLancamento;
        }
    }
}