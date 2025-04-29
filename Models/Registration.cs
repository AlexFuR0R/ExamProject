using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualificationCoursesExam.Models {
    public class Registration {
        public int RegistrationId { get; set; }
        public int StudentId { get; set; }
        public User? Student { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string? Status { get; set; }

        public Certification? Certification { get; set; }
    }
}
