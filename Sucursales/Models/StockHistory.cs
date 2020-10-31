using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sucursales.Models
{
    public class StockHistory
    {
        [Key]
        public int Id { get; set; }

        public Stock Stock { get; set; }
        public int StockId { get; set; }
        public bool Entity { get; set; }

        public int Amounth { get; set; }

        public DateTime Created { get; set; }

    }
}