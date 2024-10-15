using System.ComponentModel.DataAnnotations;

namespace Hub_App.Models.CalculadoraDiaria
{
    public class Gasto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O {0} é obrigatório")]
        public string Nome { get; set; } = string.Empty;
        [Required(ErrorMessage = "O {0} é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal Valor { get; set; }
        [Required(ErrorMessage = "A data do gasto é obrigatória")]
        public DateTime DataGasto { get; set; }
    }
}
