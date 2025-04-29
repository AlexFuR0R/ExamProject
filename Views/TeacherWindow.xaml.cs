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
using System.IO; 
using Microsoft.Win32;
using ClosedXML.Excel;
using System.Diagnostics;
using OfficeOpenXml;

namespace QualificationCoursesExam.Views {
    public partial class TeacherWindow : Window {
        private readonly User _teacher;

        public TeacherWindow(User teacher)
        {
            InitializeComponent();
            _teacher = teacher;
            LoadCoursesData();
            LoadAttendanceData();
            LoadCertificationData();
            LoadScheduleData();
            LoadStudentsData();
        }

        private void LoadCoursesData()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    coursesGrid.ItemsSource = context.Courses
                        .Where(c => c.TeacherId == _teacher.UserId)
                        .Select(c => new
                        {
                            c.Title,
                            c.Topic,
                            c.DurationHours,
                            Price = c.Price.ToString("C") 
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки курсов: {ex.Message}");
            }
        }

        private void LoadScheduleData()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    scheduleGrid.ItemsSource = context.Schedule
                        .Include(s => s.Course)
                        .Where(s => s.Course.TeacherId == _teacher.UserId)
                        .Select(s => new
                        {
                            s.ScheduleId,
                            Course = s.Course.Title,
                            s.StartDate,
                            s.EndDate,
                            s.Classroom
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки расписания: {ex.Message}");
            }
        }
        public void LoadAttendanceData()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var attendanceData = context.Attendances
                        .Include(a => a.Student)
                        .Include(a => a.Schedule)
                            .ThenInclude(s => s.Course)
                        .Where(a => a.Schedule.Course.TeacherId == _teacher.UserId)
                        .Select(a => new
                        {
                            a.AttendanceId,  
                            a.Date,
                            StudentName = a.Student.FullName,
                            CourseTitle = a.Schedule.Course.Title,
                            a.IsPresent,
                            a.Notes,
                            Classroom = a.Schedule.Classroom
                        })
                        .OrderBy(a => a.Date)
                        .ToList();

                    attendanceGrid.ItemsSource = attendanceData;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки посещаемости: {ex.Message}");
            }
        }

        private void LoadCertificationData()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    certificationGrid.ItemsSource = context.Certifications
                        .Include(c => c.Registration)
                            .ThenInclude(r => r.Student)
                        .Include(c => c.Registration)
                            .ThenInclude(r => r.Course)
                        .Where(c => c.Registration.Course.TeacherId == _teacher.UserId)
                        .Select(c => new
                        {
                            c.CertificationId,
                            StudentName = c.Registration.Student.FullName,
                            CourseTitle = c.Registration.Course.Title,
                            c.TestScore,
                            IsPassed = c.TestScore >= 70,
                            IssueDate = c.IssueDate.ToString("dd.MM.yyyy"),
                            c.CertificateNumber
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных аттестации:\n{ex.Message}");
                certificationGrid.ItemsSource = new List<dynamic>();
            }
        }

        private void LoadStudentsData()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var students = context.Registrations
                        .Include(r => r.Student)
                        .Include(r => r.Course)
                        .Where(r => r.Course.TeacherId == _teacher.UserId)
                        .Select(r => new
                        {
                            r.Student.UserId,
                            r.Student.FullName,
                            r.Student.Email,
                            r.Student.Phone,
                            Course = r.Course.Title,
                            RegistrationDate = r.RegistrationDate.ToString("dd.MM.yyyy"),
                            r.Status
                        })
                        .Distinct() 
                        .ToList();

                    studentsGrid.ItemsSource = students;

                    Debug.WriteLine($"Загружено слушателей: {students.Count}");
                    foreach (var s in students)
                    {
                        Debug.WriteLine($"{s.FullName} ({s.Course})");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки списка слушателей: {ex.Message}",
                              "Ошибка",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
                studentsGrid.ItemsSource = new List<dynamic>(); 
            }
        }

        public void MarkAttendance_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = attendanceGrid.SelectedItem as dynamic;

            if (selectedItem == null)
            {
                MessageBox.Show("Выберите запись из таблицы!");
                return;
            }

            try
            {
                using (var context = new AppDbContext())
                {
                    var attendance = context.Attendances.Find((int)selectedItem.AttendanceId);

                    if (attendance != null)
                    {
                        attendance.IsPresent = !attendance.IsPresent;
                        context.SaveChanges();

                        LoadAttendanceData();

                        attendanceGrid.SelectedItem = attendanceGrid.Items
                            .OfType<dynamic>()
                            .FirstOrDefault(x => x.AttendanceId == selectedItem.AttendanceId);

                        attendanceGrid.ScrollIntoView(attendanceGrid.SelectedItem);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при изменении статуса: {ex.Message}");
            }
        }

        public void AttendanceGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MarkAttendance_Click(sender, e);
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            ExportAttendanceToTxt();
        }

        public void ExportAttendanceToTxt()
        {
            try
            {
                var exportData = attendanceGrid.ItemsSource
                    .OfType<dynamic>()
                    .Select(item => new
                    {
                        Date = item.Date,
                        StudentName = item.StudentName,
                        CourseTitle = item.CourseTitle,
                        IsPresent = item.IsPresent,
                        Notes = item.Notes
                    }).ToList();

                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Текстовые файлы (*.txt)|*.txt",
                    FileName = "Посещаемость_" + DateTime.Now.ToString("yyyyMMdd")
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    using (var writer = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                    {
                        writer.WriteLine("Дата\tСтудент\tКурс\tПрисутствие\tПримечания");

                        foreach (var item in exportData)
                        {
                            writer.WriteLine(
                                $"{item.Date:dd.MM.yyyy}\t" +
                                $"{item.StudentName}\t" +
                                $"{item.CourseTitle}\t" +
                                $"{(item.IsPresent ? "Да" : "Нет")}\t" +
                                $"{item.Notes}");
                        }
                    }

                    MessageBox.Show("Экспорт завершен успешно!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте: {ex.Message}");
            }
        }

    }
}