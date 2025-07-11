using MassTransit;
using MPago.Domain.Events;
using MPago.Domain.ValueObjects;
using MPago.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPago.Infrastructure.Consumers
{
    public class MPagoPredeterminadoConsumer
        (IServiceProvider serviceProvider, IMPagoReadRepository mPagoReadRepository) : IConsumer<MPagoPredeterminadoEvent>
    {
        private readonly IServiceProvider ServiceProvider = serviceProvider;
        private readonly IMPagoReadRepository MPagoReadRepository = mPagoReadRepository;

        public async Task Consume(ConsumeContext<MPagoPredeterminadoEvent> context)
        {
            try
            {
                var idMPagoF = context.Message.IdMPagoFalse;
                if (! string.IsNullOrEmpty(idMPagoF))
                {
                    await MPagoReadRepository.ActualizarPredeterminadoMPago(idMPagoF, false);
                }

                var idMPagoT = context.Message.IdMPagoTrue;
                await MPagoReadRepository.ActualizarPredeterminadoMPago(idMPagoT, true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
