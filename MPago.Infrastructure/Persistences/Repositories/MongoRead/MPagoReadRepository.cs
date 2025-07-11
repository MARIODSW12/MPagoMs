using MongoDB.Bson;
using MongoDB.Driver;
using MPago.Infrastructure.Configurations;
using MPago.Infrastructure.Interfaces;
using MPago.Infrastructure.Persistences.Repositories.MongoRead.Documents;

namespace MPago.Infrastructure.Persistences.Repositories.MongoRead
{
    public class MPagoReadRepository : IMPagoReadRepository
    {
        private readonly IMongoCollection<BsonDocument> MPagoColexion;

        public MPagoReadRepository(MPagoReadDbConfig mongoConfig)
        {
            MPagoColexion = mongoConfig.db.GetCollection<BsonDocument>("mpago_read");
        }

        public async Task<BsonDocument> GetMPagoPorId(string idMPago)
        {
            try
            {
                var filtroIdMPago = Builders<BsonDocument>.Filter.Eq("_id", idMPago);

                var mpago = await MPagoColexion.Find(filtroIdMPago).FirstOrDefaultAsync();

                if (mpago == null)
                {
                    throw new Exception("No se encontró el MPago con el ID proporcionado.");
                }
                return mpago;
            }
            catch (MongoConnectionException ex)
            {
                throw new Exception("Error de conexión a la base de datos.", ex);
            }
            catch (MongoCommandException ex)
            {
                throw new Exception("Error al ejecutar el comando en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<BsonDocument>> GetMPagoPorIdPostor(string idPostor)
        {
            try
            {
                var filtroIdPostor = Builders<BsonDocument>.Filter.Eq("idPostor", idPostor);
                var mpagos = await MPagoColexion.Find(filtroIdPostor).ToListAsync();
                if (mpagos == null || mpagos.Count == 0)
                {
                    throw new Exception("No se encontraron MPagos para el ID de postor proporcionado.");
                }
                return mpagos;
            }
            catch (MongoConnectionException ex)
            {
                throw new Exception("Error de conexión a la base de datos.", ex);
            }
            catch (MongoCommandException ex)
            {
                throw new Exception("Error al ejecutar el comando en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<BsonDocument>> GetTodosMPago()
        {
            try
            {
                var mpagos = await MPagoColexion.Find(_ => true).ToListAsync();
                if (mpagos == null || mpagos.Count == 0)
                {
                    throw new Exception("No se encontraron MPagos en la base de datos.");
                }
                return mpagos;
            }
            catch (MongoConnectionException ex)
            {
                throw new Exception("Error de conexión a la base de datos.", ex);
            }
            catch (MongoCommandException ex)
            {
                throw new Exception("Error al ejecutar el comando en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task AgregarMPago(BsonDocument mpago)
        {
            try
            {
                await MPagoColexion.InsertOneAsync(mpago);
            }
            catch (MongoConnectionException ex)
            {
                throw new Exception("Error de conexión a la base de datos.", ex);
            }
            catch (MongoCommandException ex)
            {
                throw new Exception("Error al ejecutar el comando en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task EliminarMPago(string idMPago)
        {
            try
            {
                var filtroIdMPago = Builders<BsonDocument>.Filter.Eq("_id", idMPago);

                var mPagoEliminado = await MPagoColexion.DeleteOneAsync(filtroIdMPago);

                if (mPagoEliminado.DeletedCount == 0)
                {
                    throw new Exception("No se encontró el MPago con el ID proporcionado para eliminar.");
                }
            }
            catch (MongoConnectionException ex)
            {
                throw new Exception("Error de conexión a la base de datos.", ex);
            }
            catch (MongoCommandException ex)
            {
                throw new Exception("Error al ejecutar el comando en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task ActualizarPredeterminadoMPago(string idMPago, bool valor)
        {
            try
            {
                var filtroIdMPago = Builders<BsonDocument>.Filter.Eq("_id", idMPago);
                var actualizacion = Builders<BsonDocument>.Update.Set("predeterminado", valor);
                var resultado = await MPagoColexion.UpdateOneAsync(filtroIdMPago, actualizacion);
                if (resultado.ModifiedCount == 0)
                {
                    throw new Exception("No se pudo actualizar el MPago a predeterminado.");
                }
            }
            catch (MongoConnectionException ex)
            {
                throw new Exception("Error de conexión a la base de datos.", ex);
            }
            catch (MongoCommandException ex)
            {
                throw new Exception("Error al ejecutar el comando en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
