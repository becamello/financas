
using System.ComponentModel.DataAnnotations;

namespace Financas.Api.Contract.Usuario
{
    public class UsuarioLoginRequestContract
    {
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
        public string? Senha { get; set; }
    }
}