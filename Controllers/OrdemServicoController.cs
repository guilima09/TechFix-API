using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechFix_API.Data;
using TechFix_API.DTOs;
using TechFix_API.Models;

namespace TechFix_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdemServicoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdemServicoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ordensservico
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdemServicoResponseDto>>> GetOrdens()
        {
            var ordens = await _context.OrdensServico
                .Include(o => o.Cliente)
                .ToListAsync();

            var response = ordens.Select(o => new OrdemServicoResponseDto
            {
                Id = o.Id,
                Descricao = o.Descricao,
                Valor = o.Valor,
                Status = o.Status,
                DataAbertura = o.DataAbertura,
                ClienteId = o.ClienteId,
                NomeCliente = o.Cliente != null ? o.Cliente.Nome : "Cliente não encontrado"
            });

            return Ok(response);
        }

        // GET: api/ordensservico/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdemServicoResponseDto>> GetOrdem(int id)
        {
            var ordem = await _context.OrdensServico
                .Include(o => o.Cliente)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (ordem == null)
            {
                return NotFound(new { mensagem = $"Ordem de serviço {id} não encontrada." });
            }

            var response = new OrdemServicoResponseDto
            {
                Id = ordem.Id,
                Descricao = ordem.Descricao,
                Valor = ordem.Valor,
                Status = ordem.Status,
                DataAbertura = ordem.DataAbertura,
                ClienteId = ordem.ClienteId,
                NomeCliente = ordem.Cliente != null ? ordem.Cliente.Nome : "Cliente não encontrado"
            };

            return Ok(response);
        }

        // POST: api/ordensservico
        [HttpPost]
        public async Task<ActionResult<OrdemServicoResponseDto>> CreateOrdem(OrdemServicoCreateDto dto)
        {
            var clienteExiste = await _context.Clientes.AnyAsync(c => c.Id == dto.ClienteId);
            if (!clienteExiste)
            {
                return BadRequest(new { mensagem = $"Não existe nenhum cliente com o ID {dto.ClienteId}." });
            }

            var ordem = new OrdemServico
            {
                Descricao = dto.Descricao,
                Valor = dto.Valor,
                Status = dto.Status,
                ClienteId = dto.ClienteId,
                DataAbertura = DateTime.UtcNow
            };

            _context.OrdensServico.Add(ordem);
            await _context.SaveChangesAsync();

            var cliente = await _context.Clientes.FindAsync(dto.ClienteId);

            var response = new OrdemServicoResponseDto
            {
                Id = ordem.Id,
                Descricao = ordem.Descricao,
                Valor = ordem.Valor,
                Status = ordem.Status,
                DataAbertura = ordem.DataAbertura,
                ClienteId = ordem.ClienteId,
                NomeCliente = cliente?.Nome ?? string.Empty
            };

            return CreatedAtAction(nameof(GetOrdem), new { id = ordem.Id }, response);
        }

        // PUT: api/ordensservico/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrdem(int id, OrdemServicoCreateDto dto)
        {
            var ordem = await _context.OrdensServico.FindAsync(id);

            if (ordem == null)
            {
                return NotFound(new { mensagem = $"Ordem de serviço {id} não encontrada." });
            }

            var clienteExiste = await _context.Clientes.AnyAsync(c => c.Id == dto.ClienteId);
            if (!clienteExiste)
            {
                return BadRequest(new { mensagem = $"Não existe nenhum cliente com o ID {dto.ClienteId}." });
            }

            ordem.Descricao = dto.Descricao;
            ordem.Valor = dto.Valor;
            ordem.Status = dto.Status;
            ordem.ClienteId = dto.ClienteId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/ordensservico/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdem(int id)
        {
            var ordem = await _context.OrdensServico.FindAsync(id);

            if (ordem == null)
            {
                return NotFound(new { mensagem = $"Ordem de serviço {id} não encontrada." });
            }

            _context.OrdensServico.Remove(ordem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}