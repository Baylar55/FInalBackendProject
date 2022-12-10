using Core.Constants;
using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class PricingCard:BaseEntity
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
