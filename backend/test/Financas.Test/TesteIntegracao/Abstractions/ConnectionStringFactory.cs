namespace Financas.Test.IntegrationTests.Abstractions
{
    public static class ConnectionStringFactory
    {
        public static string Create() => $"User ID=postgres;Password=senhas;Host=localhost;Port=5432;Database=financas-{Guid.NewGuid()};Pooling = true";
    }
}
