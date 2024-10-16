using Hub_App.Models.ClientesProdutos;
using System.Text.Json;

namespace Hub_App.Service.ClientesProdutos
{
    public class ClienteProdutoService
    {
        private readonly string _filePathClientes;
        private readonly string _filePathProdutos;

        public ClienteProdutoService(IWebHostEnvironment env)
        {
            _filePathClientes = Path.Combine(env.WebRootPath, "clientes.json");
            _filePathProdutos = Path.Combine(env.WebRootPath, "produtos.json");

        }

        #region Clientes
        public async Task<List<Cliente>> CarregarClientesAsync()
        {
            if (!File.Exists(_filePathClientes))
            {
                return new List<Cliente>();
            }

            var json = await File.ReadAllTextAsync(_filePathClientes);
            return JsonSerializer.Deserialize<List<Cliente>>(json) ?? new List<Cliente>();
        }
        public async Task SalvarClienteAsync(Cliente cliente)
        {
            var clientes = await CarregarClientesAsync();
            cliente.Id = clientes.Count > 0 ? clientes.Max(x => x.Id) + 1 : 1;

            clientes.Add(cliente);
            var json = JsonSerializer.Serialize(clientes, new JsonSerializerOptions { WriteIndented = true});
            await File.WriteAllTextAsync(_filePathClientes, json);
        }
        public async Task ExcluirClienteAsync(int id)
        {
            var clientes = await CarregarClientesAsync();
            var cliente = clientes.FirstOrDefault(t => t.Id == id);

            if (cliente != null)
            {
                clientes.Remove(cliente);

                var json = JsonSerializer.Serialize(clientes, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(_filePathClientes, json);
            }
        }
        public async Task EditarClienteAsync(Cliente ClienteAtualizado)
        {
            var clientes = await CarregarClientesAsync();
            var cliente = clientes.FirstOrDefault(c => c.Id ==ClienteAtualizado.Id);
            if (cliente != null)
            {
                cliente.Nome = ClienteAtualizado.Nome;
                cliente.Endereco = ClienteAtualizado.Endereco;
                cliente.Telefone = ClienteAtualizado.Telefone;
                await SalvarClientesAsync(clientes);
            }
        }
        private async Task SalvarClientesAsync(List<Cliente> clientes)
        {
            var json = JsonSerializer.Serialize(clientes, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePathClientes, json);
        }
        #endregion

        #region Produtos
        public async Task<List<Produto>> CarregarProdutosAsync()
        {
            if (!File.Exists(_filePathProdutos))
            {
                return new List<Produto>();
            }

            var json = await File.ReadAllTextAsync(_filePathProdutos);
            return JsonSerializer.Deserialize<List<Produto>>(json) ?? new List<Produto>();
        }
        public async Task SalvarProdutoAsync(Produto produto)
        {
            var produtos = await CarregarProdutosAsync();
            produto.Id = produtos.Count > 0 ? produtos.Max(x => x.Id) + 1 : 1;

            produtos.Add(produto);
            var json = JsonSerializer.Serialize(produtos, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePathProdutos, json);
        }
        public async Task ExcluirProdutoAsync(int id)
        {
            var produtos = await CarregarProdutosAsync();
            var produto = produtos.FirstOrDefault(t => t.Id == id);

            if (produto != null)
            {
                produtos.Remove(produto);

                var json = JsonSerializer.Serialize(produtos, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(_filePathProdutos, json);
            }
        }
        public async Task EditarProdutoAsync(Produto produtoAtualizado)
        {
            var produtos = await CarregarProdutosAsync();
            var produto = produtos.FirstOrDefault(c => c.Id == produtoAtualizado.Id);
            if (produto != null)
            {
                produto.Nome = produtoAtualizado.Nome;
                produto.Preco = produtoAtualizado.Preco;
                produto.FotoItem = produtoAtualizado.FotoItem;
                await SalvarProdutosAsync(produtos);
            }
        }
        private async Task SalvarProdutosAsync(List<Produto> produtos)
        {
            var json = JsonSerializer.Serialize(produtos, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePathProdutos, json);
        }
        #endregion
    }
}
