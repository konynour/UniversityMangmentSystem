using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Models;
using System.Linq;

namespace FinalProject.Controllers
{
    public class TraineeController : Controller
    {
        private readonly AppDbContext _db;
        public TraineeController(AppDbContext db) { _db = db; }
        public IActionResult Index() => View(_db.Trainees.Include(t => t.Department).ToList());
        public IActionResult Details(int id) => View(_db.Trainees.Include(t => t.Department).FirstOrDefault(t => t.Id == id));
        public IActionResult Add() { ViewBag.Depts = new SelectList(_db.Departments, "Id", "Name"); return View(new Trainee()); }
        [HttpPost] public IActionResult Add(Trainee t) { if(ModelState.IsValid) { _db.Trainees.Add(t); _db.SaveChanges(); TempData["Success"] = "Trainee added."; return RedirectToAction("Index"); } ViewBag.Depts = new SelectList(_db.Departments, "Id", "Name", t.Dept_Id); return View(t); }
        public IActionResult Edit(int id) { var t = _db.Trainees.Find(id); ViewBag.Depts = new SelectList(_db.Departments, "Id", "Name", t?.Dept_Id); return View(t); }
        [HttpPost] public IActionResult Edit(Trainee t) { if(ModelState.IsValid) { _db.Trainees.Update(t); _db.SaveChanges(); TempData["Success"] = "Trainee updated."; return RedirectToAction("Index"); } ViewBag.Depts = new SelectList(_db.Departments, "Id", "Name", t.Dept_Id); return View(t); }
        [HttpPost] public IActionResult Delete(int id) { var item = _db.Trainees.Find(id); if (item != null) { _db.Trainees.Remove(item); _db.SaveChanges(); TempData["Success"] = "Trainee deleted."; } return RedirectToAction("Index"); }
    }
}
