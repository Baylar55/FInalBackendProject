using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Areas.Admin.ViewModels.Product
{
    public class ProductCreateVM
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public List<SelectListItem>? Categories { get; set; }
        public int CategoryId { get; set; }
        public IFormFile Photo { get; set; }
    }
}
