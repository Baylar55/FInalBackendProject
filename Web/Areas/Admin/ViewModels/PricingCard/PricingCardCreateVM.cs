using Core.Constants;

namespace Web.Areas.Admin.ViewModels.PricingCard
{
    public class PricingCardCreateVM
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string PriceUnit { get; set; }
        public double PriceValue { get; set; }
        public string PricePer { get; set; }
        public string Features { get; set; }
        public CardStatus Status { get; set; }
    }
}
