using MediatR;
using MPago.Application.DTOs;
using MPago.Domain.ValueObjects;
using MPago.Infrastructure.Interfaces;

namespace MPago.Infrastructure.Queries.QueryHandlers
{
    public class GetMPagoPorIdQueryHandler : IRequestHandler<GetMPagoPorIdQuery, MPagoDto>
    {
        private readonly IMPagoReadRepository MPagoReadRepository;

        public GetMPagoPorIdQueryHandler(IMPagoReadRepository mPagoReadRepository)
        {
            MPagoReadRepository = mPagoReadRepository;
        }

        public async Task<MPagoDto> Handle(GetMPagoPorIdQuery idMPago, CancellationToken cancellationToken)
        {
            try
            {
                var mpago = await MPagoReadRepository.GetMPagoPorId(idMPago.IdMPago);

                if (mpago == null)
                {
                    return null;
                }

                var mpagoPorId = new MPagoDto
                {
                    IdMPago = mpago["_id"].ToString(),
                    IdPostor = mpago["idPostor"].ToString(),
                    IdMPagoStripe = mpago["idMPagoStripe"].ToString(),
                    IdClienteStripe = mpago["idClienteStripe"].ToString(),
                    Marca = mpago["marca"].ToString(),
                    MesExpiracion = mpago["mesExpiracion"].AsInt32,
                    AnioExpiracion = mpago["anioExpiracion"].AsInt32,
                    Ultimos4 = mpago["ultimos4"].ToString(),
                    FechaRegistro = mpago["fechaRegistro"].ToLocalTime(),
                    Predeterminado = mpago["predeterminado"].AsBoolean
                };

                return mpagoPorId;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}