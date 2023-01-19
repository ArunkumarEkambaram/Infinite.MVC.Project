using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Infinite.MVC.Day1.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName ="varchar")]
        [StringLength(50)]
        public string CategoryName { get; set; }
    }
}