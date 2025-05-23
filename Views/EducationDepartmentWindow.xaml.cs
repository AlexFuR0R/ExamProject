using Microsoft.EntityFrameworkCore;
using QualificationCoursesExam.Models;
using System.Linq;
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

        private void DeleteCourse_Click(object sender, RoutedEventArgs e)
        {
            var selectedCourse = coursesGrid.SelectedItem as Course;
            if (selectedCourse == null)
            {
                MessageBox.Show("Выберите курс для удаления");
                return;
            }

            try
            {
                using (var context = new AppDbContext())
                {
                    var course = context.Courses.Find(selectedCourse.CourseId);
                    if (course != null)
                    {
                        context.Courses.Remove(course);
                        context.SaveChanges();
                        MessageBox.Show("Курс успешно удален");
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении курса: {ex.Message}");
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void DeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            var selectedStudent = studentsGrid.SelectedItem as User;
            if (selectedStudent == null)
            {
                MessageBox.Show("Выберите слушателя для удаления");
                return;
            }

            try
            {
                using (var context = new AppDbContext())
                {
                    // Удаление связанных записей в таблице Registrations
                    var registrations = context.Registrations
                        .Where(r => r.StudentId == selectedStudent.UserId)
                        .ToList();

                    context.Registrations.RemoveRange(registrations);

                    // Удаление связанных записей в таблице Attendances
                    var attendances = context.Attendances
                        .Where(a => a.StudentId == selectedStudent.UserId)
                        .ToList();

                    context.Attendances.RemoveRange(attendances);

                    // Удаление слушателя
                    var student = context.Users.Find(selectedStudent.UserId);
                    if (student != null)
                    {
                        context.Users.Remove(student);
                        context.SaveChanges();
                        MessageBox.Show("Слушатель успешно удален");
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении слушателя: {ex.Message}");
            }
        }

        private void AddCourse_Click(object sender, RoutedEventArgs e)
        {
            var addCourseWindow = new AddCourseWindow();
            addCourseWindow.ShowDialog();
            LoadData(); // Обновите данные после добавления курса
        }


        private void DeleteRegistration_Click(object sender, RoutedEventArgs e)
        {
            var selectedRegistration = registrationsGrid.SelectedItem as Registration;
            if (selectedRegistration == null)
            {
                MessageBox.Show("Выберите регистрацию для удаления");
                return;
            }

            try
            {
                using (var context = new AppDbContext())
                {
                    var registration = context.Registrations.Find(selectedRegistration.RegistrationId);
                    if (registration != null)
                    {
                        context.Registrations.Remove(registration);
                        context.SaveChanges();
                        MessageBox.Show("Регистрация успешно удалена");
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении регистрации: {ex.Message}");
            }
        }
    }
}
    