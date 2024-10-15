namespace Hub_App.Models.ClientesProdutos
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public string FotoItem { get; set; } = string.Empty;
        public int Quantidade { get; set; }
    }
}
