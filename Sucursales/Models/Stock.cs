using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sucursales.Models
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Sucursal")]
        public Branch Branch { get; set; }
        public int BranchId { get; set; }
        [Display(Name = "Producto")]
        public Product Product { get; set; }
        public int ProductId { get; set; }
        [Display(Name = "Cantidad en sucursal")]
        public int Amount { get; set; }

        public ICollection<StockHistory> StockHistories{ get; set; }
    }
}