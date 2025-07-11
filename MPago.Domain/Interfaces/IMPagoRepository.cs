﻿using MPago.Domain.Aggregates;

namespace MPago.Domain.Interfaces
{
    public interface IMPagoRepository
    {
        Task<TarjetaCredito> ObtenerMPagoPorId(string idMPago);
        Task<TarjetaCredito> AgregarMPago(TarjetaCredito mPago);
        Task ActualizarPredeterminadoTrueMPago(string idMPago); //por el id del MPago cambio el predeterminado
        Task ActualizarPredeterminadoFalseMPago(string idMPago); //por el id del MPago cambio el predeterminado
        Task EliminarMPago (string idMPago);
        //Task<bool> ExisteMPagoPredeterminado (string idPostor, string idMPagoExcluido = null);
        Task <List<TarjetaCredito>> ObtenerMPagoPorIdPostor(string idPostor);

    }
}
