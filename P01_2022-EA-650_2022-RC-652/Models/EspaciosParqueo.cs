using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace P01_2022_EA_650_2022_RC_652.Models
{
    public class EspaciosParqueo
    {
        [Key]
        public int Id { get; set; }
        public int SucursalId { get; set; }    
        public int Numero { get; set; }
        public string Ubicacion { get; set; }
        public decimal CostoPorHora { get; set; }
        public string Estado { get; set; }
    }
}
