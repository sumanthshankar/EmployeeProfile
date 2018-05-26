using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeProfile.Models
{
    public class Hobby
    {
        [Key]
        public int HobbyID { get; set; }

        public string HobbyName { get; set; }

        public List<EmployeeHobbies> EmployeeHobbies { get; set; }
    }
}
