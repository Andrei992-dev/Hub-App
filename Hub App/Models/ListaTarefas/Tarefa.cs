namespace Hub_App.Models.ListaTarefas
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public DateTime DataPrevisaConclusao { get; set; }
        public DateTime? DataConclusao { get; set; }
        public bool Concluida { get; set; }
    }
}
