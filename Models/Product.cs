using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIProducto.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; } =string.Empty;
        public String Description { get; set; } =string.Empty;
        public decimal Price { get; set; }
        public DateTime CreationDate { get; set; }
    }
}   