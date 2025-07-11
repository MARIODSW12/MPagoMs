
using MassTransit;
using MediatR;
using MPago.Domain.Events;
using MPago.Domain.Interfaces;

namespace MPago.Application.Commands.CommandHandlers
{
    public class MPagoPredeterminadoCommandHandler : IRequestHandler<MPagoPredeterminadoCommand, bool>
    {
        private readonly IMediator Mediator;
        private readonly ISendEndpointProvider PublishEndpoint;
        private readonly IMPagoRepository MPagoRepository;

        public MPagoPredeterminadoCommandHandler(IMediator mediator, IMPagoRepository mPagoRepository, ISendEndpointProvider publishEndpoint)
        {
            Mediator = mediator;
            MPagoRepository = mPagoRepository;
            PublishEndpoint = publishEndpoint;
        }

        public async Task<bool> Handle(MPagoPredeterminadoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Verificar si el MPago existe
                var mPago = await MPagoRepository.ObtenerMPagoPorId(request.IdMPago);
                if (mPago == null)
                {
                    throw new InvalidOperationException("El MPago no existe.");
                }

                var MPagosPostor = await MPagoRepository.ObtenerMPagoPorIdPostor(request.IdPostor);
                if (MPagosPostor == null || !MPagosPostor.Any())
                {
                    throw new InvalidOperationException("No se encontraron MPagos para el postor especificado.");
                }

                var actualPredeterminado = MPagosPostor
                    .FirstOrDefault(mp => mp.Predeterminado.Predeterminado && mp.IdMPago.IdMPago != request.IdMPago);

                if (actualPredeterminado != null)
                {
                    // Cambiar el MPago actual predeterminado a no predeterminado
                    await MPagoRepository.ActualizarPredeterminadoFalseMPago(actualPredeterminado.IdMPago.IdMPago);

                }

                // Establecer el MPago como predeterminado para el postor
                await MPagoRepository.ActualizarPredeterminadoTrueMPago(request.IdMPago);

                var mPagoActualizarPredeterminado = 
                    new MPagoPredeterminadoEvent(request.IdMPago,  actualPredeterminado.IdMPago.IdMPago);

                //await Mediator.Publish(mPagoActualizarPredeterminado);
                PublishEndpoint.Send(mPagoActualizarPredeterminado, cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al establecer el MPago como predeterminado.", ex);
            }
        }
    }
}
