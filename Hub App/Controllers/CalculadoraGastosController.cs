using Hub_App.Models.CalculadoraDiaria;
using Hub_App.Service.CalculadoraDiaria;
using Microsoft.AspNetCore.Mvc;

namespace Hub_App.Controllers
{
    public class CalculadoraGastosController : Controller
    {
        private readonly GastosService _gastosService;

        public CalculadoraGastosController(GastosService gastosService)
        {
            _gastosService = gastosService;
        }

        public async Task<IActionResult> Index()
        {
            var gastos = await _gastosService.CarregarGastosAsync() ?? new List<Gasto>();
            
            //Gastos Dia
            var totalGastosDoDia = gastos
                .Where(g => g.DataGasto.Date == DateTime.Today)
                .Sum(v => v.Valor);
            //Gastos Mes Atual
            var totalGastosDoMesAtual = gastos
                .Where(g => g.DataGasto.Month == DateTime.Now.Month && g.DataGasto.Year == DateTime.Now.Year)
                .Sum(g => g.Valor);

            //Gastos Mes anterior
            var totalGastosDoMesAnterior = gastos
                .Where(g => g.DataGasto.Month == (DateTime.Now.Month == 1 ? 12 : DateTime.Now.Month - 1) &&
                            g.DataGasto.Year == (DateTime.Now.Month == 1 ? DateTime.Now.Year - 1 : DateTime.Now.Year))
                .Sum(g => g.Valor);

            ViewBag.TotalGastosDoDia = totalGastosDoDia;
            ViewBag.TotalGastosDoMesAtual = totalGastosDoMesAtual;
            ViewBag.TotalGastosDoMesAnterior = totalGastosDoMesAnterior;

            return View(gastos);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Gasto gasto)
        {
            if (ModelState.IsValid)
            {
                await _gastosService.SalvarGastoAsync(gasto);

                return RedirectToAction("Index");
            }
            return View(gasto);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var gastos = await _gastosService.CarregarGastosAsync() ?? new List<Gasto>();
            var gasto = gastos.FirstOrDefault(g => g.Id == id);

            if (gasto == null)
            {
                return NotFound();
            }

            return View(gasto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Gasto gasto)
        {
            if (ModelState.IsValid)
            {
                await _gastosService.EditarGastoAsync(gasto);
                return RedirectToAction("Index");
            }

            return View(gasto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _gastosService.ExcluirGastoAsync(id);
            return RedirectToAction("Index");
        }
    }
}
