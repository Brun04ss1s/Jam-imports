using JamImports.Api.Data;
using JamImports.Api.Models;
using JamImports.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JamImports.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly KafkaProducerService _kafkaProducer;

        public ProdutosController(AppDbContext context, KafkaProducerService kafkaProducer)
        {
            _context = context;
            _kafkaProducer = kafkaProducer;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarProduto(Produto produto)
        {
            _context.produtos.Add(produto);

            await _context.SaveChangesAsync();

            string topic = "jam-imports-new-product";
            await _kafkaProducer.SendAsyncMessage(topic, produto);

            return Ok(produto);
        }

        [HttpGet]
        public async Task<IActionResult> ListarProdutos()
        {
            var produtos = await _context.produtos.ToListAsync();
            return Ok(produtos);
        }
    }
}