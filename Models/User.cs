using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QualificationCoursesExam.Models {
    public class User {
        public int UserId { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public string? Role { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }


        [InverseProperty("Student")]
        public ICollection<Registration>? RegistrationsAsStudent { get; set; }

        [InverseProperty("Teacher")]
        public ICollection<Course>? CoursesAsTeacher { get; set; }

        public ICollection<Attendance>? Attendances { get; set; }
    }

}

