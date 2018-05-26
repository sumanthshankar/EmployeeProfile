using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeProfile.Models
{
    public class Address
    {
        [Key]
        public int AddressID { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }

        public string Street { get; set; }

        public int ApartmentNumber { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }

        public Employee Employee { get; set; }

    }
}
