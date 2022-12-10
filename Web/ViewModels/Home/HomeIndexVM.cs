using Core.Entities;

namespace Web.ViewModels.Home
{
    public class HomeIndexVM
    {
        public List<HomeMainSlider>? HomeMainSlider { get; set; }
        public List<OurVision>? OurVision { get; set; }
        public List<MedicalDepartment>? MedicalDepartment { get; set; }
        public List<Core.Entities.Doctor> Doctor { get; set; }
        public WhyChoose WhyChoose { get; set; }
        public About About { get; set; }
        public List<LastestNews> LastestNews { get; set; }
        public HomeVideo HomeVideo { get; set; }
        public List<Statistics> Statistics { get; set; }
    }
}
