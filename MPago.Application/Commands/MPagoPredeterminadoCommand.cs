using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPago.Application.Commands
{
    public class MPagoPredeterminadoCommand : IRequest<bool>
    {
        public string IdMPago { get; set; }
        public string IdPostor { get; set; }
        public MPagoPredeterminadoCommand(string idMPago, string idPostor)
        {
            IdMPago = idMPago;
            IdPostor = idPostor;
        }
    }
}
