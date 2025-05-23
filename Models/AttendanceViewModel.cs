using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualificationCoursesExam.Models {
    public class AttendanceViewModel {
        public int AttendanceId { get; set; }
        public DateTime Date { get; set; }
        public string StudentName { get; set; }
        public string CourseTitle { get; set; }
        public bool IsPresent { get; set; }
        public string Notes { get; set; }
        public string Classroom { get; set; }
    }
}