using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace P01_2022_EA_650_2022_RC_652.Models
{
    public class Sucursales
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int AdministradorId { get; set; }
        public int NumeroEspacios { get; set; }
    }
}
