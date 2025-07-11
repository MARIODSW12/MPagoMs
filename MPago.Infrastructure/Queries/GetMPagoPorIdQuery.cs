using MediatR;
using MPago.Application.DTOs;

namespace MPago.Infrastructure.Queries
{
    public class GetMPagoPorIdQuery : IRequest<MPagoDto>
    {
        public string IdMPago { get; set; }
        public GetMPagoPorIdQuery(string idMPago)
        {
            IdMPago = idMPago;
        }
    }
}
