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
    /// Логика взаимодействия для CategoryWindow.xaml
    /// </summary>
    public partial class CategoryWindow : Window// Категория 
    {
        public CategoryWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CatFill();
        }

        private void CatFill()
        {
            SQLClass @class = new SQLClass();
            @class.SQLExecute("select [ID_Category], [Name_Category] from [dbo].[Category]", SQLClass.act.select);
            Category.ItemsSource = @class.table.DefaultView;
            Category.Columns[0].Visibility = Visibility.Hidden;
            Category.Columns[1].Header = "Категория";
        }

        private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Category.Items.Count != 0 & Category.SelectedItems.Count != 0)
            {
                DataRowView dataRow = (DataRowView)Category.SelectedItems[0];
                Cat.Text = dataRow[1].ToString();

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)// Удаление
        {
            try
            {
                DataRowView row = (DataRowView)Category.SelectedItems[0];
                SQLClass @class = new SQLClass();
                @class.SQLExecute($"delete from [dbo].[Category] where [ID_Category] = {row[0]}", SQLClass.act.manipulation);
                CatFill();
            }
            catch { }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)// Изменение
        {
            try
            {
                DataRowView row = (DataRowView)Category.SelectedItems[0];
                if (Cat.Text != "")
                {
                    SQLClass @class = new SQLClass();
                    @class.SQLExecute($"update [dbo].[Category] set [Name_Category] = '{Cat.Text}'  where [ID_Category] = {row[0]}", SQLClass.act.manipulation);
                    CatFill();
                }
            }
            catch { }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)// Добавление
        {
            if (Cat.Text != "")
            {
                SQLClass @class = new SQLClass();
                @class.SQLExecute($"insert into [dbo].[Category] ([Name_Category]) values ('{Cat.Text}')", SQLClass.act.manipulation);
                CatFill();
            }
        }
    }
}
