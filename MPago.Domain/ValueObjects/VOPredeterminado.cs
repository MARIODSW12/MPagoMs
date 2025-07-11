
namespace MPago.Domain.ValueObjects
{
    public class VOPredeterminado
    {
        public bool Predeterminado { get; private set; }
        public VOPredeterminado(bool predeterminado)
        {
            Predeterminado = predeterminado;
        }

    }
}
