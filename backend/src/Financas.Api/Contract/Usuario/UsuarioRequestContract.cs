
namespace Financas.Api.Contract.Usuario
{
    public class UsuarioRequestContract : UsuarioLoginRequestContract
    {
        public DateTime? DataInativacao { get; set; }
    }
}