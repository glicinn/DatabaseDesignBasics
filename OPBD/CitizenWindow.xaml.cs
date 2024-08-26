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
    }
}
