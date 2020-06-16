using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Entities
{
    public class Produto
    {
        [Key]
        public int? Codigo { get; set; }
        public string Descricao { get; set; }
        public double Quantidade { get; set; }
        public decimal Valor { get; set; }
        [ForeignKey("Categoria")]
        public int CodigoCategoria { get; set; }

        //essa propriedade deve se o fato de um produto sempre possuir uma categoria, 
        //ou seja, todo produto possui uma categoria, e toda catecoria contem uma lista de produtos,
        //uma relação de 1 para muitos
        public Categoria Categoria { get; set; }
        public ICollection<VendaProdutos> Vendas { get; set; }
    }
}
