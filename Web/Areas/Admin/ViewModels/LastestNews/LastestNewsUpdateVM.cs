namespace Web.Areas.Admin.ViewModels.LastestNews
{
    public class LastestNewsUpdateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string? PhotoName { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
