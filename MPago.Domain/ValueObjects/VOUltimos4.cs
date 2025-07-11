using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPago.Domain.ValueObjects
{
    public class VOUltimos4
    {
        public string Ultimos4 { get; private set; }

        public VOUltimos4(string ultimos4)
        {
            if (string.IsNullOrWhiteSpace(ultimos4))
                throw new ArgumentException("Los ultimos 4 digitos no puede estar vacíos.");

            if (ultimos4.Length < 4 || ultimos4.Length > 4)
                throw new ArgumentException("Los ultimos 4 digitos debe tener 4 dígitos.");

            if (!ultimos4.All(char.IsDigit))
                throw new ArgumentException("Los ultimos 4 digitos deben ser numéricos.");

            Ultimos4 = ultimos4;
        }
        //public override string ToString() => $"**** **** **** {UltimosCuatroDigitos}";
    }
}
