using System.ComponentModel.DataAnnotations;

namespace GestorTickets.Models
{
    public class ArchivosAdjuntos
    {
        [Key]
        public int IdArchivo { get; set; }
        public int IdTicket { get; set; }
        public string? NombreArchivo { get; set; }
        public string? RutaArchivo { get; set; }
    }
}
