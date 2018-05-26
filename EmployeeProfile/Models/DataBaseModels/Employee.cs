using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeProfile.Models
{
    public enum Gender
    {
        Male, Female
    }
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        public string EmailId { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public Gender Gender { get; set; }

        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateofBirth { get; set; }

        public List<EmployeeHobbies> EmployeeHobbies { get; set; }

        public Address Address { get; set; }

    }
}
