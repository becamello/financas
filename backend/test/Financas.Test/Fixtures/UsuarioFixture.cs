using Financas.Api.Domain.Models;

namespace Financas.Test.Fixtures
{
    public static class UsuarioFixture
    {
        private const string SENHA_123456_COM_HASH = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92";
        public static Usuario CriarUsuarioValido() => new() { Email = "rebecaTeste@gmail.com", Senha = "123456" };
        public static Usuario CriarUsuarioValidoComHash() => new() { Email = "rebecaTeste@gmail.com", Senha = SENHA_123456_COM_HASH };
    }
}
