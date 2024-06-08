using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GestorTickets.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        [JsonIgnore]
        public string Clave { get; set; }
        public string Estado { get; set; } = "A";
        public string Telefono { get; set; }
        public int IdRol { get; set; }

        //public DateTime FechaCreacion { get; set; } = DateTime.Now;

    }
}
