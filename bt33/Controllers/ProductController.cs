using bt33.Data;
using bt33.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace bt33.Controllers
{
    public class ProductController : Controller
    {
        private static List<Product> products => DataSeeds.products;

        private void LoadCategories(int? selectedId = null)
        {
            ViewBag.Categories = new SelectList(DataSeeds.categories, "Id", "Name", selectedId);
        }

        // Gán navigation property Category theo CategoryId (dữ liệu đang lưu trong List nên phải tự gán)
        private static void LoadCategory(Product product)
        {
            product.Category = DataSeeds.categories.FirstOrDefault(c => c.Id == product.CategoryId);
        }

        // GET: Product?search=...&sortOrder=name&desc=false&page=1
        [HttpGet]
        public ActionResult Index(string? search, string sortOrder = "id", bool desc = false, int page = 1)
        {
            const int pageSize = 5;
            products.ForEach(LoadCategory);
            IEnumerable<Product> query = products;

            // 1. Tìm kiếm: theo tên, mô tả hoặc tên danh mục (không phân biệt hoa thường)
            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();
                query = query.Where(p =>
                    p.Name.ToLower().Contains(s) ||
                    (p.Description ?? "").ToLower().Contains(s) ||
                    (p.Category?.Name ?? "").ToLower().Contains(s));
            }

            // 2. Sắp xếp theo Id hoặc Name (làm TRƯỚC phân trang để sắp xếp trên toàn bộ dữ liệu)
            query = sortOrder switch
            {
                "name" => desc ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
                _ => desc ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id),
            };

            // 3. Phân trang
            int totalItems = query.Count();
            int totalPages = Math.Max(1, (int)Math.Ceiling(totalItems / (double)pageSize));
            page = Math.Clamp(page, 1, totalPages);
            var items = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.Search = search;
            ViewBag.SortOrder = sortOrder;
            ViewBag.Desc = desc;
            ViewBag.Page = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalItems = totalItems;
            return View(items);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            LoadCategory(product);
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            LoadCategories();
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (!DataSeeds.categories.Any(c => c.Id == product.CategoryId))
                ModelState.AddModelError(nameof(Product.CategoryId), "Danh mục không tồn tại");

            if (!ModelState.IsValid)
            {
                LoadCategories(product.CategoryId);
                return View(product);
            }

            product.Id = products.Count == 0 ? 1 : products.Max(p => p.Id) + 1;
            products.Add(product);
            return RedirectToAction(nameof(Index));
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            LoadCategories(product.CategoryId);
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            if (id != product.Id) return BadRequest();

            if (!DataSeeds.categories.Any(c => c.Id == product.CategoryId))
                ModelState.AddModelError(nameof(Product.CategoryId), "Danh mục không tồn tại");

            if (!ModelState.IsValid)
            {
                LoadCategories(product.CategoryId);
                return View(product);
            }

            var existing = products.FirstOrDefault(p => p.Id == id);
            if (existing == null) return NotFound();

            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Description = product.Description;
            existing.CategoryId = product.CategoryId;
            return RedirectToAction(nameof(Index));
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            LoadCategory(product);
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null) products.Remove(product);
            return RedirectToAction(nameof(Index));
        }
        private static ProductViewModel ToViewModel(Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryName = DataSeeds.categories.FirstOrDefault(c => c.Id == product.CategoryId)?.Name ?? string.Empty,
            };
        }
    }
}
