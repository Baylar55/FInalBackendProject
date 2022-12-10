using Core.Constants;

namespace Web.Areas.Admin.ViewModels.Doctor
{
    public class DoctorUpdateVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SubTitle { get; set; }
        public string Skill { get; set; }
        public string? PhotoName { get; set; }
        public DoctorSpeciality Specialty { get; set; }
        public string Qualification { get; set; }
        public string ContactInfo { get; set; }
        public string WorkingTime { get; set; }
        public string IntroductingSubTitle { get; set; }
        public string IntroductingText { get; set; }
        public bool ShowInHome { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
