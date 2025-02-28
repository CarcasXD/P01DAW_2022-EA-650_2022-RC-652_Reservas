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

        [HttpPost]
        [Route("RegistrarEspacio")]
        public IActionResult GuardarEspacio([FromBody] EspaciosParqueo espacioParqueo)
        {
            try
            {
                
                if (_reservaContext.Sucursales.Find(espacioParqueo.SucursalId)==null)
                {
                    return BadRequest();
                }

                _reservaContext.EspaciosParqueo.Add(espacioParqueo);
                _reservaContext.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("DisponiblesPorDia/{fecha}")]
        public IActionResult EspaciosDisponibles(DateTime fecha)
        {
            var disponibles = _reservaContext.EspaciosParqueo
                .Where(e => e.Estado == "Disponible" &&
                            !_reservaContext.Reservas.Any(r => r.EspacioId == e.Id && r.Fecha == fecha))
                .ToList();

            return disponibles.Any() ? Ok(disponibles) : NotFound();
        }

        [HttpPut]
        [Route("Actualizar/{Id}")]
        public IActionResult ActualizarEspacio(int id, [FromBody] EspaciosParqueo espacioActualizado)
        {
            EspaciosParqueo? espacio = (from e in _reservaContext.EspaciosParqueo
                                        where e.Id == id
                                        select e).FirstOrDefault();

            if (espacio == null)
                return NotFound();

            espacio.Numero = espacioActualizado.Numero;
            espacio.Ubicacion = espacioActualizado.Ubicacion;
            espacio.CostoPorHora = espacioActualizado.CostoPorHora;
            espacio.Estado = espacioActualizado.Estado;

            _reservaContext.Entry(espacio).State = EntityState.Modified;
            _reservaContext.SaveChanges();

            return Ok(espacioActualizado);
        }

        [HttpDelete]
        [Route("EliminarEspacio/{id}")]
        public IActionResult EliminarEspacio(int id)
        {
            EspaciosParqueo? espacioEliminado = (from e in _reservaContext.EspaciosParqueo
                                                 where e.Id == id
                                                 select e).FirstOrDefault();
            if (espacioEliminado == null) return NotFound();

            _reservaContext.EspaciosParqueo.Attach(espacioEliminado);
            _reservaContext.EspaciosParqueo.Remove(espacioEliminado);
            _reservaContext.SaveChanges(true);

            return Ok(espacioEliminado);
        }

        [HttpGet]
        [Route("ReservadosPorDia/{fecha}")]
        public IActionResult EspaciosReservadosPorDia(DateTime fecha)
        {
            List<Reservas> espaciosReservados = (from r in _reservaContext.Reservas
                                                 where r.Fecha == fecha
                                                 select r).ToList();

            return espaciosReservados.Any() ? Ok(espaciosReservados) : NotFound();
        }

        

    }
}
