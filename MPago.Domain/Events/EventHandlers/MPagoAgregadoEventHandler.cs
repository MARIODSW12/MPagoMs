using MassTransit;
using MediatR;

namespace MPago.Domain.Events.EventHandlers
{
    public class MPagoAgregadoEventHandler : INotificationHandler<MPagoAgregadoEvent>
    {
        private readonly ISendEndpointProvider PublishEndpoint;

        public MPagoAgregadoEventHandler(ISendEndpointProvider publishEndpoint)
        {
            PublishEndpoint = publishEndpoint;
        }

        public async Task Handle(MPagoAgregadoEvent MPagoAgregadoEvento, CancellationToken cancellationToken)
        {
            try
            {
                await PublishEndpoint.Send(MPagoAgregadoEvento, cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
