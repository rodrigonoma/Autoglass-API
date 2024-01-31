using Autoglass.Application.DTOs;
using Autoglass.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Autoglass.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutosController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet("{codigoProduto}")]
        public async Task<ActionResult<ProdutoDto>> Get(int codigoProduto)
        {
            var produto = await _produtoService.ObterPorCodigoAsync(codigoProduto);
            if (produto == null) return NotFound();
            return Ok(produto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetAll(
        int? codigoProduto = null,
        string descricaoProduto = null,
        string situacaoProduto = null,
        DateTime? dataFabricacaoInicio = null,
        DateTime? dataFabricacaoFim = null,
        DateTime? dataValidadeInicio = null,
        DateTime? dataValidadeFim = null,
        int? codigoFornecedor = null,
        string descricaoFornecedor = null,
        string cnpjFornecedor = null,
        int pagina = 1,
        int tamanhoPagina = 10)
        {
            var produtos = await _produtoService.ListarTodosAsync(
                codigoProduto,
                descricaoProduto,
                situacaoProduto,
                dataFabricacaoInicio,
                dataFabricacaoFim,
                dataValidadeInicio,
                dataValidadeFim,
                codigoFornecedor,
                descricaoFornecedor,
                cnpjFornecedor,
                pagina,
                tamanhoPagina);

            return Ok(produtos);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProdutoDto produtoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _produtoService.AdicionarAsync(produtoDto);
                return CreatedAtAction(nameof(Get), new { codigoProduto = produtoDto.CodigoProduto }, produtoDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{codigoProduto}")]
        public async Task<ActionResult> Put(int codigoProduto, [FromBody] ProdutoDto produtoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (codigoProduto != produtoDto.CodigoProduto)
            {
                return BadRequest("Código do produto na URL não corresponde ao corpo da requisição.");
            }

            try
            {
                await _produtoService.AtualizarAsync(produtoDto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{codigoProduto}")]
        public async Task<ActionResult> Delete(int codigoProduto)
        {
            await _produtoService.ExcluirAsync(codigoProduto);
            return NoContent();
        }
    }
}
