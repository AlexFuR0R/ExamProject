using ClosedXML.Excel;
using Microsoft.Win32;
using QualificationCoursesExam.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace QualificationCoursesExam.Views {
    public partial class TestWindow : Window {
        private readonly User _student;
        private readonly Course _course;
        private List<Question> _questions;

        public TestWindow(User student, Course course)
        {
            InitializeComponent();
            _student = student;
            _course = course;
            LoadTestQuestions();
        }

        private void LoadTestQuestions()
        {
            _questions = new List<Question>
            {
                new Question { QuestionText = "Вопрос 1", Options = new List<string> { "Ответ 1", "Ответ 2", "Ответ 3" }, CorrectAnswer = "Ответ 1" },
                new Question { QuestionText = "Вопрос 2", Options = new List<string> { "Ответ 1", "Ответ 2", "Ответ 3" }, CorrectAnswer = "Ответ 2" },
                new Question { QuestionText = "Вопрос 3", Options = new List<string> { "Ответ 1", "Ответ 2", "Ответ 3" }, CorrectAnswer = "Ответ 3" }
            };

            questionsListBox.ItemsSource = _questions;
        }

        private void FinishTest_Click(object sender, RoutedEventArgs e)
        {
            int score = CalculateScore();
            MessageBox.Show($"Ваш результат: {score} из {_questions.Count}");

            ExportCertificate(score);
        }

        private int CalculateScore()
        {
            int score = 0;
            foreach (var item in questionsListBox.Items)
            {
                var question = item as Question;
                var listBoxItem = questionsListBox.ItemContainerGenerator.ContainerFromItem(item) as System.Windows.Controls.ListBoxItem;
                if (listBoxItem != null)
                {
                    var listBox = FindVisualChild<ListBox>(listBoxItem);
                    if (listBox != null)
                    {
                        foreach (var option in listBox.Items)
                        {
                            var radioButton = listBox.ItemContainerGenerator.ContainerFromItem(option) as System.Windows.Controls.RadioButton;
                            if (radioButton != null && radioButton.IsChecked == true && radioButton.Content.ToString() == question.CorrectAnswer)
                            {
                                score++;
                                break;
                            }
                        }
                    }
                }
            }
            return score;
        }

        private static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    var childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }
            return null;
        }


        private void ExportCertificate(int score)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Сертификат");

                worksheet.Cell(1, 1).Value = "Сертификат";
                worksheet.Cell(2, 1).Value = $"Студент: {_student.FullName}";
                worksheet.Cell(3, 1).Value = $"Курс: {_course.Title}";
                worksheet.Cell(4, 1).Value = $"Оценка: {score}";

                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    FileName = $"Сертификат_{_student.FullName}_{_course.Title}"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Сертификат успешно экспортирован в Excel");
                }
            }
        }
    }

    public class Question {
        public string QuestionText { get; set; }
        public List<string> Options { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
