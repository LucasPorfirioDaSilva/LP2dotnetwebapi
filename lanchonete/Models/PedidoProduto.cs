namespace lanchonete.Models
{
    public class PedidoProduto
    {
        public long id { get; set; }
        public long Pedidoid { get; set; }
        public Pedido pedido { get; set; }
        public long Produtoid { get; set; }   
        public Produto produto { get; set; }
        public int  pqtde { get; set; }
    }
}