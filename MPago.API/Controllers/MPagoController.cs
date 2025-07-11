using MassTransit;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using MPago.Application.Commands;
using MPago.Application.DTOs;
using MPago.Domain.ValueObjects;
using MPago.Infrastructure.Queries;
using MPago.Infrastructure.Services;
using MPago.Infrastructure.Interfaces;

namespace MPago.API.Controllers
{
    [ApiController]
    [Route("api/mpago")]
    public class MPagoController : ControllerBase
    {
        private readonly IMediator Mediator;
        private readonly IPublishEndpoint PublishEndpoint;

        public MPagoController(IMediator mediator, IPublishEndpoint publishEndpoint)
        {
            Mediator = mediator;
            PublishEndpoint = publishEndpoint;
        }

        #region GetMPagoPorId
        [HttpGet("GetMPagoPorId")]
        public async Task<IActionResult> GetMPagoPorId([FromQuery] string idMPago)
        {
            try
            {
                var MPago = await Mediator.Send(new GetMPagoPorIdQuery(idMPago));

                if (MPago == null)
                {
                    return NotFound($"No se encontró un MPago con el id {idMPago}");
                }

                return Ok(MPago);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno en el servidor.");
            }
        }
        #endregion

        #region GetMPagoPorIdPostor
        [HttpGet("GetMPagoPorIdPostor")]
        public async Task<IActionResult> GetMPagoPorIdPostor([FromQuery] string idPostor)
        {
            try
            {
                var MPago = await Mediator.Send(new GetMPagoPorIdPostorQuery(idPostor));

                if (MPago == null)
                {
                    return NotFound($"No se encontró un MPago con el id del postor {idPostor}");
                }

                return Ok(MPago);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno en el servidor.");
            }
        }
        #endregion

        #region GetTodosMPago
        [HttpGet("GetTodosMPago")]
        public async Task<IActionResult> GetTodosMPago()
        {
            try
            {
                var MPago = await Mediator.Send(new GetTodosMPagoQuery());

                if (MPago == null)
                {
                    return NotFound("No se encontró ningun MPago");
                }

                return Ok(MPago);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno en el servidor.");
            }
        }
        #endregion

        #region AgregarMPago
        [HttpPost("AgregarMPago")]
        public async Task<IActionResult> AgregarMPago([FromBody] AgregarMPagoStripeDto mpago)
        {

            try
            {
                var IdMPago = await Mediator.Send(new AgregarMPagoCommand(mpago));

                if (IdMPago == null)
                {
                    return BadRequest("No se pudo agregar el mpago.");
                }

                return CreatedAtAction(nameof(AgregarMPago), new { id = IdMPago }, new
                {
                    id = IdMPago
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Error interno en el servidor. AGREGAR MPago");
            }
        }
        #endregion

        #region EliminarMPago
        [HttpDelete("EliminarMPago/{idMPago}")]
        public async Task<IActionResult> EliminarProducto([FromRoute] string idMPago)
        {
            try
            {
                var result = await Mediator.Send(new EliminarMPagoCommand(idMPago));
                if (!result)
                {
                    return NotFound("El MPago no pudo ser eliminado.");
                }
                return Ok("MPago eliminado exitosamente.");

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno en el servidor. ELIMINAR MPago");
            }
        }
        #endregion

        #region ActualizarMPagoPredeterminado
        [HttpPut("ActualizarMPagoPredeterminado")]
        public async Task<IActionResult> ActualizarMPagoPredeterminado([FromQuery] string idMPago, [FromQuery] string idPostor)
        {
            try
            {
                var result = await Mediator.Send(new MPagoPredeterminadoCommand(idMPago, idPostor));
                if (!result)
                {
                    return NotFound("El MPago no pudo ser actualizado a predeterminado.");
                }
                return Ok("MPago actualizado a predeterminado exitosamente.");

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno en el servidor. PREDETERMINADO MPago");
            }
        }
        #endregion

    }
}
