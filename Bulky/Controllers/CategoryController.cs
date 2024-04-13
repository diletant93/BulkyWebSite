using Bulky.DataAccess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bulky.DataAccess.Repository.IRepository;
namespace Bulky.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _db;
        public CategoryController(ICategoryRepository db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
          IEnumerable<Category> objCategoryList = await _db.GetAllAsync();
            return View(objCategoryList.ToList());
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
                await _db.AddAsync(category);
                TempData["success"] = "Category created successfully";
                await _db.SaveAsync();
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
            Category? editingCategory = await _db.GetFirstOrDefaultAsync(c => c.Id == id);
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
                _db.Update(category);
                TempData["success"] = "Category updated successfully";
                await _db.SaveAsync();
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
            Category? editingCategory = await _db.GetFirstOrDefaultAsync(c => c.Id == id);
            if (editingCategory is null)
            {
                return NotFound();
            }
            return View(editingCategory);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePOST(int? id)
        {
            Category? obj = await _db.GetFirstOrDefaultAsync(c => c.Id == id);
            if (obj is null)
            {
                return NotFound();
            }
               _db.Remove(obj);
            TempData["success"] = "Category deleted successfully";
            await _db.SaveAsync();
                return RedirectToAction("Index", "Category");

        }
    } 
}
