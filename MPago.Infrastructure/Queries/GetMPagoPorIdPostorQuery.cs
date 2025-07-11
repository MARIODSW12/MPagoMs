using MediatR;
using MPago.Application.DTOs;

namespace MPago.Infrastructure.Queries
{
    public class GetMPagoPorIdPostorQuery : IRequest<List<MPagoDto>>
    {
        public string IdPostor { get; set; }
        public GetMPagoPorIdPostorQuery(string idPostor)
        {
            IdPostor = idPostor;
        }
    }
}
