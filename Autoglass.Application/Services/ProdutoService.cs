using Autoglass.Application.DTOs;
using Autoglass.Application.Interfaces;
using Autoglass.Domain.Entities;
using Autoglass.Domain.Repositories;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autoglass.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task<ProdutoDto> ObterPorCodigoAsync(int codigoProduto)
        {
            var produto = await _produtoRepository.ObterPorCodigoAsync(codigoProduto);
            return _mapper.Map<ProdutoDto>(produto);
        }

        public async Task<IEnumerable<ProdutoDto>> ListarTodosAsync(
            int? codigoProduto,
            string descricaoProduto,
            string situacaoProduto,
            DateTime? dataFabricacaoInicio,
            DateTime? dataFabricacaoFim,
            DateTime? dataValidadeInicio,
            DateTime? dataValidadeFim,
            int? codigoFornecedor,
            string descricaoFornecedor,
            string cnpjFornecedor,
            int pagina,
            int tamanhoPagina)
        {
            var produtos = await _produtoRepository.ListarComFiltrosAsync(
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

            return _mapper.Map<IEnumerable<ProdutoDto>>(produtos);
        }

        public async Task AdicionarAsync(ProdutoDto produtoDto)
        {
            var produto = Produto.Criar(
               produtoDto.DescricaoProduto,
               produtoDto.DataFabricacao,
               produtoDto.DataValidade,
               produtoDto.CodigoFornecedor,
               produtoDto.DescricaoFornecedor,
               produtoDto.CNPJFornecedor,
               produtoDto.SituacaoProduto
           );

            await _produtoRepository.AdicionarAsync(produto);
        }

        public async Task AtualizarAsync(ProdutoDto produtoDto)
        {
            var produtoExistente = await _produtoRepository.ObterPorCodigoAsync(produtoDto.CodigoProduto);
            if (produtoExistente == null)
            {
                throw new KeyNotFoundException("Produto não encontrado.");
            }

            produtoExistente.Atualizar(produtoDto.DescricaoProduto,produtoDto.SituacaoProduto, produtoDto.DataFabricacao,
                                       produtoDto.DataValidade, produtoDto.CodigoFornecedor,
                                       produtoDto.DescricaoFornecedor, produtoDto.CNPJFornecedor);

            await _produtoRepository.AtualizarAsync(produtoExistente);
        }

        public async Task ExcluirAsync(int codigoProduto)
        {
            await _produtoRepository.ExcluirAsync(codigoProduto);
        }
    }
}
