using Core.Entities;

namespace Web.ViewModels.Pages
{
    public class PagesIndexVM
    {
        public List<PricingCard>? PricingCards { get; set; }
        public List<FAQCategory>? FAQCategories { get; set; }
        public List<Question>? Questions { get; set; }
    }
}
