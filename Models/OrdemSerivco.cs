namespace TechFix_API.Models
{
    public class OrdemServico
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public StatusOrdem Status { get; set; } = StatusOrdem.Pendente;
        public DateTime DataAbertura { get; set; } = DateTime.UtcNow;

        // Chave Estrangeira e Propriedade de Navegação
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
    }
}