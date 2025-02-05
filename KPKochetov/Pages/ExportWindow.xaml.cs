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
using System.Windows.Shapes;

namespace KPKochetov.Pages
{
    /// <summary>
    /// Логика взаимодействия для ExportWindow.xaml
    /// </summary>
    public partial class ExportWindow : Window
    {
        public static ExportWindow export;
        public ExportWindow()
        {
            InitializeComponent();
            export = this;
        }

        private void ExportAccept(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "Excel (*.xlsx)|*.xlsx";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.ShowDialog();
            string[] table = new string[6];
            if (saveFileDialog.FileName != "")
            {
                if (PartsExport.IsChecked == true) table[0] = "Garage";
                if (LocationsExport.IsChecked == true) table[1] = "ceh";
                if (VoditelExport.IsChecked == true) table[2] = "voditel";
                if (TechniqueExport.IsChecked == true) table[3] = "technique";
                if (TypeOfTroopsExport.IsChecked == true) table[4] = "zapchast";
            }
            ClassConnection.Connection.Export(table, saveFileDialog.FileName);
            System.Windows.MessageBox.Show($"Экспорт выполнен.\nФайл находится по пути: {saveFileDialog.FileName}.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
