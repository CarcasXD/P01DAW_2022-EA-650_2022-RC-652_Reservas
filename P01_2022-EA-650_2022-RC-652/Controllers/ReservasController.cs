using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P01_2022_EA_650_2022_RC_652.Models;
using Microsoft.EntityFrameworkCore;


namespace P01_2022_EA_650_2022_RC_652.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly ReservaContext _reservaContext;

        public ReservasController(ReservaContext reservaContext)
        {
            _reservaContext = reservaContext;

        }

        [HttpPost]
        [Route("ApartarParqueo")]

        public IActionResult reservacionParqueo( [FromBody] Reservas reservacion) 
        {
            try
            {
                _reservaContext.Reservas.Add(reservacion);
                _reservaContext.SaveChanges();
                return Ok(reservacion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Mostraractivos")]

        public IActionResult mostraractivos()
        {
            var reservalista = (from e in _reservaContext.Reservas where e.Estado.Contains("Activa")
                                select e).ToList();

            if (reservalista== null) { return NotFound("No hay"); }

            return Ok(reservalista);
        }
    }
}
