using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sucursales.Models
{
    public class Branch
    {
        [Key]
        public int Id { set; get; }
        [
            DisplayName("Nombre"),
            Required(ErrorMessage = "Es nesario agregar el nombre")
        ]
        public String Name { get; set; }
       [
            DisplayName("Direccion")
        ]
        public Address Address { get; set; }

        public int AddressId { get; set; }
        public bool Active { get; set; }
    }
}