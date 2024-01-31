using System;
using System.ComponentModel.DataAnnotations;

namespace Autoglass.Domain.Entities
{
    public class Produto
    {
        [Key]
        public int CodigoProduto { get; private set; }
        public string DescricaoProduto { get; private set; }
        public string SituacaoProduto { get; private set; }
        public DateTime DataFabricacao { get; private set; }
        public DateTime DataValidade { get; private set; }
        public int CodigoFornecedor { get; private set; }
        public string DescricaoFornecedor { get; private set; }
        public string CNPJFornecedor { get; private set; }

        private Produto() { }

        public static Produto Criar(string descricaoProduto, DateTime dataFabricacao,
                                    DateTime dataValidade, int codigoFornecedor,
                                    string descricaoFornecedor, string cnpjFornecedor, string situacaoProduto)
        {
            if (dataFabricacao >= dataValidade)
            {
                throw new ArgumentException("A data de fabricação não pode ser maior ou igual à data de validade.");
            }

            return new Produto
            {
                DescricaoProduto = descricaoProduto,
                DataFabricacao = dataFabricacao,
                DataValidade = dataValidade,
                CodigoFornecedor = codigoFornecedor,
                DescricaoFornecedor = descricaoFornecedor,
                CNPJFornecedor = cnpjFornecedor,
                SituacaoProduto = situacaoProduto
            };
        }

        public void Atualizar(string descricaoProduto, string situacaoProduto, DateTime dataFabricacao,
                          DateTime dataValidade, int codigoFornecedor,
                          string descricaoFornecedor, string cnpjFornecedor)
        {
            if (dataFabricacao >= dataValidade)
            {
                throw new ArgumentException("A data de fabricação não pode ser maior ou igual à data de validade.");
            }

            DescricaoProduto = descricaoProduto;
            DataFabricacao = dataFabricacao;
            DataValidade = dataValidade;
            CodigoFornecedor = codigoFornecedor;
            DescricaoFornecedor = descricaoFornecedor;
            CNPJFornecedor = cnpjFornecedor;
            SituacaoProduto = situacaoProduto;            
        }

        public void AtualizarSituacao(string novaSituacao)
        {
            SituacaoProduto = novaSituacao;
        }

#if DEBUG        
        public void DefinirCodigoProdutoParaTeste(int codigoProduto)
        {
            CodigoProduto = codigoProduto;
        }
#endif
    }
}
