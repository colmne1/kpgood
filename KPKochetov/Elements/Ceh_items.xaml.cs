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
    /// Логика взаимодействия для Ceh.xaml
    /// </summary>
    public partial class Ceh_items : UserControl
    {
        ClassModules.Ceh Ceh;
        public Ceh_items(ClassModules.Ceh _Ceh)
        {
            InitializeComponent();
            if (Pages.Login_Regin.Login.UserInfo[1] != "admin") Buttons.Visibility = Visibility.Hidden;
            Ceh = _Ceh;
#pragma warning disable
            if (_Ceh.remuslug != null)
            {
                ceh_name.Content = "Цех №" + _Ceh.Id_сeh.ToString();
                oborud.Content = "Оборудование: " + _Ceh.oborud;
                Address.Content = "Адрес: " + _Ceh.Address;
                remuslug.Content = "Ремонтная услуга : " + _Ceh.remuslug;
            }
        }
        private void Click_redact(object sender, RoutedEventArgs e) => MainWindow.main.Animation_move(MainWindow.main.scroll_main, MainWindow.main.frame_main, MainWindow.main.frame_main, new Pages.PagesInTable.Ceh(Ceh));

        private void Click_remove(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить информацию о цехе?", "Удаление информации", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Pages.Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.сeh);
                    string query = $"Delete From сeh Where Id_сeh = " + Ceh.Id_сeh.ToString() + "";
                    var query_apply = Pages.Login_Regin.Login.connection.Query(query);
                    if (query_apply != null)
                    {
                        Pages.Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.сeh);
                        MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Pages.Main.page_main.ceh);
                    }
                    else MessageBox.Show("Запрос на удаление цеха не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
