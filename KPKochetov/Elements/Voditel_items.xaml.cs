using ClassConnection;
using ClassModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
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
using KPKochetov.Pages;
using KPKochetov.Pages.PagesInTable;

namespace KPKochetov.Elements
{
    /// <summary>
    /// Логика взаимодействия для Voditel_items.xaml
    /// </summary>
    public partial class Voditel_items : UserControl
    {
        Connection connection;
        ClassModules.Voditel Voditel;
        public Voditel_items(ClassModules.Voditel _Voditel)
        {
            InitializeComponent();
            connection = new ClassConnection.Connection();
            if (Pages.Login_Regin.Login.UserInfo[1] != "admin") Buttons.Visibility = Visibility.Hidden;
            Voditel = _Voditel;
            if (_Voditel.Prava != null)
            {
                Id_voditel.Content = "Водитель №" + _Voditel.Id_voditel.ToString();
                Name_voditel.Content = "Имя водителя: " + _Voditel.Name_voditel;
                Prava.Content = "Права: " + _Voditel.Prava;
                Date_foundation.Content = "Дата создания: " + _Voditel.Date_foundation.ToString("dd.MM.yyyy");
                Date_update_information.Content = "Дата обновления информации: " + _Voditel.Date_update_information.ToString("dd.MM.yyyy HH:mm:ss");
            }
            DoubleAnimation opgridAnimation = new DoubleAnimation();
            opgridAnimation.From = 0;
            opgridAnimation.To = 1;
            opgridAnimation.Duration = TimeSpan.FromSeconds(0.4);
            border.BeginAnimation(StackPanel.OpacityProperty, opgridAnimation);
        }

        private void Click_redact(object sender, RoutedEventArgs e) => MainWindow.main.Animation_move(MainWindow.main.scroll_main, MainWindow.main.frame_main, MainWindow.main.frame_main, new Pages.PagesInTable.Voditel(Voditel));

        private void Click_remove(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить информацию о водителе?", "Удаление информации", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Pages.Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.voditel);
                    string query = $"Delete From Voditel Where Id_voditel = " + Voditel.Id_voditel.ToString() + "";
                    var query_apply = Pages.Login_Regin.Login.connection.Query(query);
                    if (query_apply != null)
                    {
                        Pages.Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.voditel);
                        MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Pages.Main.page_main.Voditel);
                    }
                    else MessageBox.Show("Запрос на удаление водителя не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
