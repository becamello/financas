using Financas.Api.Data;
using Financas.Api.Domain.Services.Classes;
using Microsoft.EntityFrameworkCore;

public class DbInitializer
{
    public static void Initialize(ApplicationContext context, UsuarioService usuarioService)
    {
        // Aplica as migrations
        context.Database.Migrate();

        // Verifica se já existe o usuário admin
        var exists = context.Usuario.Any(u => u.Email == "admin@financas.com");
        if (!exists)
        {
            // Gera o hash da senha antes de inserir
            string senhaHash = usuarioService.GerarHashSenha("admin123");

            // Executa a query SQL diretamente no banco
            context.Database.ExecuteSqlRaw(@"
                INSERT INTO usuario (""Email"", ""Senha"", ""Role"", ""DataCadastro"") 
                VALUES ('admin@financas.com', {0}, '0', NOW())", senhaHash);
        }
    }
}
