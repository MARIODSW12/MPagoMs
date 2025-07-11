using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPago.Domain.Events
{
    public class MPagoPredeterminadoEvent : INotification
    {
        public string IdMPagoTrue { get; set; }
        public string? IdMPagoFalse { get; set; }
        public MPagoPredeterminadoEvent(string idMPagoTrue, string? idMPagoFalse)
        {
            IdMPagoTrue = idMPagoTrue;
            IdMPagoFalse = idMPagoFalse;
        }
    }
}
