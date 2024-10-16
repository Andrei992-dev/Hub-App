using Hub_App.Models.ClientesProdutos;
using Hub_App.Models.ListaTarefas;
using Hub_App.Service.CalculadoraDiaria;
using Hub_App.Service.ClientesProdutos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Hub_App.Controllers
{
    public class ClientesProdutosController : Controller
    {
        private readonly ClienteProdutoService _clienteProdutoService;

        public ClientesProdutosController(ClienteProdutoService clienteProdutoService)
        {
            _clienteProdutoService = clienteProdutoService;
        }

        public async Task<IActionResult> Index()
        {
            var clientes = await _clienteProdutoService.CarregarClientesAsync() ?? new List<Cliente>();

            ViewBag.Clientes = clientes;

            return View();
        }
        #region Clientes
        [HttpPost]
        public async Task<IActionResult> AdicionarCliente(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                var clientes = await _clienteProdutoService.CarregarClientesAsync();

                await _clienteProdutoService.SalvarClienteAsync(cliente);

                return RedirectToAction(nameof(Index));
            }

            return View("Index");
        }
        [HttpPost]
        public async Task<IActionResult> EditarCliente(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                await _clienteProdutoService.EditarClienteAsync(cliente);
                return RedirectToAction(nameof(Index));
            }

            return View(cliente);
        }
        [HttpPost]
        public async Task<IActionResult> ExcluirCliente(int id)
        {
            await _clienteProdutoService.ExcluirClienteAsync(id);
            return RedirectToAction("Index");
        }
        #endregion

        #region Produtos
        public async Task<IActionResult> Produtos(string searchTerm)
        {
            var produtos = await _clienteProdutoService.CarregarProdutosAsync();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                produtos = produtos
                    .Where(p => p.Nome.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            ViewBag.Produtos = produtos;
            ViewBag.SearchTerm = searchTerm;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AdicionarProduto(Produto produto)
        {
            if (ModelState.IsValid)
            {
                await _clienteProdutoService.SalvarProdutoAsync(produto);
                return RedirectToAction(nameof(Produtos));
            }

            var produtos = await _clienteProdutoService.CarregarProdutosAsync();
            ViewBag.Produtos = produtos;
            return View("Produtos");
        }
        public async Task<IActionResult> EditarProduto(int id)
        {
            var produto = await _clienteProdutoService.CarregarProdutosAsync();
            var produtoSelecionado = produto.FirstOrDefault(p => p.Id == id);
            if (produtoSelecionado == null)
            {
                return NotFound();
            }

            return View(produtoSelecionado);
        }
        [HttpPost]
        public async Task<IActionResult> SalvarEdicaoProduto(Produto produtoAtualizado)
        {
            await _clienteProdutoService.EditarProdutoAsync(produtoAtualizado);
            return RedirectToAction("Produtos");
        }
        [HttpPost]
        public async Task<IActionResult> ExcluirProduto(int id)
        {
            await _clienteProdutoService.ExcluirProdutoAsync(id);
            return RedirectToAction("Produtos");
        }
        #endregion
    }
}
