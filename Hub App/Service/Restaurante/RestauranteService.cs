namespace Hub_App.Service.Restaurante
{
    public class RestauranteService
    {
        private readonly string _filePathClienteRestaurante;
        private readonly string _filePathGarconRestaurante;
        private readonly string _filePathMesaRestaurante;
        private readonly string _filePathPratoRestaurante;

        public RestauranteService(IWebHostEnvironment env)
        {
            _filePathClienteRestaurante = Path.Combine(env.WebRootPath, "cliente-restaurante.json");
            _filePathGarconRestaurante = Path.Combine(env.WebRootPath, "garcon-restaurante.json");
            _filePathMesaRestaurante = Path.Combine(env.WebRootPath, "mesa-restaurante.json");
            _filePathPratoRestaurante = Path.Combine(env.WebRootPath, "prato-restaurante.json");
        }
    }
}
