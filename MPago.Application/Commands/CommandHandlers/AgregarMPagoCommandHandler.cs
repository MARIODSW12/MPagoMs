using MediatR;
using MPago.Domain.Aggregates;
using MPago.Domain.Events;
using MPago.Domain.Interfaces;
using MPago.Domain.ValueObjects;
using Stripe;

namespace MPago.Application.Commands.CommandHandlers
{
    public class AgregarMPagoCommandHandler : IRequestHandler<AgregarMPagoCommand, string>
    {
        private readonly IMPagoRepository MPagoWriteRepository;
        private readonly IMediator Mediator;
        private readonly IStripeService StripeService;

        public AgregarMPagoCommandHandler(IMPagoRepository mPagoWriteRepository, IMediator mediator, IStripeService stripeService)
        {
            MPagoWriteRepository = mPagoWriteRepository ?? throw new ArgumentNullException(nameof(mPagoWriteRepository));
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            StripeService = stripeService;
        }

        public async Task<string> Handle(AgregarMPagoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mPagoStripeDto = request.MPagoStripe;
                if (string.IsNullOrWhiteSpace(mPagoStripeDto.IdMPagoStripe))
                {
                    throw new ArgumentException("El IdMPagoStripe es requerido.");
                }

                // Crear cliente en Stripe y asociar el método de pago
                var customer = await StripeService.CrearTokenCUS(mPagoStripeDto.CorreoPostor, mPagoStripeDto.IdMPagoStripe);
                // Obtener detalles del método de pago desde Stripe
                var paymentMethodService = new PaymentMethodService();
                var paymentMethod = await paymentMethodService.GetAsync(mPagoStripeDto.IdMPagoStripe);
                var mPago = new TarjetaCredito
                (
                    new VOIdMPago(Guid.NewGuid().ToString()),
                    new VOIdPostor(mPagoStripeDto.IdPostor),
                    new VOIdMPagoStripe(mPagoStripeDto.IdMPagoStripe),
                    new VOIdClienteStripe(customer.Id),
                    new VOMarca(paymentMethod.Card.Brand),
                    new VOMesExpiracion(((int)paymentMethod.Card.ExpMonth)),
                    new VOAnioExpiracion(((int)paymentMethod.Card.ExpYear)),
                    new VOUltimos4(paymentMethod.Card.Last4),
                    new VOFechaRegistro(DateTime.Now),
                    new VOPredeterminado(false)
                );

                await MPagoWriteRepository.AgregarMPago(mPago);

                // Publicar el evento de usuario creado
                var MPagoAgregadoEvento = new MPagoAgregadoEvent(
                    mPago.IdMPago.IdMPago,
                    mPago.IdPostor.IdPostor,
                    mPago.IdMPagoStripe.IdMPagoStripe,
                    mPago.Marca.Marca,
                    mPago.MesExpiracion.MesExpiracion,
                    mPago.AnioExpiracion.AnioExpiracion,
                    mPago.Ultimos4.Ultimos4,
                    mPago.FechaRegistro.FechaRegistro,
                    mPago.Predeterminado.Predeterminado,
                    mPago.IdClienteStripe.IdClienteStripe
                );

                await Mediator.Publish(MPagoAgregadoEvento);

                return mPago.IdMPago.IdMPago;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
