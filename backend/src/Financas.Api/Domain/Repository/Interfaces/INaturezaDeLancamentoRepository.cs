using Financas.Api.Domain.Models;

namespace Financas.Api.Domain.Repository.Interfaces
{
    public interface INaturezaDeLancamentoRepository : IRepository<NaturezaDeLancamento, long>
    {
         Task<IEnumerable<NaturezaDeLancamento>> ObterPeloIdUsuario(long idUsuario);
    }
}