namespace Hub_App.Models.Restaurante
{
    public class MesaRestaurante
    {
        public int Id { get; set; }
        public List<PratoRestaurante> Prato { get; set; } = new List<PratoRestaurante>();
    }
}
