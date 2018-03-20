using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using lanchonete.Models;
using System.Linq;

namespace lanchonete.Controllers
{
    [Route("api/Produto")]
    //[Route("api/[Controller]")]
    public class ProdutoController : Controller
    {
        private readonly DataContext _context;

        public ProdutoController(DataContext context)
        {
            _context = context;

            if (_context.produtos.Count() == 0)
            {
                _context.produtos.Add(new Produto { Descricao = "coca", qtde = 20, tipo = "2" });
                _context.produtos.Add(new Produto { Descricao = "fanta", qtde = 20, tipo = "2" });
                _context.produtos.Add(new Produto { Descricao = "pepsi", qtde = 20, tipo = "2" });
                _context.produtos.Add(new Produto { Descricao = "guarana", qtde = 20, tipo = "2" });
                _context.produtos.Add(new Produto { Descricao = "hamburguer", qtde = 0, tipo = "1" });
                _context.produtos.Add(new Produto { Descricao = "xtudo", qtde = 0, tipo = "1" });
                _context.SaveChanges();
            }

        }


        [HttpGet]
        public IEnumerable<Produto> GetAll()
        {
            return _context.produtos.ToList();
        }


        [HttpGet("{id}", Name = "GetProduto")]
        public IActionResult GetById(long Id)
        {
            var item = _context.produtos.FirstOrDefault(t => t.Id == Id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Produto produtos)
        {
            if (produtos == null)
            {
                return BadRequest();
            }

            _context.produtos.Add(produtos);
            _context.SaveChanges();

            return CreatedAtRoute("GetProduto", new { id = produtos.Id }, produtos);
        }

        [HttpPut]
        public IActionResult Update(long id, [FromBody] Produto item)
        {

            if (item == null || item.Id != id)
            {
                return BadRequest();
            }
            var produtos = _context.produtos.FirstOrDefault(t => t.Id == id);

            if (produtos == null)
            {
                return NotFound();
            }

            produtos.Descricao = item.Descricao;
            produtos.qtde = item.qtde;
            produtos.tipo = item.tipo;

            _context.produtos.Update(produtos);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.produtos.FirstOrDefault(t => t.Id == id);

            if (todo == null)
            {
                return NotFound();
            }



            _context.produtos.Remove(todo);
            _context.SaveChanges();
            return new NoContentResult();

        }


    }
}