using Autoglass.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autoglass.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<ProdutoDto> ObterPorCodigoAsync(int codigoProduto);
        Task<IEnumerable<ProdutoDto>> ListarTodosAsync(
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

        Task AdicionarAsync(ProdutoDto produtoDto);
        Task AtualizarAsync(ProdutoDto produtoDto);
        Task ExcluirAsync(int codigoProduto);
    }
}
