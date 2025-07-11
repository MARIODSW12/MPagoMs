using MongoDB.Driver;

namespace MPago.Infrastructure.Configurations
{
    public class MPagoWriteDbConfig
    {
        public MongoClient client;
        public IMongoDatabase db;

        public MPagoWriteDbConfig()
        {
            try
            {
                string connectionUri = Environment.GetEnvironmentVariable("MONGODB_CNN_WRITE");

                if (string.IsNullOrWhiteSpace(connectionUri))
                {
                    throw new Exception("La cadena de conexión a la base de datos no está configurada.");
                }

                var settings = MongoClientSettings.FromConnectionString(connectionUri);
                settings.ServerApi = new ServerApi(ServerApiVersion.V1);

                client = new MongoClient(settings);

                string databaseName = Environment.GetEnvironmentVariable("MONGODB_NAME_WRITE");
                if (string.IsNullOrWhiteSpace(databaseName))
                {
                    throw new Exception("El nombre de la base de datos no está configurado.");
                }

                db = client.GetDatabase(databaseName);
            }
            catch (MongoException ex)
            {
                throw new Exception("Error al conectar a la base de datos MongoDB.", ex);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
