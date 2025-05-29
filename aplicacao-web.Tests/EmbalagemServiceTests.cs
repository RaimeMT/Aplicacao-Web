using Xunit;
using aplicacao_web.Services;
using aplicacao_web.Models;
using System.Collections.Generic;

namespace aplicacao_web.Tests
{
    public class EmbalagemServiceTests
    {
        private readonly EmbalagemService _service;

        public EmbalagemServiceTests()
        {
            _service = new EmbalagemService();
        }

        [Fact]
        public void Deve_Colocar_Produto_Em_Caixa_Quando_Cabe()
        {
            // Arrange
            var pedido = new Pedido
            {
                PedidoId = 1,
                Produtos = new List<Produto>
                {
                    new Produto
                    {
                        ProdutoId = "PS5",
                        Dimensoes = new Dimensoes { Altura = 20, Largura = 20, Comprimento = 20 }
                    }
                }
            };
            var request = new PedidosRequest { Pedidos = new List<Pedido> { pedido } };

            // Act
            var resultado = _service.ProcessarPedidos(request);

            // Assert
            Assert.Single(resultado.Pedidos);
            Assert.Single(resultado.Pedidos[0].Caixas);
            Assert.Contains("PS5", resultado.Pedidos[0].Caixas[0].Produtos);
            Assert.NotNull(resultado.Pedidos[0].Caixas[0].CaixaId);
        }

        [Fact]
        public void Deve_Retornar_Observacao_Quando_Produto_Nao_Cabe()
        {
            // Arrange
            var pedido = new Pedido
            {
                PedidoId = 2,
                Produtos = new List<Produto>
                {
                    new Produto
                    {
                        ProdutoId = "Cadeira Gamer",
                        Dimensoes = new Dimensoes { Altura = 200, Largura = 200, Comprimento = 200 }
                    }
                }
            };
            var request = new PedidosRequest { Pedidos = new List<Pedido> { pedido } };

            // Act
            var resultado = _service.ProcessarPedidos(request);

            // Assert
            Assert.Single(resultado.Pedidos);
            Assert.Single(resultado.Pedidos[0].Caixas);
            Assert.Null(resultado.Pedidos[0].Caixas[0].CaixaId);
            Assert.Equal("Produto não cabe em nenhuma caixa disponível.", resultado.Pedidos[0].Caixas[0].Observacao);
        }

        [Fact]
        public void Deve_Processar_Multiplos_Pedidos()
        {
            // Arrange
            var pedidos = new List<Pedido>
            {
                new Pedido
                {
                    PedidoId = 1,
                    Produtos = new List<Produto>
                    {
                        new Produto
                        {
                            ProdutoId = "PS5",
                            Dimensoes = new Dimensoes { Altura = 20, Largura = 20, Comprimento = 20 }
                        }
                    }
                },
                new Pedido
                {
                    PedidoId = 2,
                    Produtos = new List<Produto>
                    {
                        new Produto
                        {
                            ProdutoId = "Cadeira Gamer",
                            Dimensoes = new Dimensoes { Altura = 200, Largura = 200, Comprimento = 200 }
                        }
                    }
                }
            };
            var request = new PedidosRequest { Pedidos = pedidos };

            // Act
            var resultado = _service.ProcessarPedidos(request);

            // Assert
            Assert.Equal(2, resultado.Pedidos.Count);
            Assert.Single(resultado.Pedidos[0].Caixas);
            Assert.Single(resultado.Pedidos[1].Caixas);
        }
    }
}