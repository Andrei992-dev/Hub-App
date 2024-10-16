namespace Hub_App.Models.Restaurante
{
    public class MesaRestaurante
    {
        public int Id { get; set; }
        public int ClienteRestauranteId { get; set; }
        ClienteRestaurante Cliente { get; set; } = new ClienteRestaurante();
        public int GarconRestauranteId { get; set; }
        public GarconRestaurante Garcon { get; set; } = new GarconRestaurante();
        public List<PratoRestaurante> Pratos { get; set; } = new List<PratoRestaurante>();
    }
}
