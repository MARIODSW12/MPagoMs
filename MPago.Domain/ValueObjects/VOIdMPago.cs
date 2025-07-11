
using MPago.Domain.Aggregates;

namespace MPago.Domain.ValueObjects
{
    public class VOIdMPago
    {
        public string IdMPago { get; private set; }
        public VOIdMPago (string idMPago)
        {
            if (string.IsNullOrWhiteSpace(idMPago))
                throw new ArgumentException("El ID del metodo de pago no puede estar vacío.");

            if (!Guid.TryParse(idMPago, out _))
                throw new ArgumentException("El ID del metodo de pago debe ser un GUID válido.");

            IdMPago = idMPago;
        }
    }
}
