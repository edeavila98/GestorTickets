using System.ComponentModel.DataAnnotations;

namespace GestorTickets.Models
{
    public class Tickets
    {
        [Key]
        public int IdTicket { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int IdCategoria { get; set; }
        public int IdEmpleado { get; set; }
        public int IdCliente { get; set; }
        public int IdEstado { get; set; }
        //public DateTime FechaCreacion { get; set; }
        public int IdPrioridad { get; set; }
        public int IdComentario { get; set; }
    }
}
