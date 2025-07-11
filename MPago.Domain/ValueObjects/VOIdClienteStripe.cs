
namespace MPago.Domain.ValueObjects
{
    public class VOIdClienteStripe
    {
        public string IdClienteStripe { get; private set; }
        public VOIdClienteStripe(string idClienteStripe)
        {
            if (string.IsNullOrWhiteSpace(idClienteStripe))
                throw new ArgumentException("El ID del cliente no puede estar vacío.");

            if (!idClienteStripe.StartsWith("cus_"))
                throw new ArgumentException("El ID del cliente debe comenzar con 'cus_'.");

            IdClienteStripe = idClienteStripe;
        }
    }
}
