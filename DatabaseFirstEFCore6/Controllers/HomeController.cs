using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DatabaseFirstEFCore6.Models;
using Microsoft.AspNetCore.Http;

namespace DatabaseFirstEFCore6.Controllers
{
    public class HomeController : Controller
    {
        private readonly CodeFirstDBContext _context;

        public HomeController(CodeFirstDBContext context)
        {
            _context = context;
        }

        // GET: Home
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.SetString("MyKey","MySessionValue");
            TempData["SessionId"] = HttpContext.Session.Id;
              return _context.Students != null ? 
                          View(await _context.Students.ToListAsync()) :
                          Problem("Entity set 'CodeFirstDBContext.Students'  is null.");
        }

        // GET: Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Home/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentName,StudentGender,Age,Standard")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Record created successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Home/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentName,StudentGender,Age,Standard")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Updated"] = "Record updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Home/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'CodeFirstDBContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            TempData["Delete"] = "Record deleted successfully";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult About() 
        {
            //if (HttpContext.Session.GetString("MyKey") != null)
            //{
            //    ViewBag.Data = HttpContext.Session.GetString("MyKey").ToString();
            //}
            return View();
        }

        public IActionResult ContactUs()
        {
            if (HttpContext.Session.GetString("MyKey") != null)
            {
                ViewBag.Data = HttpContext.Session.GetString("MyKey").ToString();
            }
            return View();
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("MyKey") != null)
            {
                HttpContext.Session.Remove("MyKey");
            }
            return View();
        }
        private bool StudentExists(int id)
        {
          return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
