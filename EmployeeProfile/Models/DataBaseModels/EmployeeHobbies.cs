
namespace EmployeeProfile.Models
{
    public class EmployeeHobbies
    {
        public int ID { get; set; }

        public int HobbyID { get; set; }

        public Hobby Hobby { get; set; }

        public int EmployeeID { get; set; }

        public Employee Employee { get; set; }
    }
}
