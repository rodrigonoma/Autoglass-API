using System;
using System.ComponentModel.DataAnnotations;

namespace Autoglass.Application.DTOs
{
    public class ProdutoDto
    {
        public int CodigoProduto { get; set; }

        [Required(ErrorMessage = "Descrição do produto é obrigatória.")]
        public string DescricaoProduto { get; set; }

        [Required(ErrorMessage = "Situação do produto é obrigatória.")]
        [RegularExpression("(Ativo|Inativo)", ErrorMessage = "Situação do produto deve ser 'Ativo' ou 'Inativo'.")]
        public string SituacaoProduto { get; set; } = "Ativo";

        [Required(ErrorMessage = "Data de fabricação é obrigatória.")]
        public DateTime DataFabricacao { get; set; }

        [Required(ErrorMessage = "Data de validade é obrigatória.")]
        public DateTime DataValidade { get; set; }

        public int CodigoFornecedor { get; set; }

        public string DescricaoFornecedor { get; set; }

        [RegularExpression(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$", ErrorMessage = "CNPJ do fornecedor não é válido.")]
        public string CNPJFornecedor { get; set; }
    }
}
