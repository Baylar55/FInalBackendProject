using Core.Entities;

namespace Web.Areas.Admin.ViewModels.AboutUs
{
    public class AboutUsDetailsVM
    {
        public int Id { get; set; }
        public string SubTitle { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string SignaturePhotoName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public ICollection<AboutPhoto> AboutPhotos { get; set; }
    }
}
