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
            List<Sucursales> listaSucursales = (from e in _reservaContext.Sucursales select e).ToList();
            if (listaSucursales.Count == 0)
            { 
                return NotFound();
            }
            return Ok(listaSucursales);
        }

        [HttpPost]
        [Route("CrearSucursal")]
        public IActionResult GuardarSucursal([FromBody] Sucursales sucursales) 
        {
            try
            {
                _reservaContext.Sucursales.Add(sucursales);
                _reservaContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Modificar/{Id}")]
        public IActionResult ModificarSucursal(int id, [FromBody] Sucursales sucursalModificar) 
        {
            Sucursales? sucursalActual = (from e in _reservaContext.Sucursales
                                          where e.Id == id
                                          select e).FirstOrDefault();
            
            if (sucursalActual == null) { return NotFound(); }

            sucursalActual.Nombre = sucursalModificar.Nombre;
            sucursalActual.Direccion = sucursalModificar.Direccion;
            sucursalActual.Telefono = sucursalModificar.Telefono;
            sucursalActual.AdministradorId = sucursalModificar.AdministradorId;
            sucursalActual.NumeroEspacios = sucursalModificar.NumeroEspacios;

            _reservaContext.Entry(sucursalActual).State = EntityState.Modified;
            _reservaContext.SaveChanges();

            return Ok(sucursalModificar);
        }

        [HttpDelete]
        [Route("Eliminar/{Id}")]
        public IActionResult eliminarSucursal(int id)
        {
            Sucursales? sucursalEliminado = (from e in _reservaContext.Sucursales
                                             where e.Id == id
                                             select e).FirstOrDefault();
            if (sucursalEliminado == null) { return NotFound(); }
            
            _reservaContext.Sucursales.Attach(sucursalEliminado);
            _reservaContext.Sucursales.Remove(sucursalEliminado);
            _reservaContext.SaveChanges(true);

            return Ok(sucursalEliminado);

        }
    }
}
