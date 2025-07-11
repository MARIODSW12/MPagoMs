using MongoDB.Bson;
using MPago.Domain.Aggregates;

namespace MPago.Infrastructure.Interfaces
{
    public interface IMPagoReadRepository
    {
        Task<BsonDocument> GetMPagoPorId(string idMPago);
        Task<List<BsonDocument>> GetMPagoPorIdPostor(string idPostor);
        Task<List<BsonDocument>> GetTodosMPago();
        Task AgregarMPago(BsonDocument mpago);
        Task EliminarMPago(string idMPago);
        Task ActualizarPredeterminadoMPago(string idMPago, bool predeterminado);
    }
}
