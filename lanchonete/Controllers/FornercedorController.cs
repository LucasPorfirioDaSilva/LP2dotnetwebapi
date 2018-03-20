using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using lanchonete.Models;
using System.Linq;

namespace lanchonete.Controllers
{
    [Route("api/Fornecedor")]
    //[Route("api/[Controller]")]
    public class FornecedorController:Controller
    {
        private readonly DataContext _context;

        public FornecedorController(DataContext context)
        {
                _context = context;
                
                if( _context.fornecedores.Count() == 0)
                {
                    _context.fornecedores.Add(new Fornecedor { Nome = "Max Pães", Telefone = "(88) 99999-9999" });
                    _context.fornecedores.Add(new Fornecedor { Nome = "Ambev", Telefone = "(88) 99999-9999" });
                    _context.fornecedores.Add(new Fornecedor { Nome = "Padaria", Telefone = "(88) 99999-9999" });
                    _context.fornecedores.Add(new Fornecedor { Nome = "Mercearia", Telefone = "(88) 99999-9999" });
                    _context.fornecedores.Add(new Fornecedor { Nome = "Concorrência", Telefone = "(88) 99999-9999" });
                    _context.fornecedores.Add(new Fornecedor { Nome = "Tio do pão", Telefone = "(88) 99999-9999" });
                    _context.SaveChanges();
                }

        }

        
        [HttpGet]
        public IEnumerable<Fornecedor> GetAll()
            {
                return _context.fornecedores.ToList();
            }


        [HttpGet("{id}", Name = "GetFornecedor")]
        public IActionResult GetById(long Id)
            {
                var item = _context.fornecedores.FirstOrDefault(t => t.Id == Id);
                if (item==null)
                {
                    return NotFound();
                }
                return new ObjectResult (item);
            }

        [HttpPost]
        public IActionResult Create([FromBody]Fornecedor fornecedor)
            {
                if(fornecedor== null )
                {
                    return BadRequest();
                }

            _context.fornecedores.Add(fornecedor);
            _context.SaveChanges();

            return CreatedAtRoute("GetFornecedor", new {id = fornecedor.Id } ,fornecedor );   
            }

        [HttpPut]
        public IActionResult Update(long id,[FromBody] Fornecedor item)
        {
            
            if (item==null || item.Id != id)
                {
                    return BadRequest();
                }
            var fornecedor = _context.fornecedores.FirstOrDefault(t=> t.Id == id);
            
            if(fornecedor == null)
                {
                    return NotFound();
                }
                
            fornecedor.Nome = item.Nome ;
            fornecedor.Telefone = item.Telefone;

            _context.fornecedores.Update(fornecedor);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo =_context.fornecedores.FirstOrDefault(t => t.Id == id);

            if(todo==null)
            {
                return NotFound();
            }

            

            _context.fornecedores.Remove(todo);
            _context.SaveChanges();
            return new NoContentResult();

        }


    }
}