using Hub_App.Models.ListaTarefas;
using System.Text.Json;

namespace Hub_App.Service.ListaTarefas
{
    public class TarefasService
    {
        private readonly string _filePath;

        public TarefasService(IWebHostEnvironment env)
        {
            _filePath = Path.Combine(env.WebRootPath, "lista-tarefas.json");
        }
        public async Task<List<Tarefa>> CarregarTarefasAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Tarefa>();
            }

            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<Tarefa>>(json) ?? new List<Tarefa>();
        }
        public async Task SalvarTarefaAsync(Tarefa tarefa)
        {
            var tarefas = await CarregarTarefasAsync();
            tarefa.Id = tarefas.Count > 0 ? tarefas.Max(t => t.Id) + 1 : 1;

            tarefa.DataCriacao = DateTime.Now;

            tarefas.Add(tarefa);

            var json = JsonSerializer.Serialize(tarefas, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, json);
        }
        public async Task MarcarComoConcluidaAsync(int id)
        {
            var tarefas = await CarregarTarefasAsync();
            var tarefa = tarefas.FirstOrDefault(t => t.Id == id);

            if (tarefa != null)
            {
                tarefa.Concluida = true;
                tarefa.DataConclusao = DateTime.Now;
            }

            var json = JsonSerializer.Serialize(tarefas, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, json);
        }
        public async Task ExcluirTarefaAsync(int id)
        {
            var tarefas = await CarregarTarefasAsync();
            var tarefa = tarefas.FirstOrDefault(t => t.Id == id);

            if (tarefa != null)
            {
                tarefas.Remove(tarefa);

                var json = JsonSerializer.Serialize(tarefas, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(_filePath, json);
            }
        }
        public async Task EditarTarefaAsync(Tarefa tarefaAtualizada)
        {
            var tarefas = await CarregarTarefasAsync();
            var tarefa = tarefas.FirstOrDefault(t => t.Id == tarefaAtualizada.Id);
            if (tarefa != null)
            {
                tarefa.Descricao = tarefaAtualizada.Descricao;
                tarefa.DataPrevisaConclusao = tarefaAtualizada.DataPrevisaConclusao;
                // Salve as alterações no arquivo JSON
                await SalvarTarefasAsync(tarefas);
            }
        }
        private async Task SalvarTarefasAsync(List<Tarefa> tarefas)
        {
            var json = JsonSerializer.Serialize(tarefas, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, json);
        }
    }
}
