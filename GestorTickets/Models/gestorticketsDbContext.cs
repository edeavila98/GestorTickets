using Microsoft.EntityFrameworkCore;

namespace GestorTickets.Models
{
    public class gestorticketsDbContext : DbContext
    {

        public gestorticketsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ArchivosAdjuntos> ArchivosAdjuntos { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Comentarios> Comentarios { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Prioridad> Prioridad { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Tarea> Tarea { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
