using P01_2022_EA_650_2022_RC_652.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace P01_2022_EA_650_2022_RC_652.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestionEspacioParqueoController : ControllerBase
    {
        private readonly ReservaContext _reservaContext;

        public GestionEspacioParqueoController(ReservaContext reservaContext)
        {
            _reservaContext = reservaContext;
        }

        [HttpGet]
        [Route("MostrarSucursalesParqueo")]
        public IActionResult GetSucursales()
        { 
            List<sucursales> listaSucursales = (from e in _reservaContext.Sucursales select e).ToList();
            if (listaSucursales.Count == 0)
            { 
                return NotFound();
            }
            return Ok(listaSucursales);
        }

    }
}
