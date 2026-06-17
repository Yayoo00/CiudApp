namespace CiudApp.Models
{
    public class EcoRetoCompletado
    {
        public int Id { get; set; }

        public string UserId { get; set; } = "";

        public string NombreReto { get; set; } = "";

        public DateTime FechaCompletado { get; set; }
    }
}