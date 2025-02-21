
using AutoMapper;
using Financas.Api.Contract.Titulos;
using Financas.Api.Domain.Models;
using Financas.Api.Domain.Repository.Interfaces;
using Financas.Api.Domain.Services.Interfaces;
using Financas.Api.Exceptions;

namespace Financas.Api.Domain.Services.Classes
{
    public class TituloService : IService<TituloRequestContract, TituloResponseContract, long>

    {
        private readonly ITituloRepository _tituloRepository;
        private readonly INaturezaDeLancamentoRepository _naturezaDeLancamentoRepository;
        private readonly IMapper _mapper;

        public TituloService(
            ITituloRepository tituloRepository,
            INaturezaDeLancamentoRepository naturezaDeLancamentoRepository,
            IMapper mapper
            )
        {
            _tituloRepository = tituloRepository;
            _mapper = mapper;
            _naturezaDeLancamentoRepository = naturezaDeLancamentoRepository;
        }
        public async Task<TituloResponseContract> Adicionar(TituloRequestContract entidade, long idUsuario)
        {
            var naturezaDeLancamento = await _naturezaDeLancamentoRepository.Obter(entidade.IdNaturezaDeLancamento);
            if (naturezaDeLancamento == null || naturezaDeLancamento.IdUsuario != idUsuario)
            {
                throw new UnauthorizedException("Você não tem permissão para usar esta natureza de lançamento.");
            }

            var titulo = _mapper.Map<Titulo>(entidade);

            titulo.IdUsuario = idUsuario;
            titulo.DataCadastro = DateTime.Now;

            titulo = await _tituloRepository.Adicionar(titulo);

            return _mapper.Map<TituloResponseContract>(titulo);
        }

        public async Task<TituloResponseContract> Atualizar(long id, TituloRequestContract entidade, long idUsuario)
        {
            Titulo titulo = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            titulo.Descricao = entidade.Descricao;
            titulo.Observacao = entidade.Observacao;
            titulo.ValorOriginal = entidade.ValorOriginal;
            titulo.DataPagamento = entidade.DataPagamento;
            titulo.IdNaturezaDeLancamento = entidade.IdNaturezaDeLancamento;

            titulo = await _tituloRepository.Atualizar(titulo);

            return _mapper.Map<TituloResponseContract>(titulo);
        }

        private async Task<Titulo> ObterPorIdVinculadoAoIdUsuario(long id, long idUsuario)
        {
            var titulo = await _tituloRepository.Obter(id);

            if (titulo is null)
            {
                throw new NotFoundException($"Não foi encontrado nenhum título com o ID {id}.");
            }

            if (titulo.IdUsuario != idUsuario)
            {
                throw new UnauthorizedException("Você não tem permissão para acessar este título.");
            }

            return titulo;
        }

        public async Task Inativar(long id, long idUsuario)
        {
            Titulo titulo = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            await _tituloRepository.Deletar(titulo);
        }

        public async Task<IEnumerable<TituloResponseContract>> Obter(long idUsuario)
        {
            var titulosTitulo = await _tituloRepository.ObterPeloIdUsuario(idUsuario);

            return titulosTitulo.Select(t => _mapper.Map<TituloResponseContract>(t));
        }

        public async Task<TituloResponseContract> Obter(long id, long idUsuario)
        {
            Titulo Titulo = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            return _mapper.Map<TituloResponseContract>(Titulo);
        }
    }
}