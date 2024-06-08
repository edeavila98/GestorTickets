using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorTickets.Models
{
    public class Tickets
    {
        [Key]
        public int IdTicket { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int IdCategoria { get; set; }
        public int? IdEmpleado { get; set; } 
        public int IdCliente { get; set; }
        public int IdEstado { get; set; } = 1;
        //public DateTime FechaCreacion { get; set; }
        public int IdPrioridad { get; set; }

        [ForeignKey("IdCliente")]
        public Usuario Cliente { get; set; }

        [ForeignKey("IdCategoria")]
        public Categoria Categoria { get; set; }

        [ForeignKey("IdEmpleado")]
        public Usuario Empleado { get; set; }

        [ForeignKey("IdEstado")]
        public Estado Estado { get; set; }

        [ForeignKey("IdPrioridad")]
        public Prioridad Prioridad { get; set; }
    }
}
