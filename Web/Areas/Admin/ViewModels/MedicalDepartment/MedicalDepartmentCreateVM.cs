namespace Web.Areas.Admin.ViewModels.MedicalDepartment
{
    public class MedicalDepartmentCreateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
    }
}
