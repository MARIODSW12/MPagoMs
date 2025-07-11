using MongoDB.Bson.Serialization.Attributes;
using MPago.Infrastructure.Configurations;

namespace MPago.Infrastructure.Persistences.Repositories.MongoRead.Documents
{
    public class MPagoDocument
    {
        [BsonId]
        [BsonElement("id")]
        public required string IdMPago { get; set; }

        [BsonElement("idPostor")]
        public required string IdPostor { get; set; }

        [BsonElement("idMPagoStripe")]
        public required string IdMPagoStripe { get; set; }

        [BsonElement("idClienteStripe")]
        public required string IdClienteStripe { get; set; }

        [BsonElement("marca")]
        public required string Marca { get; set; }

        [BsonElement("mesExpiracion")]
        public required int MesExpiracion { get; set; }

        [BsonElement("anioExpiracion")]
        public required int AnioExpiracion { get; set; }

        [BsonElement("ultimos4")]
        public required int Ultimos4 { get; set; }

        [BsonElement("fechaRegistro")]
        public required DateTime FechaRegistro { get; set; }

        [BsonElement("predeterminado")]
        public required bool Predeterminado { get; set; }
    }
}
