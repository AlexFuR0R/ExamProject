using QualificationCoursesExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;

namespace QualificationCoursesExam.Views {
    /// <summary>
    /// Логика взаимодействия для StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window {
        private User _student;

        public StudentWindow(User student)
        {
            InitializeComponent();
            _student = student;
            this.Title = $"Слушатель: {student.FullName}";
            LoadData();
        }

        private void LoadData()
        {
            using (var context = new AppDbContext())
            {
                coursesGrid.ItemsSource = context.Courses
                    .Include(c => c.Teacher)
                    .ToList();

                myCoursesGrid.ItemsSource = context.Registrations
                    .Include(r => r.Course)
                    .Include(r => r.Certification)
                    .Where(r => r.StudentId == _student.UserId)
                    .ToList();

                scheduleGrid.ItemsSource = context.Schedule
                    .Include(s => s.Course)
                    .Where(s => s.Course.Registrations.Any(r => r.StudentId == _student.UserId))
                    .ToList();
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string searchText = searchBox.Text.ToLower();

            using (var context = new AppDbContext())
            {
                var courses = context.Courses
                    .Include(c => c.Teacher)
                    .Where(c => c.Title.ToLower().Contains(searchText) ||
                               c.Topic.ToLower().Contains(searchText) ||
                               c.Teacher.FullName.ToLower().Contains(searchText))
                    .ToList();

                coursesGrid.ItemsSource = courses;
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var selectedCourse = coursesGrid.SelectedItem as Course;
            if (selectedCourse == null)
            {
                MessageBox.Show("Выберите курс для регистрации");
                return;
            }

            using (var context = new AppDbContext())
            {
                var existingRegistration = context.Registrations
                    .FirstOrDefault(r => r.StudentId == _student.UserId &&
                                       r.CourseId == selectedCourse.CourseId &&
                                       r.Status == "Active");

                if (existingRegistration != null)
                {
                    MessageBox.Show("Вы уже зарегистрированы на этот курс");
                    return;
                }

                var registration = new Registration
                {
                    StudentId = _student.UserId,
                    CourseId = selectedCourse.CourseId,
                    RegistrationDate = DateTime.Now,
                    Status = "Active"
                };

                context.Registrations.Add(registration);
                context.SaveChanges();

                MessageBox.Show("Регистрация на курс успешно выполнена");
                LoadData();
            }
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void TakeTest_Click(object sender, RoutedEventArgs e)
        {
            var selectedCourse = myCoursesGrid.SelectedItem as Registration;
            if (selectedCourse == null)
            {
                MessageBox.Show("Выберите курс для прохождения теста");
                return;
            }

            var testWindow = new TestWindow(_student, selectedCourse.Course);
            testWindow.ShowDialog();
        }


    }
}