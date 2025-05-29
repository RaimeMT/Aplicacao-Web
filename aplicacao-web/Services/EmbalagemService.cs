using aplicacao_web.Models;

namespace aplicacao_web.Services
{
    public interface IEmbalagemService
    {
        PedidosResponse ProcessarPedidos(PedidosRequest request);
    }

    public class EmbalagemService : IEmbalagemService
    {
        private readonly List<CaixaDisponivel> _caixasDisponiveis;
        private readonly List<CaixaDisponivel> _caixasEspeciaisDisponiveis;

        public EmbalagemService()
        {
            _caixasDisponiveis = new List<CaixaDisponivel>
            {
                new() { Id = "Caixa 1", Altura = 30, Largura = 40, Comprimento = 80 },
                new() { Id = "Caixa 2", Altura = 80, Largura = 50, Comprimento = 40 },
                new() { Id = "Caixa 3", Altura = 50, Largura = 80, Comprimento = 60 }
            };
            _caixasEspeciaisDisponiveis = new List<CaixaDisponivel>
            {
                new() { Id = "Caixa Especial 1", Altura = 130, Largura = 100, Comprimento = 100 },
                new() { Id = "Caixa Especial 2", Altura = 100, Largura = 100, Comprimento = 130 },
                new() { Id = "Caixa Especial 3", Altura = 100, Largura = 130, Comprimento = 100 }
            };
        }

        public PedidosResponse ProcessarPedidos(PedidosRequest request)
        {
            var response = new PedidosResponse();

            foreach (var pedido in request.Pedidos)
            {
                var pedidoResponse = ProcessarPedido(pedido);
                response.Pedidos.Add(pedidoResponse);
            }

            return response;
        }

        private PedidoResponse ProcessarPedido(Pedido pedido)
        {
            var pedidoResponse = new PedidoResponse { PedidoId = pedido.PedidoId };
            var produtosRestantes = new List<Produto>(pedido.Produtos);

            while (produtosRestantes.Any())
            {
                var melhorCaixa = EncontrarMelhorCaixa(produtosRestantes);
                
                if (melhorCaixa.caixa == null)
                {
                    // Produto não cabe em nenhuma caixa
                    var produtoProblematico = produtosRestantes.First();
                    pedidoResponse.Caixas.Add(new Caixa
                    {
                        CaixaId = null,
                        Produtos = new List<string> { produtoProblematico.ProdutoId },
                        Observacao = "Produto não cabe em nenhuma caixa disponível."
                    });
                    produtosRestantes.Remove(produtoProblematico);
                }
                else
                {
                    pedidoResponse.Caixas.Add(new Caixa
                    {
                        CaixaId = melhorCaixa.caixa.Id,
                        Produtos = melhorCaixa.produtos.Select(p => p.ProdutoId).ToList(),
                        Observacao = $"Caixa: {melhorCaixa.caixa.Id} - Volume: {melhorCaixa.caixa.Volume.ToString("0.##")} cm³."
                    });

                    foreach (var produto in melhorCaixa.produtos)
                    {
                        produtosRestantes.Remove(produto);
                    }
                }
            }

            return pedidoResponse;
        }

        private (CaixaDisponivel? caixa, List<Produto> produtos) EncontrarMelhorCaixa(List<Produto> produtos)
        {
            // Tenta otimizar colocando múltiplos produtos em uma caixa
            foreach (var caixa in _caixasDisponiveis.OrderBy(c => c.Volume))
            {
                var produtosCabem = new List<Produto>();
                double volumeUsado = 0;

                foreach (var produto in produtos)
                {
                    if (ProdutoCabeNaCaixa(produto, caixa))
                    {
                        var volumeProduto = produto.Dimensoes.Altura * produto.Dimensoes.Largura * produto.Dimensoes.Comprimento;
                        if (volumeUsado + volumeProduto <= caixa.Volume)
                        {
                            produtosCabem.Add(produto);
                            volumeUsado += volumeProduto;
                        }
                    }
                }

                if (produtosCabem.Any())
                {
                    return (caixa, produtosCabem);
                }
            }

            return (null, new List<Produto>());
        }

        private bool ProdutoCabeNaCaixa(Produto produto, CaixaDisponivel caixa)
        {
            var p = produto.Dimensoes;
            
            // Verifica todas as orientações possíveis do produto
            return (p.Altura <= caixa.Altura && p.Largura <= caixa.Largura && p.Comprimento <= caixa.Comprimento) ||
                   (p.Altura <= caixa.Altura && p.Comprimento <= caixa.Largura && p.Largura <= caixa.Comprimento) ||
                   (p.Largura <= caixa.Altura && p.Altura <= caixa.Largura && p.Comprimento <= caixa.Comprimento) ||
                   (p.Largura <= caixa.Altura && p.Comprimento <= caixa.Largura && p.Altura <= caixa.Comprimento) ||
                   (p.Comprimento <= caixa.Altura && p.Altura <= caixa.Largura && p.Largura <= caixa.Comprimento) ||
                   (p.Comprimento <= caixa.Altura && p.Largura <= caixa.Largura && p.Altura <= caixa.Comprimento);
        }
    }
} 