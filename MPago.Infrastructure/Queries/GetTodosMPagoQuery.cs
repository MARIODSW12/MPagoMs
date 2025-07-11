using MediatR;
using MPago.Application.DTOs;

namespace MPago.Infrastructure.Queries
{
    public class GetTodosMPagoQuery : IRequest<List<MPagoDto>>
    {
    }
}
