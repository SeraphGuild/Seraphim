using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Seraphim.Storage;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SeraphimContext>((DbContextOptionsBuilder options) =>
{
    DefaultAzureCredential credential = new DefaultAzureCredential();
    SecretClient client = new SecretClient(new Uri("https://dscrduscekvdev.vault.azure.net/"), credential);
    KeyVaultSecret secret = client.GetSecret("SeraphimSqlAdminPassword");

    SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder()
    {
        DataSource = "tcp:dscrd-core-ceus-sqls.database.windows.net,1433",
        InitialCatalog = "dscrd-core-sqld-dev",
        PersistSecurityInfo = false,
        UserID = "Seraphim",
        Password = secret.Value,
        MultipleActiveResultSets = false,
        Encrypt = true,
        TrustServerCertificate = false,
        Authentication = SqlAuthenticationMethod.SqlPassword,
        ConnectTimeout = 30
    };

    options.UseSqlServer(connectionStringBuilder.ConnectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
