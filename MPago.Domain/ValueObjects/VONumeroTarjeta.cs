using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPago.Domain.ValueObjects
{
    public class VONumeroTarjeta
    {
        public string NumeroTarjeta { get; private set; }
        public VONumeroTarjeta(string numeroTarjeta)
        {
            if (string.IsNullOrWhiteSpace(numeroTarjeta))
                throw new ArgumentException("El número de tarjeta no puede estar vacío.");

            numeroTarjeta = numeroTarjeta.Replace(" ", "").Replace("-", "");

            if (numeroTarjeta.Length < 13 || numeroTarjeta.Length > 19)
                throw new ArgumentException("El número de tarjeta debe tener entre 13 y 19 dígitos.");

            if (!long.TryParse(numeroTarjeta, out _))
                throw new ArgumentException("El número de tarjeta debe contener solo dígitos.");

            NumeroTarjeta = numeroTarjeta;
        }
        //public override string ToString() => $"**** **** **** {NumeroTarjeta[^4..]}";
    }
}
