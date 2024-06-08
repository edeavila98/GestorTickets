using System.ComponentModel.DataAnnotations;

namespace GestorTickets.Models
{
    public class Tarea
    {
        [Key]
        public int IdTarea { get; set; }
        public string? Descripcion { get; set; }
        public int IdTicket { get; set; }
        public int IdEmpleado { get; set; }
    }
}
