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
        }

        private void OffFill()
        {
            SQLClass @class = new SQLClass();
            @class.SQLExecute("select [ID_Office], [Name_Office], [Office_Creation_Date], [Office_Employees_Number], [Departament_ID] from [dbo].[Office]" +
                "inner join [dbo].[Departament] on [Departament_ID] = [ID_Departament]", SQLClass.act.select);
            Office.ItemsSource = @class.table.DefaultView;
            Office.Columns[0].Visibility = Visibility.Hidden;
            Office.Columns[1].Header = "Название";
            Office.Columns[2].Header = "Дата создания";
            Office.Columns[3].Header = "Количество сотрудников";
            Office.Columns[4].Header = "Код отдела";
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
                    @class.SQLExecute($"update [dbo].[Office] set [Name_Office] = '{Naz.Text}', [Office_Creation_Date] = '{DateS.Text}', [Office_Employees_Number] = '{KolS.Text}', [Departament_ID] = '{KOt.Text}'  where [ID_Office] = {row[0]}", SQLClass.act.manipulation);
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
                @class.SQLExecute($"insert into [dbo].[Office] ([Name_Office], [Office_Creation_Date], [Office_Employees_Number], [Departament_ID]) values ('{Naz.Text}', '{DateS.Text}', '{KolS.Text}', '{KOt.Text}')", SQLClass.act.manipulation);
                OffFill();
            }
        }
    }
}
