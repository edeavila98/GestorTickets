using System.ComponentModel.DataAnnotations;

namespace GestorTickets.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }
        public string? NombreCategoria { get; set; }
    }
}
