using ClassModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.ComponentModel;
using System.IO;
using OfficeOpenXml;
using Microsoft.Win32;
using System.Windows.Forms;
using Google.Protobuf.Collections;
namespace ClassConnection
{
    public class Connection
    {
        public static bool ConnectIsTrue = false;
        public static string Path_connection;

        public static List<Voditel> voditel = new List<Voditel>();
        public static List<Ceh> ceh = new List<Ceh>();
        public static List<Garage> garage = new List<Garage>();
        public static List<Technique> technique = new List<Technique>();
        public static List<Zapchast> zapchast = new List<Zapchast>();
        public static List<Users> users = new List<Users>();
        public enum Tables
        {
            voditel, сeh, Garage, technique, zapchast, users
        }

        public static void Connect()
        {
            try
            {
                string Path = $@"Server=DESKTOP-JPS19OC\SQLEXPRESS;Database=kpkochetov1;Trusted_Connection=True;User Id=sa;Password=root";
                SqlConnection connection = new SqlConnection(Path);
                connection.Open();
                ConnectIsTrue = true;
                Path_connection = Path;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ConnectIsTrue = false;
            }
        }

        public SqlDataReader Query(string query)
        {
            try
            {
                SqlConnection connect = new SqlConnection(Path_connection);
                connect.Open();
                SqlCommand command = new SqlCommand(query, connect);
                SqlDataReader reader = command.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public int SetLastId(Tables tables)
        {
            try
            {
                LoadData(tables);
                switch (tables.ToString())
                {
                    case "voditel":
                        if (voditel.Count >= 1)
                        {
                            int max_status = voditel[0].Id_voditel;
                            max_status = voditel.Max(x => x.Id_voditel);
                            return max_status + 1;
                        }
                        else return 1;
                    case "сeh":
                        if (ceh.Count >= 1)
                        {
                            int max_status = ceh[0].Id_сeh;
                            max_status = ceh.Max(x => x.Id_сeh);
                            return max_status + 1;
                        }
                        else return 1;
                    case "Garage":
                        if (garage.Count >= 1)
                        {
                            int max_status = garage[0].Id_garage;
                            max_status = garage.Max(x => x.Id_garage);
                            return max_status + 1;
                        }
                        else return 1;
                    case "technique":
                        if (technique.Count >= 1)
                        {
                            int max_status = technique[0].Id_technique;
                            max_status = technique.Max(x => x.Id_technique);
                            return max_status + 1;
                        }
                        else return 1;
                    case "zapchast":
                        if (zapchast.Count >= 1)
                        {
                            int max_status = zapchast[0].Id_zapchast;
                            max_status = zapchast.Max(x => x.Id_zapchast);
                            return max_status + 1;
                        }
                        else return 1;

                }
                return -1;
            }
            catch
            {
                return -1;
            }
        }

        public void LoadData(Tables tables)
        {
            try
            {
                if (tables.ToString() == "voditel")
                {
                    SqlDataReader itemsVoditel = Query("Select * From " + tables.ToString() + " Order By [Id_voditel]");
                    voditel.Clear();
                    while (itemsVoditel.Read())
                    {
                        Voditel newVoditel = new Voditel
                        {
                            Id_voditel = Convert.ToInt32(itemsVoditel.GetValue(0)),
                            Name_voditel = Convert.ToString(itemsVoditel.GetValue(1)),
                            Prava = Convert.ToString(itemsVoditel.GetValue(2)),
                            Date_foundation = Convert.ToDateTime(itemsVoditel.GetValue(3)),
                            Date_update_information = Convert.ToDateTime(itemsVoditel.GetValue(4))
                        };
                        voditel.Add(newVoditel);
                    }
                    itemsVoditel.Close();
                }

                if (tables.ToString() == "сeh")
                {
                    SqlDataReader itemsCeh = Query("Select * From " + tables.ToString() + " Order By [Id_сeh]");
                    ceh.Clear();
                    while (itemsCeh.Read())
                    {
                        Ceh newCeh = new Ceh
                        {
                            Id_сeh = Convert.ToInt32(itemsCeh.GetValue(0)),
                            oborud = Convert.ToString(itemsCeh.GetValue(1)),
                            Address = Convert.ToString(itemsCeh.GetValue(2)),
                            remuslug = Convert.ToString(itemsCeh.GetValue(3))
                        };
                        ceh.Add(newCeh);
                    }
                    itemsCeh.Close();
                }
                if (tables.ToString() == "Garage")
                {
                    SqlDataReader itemsGarage = Query("Select * From " + tables.ToString() + " Order By [Id_garage]");
                    garage.Clear();
                    while (itemsGarage.Read())
                    {
                        Garage newGarage = new Garage
                        {
                            Id_garage = Convert.ToInt32(itemsGarage.GetValue(0)),
                            Locations = Convert.ToString(itemsGarage.GetValue(1)),
                            Vmestim = Convert.ToInt32(itemsGarage.GetValue(2)),
                            VidTS = Convert.ToInt32((int)itemsGarage.GetValue(3)),
                            Remrabot = Convert.ToString(itemsGarage.GetValue(4)),
                            Date_of_foundation = Convert.ToDateTime(itemsGarage.GetValue(5))
                        };
                        garage.Add(newGarage);
                    }
                    itemsGarage.Close();
                }
                if (tables.ToString() == "technique")
                {
                    SqlDataReader itemsTechnique = Query("Select * From " + tables.ToString() + " Order By [Id_technique]");
                    technique.Clear();
                    while (itemsTechnique.Read())
                    {
                        Technique newTechnique = new Technique
                        {
                            Id_technique = Convert.ToInt32(itemsTechnique.GetValue(0)),
                            Name_technique = Convert.ToString(itemsTechnique.GetValue(1)),
                            God_vipuska = Convert.ToInt32(itemsTechnique.GetValue(2)),
                            Characteristics = Convert.ToString(itemsTechnique.GetValue(3)),
                            voditel = Convert.ToInt32(itemsTechnique.GetValue(4))
                        };
                        technique.Add(newTechnique);
                    }
                    itemsTechnique.Close();
                }
                if (tables.ToString() == "zapchast")
                {
                    SqlDataReader itemsZapchast = Query("Select * From " + tables.ToString() + " Order By [Id_zapchast]");
                    zapchast.Clear();
                    while (itemsZapchast.Read())
                    {
                        Zapchast newZapchast = new Zapchast
                        {
                            Id_zapchast = Convert.ToInt32(itemsZapchast.GetValue(0)),
                            Name_zapchast = Convert.ToString(itemsZapchast.GetValue(1)),
                            Description = Convert.ToString(itemsZapchast.GetValue(2)),
                            Date_foundation = Convert.ToDateTime(itemsZapchast.GetValue(3))
                        };
                        zapchast.Add(newZapchast);
                    }
                    itemsZapchast.Close();
                }

                if (tables.ToString() == "users")
                {
                    SqlDataReader itemsUsers = Query("Select * From " + tables.ToString() + " Order By [Id]");
                    users.Clear();
                    while (itemsUsers.Read())
                    {
                        Users newUsers = new Users
                        {
                            Id = Convert.ToInt32(itemsUsers.GetValue(0)),
                            Login = Convert.ToString(itemsUsers.GetValue(1)),
                            Password = Convert.ToString(itemsUsers.GetValue(2)),
                            Role = Convert.ToString(itemsUsers.GetValue(3))
                        };
                        users.Add(newUsers);
                    }
                    itemsUsers.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void Export(string[] nameTable, string filePath)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage())
            {
                if (nameTable[0] == "Garage")
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Записи (гаража)");
                    worksheet.Cells[1, 1].Value = "Код гаража";
                    worksheet.Cells[1, 2].Value = "Место дислокации";
                    worksheet.Cells[1, 3].Value = "Вместимость";
                    worksheet.Cells[1, 4].Value = "Вид Т.С.";
                    worksheet.Cells[1, 5].Value = "Ремонтные работы";
                    worksheet.Cells[1, 6].Value = "Дата создания";
                    int row = 2;
                    foreach (var record in garage)
                    {
                        worksheet.Cells[row, 1].Value = record.Id_garage;
                        worksheet.Cells[row, 2].Value = record.Locations;
                        worksheet.Cells[row, 3].Value = record.Vmestim;
                        worksheet.Cells[row, 4].Value = technique.First(x => x.Id_technique == record.VidTS).Id_technique;
                        worksheet.Cells[row, 5].Value = record.Remrabot;
                        worksheet.Cells[row, 6].Value = record.Date_of_foundation.ToString("dd.MM.yyyy");
                        row++;
                    }
                }
                if (nameTable[1] == "ceh")
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Записи (цеха)");
                    worksheet.Cells[1, 1].Value = "Код цеха";
                    worksheet.Cells[1, 2].Value = "Оборудование";
                    worksheet.Cells[1, 3].Value = "Адрес";
                    worksheet.Cells[1, 4].Value = "Ремонтная услуга";
                    int row = 2;
                    foreach (var record in ceh)
                    {
                        worksheet.Cells[row, 1].Value = record.Id_сeh;
                        worksheet.Cells[row, 2].Value = record.oborud;
                        worksheet.Cells[row, 3].Value = record.Address;
                        worksheet.Cells[row, 4].Value = record.remuslug;
                        row++;
                    }
                }
                if (nameTable[2] == "voditel")
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Записи (водителей)");
                    worksheet.Cells[1, 1].Value = "Код водителя";
                    worksheet.Cells[1, 2].Value = "ФИО водителя";
                    worksheet.Cells[1, 3].Value = "Права";
                    worksheet.Cells[1, 4].Value = "Дата создания";
                    worksheet.Cells[1, 5].Value = "Дата обновления информации";
                    int row = 2;
                    foreach (var record in voditel)
                    {
                        worksheet.Cells[row, 1].Value = record.Id_voditel;
                        worksheet.Cells[row, 2].Value = record.Name_voditel;
                        worksheet.Cells[row, 3].Value = record.Prava;
                        worksheet.Cells[row, 4].Value = record.Date_foundation.ToString("dd.MM.yyyy");
                        worksheet.Cells[row, 5].Value = record.Date_update_information.ToString("dd.MM.yyyy");
                        row++;
                    }
                }
                if (nameTable[3] == "technique")
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Записи (техника)");
                    worksheet.Cells[1, 1].Value = "Код техники";
                    worksheet.Cells[1, 2].Value = "Название техники";
                    worksheet.Cells[1, 3].Value = "Год выпуска";
                    worksheet.Cells[1, 4].Value = "Характеристики";
                    worksheet.Cells[1, 5].Value = "Водитель";
                    int row = 2;
                    foreach (var record in technique)
                    {
                        worksheet.Cells[row, 1].Value = record.Id_technique;
                        worksheet.Cells[row, 2].Value = record.Name_technique;
                        worksheet.Cells[row, 3].Value = record.God_vipuska;
                        worksheet.Cells[row, 4].Value = record.Characteristics;
                        worksheet.Cells[row, 5].Value = record.voditel;
                        row++;
                    }
                }
                if (nameTable[4] == "zapchast")
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Записи (запчасти)");
                    worksheet.Cells[1, 1].Value = "Код запчасти";
                    worksheet.Cells[1, 2].Value = "Название запчасти";
                    worksheet.Cells[1, 3].Value = "Описание";
                    worksheet.Cells[1, 4].Value = "Дата создания";
                    int row = 2;
                    foreach (var record in zapchast)
                    {
                        worksheet.Cells[row, 1].Value = record.Id_zapchast;
                        worksheet.Cells[row, 2].Value = record.Name_zapchast;
                        worksheet.Cells[row, 3].Value = record.Description;
                        worksheet.Cells[row, 4].Value = record.Date_foundation.ToString("dd.MM.yyyy");
                        row++;
                    }
                }

                FileInfo fileInfo = new FileInfo(filePath);
                package.SaveAs(fileInfo);
            }
        }
    }
}
