using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.SQLite;

namespace Solution1
{
    public partial class MainWindow : Window
    {
        ApplicationContext DB;
        int taskNC = 0;//cчетчик невыполненных задач
        int taskC = 0;//cчетчик выполненных задач
        public List<StackPanel> panels1 = new List<StackPanel>();//cписок панелей для невыполненных задач
        public List<StackPanel> panels2 = new List<StackPanel>();//cписок панелей для выполненных задач
        public List<Label> labels1 = new List<Label>();//cписок меток для невыполненных задач
        public List<Label> labels2 = new List<Label>();//cписок меток для выполненных задач
        public List<int> iNcheck = new List<int>();//cписок ID невыполненных задач
        public List<int> iYcheck = new List<int>();//cписок ID выполненных задач
        List<Task> tasks = new List<Task>();//cписок задач

        public MainWindow()
        {
            InitializeComponent();
            DB = new ApplicationContext();
            //получение списка задач из базы данных и создание панелей для них
            List<Task> tasks = DB.Tasks.ToList();
            foreach (Task task in tasks)
            {
                if (task.completion == 0)
                {
                    iNcheck.Add(task.id);
                    taskNC++;
                    //создание панели для невыполненной задачи
                    StackPanel clonedPanel = new StackPanel
                    {
                        Background = panelPrefab.Background,
                        Width = panelPrefab.Width,
                        Height = panelPrefab.Height,
                        VerticalAlignment = panelPrefab.VerticalAlignment,
                        HorizontalAlignment = panelPrefab.HorizontalAlignment,
                        Margin = panelPrefab.Margin,
                        Visibility = Visibility.Visible
                    };
                    targetPanel.Children.Add(clonedPanel);
                    panels1.Add(clonedPanel);
                    Label clonedlabel = new Label
                    {
                        Background = lInfoLes.Background,
                        Width = lInfoLes.Width,
                        Height = lInfoLes.Height,
                        VerticalAlignment = lInfoLes.VerticalAlignment,
                        HorizontalAlignment = lInfoLes.HorizontalAlignment,
                        Margin = lInfoLes.Margin,
                        Visibility = Visibility.Visible
                    };
                    clonedPanel.Children.Add(clonedlabel);
                    clonedlabel.Visibility = Visibility.Visible;
                    clonedlabel.Content = task.name + "\n" + task.date;
                    Button clonedbutton = new Button
                    {
                        Background = bPanelPref.Background,
                        Width = bPanelPref.Width,
                        Height = bPanelPref.Height,
                        VerticalAlignment = bPanelPref.VerticalAlignment,
                        HorizontalAlignment = bPanelPref.HorizontalAlignment,
                        Margin = bPanelPref.Margin,
                        Visibility = Visibility.Visible,
                        Content = bPanelPref.Content
                    };
                    clonedPanel.Children.Add(clonedbutton);
                    clonedbutton.Visibility = Visibility.Visible;
                    clonedbutton.Click += bPanelPref_Click;
                }
                else
                {
                    iYcheck.Add(task.id);
                    taskC++;
                    //cоздание панели для выполненной задачи
                    StackPanel clonedPanel = new StackPanel
                    {
                        Background = panelPrefab2.Background,
                        Width = panelPrefab2.Width,
                        Height = panelPrefab2.Height,
                        VerticalAlignment = panelPrefab2.VerticalAlignment,
                        HorizontalAlignment = panelPrefab2.HorizontalAlignment,
                        Margin = panelPrefab2.Margin,
                        Visibility = Visibility.Visible
                    };
                    targetPanel2.Children.Add(clonedPanel);
                    panels2.Add(clonedPanel);
                    Label clonedlabel = new Label
                    {
                        Background = lInfoLes2.Background,
                        Width = lInfoLes2.Width,
                        Height = lInfoLes2.Height,
                        VerticalAlignment = lInfoLes2.VerticalAlignment,
                        HorizontalAlignment = lInfoLes2.HorizontalAlignment,
                        Margin = lInfoLes2.Margin,
                        Visibility = Visibility.Visible
                    };
                    clonedPanel.Children.Add(clonedlabel);
                    clonedlabel.Visibility = Visibility.Visible;
                    clonedlabel.Content = task.name + "\n" + task.date;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //обработчик нажатия на кнопку добавления новой задачи
            taskNC++;
            StackPanel clonedPanel = new StackPanel
            {
                Background = panelPrefab.Background,
                Width = panelPrefab.Width,
                Height = panelPrefab.Height,
                VerticalAlignment = panelPrefab.VerticalAlignment,
                HorizontalAlignment = panelPrefab.HorizontalAlignment,
                Margin = panelPrefab.Margin,
                Visibility = Visibility.Visible
            };
            targetPanel.Children.Add(clonedPanel);
            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("dd.MM.yyyy HH:mm");
            InputDialog inputDialog = new InputDialog("Введите название предмета");
            string enteredText = "";
            if (inputDialog.ShowDialog() == true)
            {
                enteredText = inputDialog.EnteredText;
            }
            Task task = new Task(enteredText, formattedDateTime, 0);
            DB.Tasks.Add(task);
            DB.SaveChanges();
            panels1.Add(clonedPanel);
            Label clonedlabel = new Label
            {
                Background = lInfoLes.Background,
                Width = lInfoLes.Width,
                Height = lInfoLes.Height,
                VerticalAlignment = lInfoLes.VerticalAlignment,
                HorizontalAlignment = lInfoLes.HorizontalAlignment,
                Margin = lInfoLes.Margin,
                Visibility = Visibility.Visible
            };
            clonedPanel.Children.Add(clonedlabel);
            clonedlabel.Visibility = Visibility.Visible;
            labels1.Add(clonedlabel);
            Button clonedbutton = new Button
            {
                Background = bPanelPref.Background,
                Width = bPanelPref.Width,
                Height = bPanelPref.Height,
                VerticalAlignment = bPanelPref.VerticalAlignment,
                HorizontalAlignment = bPanelPref.HorizontalAlignment,
                Margin = bPanelPref.Margin,
                Visibility = Visibility.Visible,
                Content = bPanelPref.Content
            };
            clonedPanel.Children.Add(clonedbutton);
            clonedbutton.Visibility = Visibility.Visible;
            clonedbutton.Click += bPanelPref_Click;
            List<Task> tasks = DB.Tasks.ToList();
            clonedlabel.Content = tasks[tasks.Count - 1].name + "\n" + tasks[tasks.Count - 1].date;
            iNcheck.Add(tasks[tasks.Count - 1].id);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //обработчик нажатия на кнопку "Удалить всё"
            string connectionString = "Data Source=database.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM Tasks";//SQL-запрос для удаления всех задач из базы данных
                using (SQLiteCommand command = new SQLiteCommand(deleteQuery, connection))
                {
                    command.ExecuteNonQuery();//выполняем SQL-запрос
                }
                connection.Close();
            }
            taskC = 0;//сбрасываем счетчик выполненных задач
            taskNC = 0;//сбрасываем счетчик невыполненных задач
            panelPrefab.Visibility = Visibility.Hidden; // Скрываем панели
            panelPrefab2.Visibility = Visibility.Hidden;
            for (int i = 0; i < panels1.Count; i++)
                targetPanel.Children.Remove(panels1[i]);//удаляем панели для невыполненных задач
            panels1.Clear();
            for (int i = 0; i < panels2.Count; i++)
                targetPanel2.Children.Remove(panels2[i]);//удаляем панели для выполненных задач
            panels2.Clear();
            iYcheck.Clear();//очищаем список ID выполненных задач
            iNcheck.Clear();//очищаем список ID невыполненных задач
        }

        private void bPanelPref_Click(object sender, RoutedEventArgs e)
        {
            //обработчик нажатия на кнопку "Выполнено" для задачи
            //получаем кнопку, на которую было совершено нажатие
            Button button = sender as Button;
            //получаем родительскую панель кнопки, где хранится информация о задаче
            Panel parentPanel = button.Parent as Panel;
            for (int j = 0; j < panels1.Count; j++)
            {
                if (panels1[j] == parentPanel)
                {
                    //находим соответствующую панель в списке невыполненных задач
                    List<Task> tasks = DB.Tasks.ToList();
                    taskC++;//увеличиваем счетчик выполненных задач
                    taskNC--;//уменьшаем счетчик невыполненных задач
                    iYcheck.Add(iNcheck[j]);//добавляем ID задачи в список выполненных задач
                    targetPanel.Children.Remove(panels1[j]);//удаляем панель из списка невыполненных задач
                    StackPanel clonedPanel = new StackPanel
                    {
                        Background = panels1[j].Background,
                        Width = panels1[j].Width,
                        Height = panels1[j].Height,
                        VerticalAlignment = panels1[j].VerticalAlignment,
                        HorizontalAlignment = panels1[j].HorizontalAlignment,
                        Margin = panels1[j].Margin,
                        Visibility = Visibility.Visible
                    };
                    targetPanel2.Children.Add(clonedPanel);
                    panels2.Add(clonedPanel);//добавляем панель в список выполненных задач
                    Label clonedlabel = new Label
                    {
                        Background = lInfoLes2.Background,
                        Width = lInfoLes2.Width,
                        Height = lInfoLes2.Height,
                        VerticalAlignment = lInfoLes2.VerticalAlignment,
                        HorizontalAlignment = lInfoLes2.HorizontalAlignment,
                        Margin = lInfoLes2.Margin,
                        Visibility = Visibility.Visible
                    };
                    clonedPanel.Children.Add(clonedlabel);
                    clonedlabel.Visibility = Visibility.Visible;
                    for (int i = 0; i < tasks.Count; i++)
                    {
                        if (tasks[i].id == iNcheck[j])
                        {
                            clonedlabel.Content = tasks[i].name + "\n" + tasks[i].date;
                        }
                    }
                    //обновляем статус задачи в базе данных на "готово"
                    string connectionString = "Data Source=database.db;Version=3;";
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        string sql = "UPDATE Tasks SET Completion = @новое_значение1 WHERE id = @ключевое_значение;";

                        using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@новое_значение1", 1);//1 означает готово
                            command.Parameters.AddWithValue("@ключевое_значение", iNcheck[j]);//ID задачи
                            int rowsUpdated = command.ExecuteNonQuery();
                        }
                        connection.Close();
                    }
                    iNcheck.Remove(iNcheck[j]);//удаляем ID задачи из списка невыполненных задач
                    panels1.Remove(panels1[j]);//удаляем панель из списка невыполненных задач
                    break;
                }
            }
        }
    }
}
