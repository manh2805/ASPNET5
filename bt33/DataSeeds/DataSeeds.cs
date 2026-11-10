using bt33.Models;

namespace bt33.Data
{
    // Lớp chứa dữ liệu mẫu dùng chung (không phải Controller)
    public static class DataSeeds
    {
        public static List<Category> categories = new List<Category>
        {
            new Category { Id = 1, Name = "Laptop" },
            new Category { Id = 2, Name = "Phone" },
            new Category { Id = 3, Name = "Tablet" }
        };

        public static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Dell XPS 13", Price = 30000000, Description = "Laptop mỏng nhẹ", CategoryId = 1 },
            new Product { Id = 2, Name = "iPhone 15", Price = 20000000, Description = "Điện thoại Apple", CategoryId = 2 },
            new Product { Id = 3, Name = "iPad Air", Price = 15000000, Description = "Máy tính bảng Apple", CategoryId = 3 },
            new Product { Id = 4, Name = "MacBook Air M3", Price = 27000000, Description = "Laptop Apple pin lâu", CategoryId = 1 },
            new Product { Id = 5, Name = "Asus ROG Strix", Price = 35000000, Description = "Laptop gaming", CategoryId = 1 },
            new Product { Id = 6, Name = "Lenovo ThinkPad X1", Price = 32000000, Description = "Laptop doanh nhân", CategoryId = 1 },
            new Product { Id = 7, Name = "Samsung Galaxy S24", Price = 22000000, Description = "Điện thoại Samsung", CategoryId = 2 },
            new Product { Id = 8, Name = "Xiaomi 14", Price = 15000000, Description = "Điện thoại cấu hình cao", CategoryId = 2 },
            new Product { Id = 9, Name = "Oppo Reno 11", Price = 10000000, Description = "Điện thoại chụp ảnh đẹp", CategoryId = 2 },
            new Product { Id = 10, Name = "Galaxy Tab S9", Price = 18000000, Description = "Máy tính bảng Samsung", CategoryId = 3 },
            new Product { Id = 11, Name = "Xiaomi Pad 6", Price = 8000000, Description = "Máy tính bảng giá rẻ", CategoryId = 3 },
            new Product { Id = 12, Name = "iPad Pro M4", Price = 30000000, Description = "Máy tính bảng cao cấp", CategoryId = 3 }
        };
    }
}
