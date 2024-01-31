using Autoglass.Domain.Entities;
using System.Collections.Generic;

namespace Autoglass.Domain.Repositories
{
    public interface IProdutoRepository
    {
        Task<Produto> ObterPorCodigoAsync(int codigoProduto);
        Task<IEnumerable<Produto>> ListarTodosAsync();
        Task AdicionarAsync(Produto produto);
        Task AtualizarAsync(Produto produto);
        Task ExcluirAsync(int codigoProduto);
        Task<IEnumerable<Produto>> ListarComFiltrosAsync(
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
            int tamanhoPagina);
    }
}
