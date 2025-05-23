using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualificationCoursesExam.Models {
    public class Certification {
        [Key]
        public int CertificationId { get; set; }

        [Required]
        public int RegistrationId { get; set; }
        public Registration? Registration { get; set; }

        [Range(0, 100)]
        public int TestScore { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }

        [StringLength(50)]
        public string? CertificateNumber { get; set; }

        public byte[]? CertificatePdf { get; set; }

        public bool IsPassed => TestScore >= 70;
    }
}