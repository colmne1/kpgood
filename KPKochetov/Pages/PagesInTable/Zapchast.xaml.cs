using ClassModules;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
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
    /// Логика взаимодействия для Zapchast.xaml
    /// </summary>
    public partial class Zapchast : Page
    {
        ClassModules.Zapchast type_of_troops;
        public Zapchast(ClassModules.Zapchast _type_of_troops)
        {
            InitializeComponent();
            type_of_troops = _type_of_troops;
            if (_type_of_troops.Name_zapchast != null)
            {
                Name_zapchast.Text = _type_of_troops.Name_zapchast;
                Description.Text = _type_of_troops.Description;
            }
            foreach (var item in ClassConnection.Connection.voditel)
            {
                ComboBoxItem cb_Vmestim = new ComboBoxItem();
                cb_Vmestim.Tag = item.Id_voditel;
                cb_Vmestim.Content = item.Name_voditel;

            }
        }

        private void Click_TypeOfTroops_Redact(object sender, RoutedEventArgs e)
        {
            ClassModules.Voditel Id_voditel_temp;
            int id = Pages.Login_Regin.Login.connection.SetLastId(ClassConnection.Connection.Tables.zapchast);
            if (type_of_troops.Name_zapchast == null)
            {
                string query = $"Insert Into zapchast ([Id_zapchast], [Name_zapchast], [Description], [Date_foundation]) Values ({id.ToString()}, N'{Name_zapchast.Text}', N'{Description.Text}', '{DateTime.Now.ToString("yyyy-MM-dd")}')";
                var query_apply = Pages.Login_Regin.Login.connection.Query(query);
                if (query_apply != null)
                {
                    Pages.Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.zapchast);
                    MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.zapchast);
                }
                else MessageBox.Show("Запрос на добавление запчасти не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                string query = $"Update zapchast Set Name_zapchast = N'{Name_zapchast.Text}', Description = N'{Description.Text}' Where Id_zapchast = {type_of_troops.Id_zapchast}";
                var query_apply = Pages.Login_Regin.Login.connection.Query(query);
                if (query_apply != null)
                {
                    Pages.Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.zapchast);
                    MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.zapchast);
                }
                else MessageBox.Show("Запрос на изменение запчасти не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Click_Cancel_TypeOfTroops_Redact(object sender, RoutedEventArgs e) => MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main);

        private void Click_Remove_TypeOfTroops_Redact(object sender, RoutedEventArgs e)
        {
            try
            {
                Pages.Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.zapchast);
                string query = "Delete From zapchast Where [Id_zapchast] = " + type_of_troops.Id_zapchast.ToString() + "";
                var query_apply = Pages.Login_Regin.Login.connection.Query(query);
                if (query_apply != null)
                {
                    Pages.Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.zapchast);
                    MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.zapchast);
                }
                else MessageBox.Show("Запрос на удаление запчасти не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextBox_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[А-Яа-яA-Za-z\s]*$");
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
                Name_zapchast.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FB3F51"));
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
                Name_zapchast.BorderBrush = brush;
            }
        }

        private void TextBox_LostFocus_2(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string[] words = textBox.Text.Split(' ');
            if (words.Any(word => word.Length == 0))
            {
                textBox.Text = "Ошибка: введите значение";
                Description.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FB3F51"));
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
                Description.BorderBrush = brush;
            }
        }

        private void TextBox_GotFocus_3(object sender, RoutedEventArgs e)
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
            }
        }
    }
}