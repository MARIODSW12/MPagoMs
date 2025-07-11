using MongoDB.Bson;
using MongoDB.Driver;
using MPago.Domain.Aggregates; 
using MPago.Domain.Interfaces;
using MPago.Domain.ValueObjects;
using MPago.Infrastructure.Configurations;

namespace MPago.Infrastructure.Persistences.Repositories.MongoWrite
{
    public class MPagoWriteRepository: IMPagoRepository
    {
        private readonly IMongoCollection<BsonDocument> MPagoColexion;

        public MPagoWriteRepository(MPagoWriteDbConfig mongoConfig)
        {
            MPagoColexion = mongoConfig.db.GetCollection<BsonDocument>("mpago_write");
        }

        public async Task<TarjetaCredito> ObtenerMPagoPorId(string idMPago)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", idMPago);
            var documento = await MPagoColexion.Find(filter).FirstOrDefaultAsync();
            if (documento == null) return null;
            return new TarjetaCredito
            (
                new VOIdMPago(documento["_id"].AsString), 
                new VOIdPostor(documento["idPostor"].AsString),
                new VOIdMPagoStripe(documento["idMPagoStripe"].AsString),
                new VOIdClienteStripe(documento["idClienteStripe"].AsString),
                new VOMarca(documento["marca"].AsString),
                new VOMesExpiracion(documento["mesExpiracion"].AsInt32),
                new VOAnioExpiracion(documento["anioExpiracion"].AsInt32),
                new VOUltimos4(documento["ultimos4"].AsString),
                new VOFechaRegistro(documento["fechaRegistro"].ToLocalTime()),
                new VOPredeterminado(documento["predeterminado"].AsBoolean)
            );
        }

        public async Task<List<TarjetaCredito>> ObtenerMPagoPorIdPostor(string idPostor)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("idPostor", idPostor);
            var documentos = await MPagoColexion.Find(filter).ToListAsync();
            var mPagos = new List<TarjetaCredito>();
            foreach (var documento in documentos)
            {
                mPagos.Add(new TarjetaCredito
                (
                    new VOIdMPago(documento["_id"].AsString),
                    new VOIdPostor(documento["idPostor"].AsString),
                    new VOIdMPagoStripe(documento["idMPagoStripe"].AsString),
                    new VOIdClienteStripe(documento["idClienteStripe"].AsString),
                    new VOMarca(documento["marca"].AsString),
                    new VOMesExpiracion(documento["mesExpiracion"].AsInt32),
                    new VOAnioExpiracion(documento["anioExpiracion"].AsInt32),
                    new VOUltimos4(documento["ultimos4"].AsString),
                    new VOFechaRegistro(documento["fechaRegistro"].ToLocalTime()),
                    new VOPredeterminado(documento["predeterminado"].AsBoolean)
                ));
            }
            return mPagos;
        }

        public async Task<TarjetaCredito> AgregarMPago(TarjetaCredito mPago)
        {
            var documento = new BsonDocument
            {
                { "_id", mPago.IdMPago.IdMPago },
                { "idPostor", mPago.IdPostor.IdPostor },
                { "idMPagoStripe", mPago.IdMPagoStripe.IdMPagoStripe },
                { "idClienteStripe", mPago.IdClienteStripe.IdClienteStripe },
                { "marca", mPago.Marca.Marca },
                { "mesExpiracion", mPago.MesExpiracion.MesExpiracion },
                { "anioExpiracion", mPago.AnioExpiracion.AnioExpiracion },
                { "cvc", mPago.Ultimos4.Ultimos4 },
                { "fechaRegistro", mPago.FechaRegistro.FechaRegistro.ToLocalTime() },
                { "predeterminado", mPago.Predeterminado.Predeterminado }
            };
            await MPagoColexion.InsertOneAsync(documento);
            return mPago;
        }

        public async Task ActualizarPredeterminadoTrueMPago(string idMPago)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", idMPago);
            var update = Builders<BsonDocument>.Update.Set("predeterminado", true);
            await MPagoColexion.UpdateOneAsync(filter, update);
        }

        public async Task ActualizarPredeterminadoFalseMPago(string idMPago)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", idMPago);
            var update = Builders<BsonDocument>.Update.Set("predeterminado", false);
            await MPagoColexion.UpdateOneAsync(filter, update);
        }

        public async Task EliminarMPago(string idMPago)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", idMPago);
            await MPagoColexion.DeleteOneAsync(filter);
        }
    }
}
