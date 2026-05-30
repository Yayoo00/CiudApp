namespace CiudApp.Models
{
    public class Recompensa
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = "";

        public string Descripcion { get; set; } = "";

        public int CostoPuntos { get; set; }
    }
}