using Microsoft.EntityFrameworkCore;
namespace lanchonete.Models
{
   public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        :base(options)
        {
        }

        public DbSet<Fornecedor> fornecedores { get; set;}
    /*  public DbSet<Produto> produtos { get; set;}
        public DbSet<Funcionario> funcionarios { get; set; }
        public DbSet<Pedido> pedidos { get; set;}
        public DbSet<PedidoProduto> pedidoproduto { get; set;}  */
    }
}