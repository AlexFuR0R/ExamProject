using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualificationCoursesExam.Models {
    public class Course {
        public int CourseId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Topic { get; set; }
        public int TeacherId { get; set; }
        public User? Teacher { get; set; }
        public int DurationHours { get; set; }
        public decimal Price { get; set; }

        public ICollection<Schedule>? Schedules { get; set; }
        public ICollection<Registration>? Registrations { get; set; }

    }
}
