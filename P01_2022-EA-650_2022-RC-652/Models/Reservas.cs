using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace P01_2022_EA_650_2022_RC_652.Models
{
    public class Reservas
    {
        [Key]
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int EspacioId { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public int CantidadHoras { get; set; }
        public string Estado { get; set; }
    }
}
