using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using SistemaVenda.DAL;
using SistemaVenda.Models;

namespace SistemaVenda.Controllers
{
    public class RelatorioController : Controller
    {
        protected ApplicationDbContext _dbContext;
        public RelatorioController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Grafico()
        {
            try
            {
                var lista = _dbContext.VendaProdutos
              .GroupBy(key => key.CodigoProduto)
              .Select(
                  obj => new GraficoViewModel
                  {
                      CodigoProduto = obj.First().CodigoProduto,
                      Descricao = obj.First().Produto.Descricao,
                      TotalVendido = obj.Sum(x => x.Quantidade)
                  }
              ).ToList();
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.);
            }
           

            return View();
        }
    }
}
