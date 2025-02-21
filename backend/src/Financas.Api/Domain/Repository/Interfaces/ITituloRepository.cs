using Financas.Api.Domain.Models;

namespace Financas.Api.Domain.Repository.Interfaces
{
    public interface ITituloRepository : IRepository<Titulo, long>
    {
         Task<IEnumerable<Titulo>> ObterPeloIdUsuario(long idUsuario);
    }
}