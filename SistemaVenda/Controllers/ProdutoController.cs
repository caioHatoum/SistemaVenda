using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaVenda.DAL;
using SistemaVenda.Entities;
using SistemaVenda.Models;

namespace SistemaVenda.Controllers
{
    public class ProdutoController : Controller
    {
        protected ApplicationDbContext _dbContext;
        public ProdutoController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            //
            IEnumerable<Produto> lista = _dbContext.Produto.Include(x=>x.Categoria).ToList();
            _dbContext.Dispose();
            return View(lista);
        }
        private IEnumerable<SelectListItem> ListaCategoria()
        {
            List<SelectListItem> Lista = new List<SelectListItem>();
            Lista.Add(new SelectListItem() {
                Value = string.Empty,
                Text = string.Empty
            });
            foreach (var item in _dbContext.Categoria.ToList())
            {
                Lista.Add(new SelectListItem()
                {
                    Value = item.Codigo.ToString(),
                    Text = item.Descricao.ToString()
                });
            }
            return Lista;
        }
        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            ProdutoViewModel viewModel = new ProdutoViewModel();
            if (id != null)
            {
                var entity = _dbContext.Produto.Where(x => x.Codigo == id).FirstOrDefault();
                viewModel.Codigo = entity.Codigo;
                viewModel.Descricao = entity.Descricao;
                viewModel.Quantidade = entity.Quantidade;
                viewModel.Valor = entity.Valor;
                viewModel.CodigoCategoria = entity.CodigoCategoria;
            }
            viewModel.ListaCategorias = ListaCategoria();
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Cadastro(ProdutoViewModel entity)
        {

            if (ModelState.IsValid)
            {
                Produto objProduto = new Produto()
                {
                    Codigo = entity.Codigo,
                    Descricao = entity.Descricao,
                    Quantidade = entity.Quantidade,
                    Valor = (decimal)entity.Valor,
                    CodigoCategoria = (int)entity.CodigoCategoria
                };
                if (entity.Codigo == null)
                {
                    _dbContext.Produto.Add(objProduto);
                }
                else
                {
                    _dbContext.Entry(objProduto).State = EntityState.Modified;
                }
                _dbContext.SaveChanges();
            }
            else
            {
                entity.ListaCategorias = ListaCategoria();
                return View(entity);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var entity = new Produto()
            {
                Codigo = id
            };
            _dbContext.Attach(entity);
            _dbContext.Remove(entity);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
       
    }
}
