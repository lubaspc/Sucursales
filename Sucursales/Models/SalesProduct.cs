using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sucursales.Models
{
    public class SalesProduct
    {
        [Key]
        public int Id { get; set; }
        public Product Product { get; set; }

        public int ProductId { get; set; }

        public Sale Sale { get; set; }
        public int SaleId { get; set; }

        public int Quantity { get; set; }
        public double SubTotal { get; set; }
    }
}