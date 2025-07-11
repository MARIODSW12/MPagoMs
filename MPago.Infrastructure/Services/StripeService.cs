using MPago.Domain.Interfaces;
using Stripe;
using System.Text.Json;

namespace MPago.Infrastructure.Services
{
    public class StripeService: IStripeService
    {
        public StripeService()
        {
            StripeConfiguration.ApiKey = Environment.GetEnvironmentVariable("STRIPE_SECRET_KEY");
        }

        public async Task<Token> CrearTokenPM(string numeroTarjeta, int expMonth, int expYear, string cvc)
        {
            var tokenOptions = new TokenCreateOptions
            {
                Card = new TokenCardOptions
                {
                    Number = numeroTarjeta,
                    ExpMonth = expMonth.ToString(),
                    ExpYear = expYear.ToString(),
                    Cvc = cvc
                }
            };

            var service = new TokenService();
            return await service.CreateAsync(tokenOptions);
        }

        public async Task<Customer> CrearTokenCUS(string email, string paymentMethodId)
        {
            // buscar si este metodo de pago ya tiene asociado un cliente
            var paymentMethodService = new PaymentMethodService();
            var existingPaymentMethod = await paymentMethodService.GetAsync(paymentMethodId);
            if (existingPaymentMethod == null) {
                throw new ArgumentException("El método de pago no existe.");
            }
            var customerService = new CustomerService();
            if (existingPaymentMethod.CustomerId != null)
            {
                return await customerService.GetAsync(existingPaymentMethod.CustomerId);
            }
            var customerOptions = new CustomerCreateOptions
            {
                Email = email,
                PaymentMethod = paymentMethodId,
                InvoiceSettings = new CustomerInvoiceSettingsOptions
                {
                    DefaultPaymentMethod = paymentMethodId
                }
            };

            return await customerService.CreateAsync(customerOptions);
        }

        public async Task EliminarMPago(string customerId, string paymentMethodId)
        {
            var paymentMethodService = new PaymentMethodService();

            await paymentMethodService.DetachAsync(paymentMethodId);
        }

    }
}