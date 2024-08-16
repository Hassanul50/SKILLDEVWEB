using Microsoft.AspNetCore.Mvc;
using SKILLDEVWEB.DataAccess.Repository.IRepository;
using SKILLDEVWEB.Model.Models;

namespace SKILLDEVWEB.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            List<Category> categorylist = _categoryRepository.GetAll().OrderBy(s => s.DisplayOrder).ToList();
            return View(categorylist);
        }
        public IActionResult CreateCategory()
        {
            var Displayorder = (_categoryRepository.GetAll().Max(selector => selector.DisplayOrder)) + 1;
            Category category = new Category();
            category.DisplayOrder = Displayorder;
            return View(category);
        }
        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            if (category.CategoryName == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CategoryName", "Display Name and Display Order canot be same");
            }
            if (category.CategoryName == "test")
            {
                ModelState.AddModelError("", "test canot be use");
            }
            if (ModelState.IsValid)
            {
                _categoryRepository.Add(category);
                _categoryRepository.Save();
                TempData["Mess"] = "Category created Succesfully";
                return RedirectToAction("index", "Category");
            }
            return View();

        }

        public IActionResult EditCategory(int id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            //Category? category = _categoryRepository.Categories.Find(id);
            Category? category = _categoryRepository.GetFirstOrDefault(u => u.CategoryId == id);
            /*Category? category2 = _categoryRepository.Categories.Where(u => u.CategoryId == id).FirstOrDefault()*/
            ;
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult EditCategory(Category category)
        {

            if (ModelState.IsValid)
            {
                _categoryRepository.Update(category);
                _categoryRepository.Save();
                TempData["Mess"] = "Category Edited Succesfully";
                return RedirectToAction("index", "Category");
            }
            return View();

        }
        public IActionResult DeleteCategory(int id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            //Category? category = _categoryRepository.Categories.Find(id);
            Category? category1 = _categoryRepository.GetFirstOrDefault(u => u.CategoryId == id);
            //Category? category2 = _categoryRepository.Categories.Where(u => u.CategoryId == id).FirstOrDefault();
            if (category1 == null)
            {
                return NotFound();
            }
            return View(category1);
        }
        [HttpPost]
        public IActionResult DeleteCategory(int? id)
        {
            Category? category = _categoryRepository.GetFirstOrDefault(u => u.CategoryId == id);
            if (ModelState.IsValid)
            {
                _categoryRepository.Remove(category);
                _categoryRepository.Save();
                TempData["Delete"] = "Category Deleted Succesfully";
                return RedirectToAction("index", "Category");
            }
            return View();

        }
    }
}
