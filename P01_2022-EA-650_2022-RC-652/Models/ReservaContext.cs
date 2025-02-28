using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace P01_2022_EA_650_2022_RC_652.Models
{
    public class ReservaContext: DbContext
    {
        public ReservaContext(DbContextOptions<ReservaContext> options): base(options)
            { 
                
            }

           public DbSet<Usuarios> Usuarios { get; set; }
           public DbSet<Sucursales> Sucursales { get; set; }
           public DbSet<Reservas> Reservas { get; set; }
           public DbSet<EspaciosParqueo> EspaciosParqueo { get; set; }
    }
}
