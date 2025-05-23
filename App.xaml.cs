using QualificationCoursesExam.Models;
using System.Configuration;
using System.Data;
using System.Windows;

namespace QualificationCoursesExam {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        public static void EnsureDatabaseCreated()
        {
            using (var context = new AppDbContext())
            {
                if (!context.Users.Any())
                {
                    context.Users.Add(new User
                    {
                        Login = "admin",
                        Password = "admin123",
                        FullName = "Администратор",
                        Role = "EducationDepartment"
                    });
                    context.SaveChanges();
                }
            }
        }
    }

}