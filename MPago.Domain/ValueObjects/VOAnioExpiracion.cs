
namespace MPago.Domain.ValueObjects
{
    public class VOAnioExpiracion
    {
        public int AnioExpiracion { get; private set; }
        public VOAnioExpiracion(int anioExpiracion)
        {
            var anioActual = DateTime.UtcNow.Year;
            if (anioExpiracion < anioActual)
                throw new ArgumentException("El año de expiración debe ser mayor al actual.");

            AnioExpiracion = anioExpiracion;
        }

    }
}
