using System.Text.Json.Serialization;

namespace aplicacao_web.Models
{
    public class PedidosRequest
    {
        public List<Pedido> Pedidos { get; set; } = new();
    }

    public class Pedido
    {
        public int PedidoId { get; set; }
        public List<Produto> Produtos { get; set; } = new();
    }

    public class Produto
    {
        public string ProdutoId { get; set; } = string.Empty;
        public Dimensoes Dimensoes { get; set; } = new();
    }

    public class Dimensoes
    {
        public double Altura { get; set; }
        public double Largura { get; set; }
        public double Comprimento { get; set; }
    }

    public class PedidosResponse
    {
        public List<PedidoResponse> Pedidos { get; set; } = new();
    }

    public class PedidoResponse
    {
        public int PedidoId { get; set; }
        public List<Caixa> Caixas { get; set; } = new();
    }

    public class Caixa
    {
        public string? CaixaId { get; set; }
        public List<string> Produtos { get; set; } = new();
        public string? Observacao { get; set; }
    }

    public class CaixaDisponivel
    {
        public string Id { get; set; } = string.Empty;
        public double Altura { get; set; }
        public double Largura { get; set; }
        public double Comprimento { get; set; }
        public double Volume => Altura * Largura * Comprimento;
    }
} 