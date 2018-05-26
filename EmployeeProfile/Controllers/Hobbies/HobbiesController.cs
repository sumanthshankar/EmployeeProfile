using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeProfile.Data;
using EmployeeProfile.Models;
using System.Diagnostics;
using System.Collections.Generic;

namespace EmployeeProfile.Controllers
{
    public class HobbiesController : Controller
    {
        private readonly EmployeeContext _context;

        public HobbiesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: Hobbies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Hobbies.ToListAsync());
        }

        // GET: Hobbies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hobby = await _context.Hobbies
                .SingleOrDefaultAsync(m => m.HobbyID == id);
            if (hobby == null)
            {
                return NotFound();
            }

            return View(hobby);
        }

        // GET: Hobbies/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HobbyID,HobbyName")] Hobby hobby)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hobby);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hobby);
        }

        // GET: Hobbies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hobby = await _context.Hobbies.SingleOrDefaultAsync(m => m.HobbyID == id);
            if (hobby == null)
            {
                return NotFound();
            }
            return View(hobby);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HobbyID,HobbyName")] Hobby hobby)
        {
            if (id != hobby.HobbyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hobby);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HobbyExists(hobby.HobbyID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(hobby);
        }

        // GET: Hobbies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hobby = await _context.Hobbies
                .SingleOrDefaultAsync(m => m.HobbyID == id);
            if (hobby == null)
            {
                return NotFound();
            }

            return View(hobby);
        }

        // POST: Hobbies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeHobby = await _context.EmployeeHobbies.SingleOrDefaultAsync(m => m.HobbyID == id);

            if(employeeHobby != null)
            {
                _context.Remove(employeeHobby);
            }

            var hobby = await _context.Hobbies.SingleOrDefaultAsync(m => m.HobbyID == id);
            _context.Hobbies.Remove(hobby);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HobbyExists(int id)
        {
            return _context.Hobbies.Any(e => e.HobbyID == id);
        }
    }
}
