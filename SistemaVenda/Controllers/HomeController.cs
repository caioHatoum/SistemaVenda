using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SistemaVenda.DAL;
using SistemaVenda.Entities;
using SistemaVenda.Models;

namespace SistemaVenda.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //contexto do banco de dados na aplicação.
        protected ApplicationDbContext Repositorio;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext repositorio)
        {
            _logger = logger;
            //construtor que seta o valor do repositorio a um valor padrao do contexto do banco de dados da aplicação.
            Repositorio = repositorio;
        }
        
        
        public IActionResult Index()
         
        {
         
           
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
