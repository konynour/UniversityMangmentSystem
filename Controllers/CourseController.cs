using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Models;
using System.Linq;

namespace FinalProject.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _db;
        public CourseController(AppDbContext db) { _db = db; }
        public IActionResult Index() => View(_db.Courses.Include(c => c.Department).ToList());
        public IActionResult Details(int id) => View(_db.Courses.Include(c => c.Department).FirstOrDefault(c => c.Id == id));
        public IActionResult Add() { ViewBag.Depts = new SelectList(_db.Departments, "Id", "Name"); return View(new Course()); }
        [HttpPost] public IActionResult Add(Course crs) { if(ModelState.IsValid) { _db.Courses.Add(crs); _db.SaveChanges(); TempData["Success"] = "Course added."; return RedirectToAction("Index"); } ViewBag.Depts = new SelectList(_db.Departments, "Id", "Name", crs.Dept_Id); return View(crs); }
        public IActionResult Edit(int id) { var c = _db.Courses.Find(id); ViewBag.Depts = new SelectList(_db.Departments, "Id", "Name", c?.Dept_Id); return View(c); }
        [HttpPost] public IActionResult Edit(Course crs) { if(ModelState.IsValid) { _db.Courses.Update(crs); _db.SaveChanges(); TempData["Success"] = "Course updated."; return RedirectToAction("Index"); } ViewBag.Depts = new SelectList(_db.Departments, "Id", "Name", crs.Dept_Id); return View(crs); }
        [HttpPost] public IActionResult Delete(int id) { var item = _db.Courses.Find(id); if (item != null) { _db.Courses.Remove(item); _db.SaveChanges(); TempData["Success"] = "Course deleted."; } return RedirectToAction("Index"); }
    }
}
