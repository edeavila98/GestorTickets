using System.ComponentModel.DataAnnotations;

namespace GestorTickets.Models
{
    public class Estado
    {
        [Key]
        public int IdEstado { get; set; }
        public string? NombreEstado { get; set; }
    }
}
