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
    /// Логика взаимодействия для OfficeWindow.xaml
    /// </summary>
    public partial class OfficeWindow : Window// Ведомтсво
    {
        public OfficeWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OffFill();
            KOtFill();
        }

        private void OffFill()
        {
            SQLClass @class = new SQLClass();
            @class.SQLExecute("select [ID_Office], [Name_Office], [Office_Creation_Date], [Office_Employees_Number], [Name_Departament] from [dbo].[Office]" +
                "inner join [dbo].[Departament] on [Departament_ID] = [ID_Departament]", SQLClass.act.select);
            Office.ItemsSource = @class.table.DefaultView;
            Office.Columns[0].Visibility = Visibility.Hidden;
            Office.Columns[1].Header = "Название";
            Office.Columns[2].Header = "Дата создания";
            Office.Columns[3].Header = "Количество сотрудников";
            Office.Columns[4].Header = "Отдел";
        }

        private void Office_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Office.Items.Count != 0 & Office.SelectedItems.Count != 0)
            {
                DataRowView dataRow = (DataRowView)Office.SelectedItems[0];
                Naz.Text = dataRow[1].ToString();
                DateS.Text = dataRow[2].ToString();
                KolS.Text = dataRow[3].ToString();
                KOt.Text = dataRow[4].ToString();

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)// Удаление
        {
            try
            {
                DataRowView row = (DataRowView)Office.SelectedItems[0];
                SQLClass @class = new SQLClass();
                @class.SQLExecute($"delete from [dbo].[Office] where [ID_Office] = {row[0]}", SQLClass.act.manipulation);
                OffFill();
            }
            catch { }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)// Изменение
        {
            try
            {
                DataRowView row = (DataRowView)Office.SelectedItems[0];
                if (Naz.Text != "" && DateS.Text != "" && KolS.Text != "" && KOt.Text != "")
                {
                    SQLClass @class = new SQLClass();
                    @class.SQLExecute($"update [dbo].[Office] set [Name_Office] = '{Naz.Text}', [Office_Creation_Date] = '{DateS.Text}', [Office_Employees_Number] = '{KolS.Text}', [Departament_ID] = {KOt.SelectedValue}  where [ID_Office] = {row[0]}", SQLClass.act.manipulation);
                    OffFill();
                }
            }
            catch { }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)// Добавление
        {
            if (Naz.Text != "" && DateS.Text != "" && KolS.Text != "" && KOt.Text != "")
            {
                SQLClass @class = new SQLClass();
                @class.SQLExecute($"insert into [dbo].[Office] ([Name_Office], [Office_Creation_Date], [Office_Employees_Number], [Departament_ID]) values ('{Naz.Text}', '{DateS.Text}', '{KolS.Text}', {KOt.SelectedValue})", SQLClass.act.manipulation);
                OffFill();
            }
        }

        private void KOtFill()
        {
            Action action = () =>
            {
                DataBaseClass dataBaseClass = new DataBaseClass();
                dataBaseClass.sqlExecute("select [ID_Departament], [Name_Departament] from [dbo].[Departament]", DataBaseClass.act.select);
                dataBaseClass.dependency.OnChange += Dependency_OnChange_KOt;
                KOt.ItemsSource = dataBaseClass.resultTable.DefaultView;
                KOt.SelectedValuePath = dataBaseClass.resultTable.Columns[0].ColumnName;
                KOt.DisplayMemberPath = dataBaseClass.resultTable.Columns[1].ColumnName;
            };
            Dispatcher.Invoke(action);
        }

        private void Dependency_OnChange_KOt(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid) KOtFill();
        }
    }
}
