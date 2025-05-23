using QualificationCoursesExam.Models;
using System;
using System.Linq;
using System.Windows;

namespace QualificationCoursesExam.Views {
    public partial class AddCourseWindow : Window {
        public AddCourseWindow()
        {
            InitializeComponent();
            LoadTeachers();
        }

        private void LoadTeachers()
        {
            using (var context = new AppDbContext())
            {
                var teachers = context.Users
                    .Where(u => u.Role == "Teacher")
                    .ToList();

                cmbTeacher.ItemsSource = teachers;
                cmbTeacher.DisplayMemberPath = "FullName";
                cmbTeacher.SelectedValuePath = "UserId";
            }
        }

        private void AddCourse_Click(object sender, RoutedEventArgs e)
        {
            string title = txtTitle.Text;
            string description = txtDescription.Text;
            string topic = txtTopic.Text;
            int durationHours = int.Parse(txtDurationHours.Text);
            decimal price = decimal.Parse(txtPrice.Text);
            int teacherId = (int)cmbTeacher.SelectedValue;

            using (var context = new AppDbContext())
            {
                var course = new Course
                {
                    Title = title,
                    Description = description,
                    Topic = topic,
                    DurationHours = durationHours,
                    Price = price,
                    TeacherId = teacherId,
                };

                context.Courses.Add(course);
                context.SaveChanges();
                MessageBox.Show("Курс успешно добавлен");
                this.Close();
            }
        }
    }
}
