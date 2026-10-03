using TechFix_API.Models;

namespace TechFix_API.DTOs
{
    public class OrdemServicoResponseDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public StatusOrdem Status { get; set; }
        public DateTime DataAbertura { get; set; }
        public int ClienteId { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
    }
}