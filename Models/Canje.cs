namespace CiudApp.Models
{
    public class Canje
    {
        public int Id { get; set; }

        public string UserId { get; set; } = "";

        public int RecompensaId { get; set; }

        public DateTime FechaCanje { get; set; }
    }
}