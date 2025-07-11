using MPago.Domain.ValueObjects;

namespace MPago.Domain.Aggregates
{
    public class TarjetaCredito
    {
        public VOIdMPago IdMPago { get; private set; }
        public VOIdPostor IdPostor { get; private set; }
        public VOIdMPagoStripe IdMPagoStripe { get; private set; }
        public VOIdClienteStripe IdClienteStripe { get; private set; }
        public VOMarca Marca { get; private set; }
        public VOMesExpiracion MesExpiracion { get; private set; }
        public VOAnioExpiracion AnioExpiracion { get; private set; }
        public VOUltimos4 Ultimos4 { get; private set; }
        public VOFechaRegistro FechaRegistro { get; private set; }
        public VOPredeterminado Predeterminado { get; private set; }

        public TarjetaCredito (VOIdMPago idMPago, VOIdPostor idPostor, VOIdMPagoStripe idMPagoStripe,
            VOIdClienteStripe idClienteStripe, VOMarca marca, VOMesExpiracion mesExpiracion,
            VOAnioExpiracion anioExpiracion, VOUltimos4 ultimos4, VOFechaRegistro fechaRegistro, VOPredeterminado predeterminado)
        {
            IdMPago = idMPago;
            IdPostor = idPostor;
            IdMPagoStripe = idMPagoStripe;
            IdClienteStripe = idClienteStripe;
            Marca = marca;
            MesExpiracion = mesExpiracion;
            AnioExpiracion = anioExpiracion;
            Ultimos4 = ultimos4;
            FechaRegistro = fechaRegistro;
            Predeterminado = predeterminado;
        }
    }
}
