
using Stripe;

namespace MPago.Infrastructure.Interfaces
{
    public interface IStripeService
    {
        Task<Token> CrearTokenPM(string numeroTarjeta, int expMonth, int expYear, string cvc);
        Task<Customer> CrearTokenCUS(string email, string paymentMethodId);
        Task EliminarMPagoAsync(string customerId, string paymentMethodId);
    }
}
