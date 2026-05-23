using System.ComponentModel.DataAnnotations;

namespace CiudApp.Models
{
    public class PerfilUsuario
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = "";

        [Required]
        public string Nombre { get; set; } = "";

        public string Universidad { get; set; } = "";

        public string Distrito { get; set; } = "";

        public string Nivel { get; set; } = "Inicial";

        public int Puntos { get; set; } = 0;

        public int RutasCompletadas { get; set; } = 0;

        public int RetosCompletados { get; set; } = 0;
    }
}