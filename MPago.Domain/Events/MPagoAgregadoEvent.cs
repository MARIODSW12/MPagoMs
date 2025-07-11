using MediatR;

namespace MPago.Domain.Events
{
    public class MPagoAgregadoEvent : INotification
    {
        public string IdMPago { get; set; }
        public string IdPostor { get; set; }
        public string IdMPagoStripe { get; set; }
        public string IdClienteStripe { get; set; }
        public string Marca { get; set; }
        public int MesExpiracion { get; set; }
        public int AnioExpiracion { get; set; }
        public string Ultimos4 { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Predeterminado { get; set; }

           public MPagoAgregadoEvent(string idMPago, string idPostor, string idMPagoStripe, 
                                    string marca, int mesExpiracion, int anioExpiracion, 
                                    string ultimos4, DateTime fechaRegistro, bool predeterminado, string idClienteStripe)
            {
                IdMPago = idMPago;
                IdPostor = idPostor;
                IdMPagoStripe = idMPagoStripe;
                Marca = marca;
                MesExpiracion = mesExpiracion;
                AnioExpiracion = anioExpiracion;
                Ultimos4 = ultimos4;
                FechaRegistro = fechaRegistro;
                Predeterminado = predeterminado;
                IdClienteStripe = idClienteStripe;
        }
    }
}
