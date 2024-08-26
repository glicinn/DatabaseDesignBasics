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
            EmpFill();
            KDFill();
            KVFill();
            KZFill();
        }

        private void EmpFill()
        {
            SQLClass @class = new SQLClass();
            @class.SQLExecute("select [ID_Employee], [First_Name_Employee]+' '+[Name_Employee]+' '+[Middle_Name_Employee], [Login_Employee], [Password_Employee], [Name_Position], [Name_Office], [Name_Rank] from [dbo].[Employee]" +
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

        private void Employee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Employee.Items.Count != 0 & Employee.SelectedItems.Count != 0)
            {
                DataRowView dataRow = (DataRowView)Employee.SelectedItems[0];
                FIO.Text = dataRow[1].ToString();
                Log.Text = dataRow[2].ToString();
                Pas.Text = dataRow[3].ToString();
                KD.Text = dataRow[4].ToString();
                KV.Text = dataRow[5].ToString();
                KZ.Text = dataRow[6].ToString();

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)// Удаление
        {
            try
            {
                DataRowView row = (DataRowView)Employee.SelectedItems[0];
                SQLClass @class = new SQLClass();
                @class.SQLExecute($"delete from [dbo].[Employee] where [ID_Employee] = {row[0]}", SQLClass.act.manipulation);
                EmpFill();
            }
            catch { }
        }

        private void Button_Click(object sender, RoutedEventArgs e)// Изменение
        {
            try
            {
                DataRowView row = (DataRowView)Employee.SelectedItems[0];
                if (FIO.Text != "" && Log.Text != "" && Pas.Text != "" && KD.Text != "" && KV.Text != "" && KZ.Text != "")
                {
                    string[] fio = FIO.Text.Split(' ');
                    SQLClass @class = new SQLClass();
                    @class.SQLExecute($"update [dbo].[Employee] set [First_Name_Employee] = '{fio[0]}', [Name_Employee] = '{fio[1]}', [Middle_Name_Employee] = '{fio[2]}', [Login_Employee] = '{Log.Text}', [Password_Employee] = '{Pas.Text}', [Position_ID] = {KD.SelectedValue}, [Office_ID] = {KV.SelectedValue}, [Rank_ID] = {KZ.SelectedValue}  where [ID_Employee] = {row[0]}", SQLClass.act.manipulation);
                    EmpFill();
                }
            }
            catch { }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)// Добавление
        {
            if (FIO.Text != "" && Log.Text != "" && Pas.Text != "" && KD.Text != "" && KV.Text != "" && KZ.Text != "")
            {
                string[] fio = FIO.Text.Split(' ');
                SQLClass @class = new SQLClass();
                @class.SQLExecute($"insert into [dbo].[Employee] ([First_Name_Employee], [Name_Employee], [Middle_Name_Employee], [Login_Employee], [Password_Employee], [Position_ID], [Office_ID], [Rank_ID]) values ('{fio[0]}', '{fio[1]}', '{fio[2]}', '{Log.Text}', '{Pas.Text}', {KD.SelectedValue}, {KV.SelectedValue}, {KZ.SelectedValue})", SQLClass.act.manipulation);
                EmpFill();
            }
        }

        private void KDFill()
        {
            Action action = () =>
            {
                DataBaseClass dataBaseClass = new DataBaseClass();
                dataBaseClass.sqlExecute("select [ID_Position], [Name_Position] from [dbo].[Position]", DataBaseClass.act.select);
                dataBaseClass.dependency.OnChange += Dependency_OnChange_KD;
                KD.ItemsSource = dataBaseClass.resultTable.DefaultView;
                KD.SelectedValuePath = dataBaseClass.resultTable.Columns[0].ColumnName;
                KD.DisplayMemberPath = dataBaseClass.resultTable.Columns[1].ColumnName;
            };
            Dispatcher.Invoke(action);
        }

        private void Dependency_OnChange_KD(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid) KDFill();
        }

        private void KVFill()
        {
            Action action = () =>
            {
                DataBaseClass dataBaseClass = new DataBaseClass();
                dataBaseClass.sqlExecute("select [ID_Office], [Name_Office] from [dbo].[Office]", DataBaseClass.act.select);
                dataBaseClass.dependency.OnChange += Dependency_OnChange_KV;
                KV.ItemsSource = dataBaseClass.resultTable.DefaultView;
                KV.SelectedValuePath = dataBaseClass.resultTable.Columns[0].ColumnName;
                KV.DisplayMemberPath = dataBaseClass.resultTable.Columns[1].ColumnName;
            };
            Dispatcher.Invoke(action);
        }

        private void Dependency_OnChange_KV(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid) KVFill();
        }

        private void KZFill()
        {
            Action action = () =>
            {
                DataBaseClass dataBaseClass = new DataBaseClass();
                dataBaseClass.sqlExecute("select [ID_Rank], [Name_Rank] from [dbo].[Rank]", DataBaseClass.act.select);
                dataBaseClass.dependency.OnChange += Dependency_OnChange_KZ;
                KZ.ItemsSource = dataBaseClass.resultTable.DefaultView;
                KZ.SelectedValuePath = dataBaseClass.resultTable.Columns[0].ColumnName;
                KZ.DisplayMemberPath = dataBaseClass.resultTable.Columns[1].ColumnName;
            };
            Dispatcher.Invoke(action);
        }

        private void Dependency_OnChange_KZ(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid) KZFill();
        }
    }
}
