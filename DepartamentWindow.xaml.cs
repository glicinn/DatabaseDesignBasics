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
    /// Логика взаимодействия для DepartamentWindow.xaml
    /// </summary>
    public partial class DepartamentWindow : Window// Отдел
    {
        public DepartamentWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DepFill();
        }

        private void DepFill()
        {
            SQLClass @class = new SQLClass();
            @class.SQLExecute("select [ID_Departament], [Name_Departament], [Departament_Creation_Date], [Departament_Employees_Number] from [dbo].[Departament]", SQLClass.act.select);
            Departament.ItemsSource = @class.table.DefaultView;
            Departament.Columns[0].Visibility = Visibility.Hidden;
            Departament.Columns[1].Header = "Название";
            Departament.Columns[2].Header = "Дата создания";
            Departament.Columns[3].Header = "Количество сотрудников";
        }

        private void Departament_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Departament.Items.Count != 0 & Departament.SelectedItems.Count != 0)
            {
                DataRowView dataRow = (DataRowView)Departament.SelectedItems[0];
                Naz.Text = dataRow[1].ToString();
                DateS.Text = dataRow[2].ToString();
                KolS.Text = dataRow[3].ToString();

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)// Удаление
        {
            try
            {
                DataRowView row = (DataRowView)Departament.SelectedItems[0];
                SQLClass @class = new SQLClass();
                @class.SQLExecute($"delete from [dbo].[Departament] where [ID_Departament] = {row[0]}", SQLClass.act.manipulation);
                DepFill();
            }
            catch { }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)// Изменение
        {
            try
            {
                DataRowView row = (DataRowView)Departament.SelectedItems[0];
                if (Naz.Text != "" && DateS.Text != "" && KolS.Text != "")
                {
                    SQLClass @class = new SQLClass();
                    @class.SQLExecute($"update [dbo].[Departament] set [Name_Departament] = '{Naz.Text}', [Departament_Creation_Date] = '{DateS.Text}', [Departament_Employees_Number] = '{KolS.Text}'  where [ID_Departament] = {row[0]}", SQLClass.act.manipulation);
                    DepFill();
                }
            }
            catch { }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)// Добавление
        {
            if (Naz.Text != "" && DateS.Text != "" && KolS.Text != "")
            {
                SQLClass @class = new SQLClass();
                @class.SQLExecute($"insert into [dbo].[Departament] ([Name_Departament], [Departament_Creation_Date], [Departament_Employees_Number]) values ('{Naz.Text}', '{DateS.Text}', '{KolS.Text}')", SQLClass.act.manipulation);
                DepFill();
            }
        }
    }
}
