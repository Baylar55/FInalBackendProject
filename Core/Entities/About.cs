using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class About :BaseEntity
    {
        public string SubTitle { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string SignaturePhotoName { get; set; }
        public ICollection<AboutPhoto> AboutPhotos { get; set; }
    }
}
