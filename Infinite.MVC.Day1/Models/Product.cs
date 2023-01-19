using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infinite.MVC.Day1.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name cannot be blank")]
        [Display(Name = "Product Name")]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Product Price")]
        [Range(0.0, 100000.0, ErrorMessage = "Price should be between 0 and 100000")]
        public double Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity should be greater than zero")]
        public int Quantity { get; set; }

        //Navigation Properties
        public Category Category { get; set; }//This is not a column

        public string Description { get; set; }

        //Foreign Key       
        public int CategoryId { get; set; }

        //[NotMapped]
        //public List<Category> Categories { get; set; }
    }
}