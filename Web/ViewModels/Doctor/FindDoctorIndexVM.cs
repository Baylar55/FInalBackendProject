using Core.Entities;

namespace Web.ViewModels.FindDoctor
{
    public class FindDoctorIndexVM
    {
        public List<Core.Entities.Doctor> Doctor { get; set; }
        public string? DoctorName { get; set; }
    }
}
