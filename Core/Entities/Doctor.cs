using Core.Constants;
using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Doctor:BaseEntity
    {
        public string Name { get; set; }
        public string SubTitle { get; set; }
        public string Skill { get; set; }
        public string PhotoName { get; set; }
        public string Qualification { get; set;}
        public string ContactInfo { get; set;}
        public string WorkingTime { get; set;}
        public string IntroductingSubTitle { get; set;}
        public string IntroductingText { get; set;}
        public bool ShowInHome { get; set; }
        public DoctorSpeciality Speciality { get; set; }
    }
}
