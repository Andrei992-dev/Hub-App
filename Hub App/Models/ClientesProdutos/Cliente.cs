namespace Hub_App.Models.ClientesProdutos
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public long Telefone { get; set; }
        public string Endereco { get; set; } = string.Empty;
        public bool Ativo {  get; set; } = true;
    }
}
