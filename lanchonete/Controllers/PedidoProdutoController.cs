using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using lanchonete.Models;
using System.Linq;

namespace lanchonete.Controllers
{
    [Route("api/PedidoProduto")]
    //[Route("api/[Controller]")]
    public class PedidoProdutoController : Controller
    {
        private readonly DataContext _context;

        public PedidoProdutoController(DataContext context)
        {
            _context = context;
            if( _context.pedidoproduto.Count() == 0)
                {
                    _context.pedidoproduto.Add(new PedidoProduto { Pedidoid =4 ,Produtoid=3 ,pqtde=10 });
                    _context.pedidoproduto.Add(new PedidoProduto { Pedidoid =4 ,Produtoid=2 ,pqtde=5 });
                    _context.pedidoproduto.Add(new PedidoProduto { Pedidoid =2 ,Produtoid=3 ,pqtde=6 });
                    _context.pedidoproduto.Add(new PedidoProduto { Pedidoid =2 ,Produtoid=1 ,pqtde=2 });
                    _context.pedidoproduto.Add(new PedidoProduto { Pedidoid =4 ,Produtoid=5 ,pqtde=3 });
                    _context.SaveChanges();
                }

        }

        [HttpGet]
        public IEnumerable<PedidoProduto> GetAll()
        {
            return _context.pedidoproduto.ToList();
        }


        [HttpGet("{id}", Name = "GetPedidoProduto")]
        public IActionResult GetById(long Id)
        {
            var item = _context.pedidos.FirstOrDefault(t => t.id == Id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody]PedidoProduto pedidoproduto)
        {
            if (pedidoproduto == null)
            {
                return BadRequest();
            }

            _context.pedidoproduto.Add(pedidoproduto);
            _context.SaveChanges();

            return CreatedAtRoute("GetProdutoPedido", new { id = pedidoproduto.id }, pedidoproduto);
        }

        [HttpPut]
        public IActionResult Update(long id, [FromBody] PedidoProduto item)
        {

            if (item == null || item.id != id)
            {
                return BadRequest();
            }
            var pedidoproduto = _context.pedidoproduto.FirstOrDefault(t => t.id == id);

            if (pedidoproduto == null)
            {
                return NotFound();
            }

            pedidoproduto = item;

            _context.pedidoproduto.Update(pedidoproduto);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.pedidoproduto.FirstOrDefault(t => t.id == id);

            if (todo == null)
            {
                return NotFound();
            }



            _context.pedidoproduto.Remove(todo);
            _context.SaveChanges();
            return new NoContentResult();

        }


    }
}