
namespace MPago.Domain.ValueObjects
{
    public class VOMarca
    {
        private static readonly HashSet<string> MarcasValidas = new()
        {"visa", "mastercard", "american express", "discover", "jcb", "diners club", "unionpay"};
        public string Marca { get; private set; }
        public VOMarca(string marca)
        {
            if (string.IsNullOrWhiteSpace(marca))
                throw new ArgumentException("La marca de la tarjeta no puede estar vacía.");

            if (!MarcasValidas.Contains(marca.ToLower()))
                throw new ArgumentException($"Marca de tarjeta no reconocida: '{marca}'.");

            Marca = marca;
        }

    }
}
