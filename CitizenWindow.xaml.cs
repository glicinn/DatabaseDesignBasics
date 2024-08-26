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
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace OPBD
{
    /// <summary>
    /// Логика взаимодействия для CitizenWindow.xaml
    /// </summary>
    public partial class CitizenWindow : Window
    {
        public CitizenWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CitFill();
        }

        private void CitFill()
        {
            SQLClass @class = new SQLClass();
            @class.SQLExecute("select [ID_Citizen], [First_Name_Citizen]+' '+[Name_Citizen]+' '+[Middle_Name_Citizen], [Login_Citizen], [Password_Citizen], [Passport_Series_Citizen]+' '+[Passport_Number_Citizen], [Citizen_Number], [Citizen_Address], [Citizen_E_Mail] from [dbo].[Citizen]", SQLClass.act.select);
            Citizen.ItemsSource = @class.table.DefaultView;
            Citizen.Columns[0].Visibility = Visibility.Hidden;
            Citizen.Columns[1].Header = "ФИО";
            Citizen.Columns[2].Header = "Логин";
            Citizen.Columns[3].Header = "Пароль";
            Citizen.Columns[4].Header = "Данные паспорта";
            Citizen.Columns[5].Header = "Телефон";
            Citizen.Columns[6].Header = "Адрес";
            Citizen.Columns[7].Header = "Адрес почты";

        }

        private void Citizen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Citizen.Items.Count != 0 & Citizen.SelectedItems.Count != 0)
            {
                DataRowView dataRow = (DataRowView)Citizen.SelectedItems[0];
                FIO.Text = dataRow[1].ToString();
                Log.Text = dataRow[2].ToString();
                Pas.Text = dataRow[3].ToString();
                Passp.Text = dataRow[4].ToString();
                Tel.Text = dataRow[5].ToString();
                Adr.Text = dataRow[6].ToString();
                Mail.Text = dataRow[7].ToString();

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // Удаление
        {
            try
            {
                DataRowView row = (DataRowView)Citizen.SelectedItems[0];
                SQLClass @class = new SQLClass();
                @class.SQLExecute($"delete from [dbo].[Citizen] where [ID_Citizen] = {row[0]}", SQLClass.act.manipulation);
                CitFill();
            }
            catch { }
        }

        private void Button_Click(object sender, RoutedEventArgs e) //Изменение
        {
            try
            {
                DataRowView row = (DataRowView)Citizen.SelectedItems[0];
                if (FIO.Text != "" && Log.Text != "" && Pas.Text != "" && Passp.Text != "" && Tel.Text != "" && Adr.Text != "" && Mail.Text != "")
                {
                    string[] fio = FIO.Text.Split(' ');
                    string[] passp = Passp.Text.Split(' ');
                    SQLClass @class = new SQLClass();
                    @class.SQLExecute($"update [dbo].[Citizen] set [First_Name_Citizen] = '{fio[0]}', [Name_Citizen] = '{fio[1]}', [Middle_Name_Citizen] = '{fio[2]}', [Login_Citizen] = '{Log.Text}', [Password_Citizen] = '{Pas.Text}', [Passport_Series_Citizen] = '{passp[0]}', [Passport_Number_Citizen] = '{passp[1]}', [Citizen_Number] = '{Tel.Text}', [Citizen_Address] = '{Adr.Text}', [Citizen_E_Mail] = '{Mail.Text}'  where [ID_Citizen] = {row[0]}", SQLClass.act.manipulation);
                    CitFill();
                }
            }
            catch { }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //Добавление
        {
            if (FIO.Text != "" && Log.Text != "" && Pas.Text != "" && Passp.Text != "" && Tel.Text != "" && Adr.Text != "" && Mail.Text != "")
            {
                string[] fio = FIO.Text.Split(' ');
                string[] passp = Passp.Text.Split(' ');
                SQLClass @class = new SQLClass();
                @class.SQLExecute($"insert into [dbo].[Citizen] ([First_Name_Citizen], [Name_Citizen], [Middle_Name_Citizen], [Login_Citizen], [Password_Citizen], [Passport_Series_Citizen], [Passport_Number_Citizen], [Citizen_Number], [Citizen_Address], [Citizen_E_Mail]) values ('{fio[0]}', '{fio[1]}', '{fio[2]}', '{Log.Text}', '{Pas.Text}', '{passp[0]}', '{passp[1]}', '{Tel.Text}', '{Adr.Text}', '{Mail.Text}')", SQLClass.act.manipulation);
                CitFill();
            }
        }
    }
}
