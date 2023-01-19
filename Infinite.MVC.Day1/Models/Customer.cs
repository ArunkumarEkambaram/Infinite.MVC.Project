using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infinite.MVC.Day1.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        public string MobileNo { get; set; }

        [Required, StringLength(100), EmailAddress]
        public string EmailId { get; set; }

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "dd/MM/yyyy")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [NotMapped]
        public int Age { get; set; }
    }
}