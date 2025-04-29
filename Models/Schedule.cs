using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualificationCoursesExam.Models {
    [Table("Schedules")]
    public class Schedule {
        public int ScheduleId { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Classroom { get; set; }
    }
}
