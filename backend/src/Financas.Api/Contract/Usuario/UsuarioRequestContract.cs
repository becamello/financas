
using System.ComponentModel.DataAnnotations;
using Financas.Api.Domain.Enums;

namespace Financas.Api.Contract.Usuario
{
    public class UsuarioRequestContract : UsuarioLoginRequestContract
    {
        public DateTime? DataInativacao { get; set; }

        [Required]
        public RoleEnum Role { get; set; }
    }
}