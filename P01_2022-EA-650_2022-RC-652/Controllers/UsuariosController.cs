using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P01_2022_EA_650_2022_RC_652.Models;
using Microsoft.EntityFrameworkCore;

namespace P01_2022_EA_650_2022_RC_652.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ReservaContext _reservaContext;

        public UsuariosController(ReservaContext reservaContext)
        {
            _reservaContext = reservaContext;

        }

        [HttpPost]
        [Route("CrearUsuario")]

        public IActionResult GuardarUsuario([FromBody] Usuarios usuario)
        {
            
            try
            {
                _reservaContext.Usuarios.Add(usuario);
                _reservaContext.SaveChanges();
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        
        [HttpGet]
        [Route("ValidarCredenciales/{usuario}/{contrasena}")]

        public IActionResult validarUserPassW(string usuario, string contrasena) 
        {
            var usuariolsita = (from e in _reservaContext.Usuarios
                                where e.Usuario == usuario && e.Contraseña == contrasena
                                select e).FirstOrDefault();

            if (usuariolsita == null) { return NotFound("Credenciales Incorrectas"); }

            return Ok("Credenciales Correctas");
        }
        
        [HttpGet]
        [Route("MostrarUsuarios")]

        public IActionResult Get() { 
            List<Usuarios> listaUsuario = (from e in _reservaContext.Usuarios select e).ToList();

            if(listaUsuario.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listaUsuario);
        }

        [HttpPut]
        [Route("ModificarUsuario/{id}")]

        public IActionResult modificarUsuario(int id, [FromBody] Usuarios usuarioModificar) 
        {
            Usuarios? usuarioActual = (from e in _reservaContext.Usuarios where e.Id == id select e).FirstOrDefault();

            if (usuarioActual == null) { return NotFound(); }

            usuarioActual.Nombre = usuarioModificar.Nombre;
            usuarioActual.Usuario = usuarioModificar.Usuario;
            usuarioActual.Correo = usuarioModificar.Correo;
            usuarioActual.Telefono = usuarioModificar.Telefono;
            usuarioActual.Contraseña = usuarioModificar.Contraseña;
            usuarioActual.Rol = usuarioModificar.Rol;

            _reservaContext.Entry(usuarioActual).State = EntityState.Modified;
            _reservaContext.SaveChanges();

            return Ok(usuarioModificar);

        }

        [HttpDelete]
        [Route("EliminarUsuario/{id}")]
        public IActionResult eliminarUsuario(int id) 
        {
            Usuarios? usuariolista = (from e in _reservaContext.Usuarios where e.Id == id select e).FirstOrDefault();

            if (usuariolista == null) { return NotFound(); }

            _reservaContext.Usuarios.Attach(usuariolista);
            _reservaContext.Usuarios.Remove(usuariolista);
            _reservaContext.SaveChanges();

            return Ok(usuariolista);
        }
    }
}
