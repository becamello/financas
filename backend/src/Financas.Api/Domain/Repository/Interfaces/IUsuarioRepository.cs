

using Financas.Api.Domain.Models;

namespace Financas.Api.Domain.Repository.Interfaces
{
    public interface IUsuarioRepository: IRepository<Usuario, long>
    {
         Task<Usuario?> Obter(string  email);
    }
}