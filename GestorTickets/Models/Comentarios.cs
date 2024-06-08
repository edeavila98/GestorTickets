using System.ComponentModel.DataAnnotations;

namespace GestorTickets.Models
{
    public class Comentarios
    {
        [Key]
        public int IdComentario { get; set; }
        public string? Comentario { get; set; }
    }
}
