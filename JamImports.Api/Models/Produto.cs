namespace JamImports.Api.Models
{
    public class Produto
    {
        public int ID { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Tamanho { get; set; } = string.Empty;
        public decimal Preco { get; set; }
    }
}