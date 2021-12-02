using CadastroPessoas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CadastroPessoas.Controllers {
    public class PessoasController : Controller {

        private readonly Contexto _context;

        public PessoasController(Contexto context) {
            _context = context;
        }
            
         public async Task<IActionResult> Index() {
            return View(await _context.Pessoas.ToListAsync());
        }

        [HttpGet]
        public IActionResult CriarPessoa() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CriarPessoa(Pessoa pessoa) {
            if (ModelState.IsValid) {
                _context.Add(pessoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            } else {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarPessoa(int? id) {
            if (id != null) {
                Pessoa pessoa = await _context.Pessoas.FindAsync(id);
                return View(pessoa);
            } else {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarPessoa(int? id, Pessoa pessoa) {
            if(id != null) {
                if (ModelState.IsValid) {
                    _context.Update(pessoa);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                } else {
                    return NotFound();
                }

            } else {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> ExcluirPessoa(int? id) {
            if(id != null) {
                Pessoa pessoa = await _context.Pessoas.FindAsync(id);
                return View(pessoa);
            } else {
                return NotFound();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> ExcluirPessoa(int? id, Pessoa pessoa) {
            if(id != null) {
                _context.Remove(pessoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            } else {
                return NotFound();
            }
        }
    }
}
