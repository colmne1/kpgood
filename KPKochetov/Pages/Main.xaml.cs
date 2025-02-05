using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using KPKochetov.Pages.Login_Regin;
using ClassModules;
using ClassConnection;
using KPKochetov.Elements;

namespace KPKochetov.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public enum page_main
        {
            Voditel, ceh, Garage, technique, zapchast, none
        }

        public static page_main page_select;
        public static Main main;

        public Main()
        {
            InitializeComponent();
            main = this;
            page_select = page_main.none;
        }

        public void CreateConnect(bool connectApply)
        {
            if (connectApply == true)
            {
                Login.connection.LoadData(Connection.Tables.voditel);
                Login.connection.LoadData(Connection.Tables.сeh);
                Login.connection.LoadData(Connection.Tables.Garage);
                Login.connection.LoadData(Connection.Tables.technique);
                Login.connection.LoadData(Connection.Tables.zapchast);
            }
        }

        public void RoleUser()
        {
            if (Login.UserInfo[1] == "admin") WhoAmI.Content = $"Здравствуйте, {Login.UserInfo[0]}! Роль - {Login.UserInfo[1]}";
            else WhoAmI.Content = $"Здравствуйте, {Login.UserInfo[0]}! Роль - {Login.UserInfo[1]}";
        }

        public void OpenPageLogin()
        {
            DoubleAnimation opgridAnimation = new DoubleAnimation();
            opgridAnimation.From = 1;
            opgridAnimation.To = 0;
            opgridAnimation.Duration = TimeSpan.FromSeconds(0.6);
            opgridAnimation.Completed += delegate
            {
                MainWindow.init.frame.Navigate(new Login());
                DoubleAnimation opgrisdAnimation = new DoubleAnimation();
                opgrisdAnimation.From = 0;
                opgrisdAnimation.To = 1;
                opgrisdAnimation.Duration = TimeSpan.FromSeconds(1.2);
                MainWindow.init.frame.BeginAnimation(Frame.OpacityProperty, opgrisdAnimation);
            };
            MainWindow.init.frame.BeginAnimation(Frame.OpacityProperty, opgridAnimation);
        }

        private void LoadGarages()
        {
            Dispatcher.InvokeAsync(async () =>
            {
                foreach (ClassModules.Garage garage_items in ClassConnection.Connection.garage)
                {
                    if (page_select == page_main.Garage)
                    {
                        parrent.Children.Add(new Elements.Garage_items(garage_items));
                        await Task.Delay(90);
                    }
                }
                if (page_select == page_main.Garage)
                {
                    if (Login.UserInfo[1] == "admin")
                    {
                        var add = new Pages.PagesInTable.Garage(new ClassModules.Garage());
                        parrent.Children.Add(new Elements.Add(add));
                    }
                }
            });
        }

        private void Click_Garage(object sender, RoutedEventArgs e)
        {
            Search.IsEnabled = true;
            garage.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF"));
            ceh_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            Voditel_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            technique_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            zapchast_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));

            if (frame_main.Visibility == Visibility.Visible) MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main);
            if (page_select != page_main.Garage)
            {
                page_select = page_main.Garage;
                DoubleAnimation opgridAnimation = new DoubleAnimation();
                opgridAnimation.From = 1;
                opgridAnimation.To = 0;
                opgridAnimation.Duration = TimeSpan.FromSeconds(0.2);
                opgridAnimation.Completed += delegate
                {
                    parrent.Children.Clear();
                    DoubleAnimation opgriAnimation = new DoubleAnimation();
                    opgriAnimation.From = 0;
                    opgriAnimation.To = 1;
                    opgriAnimation.Duration = TimeSpan.FromSeconds(0.2);
                    opgriAnimation.Completed += delegate
                    {
                        LoadGarages();
                    };
                    parrent.BeginAnimation(StackPanel.OpacityProperty, opgriAnimation);
                };
                parrent.BeginAnimation(StackPanel.OpacityProperty, opgridAnimation);
            }
        }

        private void LoadCeh()
        {
            Dispatcher.InvokeAsync(async () =>
            {
                foreach (ClassModules.Ceh ceh_items in ClassConnection.Connection.ceh)
                {
                    if (page_select == page_main.ceh)
                    {
                        parrent.Children.Add(new Elements.Ceh_items(ceh_items));
                        await Task.Delay(90);
                    }
                }
                if (page_select == page_main.ceh)
                {
                    if (Login.UserInfo[1] == "admin")
                    {
                        var add = new Pages.PagesInTable.Ceh(new ClassModules.Ceh());
                        parrent.Children.Add(new Elements.Add(add));
                    }
                }
            });
        }

        private void Click_Ceh(object sender, RoutedEventArgs e)
        {
            Search.IsEnabled = true;
            garage.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            ceh_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF"));
            Voditel_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            technique_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            zapchast_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            if (frame_main.Visibility == Visibility.Visible) MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main);
            if (page_select != page_main.ceh)
            {
                page_select = page_main.ceh;
                DoubleAnimation opgridAnimation = new DoubleAnimation();
                opgridAnimation.From = 1;
                opgridAnimation.To = 0;
                opgridAnimation.Duration = TimeSpan.FromSeconds(0.2);
                opgridAnimation.Completed += delegate
                {
                    parrent.Children.Clear();
                    DoubleAnimation opgriAnimation = new DoubleAnimation();
                    opgriAnimation.From = 0;
                    opgriAnimation.To = 1;
                    opgriAnimation.Duration = TimeSpan.FromSeconds(0.2);
                    opgriAnimation.Completed += delegate
                    {
                        LoadCeh();
                    };
                    parrent.BeginAnimation(StackPanel.OpacityProperty, opgriAnimation);
                };
                parrent.BeginAnimation(StackPanel.OpacityProperty, opgridAnimation);
            }
        }

        private void LoadVoditel()
        {
            Dispatcher.InvokeAsync(async () =>
            {
                foreach (ClassModules.Voditel Voditel_items in ClassConnection.Connection.voditel)
                {
                    if (page_select == page_main.Voditel)
                    {
                        parrent.Children.Add(new Elements.Voditel_items(Voditel_items));
                        await Task.Delay(90);
                    }
                }
                if (page_select == page_main.Voditel)
                {
                    if (Login.UserInfo[1] == "admin")
                    {
                        var add = new Pages.PagesInTable.Voditel(new ClassModules.Voditel());
                        parrent.Children.Add(new Elements.Add(add));
                    }
                }
            });
        }

        private void Click_Voditel(object sender, RoutedEventArgs e)
        {
            Search.IsEnabled = true;
            garage.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            ceh_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            Voditel_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF"));
            technique_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            zapchast_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));

            if (frame_main.Visibility == Visibility.Visible) MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main);
            if (page_select != page_main.Voditel)
            {
                page_select = page_main.Voditel;
                DoubleAnimation opgridAnimation = new DoubleAnimation();
                opgridAnimation.From = 1;
                opgridAnimation.To = 0;
                opgridAnimation.Duration = TimeSpan.FromSeconds(0.2);
                opgridAnimation.Completed += delegate
                {
                    parrent.Children.Clear();
                    DoubleAnimation opgriAnimation = new DoubleAnimation();
                    opgriAnimation.From = 0;
                    opgriAnimation.To = 1;
                    opgriAnimation.Duration = TimeSpan.FromSeconds(0.2);
                    opgriAnimation.Completed += delegate
                    {
                        LoadVoditel();
                    };
                    parrent.BeginAnimation(StackPanel.OpacityProperty, opgriAnimation);
                };
                parrent.BeginAnimation(StackPanel.OpacityProperty, opgridAnimation);
            }
        }

        private void LoadTechnique()
        {
            Dispatcher.InvokeAsync(async () =>
            {
                foreach (ClassModules.Technique technique_items in ClassConnection.Connection.technique)
                {
                    if (page_select == page_main.technique)
                    {
                        parrent.Children.Add(new Elements.Technique_items(technique_items));
                        await Task.Delay(90);
                    }
                }
                if (page_select == page_main.technique)
                {
                    if (Login.UserInfo[1] == "admin")
                    {
                        var add = new Pages.PagesInTable.Technique(new ClassModules.Technique());
                        parrent.Children.Add(new Elements.Add(add));
                    }
                }
            });
        }

        private void Click_Technique(object sender, RoutedEventArgs e)
        {
            Search.IsEnabled = true;
            garage.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            ceh_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            Voditel_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            technique_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF"));
            zapchast_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));

            if (frame_main.Visibility == Visibility.Visible) MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main);
            if (page_select != page_main.technique)
            {
                page_select = page_main.technique;
                DoubleAnimation opgridAnimation = new DoubleAnimation();
                opgridAnimation.From = 1;
                opgridAnimation.To = 0;
                opgridAnimation.Duration = TimeSpan.FromSeconds(0.2);
                opgridAnimation.Completed += delegate
                {
                    parrent.Children.Clear();
                    DoubleAnimation opgriAnimation = new DoubleAnimation();
                    opgriAnimation.From = 0;
                    opgriAnimation.To = 1;
                    opgriAnimation.Duration = TimeSpan.FromSeconds(0.2);
                    opgriAnimation.Completed += delegate
                    {
                        LoadTechnique();
                    };
                    parrent.BeginAnimation(StackPanel.OpacityProperty, opgriAnimation);
                };
                parrent.BeginAnimation(StackPanel.OpacityProperty, opgridAnimation);
            }
        }

        private void LoadZapchast()
        {
            Dispatcher.InvokeAsync(async () =>
            {
                foreach (ClassModules.Zapchast zapchast_items in ClassConnection.Connection.zapchast)
                {
                    if (page_select == page_main.zapchast)
                    {
                        parrent.Children.Add(new Elements.Zapchast_items(zapchast_items));
                        await Task.Delay(90);
                    }
                }
                if (page_select == page_main.zapchast)
                {
                    if (Login.UserInfo[1] == "admin")
                    {
                        var add = new Pages.PagesInTable.Zapchast(new ClassModules.Zapchast());
                        parrent.Children.Add(new Elements.Add(add));
                    }
                }
            });
        }

        private void Click_Zapchast(object sender, RoutedEventArgs e)
        {
            Search.IsEnabled = true;
            garage.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            ceh_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            Voditel_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            technique_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            zapchast_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF"));
            if (frame_main.Visibility == Visibility.Visible) MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main);
            if (page_select != page_main.zapchast)
            {
                page_select = page_main.zapchast;
                DoubleAnimation opgridAnimation = new DoubleAnimation();
                opgridAnimation.From = 1;
                opgridAnimation.To = 0;
                opgridAnimation.Duration = TimeSpan.FromSeconds(0.2);
                opgridAnimation.Completed += delegate
                {
                    parrent.Children.Clear();
                    DoubleAnimation opgriAnimation = new DoubleAnimation();
                    opgriAnimation.From = 0;
                    opgriAnimation.To = 1;
                    opgriAnimation.Duration = TimeSpan.FromSeconds(0.2);
                    opgriAnimation.Completed += delegate
                    {
                        LoadZapchast();
                    };
                    parrent.BeginAnimation(StackPanel.OpacityProperty, opgriAnimation);
                };
                parrent.BeginAnimation(StackPanel.OpacityProperty, opgridAnimation);
            }
        }





        private void Click_Export(object sender, MouseButtonEventArgs e)
        {
            Search.IsEnabled = false;
            garage.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            ceh_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            Voditel_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            technique_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            zapchast_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            parrent.Children.Clear();
            page_select = page_main.none;
            var export = new ExportWindow();
            export.ShowDialog();
        }

        private bool isDataLoaded = false;

        private async void SearchTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Search.Text) && Search.Text != "Поиск")
            {
                await Task.Delay(100);
                if (page_select == page_main.Garage)
                {
                    parrent.Children.Clear();
                    var parts = Connection.garage.FindAll(x => x.Id_garage.ToString() == Search.Text);
                    foreach (var itemSearch in parts) parrent.Children.Add(new Elements.Garage_items(itemSearch));
                }
                else if (page_select == page_main.ceh)
                {
                    parrent.Children.Clear();
                    var country = Connection.ceh.FindAll(x => x.oborud.Contains(Search.Text));
                    var countryIds = country.Select(c => c.Id_сeh).ToList();
                    var locationsByCountry = Connection.ceh.Where(l => countryIds.Contains(l.Id_сeh)).ToList();
                    foreach (var itemSearch in locationsByCountry) parrent.Children.Add(new Elements.Ceh_items(itemSearch));
                }
                else if (page_select == page_main.Voditel)
                {
                    parrent.Children.Clear();
                    var VoditelById = Connection.voditel.FindAll(x => x.Id_voditel.ToString().Contains(Search.Text));
                    foreach (var itemSearch in VoditelById) parrent.Children.Add(new Elements.Voditel_items(itemSearch));
                }
                else if (page_select == page_main.technique)
                {
                    parrent.Children.Clear();
                    var techniqueByName = Connection.technique.FindAll(x => x.Name_technique.Contains(Search.Text));
                    foreach (var itemSearch in techniqueByName) parrent.Children.Add(new Elements.Technique_items(itemSearch));
                }
                else if (page_select == page_main.zapchast)
                {
                    parrent.Children.Clear();
                    var typeOfTroopByName = Connection.zapchast.FindAll(x => x.Name_zapchast.Contains(Search.Text));
                    foreach (var itemSearch in typeOfTroopByName) parrent.Children.Add(new Elements.Zapchast_items(itemSearch));
                }

            }
            else
            {
                await Task.Delay(100);
                if (string.IsNullOrWhiteSpace(Search.Text))
                {
                    parrent.Children.Clear();
                    return;
                }
                if (!isDataLoaded || Search.Text == "Поиск")
                {
                    if (page_select == page_main.Garage)
                    {
                        if (parrent != null) parrent.Children.Clear();
                        LoadGarages();
                    }
                    else if (page_select == page_main.ceh)
                    {
                        if (parrent != null) parrent.Children.Clear();
                        LoadCeh();
                    }
                    else if (page_select == page_main.Voditel)
                    {
                        if (parrent != null) parrent.Children.Clear();
                        LoadVoditel();
                    }
                    else if (page_select == page_main.technique)
                    {
                        if (parrent != null) parrent.Children.Clear();
                        LoadTechnique();
                    }
                    else if (page_select == page_main.zapchast)
                    {
                        if (parrent != null) parrent.Children.Clear();
                        LoadZapchast();
                    }

                    isDataLoaded = true;
                }
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e) => Search.Text = "Поиск";

        private void TextBox_GotFocus(object sender, RoutedEventArgs e) => Search.Text = "";

        private void Click_Back(object sender, RoutedEventArgs e)
        {
            Search.IsEnabled = false;
            garage.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            ceh_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            Voditel_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            technique_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            zapchast_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            parrent.Children.Clear();
            page_select = page_main.none;
            Login.UserInfo[0] = ""; Login.UserInfo[1] = "";
            OpenPageLogin();
        }

        public void Animation_move(Control control1, Control control2, Frame frame_main = null, Page pages = null, page_main page_restart = page_main.none)
        {
            if (page_restart != page_main.none)
            {
                if (page_restart == page_main.Garage)
                {
                    page_select = page_main.none;
                    Click_Garage(new object(), new RoutedEventArgs());
                }
                else if (page_restart == page_main.ceh)
                {
                    page_select = page_main.none;
                    Click_Ceh(new object(), new RoutedEventArgs());
                }
                else if (page_restart == page_main.Voditel)
                {
                    page_select = page_main.none;
                    Click_Voditel(new object(), new RoutedEventArgs());
                }
                else if (page_restart == page_main.technique)
                {
                    page_select = page_main.none;
                    Click_Technique(new object(), new RoutedEventArgs());
                }
                else if (page_restart == page_main.zapchast)
                {
                    page_select = page_main.none;
                    Click_Zapchast(new object(), new RoutedEventArgs());
                }

            }
            else
            {
                DoubleAnimation opgridAnimation = new DoubleAnimation();
                opgridAnimation.From = 1;
                opgridAnimation.To = 0;
                opgridAnimation.Duration = TimeSpan.FromSeconds(0.3);
                opgridAnimation.Completed += delegate
                {
                    if (pages != null)
                    {
                        frame_main.Navigate(pages);
                    }
                    control1.Visibility = Visibility.Hidden;
                    control2.Visibility = Visibility.Visible;
                    DoubleAnimation opgriAnimation = new DoubleAnimation();
                    opgriAnimation.From = 0;
                    opgriAnimation.To = 1;
                    opgriAnimation.Duration = TimeSpan.FromSeconds(0.4);
                    control2.BeginAnimation(ScrollViewer.OpacityProperty, opgriAnimation);
                };
                control1.BeginAnimation(ScrollViewer.OpacityProperty, opgridAnimation);
            }
        }

    }
}