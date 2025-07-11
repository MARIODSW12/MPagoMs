using MediatR;

namespace MPago.Domain.Events
{
    public class MPagoEliminadoEvent : INotification
    {
        public string IdMPago { get; set; }

        public MPagoEliminadoEvent(string idMPago)
        {
            IdMPago = idMPago;
        }
    }
}
