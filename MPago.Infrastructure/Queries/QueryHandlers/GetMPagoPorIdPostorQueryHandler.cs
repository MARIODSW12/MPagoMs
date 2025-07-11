using MediatR;
using MPago.Application.DTOs;
using MPago.Domain.ValueObjects;
using MPago.Infrastructure.Interfaces;

namespace MPago.Infrastructure.Queries.QueryHandlers
{
    public class GetMPagoPorIdPostorQueryHandler : IRequestHandler<GetMPagoPorIdPostorQuery, List<MPagoDto>>
    {
        private readonly IMPagoReadRepository MPagoReadRepository;

        public GetMPagoPorIdPostorQueryHandler(IMPagoReadRepository mPagoReadRepository)
        {
            MPagoReadRepository = mPagoReadRepository;
        }

           public async Task<List<MPagoDto>> Handle(GetMPagoPorIdPostorQuery IdPostor, CancellationToken cancellationToken)
            {
                try
                {
                    var mpagos = await MPagoReadRepository.GetMPagoPorIdPostor(IdPostor.IdPostor);
    
                    if (mpagos == null || !mpagos.Any())
                    {
                        return new List<MPagoDto>();
                    }
    
                    var listaMPagos = mpagos.Select(mp => new MPagoDto
                    {
                        IdMPago = mp["_id"].ToString(),
                        IdPostor = mp["idPostor"].ToString(),
                        IdMPagoStripe = mp["idMPagoStripe"].ToString(),
                        IdClienteStripe = mp["idClienteStripe"].ToString(),
                        Marca = mp["marca"].ToString(),
                        MesExpiracion = mp["mesExpiracion"].AsInt32,
                        AnioExpiracion = mp["anioExpiracion"].AsInt32,
                        Ultimos4 = mp["ultimos4"].ToString(),
                        FechaRegistro = mp["fechaRegistro"].ToLocalTime(),
                        Predeterminado = mp["predeterminado"].AsBoolean
                    }).ToList();

                    return listaMPagos;
                }
                catch (Exception ex)
                {
                    throw;
                }
        }
    }
}
