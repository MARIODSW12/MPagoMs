using MediatR;
using MPago.Application.DTOs;

namespace MPago.Application.Commands
{
    public class AgregarMPagoCommand: IRequest<String>
    {
        public AgregarMPagoStripeDto MPagoStripe { get; set; }
        public AgregarMPagoCommand(AgregarMPagoStripeDto mPagoStripe)
        {
            MPagoStripe = mPagoStripe;
            //?? throw new ArgumentNullException(nameof(mPago))
        }
    }
}
