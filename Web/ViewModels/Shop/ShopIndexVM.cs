using Core.Entities;

namespace Web.ViewModels.Shop
{
    public class ShopIndexVM
    {
        public List<ProductCategory> ProductCategories { get; set; }
        public List<Product> Products { get; set; }
        public string? Name { get; set; }
        public int Page { get; set; } = 1;
        public int Take { get; set; } = 3;
        public int PageCount { get; set; }
    }
}
