using Microsoft.AspNetCore.Mvc;
using FinalProject.Data;
using FinalProject.Models;
using System.Linq;

namespace FinalProject.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _db;
        public DepartmentController(AppDbContext db) { _db = db; }
        public IActionResult Index() => View(_db.Departments.ToList());
        public IActionResult Details(int id) => View(_db.Departments.FirstOrDefault(d => d.Id == id));
        public IActionResult Add() => View(new Department());
        [HttpPost] public IActionResult Add(Department dept) { if(ModelState.IsValid) { _db.Departments.Add(dept); _db.SaveChanges(); TempData["Success"] = "Department added."; return RedirectToAction("Index"); } return View(dept); }
        public IActionResult Edit(int id) => View(_db.Departments.Find(id));
        [HttpPost] public IActionResult Edit(Department dept) { if(ModelState.IsValid) { _db.Departments.Update(dept); _db.SaveChanges(); TempData["Success"] = "Department updated."; return RedirectToAction("Index"); } return View(dept); }
        [HttpPost] public IActionResult Delete(int id) { var item = _db.Departments.Find(id); if (item != null) { _db.Departments.Remove(item); _db.SaveChanges(); TempData["Success"] = "Department deleted."; } return RedirectToAction("Index"); }
    }
}
