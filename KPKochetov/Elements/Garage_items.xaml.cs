using ClassConnection;
using ClassModules;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using KPKochetov.Pages;
using KPKochetov.Pages.PagesInTable;

namespace KPKochetov.Elements
{
    /// <summary>
    /// Логика взаимодействия для Garage.xaml
    /// </summary>
    public partial class Garage_items : UserControl
    {
        ClassModules.Garage garage;
        public Garage_items(ClassModules.Garage _garage)
        {
            InitializeComponent();
            if (Pages.Login_Regin.Login.UserInfo[1] != "admin") Buttons.Visibility = Visibility.Hidden;
            garage = _garage;
            if (_garage.Date_of_foundation != null)
            {
                Id_garage.Content = "Гараж " + _garage.Id_garage;
                ClassModules.Garage item_location = Connection.garage.Find(x => x.Id_garage == _garage.Id_garage);
                Locations.Content = "Дислокация " + _garage.Locations;
                Vmestim.Content = "Вместимость: " + _garage.Vmestim;
                Date_of_foundation.Content = "Дата основания: " + _garage.Date_of_foundation.ToString("dd.MM.yyyy");
                remuslug.Content = "Ремонтные услуgи: " + _garage.Remrabot;
                vidTS.Content = "Вид транспорта: " + Connection.technique.FirstOrDefault(x => x.Id_technique == _garage.VidTS).Name_technique;
            }
        }

        private void Click_redact(object sender, RoutedEventArgs e) => MainWindow.main.Animation_move(MainWindow.main.scroll_main, MainWindow.main.frame_main, MainWindow.main.frame_main, new Pages.PagesInTable.Garage(garage));

        private void Click_remove(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить информацию о гараже?", "Удаление информации", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Pages.Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.Garage);
                    string query = $"Delete From Garage Where Id_garage = " + garage.Id_garage.ToString() + "";
                    var query_apply = Pages.Login_Regin.Login.connection.Query(query);
                    if (query_apply != null)
                    {
                        Pages.Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.Garage);
                        MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Pages.Main.page_main.Garage);
                    }
                    else MessageBox.Show("Запрос на удаление гаража не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
