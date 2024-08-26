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
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Win32;

namespace OPBD
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string con;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //string pathExcel = @"C:\Program Files\Microsoft Office 15\root\office15\EXCEL.EXE";
            //string pathWord = @"C:\Program Files\Microsoft Office 15\root\office15\WINWORD.EXE";
            string pathMSSQL = @"C:\Program Files (x86)\Microsoft SQL Server Management Studio 18\Common7\IDE\Ssms.exe";

            /*
            if (!File.Exists(pathExcel))
            {
                if (MessageBox.Show("Программа Excel отсутствует по стандартному пути. Продолжить выполение программы?", "ВНИМАНИЕ", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    this.Close();
                }
            }
            */
            if (!File.Exists(pathMSSQL))
            {
                if (MessageBox.Show("Программа Microsoft SQL Server Management Studio 18 отсутствует по стандартному пути. Продолжить выполение программы?", "ВНИМАНИЕ", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    this.Close();
                }
            }
            /*
            if (!File.Exists(pathWord))
            {
                if (MessageBox.Show("Программа Word отсутствует по стандартному пути. Продолжить выполение программы?", "ВНИМАНИЕ", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    this.Close();
                }
            }
            */
            RegistryView regedit = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;
            using (RegistryKey Key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, regedit))
            {
                RegistryKey instanceKey = Key.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL", false);
                if (instanceKey != null)
                {
                    foreach (var instanceName in instanceKey.GetValueNames())
                    {
                        cbServer.Items.Add(Environment.MachineName + @"\" + instanceName);
                    }
                }
            }
            cbServer.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SQLClass @class = new SQLClass();
            @class.connection = new SqlConnection(con);
            @class.SQLExecute($"Select [ID_Employee],[Password_Employee] from [dbo].[Employee] where [Login_Employee] = '{LoginBox.Text}'", SQLClass.act.select);
            if (@class.table.Rows.Count > 0)
            {
                DataRow dataRow = @class.table.Rows[0];
                if (dataRow[1].ToString() == PassBox.Password)
                {
                    MainMenuEmployee menuEmployee = new MainMenuEmployee();
                    menuEmployee.Current_Employee_ID = int.Parse(dataRow[0].ToString());
                    menuEmployee.Show();
                    this.Close();
                }
            }
        }

        private void cbServer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbDatabase.Items.Clear();
            SqlConnection connection = new SqlConnection($"Data Source = {cbServer.Items[cbServer.SelectedIndex].ToString()}; Initial Catalog = Ministry_Of_Internal_Affairs; Integrated Security = True;");
            SqlCommand cmd = new SqlCommand("select name from sys.databases", connection);
            DataTable table = new DataTable();
            try
            {
                connection.Open();
                table.Load(cmd.ExecuteReader());
                foreach (DataRow row in table.Rows)
                {
                    cbDatabase.Items.Add(row[0]);
                }
            }
            catch (Exception message)
            {
                MessageBox.Show(message.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void cbDatabase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            con = $"Data Source = {(string)cbServer.SelectedItem}; Initial Catalog = {(string)cbDatabase.SelectedItem}; Integrated Security = true";
        }
    }
}


