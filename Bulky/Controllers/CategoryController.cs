using Bulky.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bulky.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            List<Category> objCategoryList = await _db.Categories.ToListAsync();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Create(Category category)
        {
            if(ModelState.IsValid)
            {
                await _db.Categories.AddAsync(category);
                TempData["success"] = "Category created successfully";
                await _db.SaveChangesAsync();
                return RedirectToAction("Index","Category");
            }
            else
            {
                return View();
            }
            
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if(id is null || id == 0)
            {
                return NotFound();
            }
            Category? editingCategory = await _db.Categories.FindAsync(id);
            if(editingCategory is null)
            {
                return NotFound();
            }
            return View(editingCategory);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                TempData["success"] = "Category updated successfully";
                await _db.SaveChangesAsync();
                return RedirectToAction("Index", "Category");
            }
            else
            {
                return View();
            }

        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            Category? editingCategory = await _db.Categories.FindAsync(id);
            if (editingCategory is null)
            {
                return NotFound();
            }
            return View(editingCategory);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePOST(int? id)
        {
            Category? obj = await _db.Categories.FindAsync(id);
            if (obj is null)
            {
                return NotFound();
            }
               _db.Categories.Remove(obj);
            TempData["success"] = "Category deleted successfully";
            await _db.SaveChangesAsync();
                return RedirectToAction("Index", "Category");

        }
    } 
}
