using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVenda.DAL;
using SistemaVenda.Entities;
using SistemaVenda.Models;

namespace SistemaVenda.Controllers
{
    public class CategoriaController : Controller
    {
        protected ApplicationDbContext _dbContext;
        public CategoriaController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            //
            IEnumerable<Categoria> lista = _dbContext.Categoria.ToList();
            _dbContext.Dispose();
            return View(lista);
        }
        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            CategoriaViewModel viewModel = new CategoriaViewModel();
            if (id != null)
            {
                var entity = _dbContext.Categoria.Where(x => x.Codigo == id).FirstOrDefault();
                viewModel.Codigo = entity.Codigo;
                viewModel.Descricao = entity.Descricao;
            }
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Cadastro(CategoriaViewModel entity)
        {
           
            if (ModelState.IsValid)
            {
                Categoria objCategoria = new Categoria()
                {
                    Codigo = entity.Codigo ,
                    Descricao = entity.Descricao
                };
                if(entity.Codigo == null)
                {
                    _dbContext.Categoria.Add(objCategoria);
                }
                else
                {
                    _dbContext.Entry(objCategoria).State = EntityState.Modified;
                }
                _dbContext.SaveChanges();
            }
            else
            {
                return View(entity);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var entity = new Categoria()
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
