using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPago.Domain.Events.EventHandlers
{
    public class MPagoPredeterminadoEventHandler : INotificationHandler<MPagoPredeterminadoEvent>
    {
        private readonly ISendEndpointProvider PublishEndpoint;

        public MPagoPredeterminadoEventHandler(ISendEndpointProvider publishEndpoint)
        {
            PublishEndpoint = publishEndpoint;
        }

        public async Task Handle(MPagoPredeterminadoEvent MPagoPredeterminadoEvento, CancellationToken cancellationToken)
        {
            try
            {
                await PublishEndpoint.Send(MPagoPredeterminadoEvento, cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
