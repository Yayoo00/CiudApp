namespace CiudApp.Models
{
    public class Sugerencia
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = "";

        public string Correo { get; set; } = "";

        public string Asunto { get; set; } = "";

        public string Comentario { get; set; } = "";

        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}