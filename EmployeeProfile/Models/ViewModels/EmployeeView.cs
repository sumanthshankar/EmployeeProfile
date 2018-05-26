using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeProfile.Models
{
    public class EmployeeView
    {
        public int EmployeeID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailId { get; set; }

        public Gender Gender { get; set; }

        public string PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateofBirth { get; set; }

        public List<CheckBoxItem> CheckHobbies { get; set; }

        public Address Address { get; set; }
    }
}
