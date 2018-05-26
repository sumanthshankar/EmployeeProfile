using System.Linq; 
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeProfile.Data;
using EmployeeProfile.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace EmployeeProfile.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeContext _context;

        public EmployeesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                                         .Include(a => a.Address)
                                         .AsNoTracking()
                                         .SingleOrDefaultAsync(e => e.EmployeeID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {

            var hobbies = _context.Hobbies.ToList();

            EmployeeView employeeView = new EmployeeView();
            employeeView.CheckHobbies = hobbies.Select(checBoxItem => new CheckBoxItem()
            {
                ID = checBoxItem.HobbyID,
                Title = checBoxItem.HobbyName,
                IsChecked = false
            }).ToList();
            return View(employeeView);
        }

        // POST: Employees/Create
        // [Bind("EmployeeID,FirstName,LastName,EmailId,Gender,PhoneNumber,DateofBirth")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeView employeeView)
        {
            Employee employee = new Employee();
            List<EmployeeHobbies> employeeHobbies = new List<EmployeeHobbies>();
            Address address = new Address();

            employee.FirstName = employeeView.FirstName;
            employee.LastName = employeeView.LastName;
            employee.EmailId = employeeView.EmailId;
            employee.PhoneNumber = employeeView.PhoneNumber;
            employee.DateofBirth = employeeView.DateofBirth;
            employee.Gender = employeeView.Gender;
            _context.Employees.Add(employee);
            int employeeID = employee.EmployeeID;

            address.EmployeeID = employeeID;
            address.ApartmentNumber = employeeView.Address.ApartmentNumber;
            address.State = employeeView.Address.State;
            address.City = employeeView.Address.City;
            address.Street = employeeView.Address.Street;
            address.Country = employeeView.Address.Country;
            address.ZipCode = employeeView.Address.ZipCode;
            _context.Addresses.Add(address);

            foreach (var hobby in employeeView.CheckHobbies)
            {
                if(hobby.IsChecked)
                {
                    employeeHobbies.Add(new EmployeeHobbies { EmployeeID = employeeID, HobbyID = hobby.ID });
                }
            }

            foreach(var hobby in employeeHobbies)
            {
                _context.EmployeeHobbies.Add(hobby);
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee employee = new Employee();

            employee = await _context.Employees
                                     .Include(a => a.Address)
                                     .Include(eh => eh.EmployeeHobbies)
                                     .ThenInclude(h => h.Hobby)
                                     .AsNoTracking()
                                     .SingleOrDefaultAsync(e => e.EmployeeID == id);

            //Debug.WriteLine("Guru");
            if (employee == null)
            {
                return NotFound();
            }

            EmployeeView employeeView = new EmployeeView();
            List<EmployeeHobbies> EmployeeHobbies = new List<EmployeeHobbies>();

            EmployeeHobbies = employee.EmployeeHobbies;
            employeeView.EmployeeID = employee.EmployeeID;
            employeeView.FirstName = employee.FirstName;
            employeeView.LastName = employee.LastName;
            employeeView.EmailId = employee.EmailId;
            employeeView.PhoneNumber = employee.PhoneNumber;
            employeeView.DateofBirth = employee.DateofBirth;
            employeeView.Gender = employee.Gender;

            Address address = new Address();

            address.AddressID = employee.Address.AddressID;
            address.ApartmentNumber = employee.Address.ApartmentNumber;
            address.Street = employee.Address.Street;
            address.City = employee.Address.City;
            address.State = employee.Address.State;
            address.Country = employee.Address.Country;
            address.ZipCode = employee.Address.ZipCode;

            employeeView.Address = address;
            IEnumerable<int> selectedHobbies = EmployeeHobbies.Where(e => e.EmployeeID == id)
                                                              .Select(h => h.HobbyID);

            var allHobbies = _context.Hobbies.Select(c => new CheckBoxItem()
            {
                ID = c.HobbyID,
                Title = c.HobbyName,
                IsChecked = selectedHobbies.Contains(c.HobbyID)
            }).ToList();

            employeeView.CheckHobbies = allHobbies;

            return View(employeeView);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeView employeeView)
        {
            Employee employee = new Employee();
            List<EmployeeHobbies> employeeHobbies = new List<EmployeeHobbies>();

            employee.EmployeeID = employeeView.EmployeeID;
            employee.FirstName = employeeView.FirstName;
            employee.LastName = employeeView.LastName;
            employee.EmailId = employeeView.EmailId;
            employee.PhoneNumber = employeeView.PhoneNumber;
            employee.DateofBirth = employeeView.DateofBirth;
            employee.Gender = employeeView.Gender;
            _context.Update(employee);



            Address address = new Address();

            address.AddressID = employeeView.Address.AddressID;
            address.EmployeeID = employeeView.EmployeeID;
            address.ApartmentNumber = employeeView.Address.ApartmentNumber;
            address.State = employeeView.Address.State;
            address.City = employeeView.Address.City;
            address.Street = employeeView.Address.Street;
            address.Country = employeeView.Address.Country;
            address.ZipCode = employeeView.Address.ZipCode;

            _context.Update(address);

            List<EmployeeHobbies> employeeHobbiesList = new List<EmployeeHobbies>();
            employeeHobbiesList = _context.EmployeeHobbies.ToList();

            foreach(var list in employeeHobbiesList)
            {
                Debug.WriteLine(list.HobbyID);
                EmployeeHobbies employeeHobby = await _context.EmployeeHobbies.SingleOrDefaultAsync(m => m.HobbyID == list.HobbyID);
                _context.Remove(employeeHobby);
            }

            foreach(var hobby in employeeView.CheckHobbies)
            {
                if (hobby.IsChecked)
                {
                    employeeHobbies.Add(new EmployeeHobbies { EmployeeID = employeeView.EmployeeID, HobbyID = hobby.ID });
                }
            }

            foreach (var hobby in employeeHobbies)
            {
                _context.EmployeeHobbies.Add(hobby);
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.SingleOrDefaultAsync(m => m.EmployeeID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.SingleOrDefaultAsync(m => m.EmployeeID == id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeID == id);
        }
    }
}
