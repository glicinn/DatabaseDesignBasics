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
    /// Логика взаимодействия для StatusWindow.xaml
    /// </summary>
    public partial class StatusWindow : Window// Статус
    {
        public StatusWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StFill();
        }

        private void StFill()
        {
            SQLClass @class = new SQLClass();
            @class.SQLExecute("select [ID_Status], [Name_Status] from [dbo].[Status]", SQLClass.act.select);
            Status.ItemsSource = @class.table.DefaultView;
            Status.Columns[0].Visibility = Visibility.Hidden;
            Status.Columns[1].Header = "Статус";
        }

        private void Status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Status.Items.Count != 0 & Status.SelectedItems.Count != 0)
            {
                DataRowView dataRow = (DataRowView)Status.SelectedItems[0];
                St.Text = dataRow[1].ToString();

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)// Удаление
        {
            try
            {
                DataRowView row = (DataRowView)Status.SelectedItems[0];
                SQLClass @class = new SQLClass();
                @class.SQLExecute($"delete from [dbo].[Status] where [ID_Status] = {row[0]}", SQLClass.act.manipulation);
                StFill();
            }
            catch { }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)// Изменение
        {
            try
            {
                DataRowView row = (DataRowView)Status.SelectedItems[0];
                if (St.Text != "")
                {
                    SQLClass @class = new SQLClass();
                    @class.SQLExecute($"update [dbo].[Status] set [Name_Status] = '{St.Text}'  where [ID_Status] = {row[0]}", SQLClass.act.manipulation);
                    StFill();
                }
            }
            catch { }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)// Добавление
        {
            if (St.Text != "")
            {
                SQLClass @class = new SQLClass();
                @class.SQLExecute($"insert into [dbo].[Status] ([Name_Status]) values ('{St.Text}')", SQLClass.act.manipulation);
                StFill();
            }
        }
    }
}
