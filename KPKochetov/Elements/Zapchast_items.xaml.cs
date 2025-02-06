using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KPKochetov.Elements
{
    /// <summary>
    /// Логика взаимодействия для Zapchast_items.xaml
    /// </summary>
    public partial class Zapchast_items : UserControl
    {
        ClassModules.Zapchast zapchast;
        public Zapchast_items(ClassModules.Zapchast _zapchast)
        {
            InitializeComponent();
            if (Pages.Login_Regin.Login.UserInfo[1] != "admin") Buttons.Visibility = Visibility.Hidden;
            zapchast = _zapchast;
            if (_zapchast.Name_zapchast != null)
            {
                Name_zapchast.Content = "Название запчасти: " + _zapchast.Name_zapchast;
                Description.Content = "Описание: " + _zapchast.Description;
                Date_foundation.Content = "Дата создания: " + _zapchast.Date_foundation.ToString("dd.MM.yyyy");
            }
        }

        private void Click_redact(object sender, RoutedEventArgs e) => MainWindow.main.Animation_move(MainWindow.main.scroll_main, MainWindow.main.frame_main, MainWindow.main.frame_main, new Pages.PagesInTable.Zapchast(zapchast));

        private void Click_remove(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить информацию о виде запчасти?", "Удаление информации", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Pages.Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.zapchast);
                    string query = $"Delete From zapchast Where Id_zapchast = " + zapchast.Id_zapchast.ToString() + "";
                    var query_apply = Pages.Login_Regin.Login.connection.Query(query);
                    if (query_apply != null)
                    {
                        Pages.Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.zapchast);
                        MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Pages.Main.page_main.zapchast);
                    }
                    else MessageBox.Show("Запрос на удаление вида запчасти не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}