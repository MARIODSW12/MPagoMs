
namespace MPago.Domain.ValueObjects
{
    public class VOMesExpiracion
    {
        public int MesExpiracion { get; private set; }
        public VOMesExpiracion(int mesExpiracion)
        {
            if (mesExpiracion < 1 || mesExpiracion > 12)
                throw new ArgumentException("El mes de expiración debe estar entre 1 y 12.");

            MesExpiracion = mesExpiracion;
        }

    }
}
