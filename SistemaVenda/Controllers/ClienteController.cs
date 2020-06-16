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
    public class ClienteController : Controller
    {
        protected ApplicationDbContext _dbContext;
        public ClienteController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            //
            IEnumerable<Cliente> lista = _dbContext.Cliente.ToList();
            _dbContext.Dispose();
            return View(lista);
        }
        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            ClienteViewModel viewModel = new ClienteViewModel();
            if (id != null)
            {
                var entity = _dbContext.Cliente.Where(x => x.Codigo == id).FirstOrDefault();
                viewModel.Codigo = entity.Codigo;
                viewModel.Nome = entity.Nome;
                viewModel.CNPJ_CPF = entity.CNPJ_CPF;
                viewModel.Email = entity.Email;
                viewModel.Celular = entity.Celular;
            }
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Cadastro(ClienteViewModel entity)
        {

            if (ModelState.IsValid)
            {
                Cliente objCliente = new Cliente()
                {
                    Codigo = entity.Codigo,
                    Nome = entity.Nome,
                    CNPJ_CPF = entity.CNPJ_CPF,
                    Email = entity.Email,
                    Celular = entity.Celular

                };
                if (entity.Codigo == null)
                {
                    _dbContext.Cliente.Add(objCliente);
                }
                else
                {
                    _dbContext.Entry(objCliente).State = EntityState.Modified;
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
            var entity = new Cliente()
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
