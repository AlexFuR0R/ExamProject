using QualificationCoursesExam.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QualificationCoursesExam.Views; 

namespace QualificationCoursesExam {
    public partial class MainWindow : Window {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Login_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPassword.Password;

            using (var context = new AppDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Login == login && u.Password == password);

                if (user != null)
                {
                    OpenRoleWindow(user);
                    this.Hide();
                }
                else
                {
                    txtError.Text = "Неверный логин или пароль";
                }
            }
        }

        public void OpenRoleWindow(User user)
        {
            switch (user.Role)
            {
                case "Student":
                    new StudentWindow(user).Show();
                    break;
                case "Teacher":
                    new TeacherWindow(user).Show();
                    break;
                case "EducationDepartment":
                    new EducationDepartmentWindow().Show();
                    break;
            }
        }
    }
}