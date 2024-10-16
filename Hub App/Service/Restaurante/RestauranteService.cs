using Hub_App.Models.Restaurante;
using System.Text.Json;

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

        public async Task<List<MesaRestaurante>> CarregarMesasAsync()
        {
            if (!File.Exists(_filePathMesaRestaurante))
            {
                return new List<MesaRestaurante>();
            }

            var json = await File.ReadAllTextAsync(_filePathMesaRestaurante);
            return JsonSerializer.Deserialize<List<MesaRestaurante>>(json) ?? new List<MesaRestaurante>();
        }

        public async Task SalvarMesaAsync(MesaRestaurante mesa)
        {
            var mesas = await CarregarMesasAsync();
            mesa.Id = mesas.Count > 0 ? mesas.Max(x => x.Id) + 1 : 1;
            mesas.Add(mesa);
            var json = JsonSerializer.Serialize(mesas, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePathMesaRestaurante, json);
        }

        public async Task<List<ClienteRestaurante>> CarregarClientesAsync()
        {
            if (!File.Exists(_filePathClienteRestaurante))
            {
                var clientesVazios = new List<ClienteRestaurante>();
                var jsonVazio = JsonSerializer.Serialize(clientesVazios, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(_filePathClienteRestaurante, jsonVazio);

                return clientesVazios;
            }

            var json = await File.ReadAllTextAsync(_filePathClienteRestaurante);
            return JsonSerializer.Deserialize<List<ClienteRestaurante>>(json) ?? new List<ClienteRestaurante>();
        }
        public async Task<List<GarconRestaurante>> CarregarGarconsAsync()
        {
            if (!File.Exists(_filePathGarconRestaurante))
            {
                var garconsVazios = new List<GarconRestaurante>();
                var jsonVazio = JsonSerializer.Serialize(garconsVazios, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(_filePathGarconRestaurante, jsonVazio);

                return garconsVazios;
            }

            var json = await File.ReadAllTextAsync(_filePathGarconRestaurante);
            return JsonSerializer.Deserialize<List<GarconRestaurante>>(json) ?? new List<GarconRestaurante>();
        }

        public async Task<List<PratoRestaurante>> CarregarPratosAsync()
        {
            if (!File.Exists(_filePathPratoRestaurante))
            {
                // Cria um arquivo vazio se ele não existir
                var pratosVazios = new List<PratoRestaurante>();
                var jsonVazio = JsonSerializer.Serialize(pratosVazios, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(_filePathPratoRestaurante, jsonVazio);

                return pratosVazios;
            }

            var json = await File.ReadAllTextAsync(_filePathPratoRestaurante);
            return JsonSerializer.Deserialize<List<PratoRestaurante>>(json) ?? new List<PratoRestaurante>();
        }
        public async Task AdicionarClienteAsync(ClienteRestaurante cliente)
        {
            var clientes = await CarregarClientesAsync();

            // Verifica se a lista está vazia
            if (clientes.Any())
            {
                // Se a lista tiver elementos, usa o Max para definir o próximo ID
                cliente.Id = clientes.Max(c => c.Id) + 1;
            }
            else
            {
                // Se a lista estiver vazia, define o primeiro ID como 1
                cliente.Id = 1;
            }

            clientes.Add(cliente);
            var json = JsonSerializer.Serialize(clientes, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePathClienteRestaurante, json);
        }


        public async Task AdicionarGarconAsync(GarconRestaurante garcon)
        {
            var garcons = await CarregarGarconsAsync();

            // Verifica se a lista está vazia
            if (garcons.Any())
            {
                // Se a lista tiver elementos, usa o Max para definir o próximo ID
                garcon.Id = garcons.Max(g => g.Id) + 1;
            }
            else
            {
                // Se a lista estiver vazia, define o primeiro ID como 1
                garcon.Id = 1;
            }

            garcons.Add(garcon);
            var json = JsonSerializer.Serialize(garcons, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePathGarconRestaurante, json);
        }


    }
}
