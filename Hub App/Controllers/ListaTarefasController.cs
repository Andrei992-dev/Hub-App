using Hub_App.Models.ListaTarefas;
using Hub_App.Service.ListaTarefas;
using Microsoft.AspNetCore.Mvc;

namespace Hub_App.Controllers
{
    public class ListaTarefasController : Controller
    {
        private readonly TarefasService _tarefasService;

        public ListaTarefasController(TarefasService tarefasService)
        {
            _tarefasService = tarefasService;
        }

        public async Task<IActionResult> Index()
        {
            var tarefas = await _tarefasService.CarregarTarefasAsync() ?? new List<Tarefa>();
            var tarefasAFazer = tarefas.Where(t => !t.Concluida).ToList();
            var tarefasConcluidas = tarefas.Where(t => t.Concluida).ToList();

            ViewBag.TarefasAFazer = tarefasAFazer;
            ViewBag.TarefasConcluidas = tarefasConcluidas;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarTarefa(Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                tarefa.DataCriacao = DateTime.Now;
                tarefa.Concluida = false;
                await _tarefasService.SalvarTarefaAsync(tarefa);
                return RedirectToAction("Index");
            }

            return View(tarefa);
        }
        [HttpPost]
        public async Task<IActionResult> MarcarComoConcluida(int id)
        {
            await _tarefasService.MarcarComoConcluidaAsync(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> ExcluirTarefa(int id)
        {
            await _tarefasService.ExcluirTarefaAsync(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> EditarTarefa(Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                await _tarefasService.EditarTarefaAsync(tarefa);
                return RedirectToAction("Index");
            }

            return View(tarefa);
        }
    }
}
