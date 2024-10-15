using Hub_App.Models.CalculadoraDiaria;
using System.Text.Json;

namespace Hub_App.Service.CalculadoraDiaria
{
    public class GastosService
    {
        private readonly string _filePath;

        public GastosService(IWebHostEnvironment env)
        {
            _filePath = Path.Combine(env.WebRootPath, "gastos.json");
        }

        public async Task SalvarGastoAsync(Gasto gasto)
        {
            List<Gasto> gastos = await CarregarGastosAsync() ?? new List<Gasto>();

            gasto.Id = gastos.Count > 0 ? gastos.Max(g => g.Id) + 1 : 1;

            gastos.Add(gasto);

            var json = JsonSerializer.Serialize(gastos, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, json);
        }
        public async Task<List<Gasto>?> CarregarGastosAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Gasto>();
            }

            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<Gasto>>(json);
        }
        public async Task EditarGastoAsync(Gasto gastoAtualizado)
        {
            List<Gasto> gastos = await CarregarGastosAsync() ?? new List<Gasto>();

            var gastoExistente = gastos.FirstOrDefault(g => g.Id == gastoAtualizado.Id);

            if(gastoExistente != null)
            {
                gastoExistente.Nome = gastoAtualizado.Nome;
                gastoExistente.Valor = gastoAtualizado.Valor;
                gastoExistente.DataGasto = gastoAtualizado.DataGasto;

                var json = JsonSerializer.Serialize(gastos, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(_filePath, json);
            }
        }
        public async Task ExcluirGastoAsync(int id)
        {
            List<Gasto> gastos = await CarregarGastosAsync() ?? new List<Gasto>();

            var gastoToRemove = gastos.FirstOrDefault(g => g.Id == id);

            if (gastoToRemove != null)
            {
                
                gastos.Remove(gastoToRemove);

                var json = JsonSerializer.Serialize(gastos, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(_filePath, json);
            }
        }
        public async Task<List<Gasto>> ObterGastosPorMesAsync()
        {
            var gastos = await CarregarGastosAsync() ?? new List<Gasto>();
            var mesAtual = DateTime.Now.Month;
            var anoAtual = DateTime.Now.Year;

            return gastos.Where(g =>
            (g.DataGasto.Month == mesAtual && g.DataGasto.Year == anoAtual) ||
            (g.DataGasto.Month == (mesAtual == 1 ? 12 : mesAtual - 1) && g.DataGasto.Year == (mesAtual == 1 ? anoAtual - 1 : anoAtual)))
            .ToList();
        }
    }
}
