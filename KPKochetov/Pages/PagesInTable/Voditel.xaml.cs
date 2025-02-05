using ClassModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KPKochetov.Pages.PagesInTable
{
    /// <summary>
    /// Логика взаимодействия для Voditel.xaml
    /// </summary>
    public partial class Voditel : Page
    {
        ClassModules.Voditel voditel;
        public Voditel(ClassModules.Voditel _Voditel)
        {
            InitializeComponent();
            voditel = _Voditel;
            if (_Voditel.Prava != null)
            {
                Name_voditel.Text = _Voditel.Name_voditel;
                Prava.Text = _Voditel.Prava;
            }
        }

        private void Click_Voditel_Redact(object sender, RoutedEventArgs e)
        {
            string[] FIOPrava = Prava.Text.Split(' ');
            if (FIOPrava.Length <= 3)
            {
                int id = Login_Regin.Login.connection.SetLastId(ClassConnection.Connection.Tables.voditel);
                if (voditel.Prava == null)
                {
                    string query = $"INSERT INTO Voditel ([Id_voditel], [Name_voditel], [Prava], [Date_foundation], [Date_update_information]) VALUES ({id.ToString()}, N'{Name_voditel.Text}', N'{Prava.Text}', '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')";
                    var query_apply = Login_Regin.Login.connection.Query(query);
                    if (query_apply != null)
                    {
                        Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.voditel);
                        MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.Voditel);
                    }
                    else MessageBox.Show("Запрос на добавление водителя не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    string query = $"UPDATE Voditel SET Name_voditel = N'{Name_voditel.Text}', Prava = N'{Prava.Text}', Date_update_information = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE Id_voditel = {voditel.Id_voditel}";
                    var query_apply = Login_Regin.Login.connection.Query(query);
                    if (query_apply != null)
                    {
                        Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.voditel);
                        MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.Voditel);
                    }
                    else MessageBox.Show("Запрос на изменение водителя не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void Click_Cancel_Voditel_Redact(object sender, RoutedEventArgs e) => MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main);

        private void Click_Remove_Voditel_Redact(object sender, RoutedEventArgs e)
        {
            try
            {
                Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.voditel);
                string query = "Delete From Voditel Where [Id_voditel] = " + voditel.Id_voditel.ToString() + "";
                var query_apply = Login_Regin.Login.connection.Query(query);
                if (query_apply != null)
                {
                    Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.voditel);
                    MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.Voditel);
                }
                else MessageBox.Show("Запрос на удаление водителя не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextBox_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[А-Яа-яA-Za-z0-9\s]*$");
            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true;
            }
        }

        private void TextBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string[] words = textBox.Text.Split(' ');
            if (words.Any(word => word.Length == 0))
            {
                textBox.Text = "Ошибка: введите значение";
                Name_voditel.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FB3F51"));
            }
        }

        private void TextBox_GotFocus_1(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.StartsWith("Ошибка:"))
            {
                textBox.Text = "";
                ColorAnimation animation = new ColorAnimation();
                animation.From = (Color)ColorConverter.ConvertFromString("#FB3F51");
                animation.To = Colors.Transparent;
                animation.Duration = new Duration(TimeSpan.FromSeconds(2));
                SolidColorBrush brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FB3F51"));
                brush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
                Name_voditel.BorderBrush = brush;
            }
        }

        private void TextBox_PreviewTextInput_2(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[0-9\s]*$");
            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true;
            }
        }

        private void TextBox_LostFocus_2(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string[] words = textBox.Text.Split(' ');
            if (words.Length != 3 || words.Any(word => word.Length == 0))
            {
                textBox.Text = "Ошибка: Вводите только цифры через пробел";
                Prava.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FB3F51"));
            }
        }
        

        private void TextBox_GotFocus_2(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.StartsWith("Ошибка:"))
            {
                textBox.Text = "";
                ColorAnimation animation = new ColorAnimation();
                animation.From = (Color)ColorConverter.ConvertFromString("#FB3F51");
                animation.To = Colors.Transparent;
                animation.Duration = new Duration(TimeSpan.FromSeconds(2));
                SolidColorBrush brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FB3F51"));
                brush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
                Prava.BorderBrush = brush;
            }
        }
    }
}
