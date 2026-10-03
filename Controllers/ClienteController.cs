using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechFix_API.Data;
using TechFix_API.DTOs;
using TechFix_API.Models;

namespace TechFix_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Injeção de Dependência do DbContext no construtor
        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteResponseDto>>> GetClientes()
        {
            var clientes = await _context.Clientes.ToListAsync();

            // Mapeando a lista de Entidades para a lista de DTOs
            var response = clientes.Select(c => new ClienteResponseDto
            {
                Id = c.Id,
                Nome = c.Nome,
                Email = c.Email,
                Telefone = c.Telefone,
                DataCadastro = c.DataCadastro
            });

            return Ok(response);
        }

        // GET: api/clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteResponseDto>> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound(new { mensagem = $"Cliente com ID {id} não foi encontrado." });
            }

            var response = new ClienteResponseDto
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Email = cliente.Email,
                Telefone = cliente.Telefone,
                DataCadastro = cliente.DataCadastro
            };

            return Ok(response);
        }

        // POST: api/clientes
        [HttpPost]
        public async Task<ActionResult<ClienteResponseDto>> CreateCliente(ClienteCreateDto dto)
        {
            // Mapeando do DTO de Entrada para a Entidade do Banco
            var cliente = new Cliente
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Telefone = dto.Telefone,
                DataCadastro = DateTime.UtcNow
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            // Preparando o DTO de Resposta
            var response = new ClienteResponseDto
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Email = cliente.Email,
                Telefone = cliente.Telefone,
                DataCadastro = cliente.DataCadastro
            };

            // Retorna o HTTP 201 Created com a rota para buscar o item recém-criado
            return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, response);
        }

        // PUT: api/clientes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, ClienteCreateDto dto)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound(new { mensagem = $"Cliente com ID {id} não foi encontrado." });
            }

            // Atualiza os campos da Entidade com os dados recebidos no DTO
            cliente.Nome = dto.Nome;
            cliente.Email = dto.Email;
            cliente.Telefone = dto.Telefone;

            await _context.SaveChangesAsync();

            return NoContent(); // HTTP 204 sem conteúdo no corpo
        }

        // DELETE: api/clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound(new { mensagem = $"Cliente com ID {id} não foi encontrado." });
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}