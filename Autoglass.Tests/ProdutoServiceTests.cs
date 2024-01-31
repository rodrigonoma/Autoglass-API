using Autoglass.Application.DTOs;
using Autoglass.Application.Services;
using Autoglass.Domain.Entities;
using Autoglass.Domain.Repositories;
using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

public class ProdutoServiceTests
{
    private readonly Mock<IProdutoRepository> _produtoRepositoryMock = new Mock<IProdutoRepository>();
    private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
    private readonly ProdutoService _produtoService;

    public ProdutoServiceTests()
    {
        _produtoService = new ProdutoService(_produtoRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task ObterPorCodigoAsync_DeveRetornarProduto_QuandoExistir()
    {
        // Arrange
        var produtoMock = Produto.Criar("Descrição Teste", DateTime.UtcNow, DateTime.UtcNow.AddDays(10), 1, "Fornecedor Teste", "123456789", "Ativo");
        var produtoDtoMock = new ProdutoDto();
        _produtoRepositoryMock.Setup(repo => repo.ObterPorCodigoAsync(It.IsAny<int>()))
                              .ReturnsAsync(produtoMock);
        _mapperMock.Setup(mapper => mapper.Map<ProdutoDto>(produtoMock))
                   .Returns(produtoDtoMock);

        // Act
        var result = await _produtoService.ObterPorCodigoAsync(1);

        // Assert
        result.Should().BeEquivalentTo(produtoDtoMock);
    }

    [Fact]
    public async Task ListarTodosAsync_DeveRetornarProdutos()
    {
        // Arrange
        var produtoMock1 = Produto.Criar(
            "Descrição Teste 1",
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(10),
            1,
            "Fornecedor Teste 1",
            "123456789",
            "Ativo");

        var produtoMock2 = Produto.Criar(
            "Descrição Teste 2",
            DateTime.UtcNow.AddDays(-5),
            DateTime.UtcNow.AddDays(5),
            2,
            "Fornecedor Teste 2",
            "987654321", 
            "Ativo");

        var produtosMock = new List<Produto> { produtoMock1, produtoMock2 };

        var produtosDtoMock = new List<ProdutoDto>
    {
        new ProdutoDto
        {
            CodigoProduto = 1,
            DescricaoProduto = "Produto Teste 1",
            DataFabricacao = new DateTime(2022, 1, 1),
            DataValidade = new DateTime(2023, 1, 1),
            CodigoFornecedor = 101,
            DescricaoFornecedor = "Fornecedor Teste 1",
            CNPJFornecedor = "12.345.678/0001-99",
            SituacaoProduto = "Ativo"
        },
        new ProdutoDto
        {
            CodigoProduto = 2,
            DescricaoProduto = "Produto Teste 2",
            DataFabricacao = new DateTime(2022, 2, 1),
            DataValidade = new DateTime(2023, 2, 1),
            CodigoFornecedor = 102,
            DescricaoFornecedor = "Fornecedor Teste 2",
            CNPJFornecedor = "98.765.432/0001-00",
            SituacaoProduto = "Ativo"
        }
    };

        _produtoRepositoryMock.Setup(repo => repo.ListarComFiltrosAsync(
            null, null, null, null, null, null, null, null, null, null, 1, 10))
            .ReturnsAsync(produtosMock);

        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<ProdutoDto>>(produtosMock))
            .Returns(produtosDtoMock);

        // Act
        var result = await _produtoService.ListarTodosAsync(null, null, null, null, null, null, null, null, null, null, 1, 10);

        // Assert
        result.Should().BeEquivalentTo(produtosDtoMock);
        _produtoRepositoryMock.Verify(repo => repo.ListarComFiltrosAsync(
            null, null, null, null, null, null, null, null, null, null, 1, 10), Times.Once);
    }


    [Fact]
    public async Task AdicionarAsync_DeveAdicionarProduto()
    {
        // Arrange
        var produtoDto = new ProdutoDto
        {
            DescricaoProduto = "Descrição Teste",
            DataFabricacao = DateTime.UtcNow,
            DataValidade = DateTime.UtcNow.AddDays(10),
            CodigoFornecedor = 1,
            DescricaoFornecedor = "Fornecedor Teste",
            CNPJFornecedor = "123456789"
        };

        // Act
        await _produtoService.AdicionarAsync(produtoDto);

        // Assert
        _produtoRepositoryMock.Verify(repo => repo.AdicionarAsync(It.Is<Produto>(p =>
            p.DescricaoProduto == produtoDto.DescricaoProduto &&
            p.DataFabricacao == produtoDto.DataFabricacao &&
            p.DataValidade == produtoDto.DataValidade &&
            p.CodigoFornecedor == produtoDto.CodigoFornecedor &&
            p.DescricaoFornecedor == produtoDto.DescricaoFornecedor &&
            p.CNPJFornecedor == produtoDto.CNPJFornecedor
        )), Times.Once);
    }

    [Fact]
    public async Task AtualizarAsync_DeveAtualizarProduto()
    {
        // Arrange
        var produtoDtoMock = new ProdutoDto
        {
            CodigoProduto = 1,
            DescricaoProduto = "Descrição Teste",
            SituacaoProduto = "Ativo",
            DataFabricacao = DateTime.UtcNow,
            DataValidade = DateTime.UtcNow.AddDays(10),
            CodigoFornecedor = 1,
            DescricaoFornecedor = "Fornecedor Teste",
            CNPJFornecedor = "123456789"
        };

        var produtoExistenteMock = Produto.Criar("Descrição Teste", DateTime.UtcNow, DateTime.UtcNow.AddDays(10), 1, "Fornecedor Teste", "123456789", "Ativo");
        produtoExistenteMock.DefinirCodigoProdutoParaTeste(1);
        _produtoRepositoryMock.Setup(repo => repo.ObterPorCodigoAsync(produtoDtoMock.CodigoProduto))
                              .ReturnsAsync(produtoExistenteMock);

        // Act
        await _produtoService.AtualizarAsync(produtoDtoMock);

        // Assert
        _produtoRepositoryMock.Verify(repo => repo.AtualizarAsync(produtoExistenteMock), Times.Once);
    }


    [Fact]
    public async Task ExcluirAsync_DeveExcluirProduto()
    {
        // Arrange
        var codigoProduto = 1;

        // Act
        await _produtoService.ExcluirAsync(codigoProduto);

        // Assert
        _produtoRepositoryMock.Verify(repo => repo.ExcluirAsync(codigoProduto), Times.Once);
    }
}
