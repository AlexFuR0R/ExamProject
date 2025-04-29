using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualificationCoursesExam.Models {
    public class Attendance {
        [Key]
        public int AttendanceId { get; set; }

        [Required]
        public int ScheduleId { get; set; }
        public Schedule? Schedule { get; set; }

        [Required]
        public int StudentId { get; set; }
        public User? Student { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool IsPresent { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }
    }


}
