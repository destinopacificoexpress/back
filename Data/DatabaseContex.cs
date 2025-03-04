using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace DestinopacificoExpres.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
        // public DbSet<Role> Roles { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Sesion> Sesiones { get; set; }
        public DbSet<Permisos> Permisos { get; set; }
        public DbSet<RolePermisos> RolePermisos { get; set; }
        public DbSet<Tiquete> Tiquetes { get; set; }
        public DbSet<Pasajero> Pasajeros { get; set; }
        public DbSet<TipoTiquete> TipoTiquete { get; set; }
        public DbSet<Agencias> Agencias { get; set; }
        public DbSet<Grupos> Grupos { get; set; }
        public DbSet<Destino> Destinos { get; set; }
        public DbSet<InfoDestino> InfoDestino { get; set; }
        public DbSet<FormaPago> FormasPago { get; set; }

        public DbSet<Lancha> Lanchas { get; set; }
        public DbSet<Viaje> Viajes { get; set; }

        public DbSet<Aprobador> Aprobadores { get; set; }
        public DbSet<PasajerosCortecia> PasajerosCortecias { get; set; }
        public DbSet<Cortesia> Cortesias { get; set; }

        public DbSet<HorarioDisponible> HorarioDisponibles { get; set; }

        public DbSet<TipoDocumento> TipoDocumento { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InfoDestino>()
                .HasOne(d => d.Grupos)
                .WithMany(g => g.InfoDestino)
                .HasForeignKey(d => d.GrupoId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

    }
}