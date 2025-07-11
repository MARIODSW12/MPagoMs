using MassTransit;
using MediatR;

namespace MPago.Domain.Events.EventHandlers
{
    public class MPagoEliminadoEventHandler : INotificationHandler<MPagoEliminadoEvent>
    {
        private readonly ISendEndpointProvider PublishEndpoint;

        public MPagoEliminadoEventHandler(ISendEndpointProvider publishEndpoint)
        {
            PublishEndpoint = publishEndpoint;
        }

        public async Task Handle(MPagoEliminadoEvent MPagoEliminadoEvento, CancellationToken cancellationToken)
        {
            try
            {
                await PublishEndpoint.Send(MPagoEliminadoEvento, cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
