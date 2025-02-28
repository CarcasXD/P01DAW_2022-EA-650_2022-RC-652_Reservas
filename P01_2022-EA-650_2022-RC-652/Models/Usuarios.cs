using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace P01_2022_EA_650_2022_RC_652.Models
{
    public class Usuarios
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Contraseña { get; set; }
        public string Rol { get; set; }
    }
}
