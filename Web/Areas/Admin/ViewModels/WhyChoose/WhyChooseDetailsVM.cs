namespace Web.Areas.Admin.ViewModels.WhyChoose
{
    public class WhyChooseDetailsVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Check { get; set; }
        public int SatisfiedPatientsCount { get; set; }
        public int DoctorsCount { get; set; }
        public double Quality { get; set; }
        public int Experience { get; set; }
        public string PhotoName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
