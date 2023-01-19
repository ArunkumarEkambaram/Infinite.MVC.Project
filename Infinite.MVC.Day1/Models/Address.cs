using System.ComponentModel.DataAnnotations;

namespace Infinite.MVC.Day1.Models
{
    public class Address
    {
        public int AddressId { get; set; }

        [Required]
        [StringLength(20)]
        public string HouseNo { get; set; }

        [Required]
        [StringLength(50)]
        public string StreetName { get; set; }

        [StringLength(10)]
        public string FlatNo { get; set; }

        [Required]
        [StringLength(100)]
        public string AddressLine1 { get; set; }

        [StringLength(100)]
        public string AddressLine2 { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(6)]
        public string Pincode { get; set; }

        //Navigation Property
        public Customer Customer { get; set; }

        //Foreign Key
        public int CustomerId { get; set; }
    }
}