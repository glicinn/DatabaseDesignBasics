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
    /// Логика взаимодействия для MarkWindow.xaml
    /// </summary>
    public partial class RankWindow : Window // Звание
    {
        public RankWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RankFill();
        }

        private void RankFill()
        {
            SQLClass @class = new SQLClass();
            @class.SQLExecute("select [ID_Rank], [Name_Rank] from [dbo].[Rank]", SQLClass.act.select);
            Rank.ItemsSource = @class.table.DefaultView;
            Rank.Columns[0].Visibility = Visibility.Hidden;
            Rank.Columns[1].Header = "Звание";
        }

        private void Rank_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Rank.Items.Count != 0 & Rank.SelectedItems.Count != 0)
            {
                DataRowView dataRow = (DataRowView)Rank.SelectedItems[0];
                Zv.Text = dataRow[1].ToString();

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)// Удаление
        {
            try
            {
                DataRowView row = (DataRowView)Rank.SelectedItems[0];
                SQLClass @class = new SQLClass();
                @class.SQLExecute($"delete from [dbo].[Rank] where [ID_Rank] = {row[0]}", SQLClass.act.manipulation);
                RankFill();
            }
            catch { }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)// Изменение
        {
            try
            {
                DataRowView row = (DataRowView)Rank.SelectedItems[0];
                if (Zv.Text != "")
                {
                    SQLClass @class = new SQLClass();
                    @class.SQLExecute($"update [dbo].[Rank] set [Name_Rank] = '{Zv.Text}'  where [ID_Rank] = {row[0]}", SQLClass.act.manipulation);
                    RankFill();
                }
            }
            catch { }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)// Добавление
        {
            if (Zv.Text != "")
            {
                SQLClass @class = new SQLClass();
                @class.SQLExecute($"insert into [dbo].[Rank] ([Name_Rank]) values ('{Zv.Text}')", SQLClass.act.manipulation);
                RankFill();
            }
        }
    }
}

