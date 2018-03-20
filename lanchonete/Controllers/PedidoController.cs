using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using lanchonete.Models;
using System.Linq;

namespace lanchonete.Controllers
{
    [Route("api/Pedido")]
    //[Route("api/[Controller]")]
    public class PedidoController : Controller
    {
        private readonly DataContext _context;

        public PedidoController(DataContext context)
        {
            _context = context;
            if (_context.pedidos.Count() == 0)
            {
                _context.pedidos.Add(new Pedido { fun_id = 1, pid = 1 , pqtde = 2 });
                _context.pedidos.Add(new Pedido { fun_id = 0, pid = 5, pqtde = 2 });
                _context.pedidos.Add(new Pedido { fun_id = 2, pid = 6, pqtde = 2 });
                _context.pedidos.Add(new Pedido { fun_id = 3, pid = 2, pqtde = 2 });
                _context.pedidos.Add(new Pedido { fun_id = 4, pid = 3 , pqtde = 2 });
                _context.pedidos.Add(new Pedido { fun_id = 5, pid = 4 , pqtde = 2 });
                _context.SaveChanges();
            }

        }


        [HttpGet]
        public IEnumerable<Pedido> GetAll()
        {
            return _context.pedidos.ToList();
        }


        [HttpGet("{id}", Name = "GetPedido")]
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
        public IActionResult Create([FromBody]Pedido pedidos)
        {
            if (pedidos == null)
            {
                return BadRequest();
            }

            _context.pedidos.Add(pedidos);
            _context.SaveChanges();

            return CreatedAtRoute("GetProduto", new { id = pedidos.id }, pedidos);
        }

        [HttpPut]
        public IActionResult Update(long id, [FromBody] Pedido item)
        {

            if (item == null || item.id != id)
            {
                return BadRequest();
            }
            var pedidos = _context.pedidos.FirstOrDefault(t => t.id == id);

            if (pedidos == null)
            {
                return NotFound();
            }

            pedidos = item;

            _context.pedidos.Update(pedidos);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.pedidos.FirstOrDefault(t => t.id == id);

            if (todo == null)
            {
                return NotFound();
            }



            _context.pedidos.Remove(todo);
            _context.SaveChanges();
            return new NoContentResult();

        }


    }
}