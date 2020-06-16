using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVenda.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Models
{
    public class VendaViewModel
    {

        public int? Codigo { get; set; }
        [Required(ErrorMessage = "Informe a data da venda!")]
        public DateTime? Data { get; set; }
        [Required(ErrorMessage = "Informe o cliente!")]
        public int? CodigoCliente { get; set; }
        public IEnumerable<SelectListItem> ListaCliente { get; set; }
        public IEnumerable<SelectListItem> ListaProdutos { get; set; }
        public string JsonProdutos { get; set; }
        public decimal Total { get; set; }

    }
}
