using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sucursales.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [
            DisplayName("Nombre"),
            Required(ErrorMessage = "Es nesario agregar el nombre")
        ]
        public String Name { get; set; }
        [
            DisplayName("Precio"),
            DisplayFormat(DataFormatString = "{0:C}"),
            Required(ErrorMessage = "Es nesario definir el precio")
        ]
        public Double Price { get; set; }
        [
            DisplayName("Codigo de barras"),
            Required(ErrorMessage = "Es nesario agregar el codigo de barras")
        ]
        public String CodeBar { get; set; }
        public bool Active { get; set; }
    }
}