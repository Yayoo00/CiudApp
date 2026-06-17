namespace CiudApp.Models
{
    public class ReporteCiudad
    {
        public int Id { get; set; }

        public string UserId { get; set; } = "";

        public string Tipo { get; set; } = "";

        public string Ubicacion { get; set; } = "";

        public string Descripcion { get; set; } = "";

        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}