using ClassConnection;
using ClassModules;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

namespace KPKochetov.Pages.PagesInTable
{
    /// <summary>
    /// Логика взаимодействия для Garage.xaml
    /// </summary>
    public partial class Garage : Page
    {
        ClassModules.Garage parts;
        public Garage(ClassModules.Garage _garage)
        {
            InitializeComponent();
            parts = _garage;
            foreach (var item in Connection.technique)
            {
                ComboBoxItem cb_locations = new ComboBoxItem();
                cb_locations.Tag = item.Id_technique;
                cb_locations.Content = "Вид техники: " + item.Name_technique;
                Locations.Text = _garage.Locations;
                Vmestim.Text = _garage.Vmestim.ToString();
                Remrabot.Text = _garage.Remrabot;
                cb_locations.IsSelected = true;

                VidTS.Items.Add(cb_locations);
            }
        }

        private void Click_Garage_Redact(object sender, RoutedEventArgs e)
        {
            if (VidTS.SelectedItem != null)
            {
                ClassModules.Technique Id_сeh_temp;
                Id_сeh_temp = ClassConnection.Connection.technique.Find(x => x.Id_technique == Convert.ToInt32(((ComboBoxItem)VidTS.SelectedItem).Tag));
                int id = Login_Regin.Login.connection.SetLastId(ClassConnection.Connection.Tables.Garage);
                if (parts.Vmestim == 0)
                {
                    string query = $"Insert Into Garage ([Id_garage], [Locations], [Vmestim], [VidTS], [Remrabot], [Date_of_foundation])" +
                        $"Values ({id.ToString()}, '{Locations.Text}',{Vmestim.Text} ,{Id_сeh_temp.Id_technique.ToString()},'{Remrabot.Text}' ,'{DateTime.Now.ToString("yyyy-MM-dd")}')";
                    var query_apply = Login_Regin.Login.connection.Query(query);
                    if (query_apply != null)
                    {
                        Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.Garage);
                        MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.Garage);
                    }
                    else MessageBox.Show("Запрос на добавление гаража не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    string query = $"Update Garage Set Locations = '{Locations.Text}', Vmestim = '{Vmestim.Text}', VidTS = '{Id_сeh_temp.Id_technique.ToString()}', Remrabot='{Remrabot.Text}' Where Id_garage = {parts.Id_garage}";
                    var query_apply = Login_Regin.Login.connection.Query(query);
                    if (query_apply != null)
                    {
                        Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.Garage);
                        MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.Garage);
                    }
                    else MessageBox.Show("Запрос на изменение гаража не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }


        private void Click_Cancel_Garage_Redact(object sender, RoutedEventArgs e) => MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main);

        private void Click_Remove_Garage_Redact(object sender, RoutedEventArgs e)
        {
            try
            {
                Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.Garage);
                string query = "Delete Garage Where [Id_garage] = " + parts.Id_garage.ToString() + "";
                var query_apply = Login_Regin.Login.connection.Query(query);
                if (query_apply != null)
                {
                    Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.Garage);
                    Main.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.Garage);
                }
                else MessageBox.Show("Запрос на удаление гаража не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
