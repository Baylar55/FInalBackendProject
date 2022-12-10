using Core.Entities;

namespace Web.Areas.Admin.ViewModels.AboutUs
{
    public class AboutUsCreateVM
    {
        public string SubTitle { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public IFormFile SignaturePhoto { get; set; }
        public ICollection<IFormFile> Photos { get; set; }
    }
}
