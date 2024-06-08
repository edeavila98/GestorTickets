using System.ComponentModel.DataAnnotations;

namespace GestorTickets.Models
{
    public class Prioridad
    {
        [Key]
        public int IdPrioridad { get; set; }
        public string? NombrePrioridad { get; set; }
    }
}
