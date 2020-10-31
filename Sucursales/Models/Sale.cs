using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sucursales.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Sucursal")]
        public Branch Branch { get; set; }
        public int BranchId { get; set; }
        [
            DisplayFormat(DataFormatString = "{0:C}")
        ]
        public Double Total { get; set; }
        [Display(Name = "Fecha de compra")]
        public DateTime Created { get; set; }
        public ApplicationUser User { get; set; }
        [Display(Name = "Vendedor")]
        public string UserId { get; set; }
        public ICollection<SalesProduct> saleProducts { get; set; }

    }
}