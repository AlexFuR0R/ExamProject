using Microsoft.EntityFrameworkCore;
using QualificationCoursesExam.Models;
using System.Windows;

namespace QualificationCoursesExam.Views {
    public partial class EducationDepartmentWindow : Window {
        public EducationDepartmentWindow()
        {
            InitializeComponent();
            Loaded += (s, e) => LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var courses = context.Courses
                        .Include(c => c.Teacher)
                        .AsNoTracking()
                        .ToList();
                    coursesGrid.ItemsSource = courses;

                    var students = context.Users
                        .Where(u => u.Role == "Student")
                        .AsNoTracking()
                        .ToList();
                    studentsGrid.ItemsSource = students;

                    var registrations = context.Registrations
                        .Include(r => r.Student)
                        .Include(r => r.Course)
                        .AsNoTracking()
                        .ToList();
                    registrationsGrid.ItemsSource = registrations;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }
    }
}