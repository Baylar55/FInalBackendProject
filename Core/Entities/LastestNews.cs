using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class LastestNews:BaseEntity
    {
        public string PhotoName { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
    }
}
