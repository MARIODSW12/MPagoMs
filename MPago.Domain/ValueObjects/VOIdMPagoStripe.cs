
namespace MPago.Domain.ValueObjects
{
    public class VOIdMPagoStripe
    {
        public string IdMPagoStripe { get; private set; }
        public VOIdMPagoStripe(string idMPagoStripe)
        {
            if (string.IsNullOrWhiteSpace(idMPagoStripe))
                throw new ArgumentException("El ID del método de pago no puede estar vacío.");

            if (!idMPagoStripe.StartsWith("pm_"))
                throw new ArgumentException("El ID del método de pago debe comenzar con 'pm_'.");

            IdMPagoStripe = idMPagoStripe;
        }

    }
}
