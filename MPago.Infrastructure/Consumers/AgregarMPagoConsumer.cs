using MassTransit;
using MongoDB.Bson;
using MPago.Domain.Events;
using MPago.Infrastructure.Interfaces;

namespace MPago.Infrastructure.Consumers
{
    public class AgregarMPagoConsumer
        (IServiceProvider serviceProvider, IMPagoReadRepository mPagoReadRepository) : IConsumer<MPagoAgregadoEvent>
    {
        private readonly IServiceProvider ServiceProvider = serviceProvider;
        private readonly IMPagoReadRepository MPagoReadRepository = mPagoReadRepository;

        public async Task Consume(ConsumeContext<MPagoAgregadoEvent> context)
        {
            try
            {
                var mpago = new BsonDocument
                {
                    { "_id", context.Message.IdMPago },
                    { "idPostor", context.Message.IdPostor },
                    { "idMPagoStripe", context.Message.IdMPagoStripe },
                    { "idClienteStripe", context.Message.IdClienteStripe },
                    { "ultimos4", context.Message.Ultimos4 },
                    { "marca", context.Message.Marca },
                    { "mesExpiracion", context.Message.MesExpiracion },
                    { "anioExpiracion", context.Message.AnioExpiracion },
                    { "fechaRegistro", context.Message.FechaRegistro },
                    { "predeterminado", context.Message.Predeterminado }
                };

                await MPagoReadRepository.AgregarMPago(mpago);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
