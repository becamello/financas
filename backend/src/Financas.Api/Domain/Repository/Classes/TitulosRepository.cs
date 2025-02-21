using Financas.Api.Data;
using Financas.Api.Domain.Models;
using Financas.Api.Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Financas.Api.Domain.Repository.Classes
{
    public class TituloRepository : ITituloRepository
    {
        private readonly ApplicationContext _contexto;

        public TituloRepository(ApplicationContext contexto)
        {
            _contexto = contexto;
        }
        public async Task<Titulo> Adicionar(Titulo entidade)
        {
            await _contexto.Titulo.AddAsync(entidade);
            await _contexto.SaveChangesAsync();

            return entidade;
        }


        public async Task<Titulo> Atualizar(Titulo entidade)
        {
            Titulo entidadeBanco = _contexto.Titulo
            .Where(u => u.Id == entidade.Id)
            .FirstOrDefault();

            _contexto.Entry(entidadeBanco).CurrentValues.SetValues(entidade);
            _contexto.Update<Titulo>(entidadeBanco);

            await _contexto.SaveChangesAsync();
            return entidadeBanco;
        }

        public async Task Deletar(Titulo entidade)
        {
            entidade.DataInativacao = DateTime.Now;
            await Atualizar(entidade);
        }


        public async Task<IEnumerable<Titulo>> Obter()
        {
            return await _contexto.Titulo.AsNoTracking()
            .OrderBy(n => n.Id)
            .ToListAsync();
        }

        public async Task<Titulo?> Obter(long id)
        {
            return await _contexto.Titulo.AsNoTracking()
            .Where(n => n.Id == id)
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Titulo>> ObterPeloIdUsuario(long idUsuario)
        {
            return await _contexto.Titulo.AsNoTracking()
            .Where(n => n.IdUsuario == idUsuario)
            .OrderBy(n => n.Id)
            .ToListAsync();
        }

    }
}