using DataAccess.Repositories.Abstract;
using Web.Services.Abstract;
using Web.ViewModels.Home;

namespace Web.Services.Concrete
{
    public class HomeService : IHomeService
    {
        private readonly IHomeMainSliderRepository _homeMainSliderRepository;
        private readonly IOurVisionRepository _ourVisionRepository;
        private readonly IMedicalDepartmentRepository _medicalDepartmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IWhyChooseRepository _whyChooseRepository;
        private readonly IAboutUsRepository _aboutUsRepository;
        private readonly ILastestNewsRepository _lastestNewsRepository;
        private readonly IHomeVideoRepository _homeVideoRepository;
        private readonly IStatisticsRepository _statisticsRepository;

        public HomeService(IHomeMainSliderRepository homeMainSliderRepository,
                           IOurVisionRepository ourVisionRepository,
                           IMedicalDepartmentRepository medicalDepartmentRepository,
                           IDoctorRepository doctorRepository,
                           IWhyChooseRepository whyChooseRepository,
                           IAboutUsRepository aboutUsRepository,
                           ILastestNewsRepository lastestNewsRepository,
                           IHomeVideoRepository homeVideoRepository,
                           IStatisticsRepository statisticsRepository)
        {
            _homeMainSliderRepository = homeMainSliderRepository;
            _ourVisionRepository = ourVisionRepository;
            _medicalDepartmentRepository = medicalDepartmentRepository;
            _doctorRepository = doctorRepository;
            _whyChooseRepository = whyChooseRepository;
            _aboutUsRepository = aboutUsRepository;
            _lastestNewsRepository = lastestNewsRepository;
            _homeVideoRepository = homeVideoRepository;
            _statisticsRepository = statisticsRepository;
        }
        public async Task<HomeIndexVM> GetAsync()
        {
            var model = new HomeIndexVM()
            {
                HomeMainSlider = await _homeMainSliderRepository.GetAllAsync(),
                OurVision = await _ourVisionRepository.GetAllAsync(),
                MedicalDepartment = await _medicalDepartmentRepository.GetAllAsync(),
                Doctor = await _doctorRepository.GetFourDoctorAsync(),
                WhyChoose = await _whyChooseRepository.GetAsync(),
                About = await _aboutUsRepository.GetAsync(),
                LastestNews = await _lastestNewsRepository.GetAllAsync(),
                HomeVideo = await _homeVideoRepository.GetAsync(),
                Statistics=await _statisticsRepository.GetAllAsync(),
            };
            return model;
        }
    }
}
