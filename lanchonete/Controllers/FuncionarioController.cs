using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using lanchonete.Models;
using System.Linq;

namespace lanchonete.Controllers
{
    [Route("api/Funcionario")]
    //[Route("api/[Controller]")]
    public class FuncionarioController : Controller
    {
        private readonly DataContext _context;

        public FuncionarioController(DataContext context)
        {
            _context = context;
            
            if (_context.funcionarios.Count() == 0)
            {
                _context.funcionarios.Add(new Funcionario { Nome = "bigode", telefone = "(88) 99999-9999" });
                _context.funcionarios.Add(new Funcionario { Nome = "elaine", telefone = "(88) 99999-9999" });
                _context.funcionarios.Add(new Funcionario { Nome = "jones", telefone = "(88) 99999-9999" });
                _context.funcionarios.Add(new Funcionario { Nome = "batista", telefone = "(88) 99999-9999" });
                _context.funcionarios.Add(new Funcionario { Nome = "rodrigo", telefone = "(88) 99999-9999" });
                _context.funcionarios.Add(new Funcionario { Nome = "jesus", telefone = "(88) 99999-9999" });
                _context.SaveChanges();
            }
        }


        [HttpGet]
        public IEnumerable<Funcionario> GetAll()
        {
            return _context.funcionarios.ToList();
        }


        [HttpGet("{id}", Name = "GetFuncionario")]
        public IActionResult GetById(long Id)
        {
            var item = _context.funcionarios.FirstOrDefault(t => t.Id == Id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Funcionario funcionarios)
        {
            if (funcionarios == null)
            {
                return BadRequest();
            }

            _context.funcionarios.Add(funcionarios);
            _context.SaveChanges();

            return CreatedAtRoute("GetFuncionario", new { id = funcionarios.Id }, funcionarios);
        }

        [HttpPut]
        public IActionResult Update(long id, [FromBody] Funcionario item)
        {

            if (item == null || item.Id != id)
            {
                return BadRequest();
            }
            var funcionarios = _context.funcionarios.FirstOrDefault(t => t.Id == id);

            if (funcionarios == null)
            {
                return NotFound();
            }

            funcionarios.Nome = item.Nome;
            funcionarios.telefone = item.telefone;

            _context.funcionarios.Update(funcionarios);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.funcionarios.FirstOrDefault(t => t.Id == id);

            if (todo == null)
            {
                return NotFound();
            }



            _context.funcionarios.Remove(todo);
            _context.SaveChanges();
            return new NoContentResult();

        }


    }
}