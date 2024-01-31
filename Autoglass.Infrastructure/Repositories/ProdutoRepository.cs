using Autoglass.Domain.Entities;
using Autoglass.Domain.Repositories;
using Autoglass.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Autoglass.Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly MeuDbContext _context;

        public ProdutoRepository(MeuDbContext context)
        {
            _context = context;
        }

        public async Task<Produto> ObterPorCodigoAsync(int codigoProduto)
        {
            return _context.Produtos.FirstOrDefault(p => p.CodigoProduto == codigoProduto);
        }

        public async Task<IEnumerable<Produto>> ListarTodosAsync()
        {
            return _context.Produtos.ToList();
        }

        public async Task AdicionarAsync(Produto produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();
        }

        public async Task AtualizarAsync(Produto produto)
        {
            _context.Produtos.Update(produto);
            _context.SaveChanges();
        }

        public async Task ExcluirAsync(int codigoProduto)
        {
            var produto = await ObterPorCodigoAsync(codigoProduto);
            if (produto != null)
            {
                produto.AtualizarSituacao("Inativo");
                _context.SaveChanges();
            }
        }
        public async Task<IEnumerable<Produto>> ListarComFiltrosAsync(
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
            var query = _context.Produtos.AsQueryable();

            if (codigoProduto.HasValue)
            {
                query = query.Where(p => p.CodigoProduto == codigoProduto.Value);
            }

            if (!string.IsNullOrWhiteSpace(descricaoProduto))
            {
                query = query.Where(p => p.DescricaoProduto.Contains(descricaoProduto));
            }

            if (!string.IsNullOrWhiteSpace(situacaoProduto))
            {
                query = query.Where(p => p.SituacaoProduto == situacaoProduto);
            }

            if (dataFabricacaoInicio.HasValue && dataFabricacaoFim.HasValue)
            {
                query = query.Where(p => p.DataFabricacao >= dataFabricacaoInicio.Value && p.DataFabricacao <= dataFabricacaoFim.Value);
            }

            if (dataValidadeInicio.HasValue && dataValidadeFim.HasValue)
            {
                query = query.Where(p => p.DataValidade >= dataValidadeInicio.Value && p.DataValidade <= dataValidadeFim.Value);
            }

            if (codigoFornecedor.HasValue)
            {
                query = query.Where(p => p.CodigoFornecedor == codigoFornecedor.Value);
            }

            if (!string.IsNullOrWhiteSpace(descricaoFornecedor))
            {
                query = query.Where(p => p.DescricaoFornecedor.Contains(descricaoFornecedor));
            }

            if (!string.IsNullOrWhiteSpace(cnpjFornecedor))
            {
                query = query.Where(p => p.CNPJFornecedor == cnpjFornecedor);
            }

            query = query.Skip((pagina - 1) * tamanhoPagina).Take(tamanhoPagina);

            return await query.ToListAsync();
        }
    }
}
