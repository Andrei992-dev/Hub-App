using Hub_App.Models.Restaurante;
using Hub_App.Service.Restaurante;
using Microsoft.AspNetCore.Mvc;

namespace Hub_App.Controllers
{
    public class RestauranteController : Controller
    {
        private readonly RestauranteService _restauranteService;

        public RestauranteController(RestauranteService restauranteService)
        {
            _restauranteService = restauranteService;
        }

        public async Task<IActionResult> Index()
        {
            var clientes = await _restauranteService.CarregarClientesAsync();
            var garcons = await _restauranteService.CarregarGarconsAsync();
            var pratos = await _restauranteService.CarregarPratosAsync();

            ViewBag.Clientes = clientes;
            ViewBag.Garcons = garcons;
            ViewBag.Pratos = pratos;
            return View(new MesaRestaurante());
        }

        [HttpPost]
        public async Task<IActionResult> Index(MesaRestaurante mesa, int[] pratosSelecionados)
        {
            // Carregar os pratos do serviço
            var pratos = await _restauranteService.CarregarPratosAsync();

            // Adicionar os pratos selecionados à mesa
            mesa.Pratos = pratos.Where(p => pratosSelecionados.Contains(p.Id)).ToList();

            // Salvar a mesa no banco de dados
            await _restauranteService.SalvarMesaAsync(mesa);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CriarCliente()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CriarCliente(ClienteRestaurante cliente)
        {
            await _restauranteService.AdicionarClienteAsync(cliente);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CriarGarcon()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CriarGarcon(GarconRestaurante garcon)
        {
            await _restauranteService.AdicionarGarconAsync(garcon);
            return RedirectToAction("Index");
        }
    }
}
