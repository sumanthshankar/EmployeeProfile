using EmployeeProfile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeProfile.Data
{
    public class DbInitializer
    {
        public static void Initialize(EmployeeContext context)
        {
            context.Database.EnsureCreated();
            if (context.Employees.Any())
            {
                return;   // DB has been seeded
            }

            var HobbyOne = new Hobby { HobbyName = "Biking" };
            var HobbyTwo = new Hobby { HobbyName = "Hiking" };
            var HobbyThree = new Hobby { HobbyName = "Racing" };
            var HobbyFour = new Hobby { HobbyName = "Reading" };
            var HobbyFive = new Hobby { HobbyName = "Bowling" };

            context.Hobbies.Add(HobbyOne);
            context.Hobbies.Add(HobbyTwo);
            context.Hobbies.Add(HobbyThree);
            context.Hobbies.Add(HobbyFour);
            context.Hobbies.Add(HobbyFive);
            context.SaveChanges();

            ////First record
            //var addressOne = new Address { Street = "1700 N 1st Street", ApartmentNumber = 338, City = "San Jose", State = "CA", Country = "USA", ZipCode = "95112" };
            //List<Hobby> hobbiesOne = new List<Hobby>();
            //    hobbiesOne.Add(new Hobby { HobbyName = "Biking", IsChecked = true });
            //    hobbiesOne.Add(new Hobby { HobbyName = "Reading", IsChecked = true });
            //var employeeOne = new Employee {FirstName = "Carson", LastName = "Alexander", EmailId = "carson@gmail.com", Gender = Gender.Male, PhoneNumber = "333-455-7896", DateofBirth = DateTime.Parse("2005-09-01"), Address = addressOne};
            //context.Employees.Add(employeeOne);
            //context.SaveChanges();


            ////Second record
            //var addressTwo = new Address {Street = "1777 S 10th Street", ApartmentNumber = 338, City = "San Jose", State = "CA", Country = "USA", ZipCode = "95112" };
            //List<Hobby> hobbiesTwo = new List<Hobby>();
            //    hobbiesTwo.Add(new Hobby { HobbyName = "Biking", IsChecked = true });
            //    hobbiesTwo.Add(new Hobby { HobbyName = "Reading", IsChecked = true });
            //var employeeTwo = new Employee {FirstName = "Meredith", LastName = "Alonso", EmailId = "alonso@gmail.com", Gender = Gender.Female, PhoneNumber = "333-455-7896", DateofBirth = DateTime.Parse("2005-09-01"), Address = addressTwo};
            //context.Employees.Add(employeeTwo);
            //context.SaveChanges();
            //context.SaveChanges();
        }
    }
}
