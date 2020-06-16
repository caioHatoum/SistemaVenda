using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SistemaVenda.DAL;
using SistemaVenda.Entities;
using SistemaVenda.Models;

namespace SistemaVenda.Controllers
{
    public class VendaController : Controller
    {
        protected ApplicationDbContext _dbContext;
        public VendaController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            //
            IEnumerable<Venda> lista = _dbContext.Venda.Include(x=>x.Cliente).ToList();
            _dbContext.Dispose();
            return View(lista);
        }
        private IEnumerable<SelectListItem> ListaProdutos()
        {
            List<SelectListItem> Lista = new List<SelectListItem>();
            Lista.Add(new SelectListItem()
            {
                Value = string.Empty,
                Text = string.Empty
            });
            foreach (var item in _dbContext.Produto.ToList())
            {
                Lista.Add(new SelectListItem()
                {
                    Value = item.Codigo.ToString(),
                    Text = item.Descricao.ToString()
                });
            }
            return Lista;
        }
        private IEnumerable<SelectListItem> ListaClientes()
        {
            List<SelectListItem> Lista = new List<SelectListItem>();
            Lista.Add(new SelectListItem()
            {
                Value = string.Empty,
                Text = string.Empty
            });
            foreach (var item in _dbContext.Cliente.ToList())
            {
                Lista.Add(new SelectListItem()
                {
                    Value = item.Codigo.ToString(),
                    Text = item.Nome.ToString()
                });
            }
            return Lista;
        }
        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            VendaViewModel viewModel = new VendaViewModel();
            viewModel.ListaCliente = ListaClientes();
            viewModel.ListaProdutos = ListaProdutos();
            if (id != null)
            {
                var entity = _dbContext.Venda.Where(x => x.Codigo == id).FirstOrDefault();
                viewModel.Codigo = entity.Codigo;
                viewModel.Data = entity.Data;
                viewModel.CodigoCliente = entity.CodigoCliente;
                viewModel.Total = entity.Total;
            }
        
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Cadastro(VendaViewModel entity)
        {

            if (ModelState.IsValid)
            {
                Venda objVenda = new Venda()
                {
                    Codigo = entity.Codigo,
                    Data = (DateTime)entity.Data,
                    CodigoCliente = (int)entity.CodigoCliente,
                    Total = (decimal)entity.Total,
                    Produto = JsonConvert.DeserializeObject < ICollection < VendaProdutos >> (entity.JsonProdutos)
                };
                if (entity.Codigo == null)
                {
                    _dbContext.Venda.Add(objVenda);
                }
                else
                {
                    _dbContext.Entry(objVenda).State = EntityState.Modified;
                }
                _dbContext.SaveChanges();
            }
            else
            {
                entity.ListaCliente = ListaClientes();
                entity.ListaProdutos = ListaProdutos();
                return View(entity);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var entity = new Venda()
            {
                Codigo = id
            };
            _dbContext.Attach(entity);
            _dbContext.Remove(entity);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet("LerValorProduto/{CodigoProduto}")]
        public decimal LerValorProduto(int CodigoProduto)
        {
            return _dbContext.Produto.Where(x => x.Codigo == CodigoProduto).Select(x => x.Valor).FirstOrDefault();
        }

    }
}
