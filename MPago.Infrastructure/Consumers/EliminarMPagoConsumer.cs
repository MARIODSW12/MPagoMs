using MassTransit;
using MongoDB.Bson;
using MPago.Domain.Events;
using MPago.Infrastructure.Interfaces;

namespace MPago.Infrastructure.Consumers
{
    public class EliminarMPagoConsumer
        (IServiceProvider serviceProvider, IMPagoReadRepository mPagoReadRepository) : IConsumer<MPagoEliminadoEvent>
    {
        private readonly IServiceProvider ServiceProvider = serviceProvider;
        private readonly IMPagoReadRepository MPagoReadRepository = mPagoReadRepository;

        public async Task Consume(ConsumeContext<MPagoEliminadoEvent> context)
        {
            try
            {
                var idMPago = context.Message.IdMPago;
                await MPagoReadRepository.EliminarMPago(idMPago);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
