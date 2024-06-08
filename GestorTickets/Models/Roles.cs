using System.ComponentModel.DataAnnotations;

namespace GestorTickets.Models
{
    public class Roles
    {
        [Key]
        public int IdRol { get; set; }
        public string? NombreRol { get; set; }
    }
}
