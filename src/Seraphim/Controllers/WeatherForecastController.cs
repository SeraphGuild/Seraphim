using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Seraphim.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet(Name = "WeatherForecast")]
        public async Task<object> Get()
        {
            DefaultAzureCredential credential = new DefaultAzureCredential();
            SecretClient client = new SecretClient(new Uri("https://dscrduscekvdev.vault.azure.net/"), credential);
            KeyVaultSecret secret = await client.GetSecretAsync("SeraphimSqlAdminPassword");

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
            
            using SqlConnection conn = new SqlConnection(connectionStringBuilder.ConnectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "";

            SqlDataReader reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return reader.GetValues(new object[reader.FieldCount]);
            }

            return Array.Empty<object>();
        }

        [HttpPost(Name = "WeatherForecast")]
        public string Post()
        {
            return "Hello World";
        }
    }
}