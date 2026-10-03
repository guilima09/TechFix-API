using System.ComponentModel.DataAnnotations;
using TechFix_API.Models;

namespace TechFix_API.DTOs
{
    public class OrdemServicoCreateDto
    {
        [Required(ErrorMessage = "A descrição do problema é obrigatória.")]
        [StringLength(500, ErrorMessage = "A descrição não pode ter mais de 500 caracteres.")]
        public string Descricao { get; set; } = string.Empty;

        [Range(0, 999999.99, ErrorMessage = "O valor deve ser positivo.")]
        public decimal Valor { get; set; }

        public StatusOrdem Status { get; set; } = StatusOrdem.Pendente;

        [Required(ErrorMessage = "O ID do cliente é obrigatório.")]
        public int ClienteId { get; set; }
    }
}