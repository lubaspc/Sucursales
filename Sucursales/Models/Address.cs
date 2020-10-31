using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sucursales.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [
            DisplayName("Calle"),
            Required(ErrorMessage = "Es nesario agregar la calle")
        ]
        public String Street { get; set; }
        [
            DisplayName("Numero exterior"),
            Required(ErrorMessage = "Es nesario agregar el numero exterior")
        ]
        public String ExtNum { get; set; }
        [
            DisplayName("Codigo postal"),
            Required(ErrorMessage = "Es nesario agregar el codigo postal")
        ]
        public String ZipCode { get; set; }
        [
            DisplayName("Colonia"),
            Required(ErrorMessage = "Es nesario agregar la colonia o municipio")
        ]
        public String Colony { get; set; }
        [
            DisplayName("Ciudad"),
            Required(ErrorMessage = "Es nesario agregar la ciudad")
        ]
        public String City { get; set; }
        [
            DisplayName("Estado"),
            Required(ErrorMessage = "Es nesario agregar el estado")
        ]
        public String State { get; set; }
        [NotMapped]
        public String FullAddress
        {
            get
            {
                return this.Street + " ,No. " + this.ExtNum + " ," + this.Colony + " ," + this.City + " ," + this.State;
            }
        }
       
    }
}