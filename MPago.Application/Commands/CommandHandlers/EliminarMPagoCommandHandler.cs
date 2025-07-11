using MediatR;
using MPago.Domain.Interfaces;
using MPago.Domain.Events;

namespace MPago.Application.Commands.CommandHandlers
{
    public class EliminarMPagoCommandHandler : IRequestHandler<EliminarMPagoCommand, bool>
    {
        private readonly IMediator Mediator;
        private readonly IMPagoRepository MPagoRepository;
        private readonly IStripeService StripeService;

        public EliminarMPagoCommandHandler(IMediator mediator, IMPagoRepository mPagoRepository, IStripeService stripeService)
        {
            Mediator = mediator;
            MPagoRepository = mPagoRepository;
            StripeService = stripeService;
        }

        public async Task<bool> Handle(EliminarMPagoCommand idMpago, CancellationToken cancellationToken)
        {
            try
            {
                var mPago = await MPagoRepository.ObtenerMPagoPorId(idMpago.IdMPago);
                if (mPago == null)
                {
                    throw new InvalidOperationException("El MPago no existe.");
                }

                // Eliminar en Stripe
                await StripeService.EliminarMPago(mPago.IdClienteStripe.IdClienteStripe, mPago.IdMPagoStripe.IdMPagoStripe);

                // Eliminar en Mongo
                await MPagoRepository.EliminarMPago(idMpago.IdMPago);

                // Publicar evento de eliminación de producto
                var mPagoEliminado = new MPagoEliminadoEvent(mPago.IdMPago.IdMPago);

                await Mediator.Publish(mPagoEliminado);

                // Retornar true si la eliminación fue exitosa
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al eliminar el MPago.", ex);
            }
        }
    }
}
