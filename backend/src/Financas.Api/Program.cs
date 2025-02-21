using System.Text;
using AutoMapper;
using Financas.Api.AutoMapper;
using Financas.Api.Contract.NaturezaDeLancamento;
using Financas.Api.Contract.Titulos;
using Financas.Api.Data;
using Financas.Api.Domain.Repository.Classes;
using Financas.Api.Domain.Repository.Interfaces;
using Financas.Api.Domain.Services.Classes;
using Financas.Api.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

ConfigurarServices(builder);

ConfigurarInjecaoDeDependencia(builder);

var app = builder.Build();

ConfigurarAplicacao(app);

app.Run();

// Metodo que configrua as injeções de dependencia do projeto.
static void ConfigurarInjecaoDeDependencia(WebApplicationBuilder builder)
{
    string? connectionString = builder.Configuration.GetConnectionString("PADRAO");

    builder.Services.AddDbContext<ApplicationContext>(options =>
     options.UseNpgsql(connectionString), ServiceLifetime.Transient, ServiceLifetime.Transient);

     var config = new MapperConfiguration(cfg => {
        cfg.AddProfile<UsuarioProfile>();
        cfg.AddProfile<NaturezaDeLancamentoProfile>();
        cfg.AddProfile<TituloProfile>();
    });

    IMapper mapper = config.CreateMapper();

    builder.Services
    .AddSingleton(builder.Configuration)
    .AddSingleton(builder.Environment)
    .AddSingleton(mapper)
    .AddScoped<TokenService>()
    .AddScoped<IUsuarioRepository, UsuarioRepository>()
    .AddScoped<IUsuarioService, UsuarioService>()
    .AddScoped<INaturezaDeLancamentoRepository, NaturezaDeLancamentoRepository>()
    .AddScoped<IService<NaturezaDeLancamentoRequestContract, NaturezaDeLancamentoResponseContract, long>, NaturezaDeLancamentoService>()
    .AddScoped<ITituloRepository, TituloRepository>()
    .AddScoped<IService<TituloRequestContract, TituloResponseContract, long>, TituloService>();
}

static object mapper(IServiceProvider provider)
{
    throw new NotImplementedException();
}

// Configura o serviços da API.
static void ConfigurarServices(WebApplicationBuilder builder)
{
    builder.Services
    .AddCors()
    .AddControllers().ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    }).AddNewtonsoftJson();

    builder.Services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JTW Authorization header using the Beaerer scheme (Example: 'Bearer 12345abcdef')",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });

        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Financas.Api", Version = "v1" });
    });

    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })

    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["KeySecret"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
}

// Configura os serviços na aplicação.
static void ConfigurarAplicacao(WebApplication app)
{
    // Configura o contexto do postgreSql para usar timestamp sem time zone.
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

    app.UseDeveloperExceptionPage()
        .UseRouting();  // A ordem é importante!

    app.UseSwagger()
        .UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Financas.Api v1");
            c.RoutePrefix = string.Empty;  // Garante que o Swagger será acessível na raiz (/)
        });

    // Adiciona a middleware de CORS
    app.UseCors(x => x
        .AllowAnyOrigin() // Permite todas as origens
        .AllowAnyMethod() // Permite todos os métodos
        .AllowAnyHeader()) // Permite todos os cabeçalhos
        .UseAuthentication();  // Necessário para autenticação de tokens

    app.UseAuthorization();  // IMPORTANTE: Coloque aqui!

    // A ordem de "UseAuthorization()" é essencial para garantir que a autorização seja verificada corretamente.
    app.UseEndpoints(endpoints => endpoints.MapControllers());  // Mapeia os endpoints dos controllers

    app.MapControllers();
}
