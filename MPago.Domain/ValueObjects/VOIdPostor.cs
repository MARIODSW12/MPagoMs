
namespace MPago.Domain.ValueObjects
{
    public class VOIdPostor
    {
        public string IdPostor { get; private set; }
        public VOIdPostor(string idPostor)
        {
            if (string.IsNullOrWhiteSpace(idPostor))
                throw new ArgumentException("El ID de usuario no puede estar vacío.");

            if (!Guid.TryParse(idPostor, out _))
                throw new ArgumentException("El ID de usuario debe ser un GUID válido.");

            IdPostor = idPostor;
        }
    }
}
