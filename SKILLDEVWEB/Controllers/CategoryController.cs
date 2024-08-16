using Microsoft.AspNetCore.Mvc;
using SKILLDEVWEB.DataAccess.Repository.IRepository;
using SKILLDEVWEB.Model.Models;

namespace SKILLDEVWEB.Controllers
{
    public class CategoryController : Controller
    {
        //private readonly ICategoryRepository _unitOfWork;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> categorylist = _unitOfWork.Category.GetAll().OrderBy(s => s.DisplayOrder).ToList();
            return View(categorylist);
        }
        public IActionResult CreateCategory()
        {
            var Displayorder = (_unitOfWork.Category.GetAll().Max(selector => selector.DisplayOrder)) + 1;
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
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
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
            //Category? category = _unitOfWork.Categories.Find(id);
            Category? category = _unitOfWork.Category.GetFirstOrDefault(u => u.CategoryId == id);
            /*Category? category2 = _unitOfWork.Categories.Where(u => u.CategoryId == id).FirstOrDefault()*/
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
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
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
            //Category? category = _unitOfWork.Categories.Find(id);
            Category? category1 = _unitOfWork.Category.GetFirstOrDefault(u => u.CategoryId == id);
            //Category? category2 = _unitOfWork.Categories.Where(u => u.CategoryId == id).FirstOrDefault();
            if (category1 == null)
            {
                return NotFound();
            }
            return View(category1);
        }
        [HttpPost]
        public IActionResult DeleteCategory(int? id)
        {
            Category? category = _unitOfWork.Category.GetFirstOrDefault(u => u.CategoryId == id);
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Remove(category);
                _unitOfWork.Save();
                TempData["Delete"] = "Category Deleted Succesfully";
                return RedirectToAction("index", "Category");
            }
            return View();

        }
    }
}
