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
    /// Логика взаимодействия для EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        public EmployeeWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SQLClass @class = new SQLClass();
            @class.SQLExecute("select [ID_Employee], [First_Name_Employee]+' '+[Name_Employee]+' '+[Middle_Name_Employee], [Login_Employee], [Password_Employee], [Position_ID], [Office_ID], [Rank_ID] from [dbo].[Employee]" +
                "inner join [dbo].[Position] on [Position_ID] = [ID_Position]" +
                "inner join [dbo].[Office] on [Office_ID] = [ID_Office]" +
                "inner join [dbo].[Rank] on [Rank_ID] = [ID_Rank]", SQLClass.act.select);
            Employee.ItemsSource = @class.table.DefaultView;
            Employee.Columns[0].Visibility = Visibility.Hidden;
            Employee.Columns[1].Header = "ФИО";
            Employee.Columns[2].Header = "Логин";
            Employee.Columns[3].Header = "Пароль";
            Employee.Columns[4].Header = "Код должности";
            Employee.Columns[5].Header = "Код ведомства";
            Employee.Columns[6].Header = "Код звания";
        }
    }
}
