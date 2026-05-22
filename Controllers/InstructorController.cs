using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Models;
using System.Linq;

namespace FinalProject.Controllers
{
    public class InstructorController : Controller
    {
        private readonly AppDbContext _db;
        public InstructorController(AppDbContext db) { _db = db; }

        public IActionResult Index(string? search)
        {
            var instructors = _db.Instructors
                .Include(i => i.Department)
                .Include(i => i.Course)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
                instructors = instructors.Where(i => i.Name.Contains(search));

            ViewBag.Search = search;
            return View(instructors.ToList());
        }

        public IActionResult Details(int id)
        {
            var instructor = _db.Instructors
                .Include(i => i.Department)
                .Include(i => i.Course)
                .FirstOrDefault(i => i.Id == id);
            
            if (instructor == null) return NotFound();

            return View(instructor);
        }

        public IActionResult Add()
        {
            ViewBag.Depts = new SelectList(_db.Departments, "Id", "Name");
            ViewBag.Courses = new SelectList(_db.Courses, "Id", "Name");
            return View(new Instructor());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                _db.Instructors.Add(instructor);
                _db.SaveChanges();
                TempData["Success"] = "Instructor added successfully!";
                return RedirectToAction("Index");
            }
            ViewBag.Depts = new SelectList(_db.Departments, "Id", "Name", instructor.Dept_Id);
            ViewBag.Courses = new SelectList(_db.Courses, "Id", "Name", instructor.Crs_Id);
            return View(instructor);
        }

        public IActionResult Edit(int id)
        {
            var instructor = _db.Instructors.Find(id);
            if (instructor == null) return NotFound();

            ViewBag.Depts = new SelectList(_db.Departments, "Id", "Name", instructor.Dept_Id);
            ViewBag.Courses = new SelectList(_db.Courses, "Id", "Name", instructor.Crs_Id);
            return View(instructor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                _db.Instructors.Update(instructor);
                _db.SaveChanges();
                TempData["Success"] = "Instructor updated successfully!";
                return RedirectToAction("Index");
            }
            ViewBag.Depts = new SelectList(_db.Departments, "Id", "Name", instructor.Dept_Id);
            ViewBag.Courses = new SelectList(_db.Courses, "Id", "Name", instructor.Crs_Id);
            return View(instructor);
        }

        [HttpPost]
        public IActionResult Delete(int id) 
        { 
            var item = _db.Instructors.Find(id); 
            if (item != null) { 
                _db.Instructors.Remove(item); 
                _db.SaveChanges(); 
                TempData["Success"] = "Instructor deleted."; 
            } 
            return RedirectToAction("Index"); 
        }
    }
}
