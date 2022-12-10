namespace Web.Areas.Admin.ViewModels.HomeVideo
{
    public class HomeVideoUpdateVM
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string? PhotoName { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
