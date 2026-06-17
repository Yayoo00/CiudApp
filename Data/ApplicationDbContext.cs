using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CiudApp.Models;

namespace CiudApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PerfilUsuario> PerfilesUsuario { get; set; }
        
        public DbSet<Recompensa> Recompensas { get; set; }

        public DbSet<Canje> Canjes { get; set; }

        public DbSet<EcoRetoCompletado> EcoRetosCompletados { get; set; }

        public DbSet<ReporteCiudad> ReportesCiudad { get; set; }
        public DbSet<Sugerencia> Sugerencias { get; set; }
    }
}