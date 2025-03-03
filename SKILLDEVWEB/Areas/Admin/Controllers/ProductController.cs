using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SKILLDEVWEB.DataAccess.Repository.IRepository;
using SKILLDEVWEB.Model.Models;
using SKILLDEVWEB.Model.Models.ViewModels;

namespace SKILLDEVWEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> Productlist = _unitOfWork.Product.GetAll().OrderBy(s => s.ProductId).ToList();

            return View(Productlist);
        }
        public IActionResult CreateUpdateProduct(int? id)
        {
            //var Displayorder = _unitOfWork.Product.GetAll().Max(selector => selector.ProductId) + 1;
            Product Product = new Product();
            //Product.ProductId = Displayorder;
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.CategoryName,
                Value = u.CategoryId.ToString()
            });
            ProductVM productVM = new ProductVM()
            {
                Product = Product,
                CategoryList = CategoryList
            };

            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = _unitOfWork.Product.GetFirstOrDefault(s => s.ProductId == id);
                return View(productVM);
            }
            //ViewBag.CategoryList = CategoryList;

        }
        [HttpPost]
        public IActionResult CreateUpdateProduct(ProductVM productVM, IFormFile? file)
        {
            //if (Product.Title == Product.t.ToString())
            //{
            //    ModelState.AddModelError("Title", "Display title and Display Order canot be same");
            //}
            //if (Product.ProductName == "test")
            //{
            //    ModelState.AddModelError("", "test canot be use");
            //}
            if (ModelState.IsValid)
            {
                string wwwRoothPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string FileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRoothPath, @"images\product");
                    using (var fileStream = new FileStream(Path.Combine(productPath, FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImgUrl = @"\images\product\" + FileName;
                }
                //var Displayorder = _unitOfWork.Product.GetAll().Max(selector => selector.ProductId) + 1;
                //productVM.Product.ProductId = Displayorder;
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["Mess"] = "Product created Succesfully";
                return RedirectToAction("index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.CategoryId.ToString()
                });
                return View(productVM);
            }


        }

        //public IActionResult EditProduct(int id)
        //{
        //    if (id == 0 || id == null)
        //    {
        //        return NotFound();
        //    }
        //    //Product? Product = _unitOfWork.Categories.Find(id);
        //    Product? Product = _unitOfWork.Product.GetFirstOrDefault(u => u.ProductId == id);
        //    /*Product? Product2 = _unitOfWork.Categories.Where(u => u.ProductId == id).FirstOrDefault()*/
        //    ;
        //    if (Product == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(Product);
        //}
        [HttpPost]
        public IActionResult EditProduct(Product Product)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(Product);
                _unitOfWork.Save();
                TempData["Mess"] = "Product Edited Succesfully";
                return RedirectToAction("index", "Product");
            }
            return View();

        }
        public IActionResult DeleteProduct(int id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            //Product? Product = _unitOfWork.Categories.Find(id);
            Product? Product1 = _unitOfWork.Product.GetFirstOrDefault(u => u.ProductId == id);
            //Product? Product2 = _unitOfWork.Categories.Where(u => u.ProductId == id).FirstOrDefault();
            if (Product1 == null)
            {
                return NotFound();
            }
            return View(Product1);
        }
        [HttpPost]
        public IActionResult DeleteProduct(int? id)
        {
            Product? Product = _unitOfWork.Product.GetFirstOrDefault(u => u.ProductId == id);
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Remove(Product);
                _unitOfWork.Save();
                TempData["Delete"] = "Product Deleted Succesfully";
                return RedirectToAction("index", "Product");
            }
            return View();

        }
    }
}
