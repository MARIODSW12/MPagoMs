using MediatR;

namespace MPago.Application.Commands
{
    public class EliminarMPagoCommand : IRequest<bool>
    {
        public string IdMPago { get; set; }
        public EliminarMPagoCommand(string idMPago)
        {
            IdMPago = idMPago;
            //?? throw new ArgumentNullException(nameof(idMPago))
        }
    }
}
