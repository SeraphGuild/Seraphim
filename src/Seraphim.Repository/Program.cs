using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using Seraphim.Repository;

const string SqlServerPasswordName = "SeraphimSqlAdminPassword";

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigureDependencyContainer(builder.Services);

WebApplication app = builder.Build();
ConfigureWebApp(app);

static void ConfigureDependencyContainer(IServiceCollection container)
{
    container.AddControllers();
    container.AddEndpointsApiExplorer();
    container.AddSwaggerGen();
    container.AddDbContext<SeraphimContext>((DbContextOptionsBuilder options) =>
    {
        KeyVaultSecret sqlServerPasswordSecret = GetSecret(SqlServerPasswordName);
        string connectionString = GetDatabaseConnectionString(sqlServerPasswordSecret);
        options.UseSqlServer(connectionString);
    });
}

static KeyVaultSecret GetSecret(string secretName)
{
    DefaultAzureCredential credential = new DefaultAzureCredential();
    SecretClient client = new SecretClient(new Uri("https://srphmuscekvdev.vault.azure.net/"), credential);
    return client.GetSecret(secretName);
}

static string GetDatabaseConnectionString(KeyVaultSecret sqlAuthPasswordSecret)
{
    return new SqlConnectionStringBuilder()
    {
        DataSource = "tcp:srphm-data-ceus-sqls.database.windows.net,1433",
        InitialCatalog = "srphm-data-ceus-sqld-dev",
        PersistSecurityInfo = false,
        UserID = "Seraphim",
        Password = sqlAuthPasswordSecret.Value,
        MultipleActiveResultSets = false,
        Encrypt = true,
        TrustServerCertificate = false,
        Authentication = SqlAuthenticationMethod.SqlPassword,
        ConnectTimeout = 30
    }.ConnectionString;
}

static void ConfigureWebApp(WebApplication app)
{
    // While in a development environment, automatically apply migrations and enable swagger documentation
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        using IServiceScope scope = app.Services.CreateScope();
        SeraphimContext seraphimContext = scope.ServiceProvider.GetRequiredService<SeraphimContext>();
        seraphimContext.Database.Migrate();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}