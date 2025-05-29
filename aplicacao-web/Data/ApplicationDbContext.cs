using aplicacao_web.Models;
using Microsoft.EntityFrameworkCore;

namespace aplicacao_web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<CaixaDisponivel> CaixasDisponiveis { get; set; }
    }
}
