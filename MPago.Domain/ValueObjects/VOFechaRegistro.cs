
namespace MPago.Domain.ValueObjects
{
    public class VOFechaRegistro
    {
        public DateTime FechaRegistro { get; private set; }
        public VOFechaRegistro(DateTime fechaRegistro)
        {
            if (fechaRegistro > DateTime.UtcNow.AddMinutes(2))
                throw new ArgumentException("La fecha de registro no puede ser en el futuro.");

            FechaRegistro = fechaRegistro;
        }
    }
}
