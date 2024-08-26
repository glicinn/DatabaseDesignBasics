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
    /// Логика взаимодействия для ArticleWindow.xaml
    /// </summary>
    public partial class ArticleWindow : Window// Статья
    {
        public ArticleWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ArtFill();
        }

        private void ArtFill()
        {
            SQLClass @class = new SQLClass();
            @class.SQLExecute("select [ID_Article], [Name_Article], [Article_Number], [Code_ID] from [dbo].[Article]" +
                "inner join [dbo].[Code] on [Code_ID] = [ID_Code]", SQLClass.act.select);
            Article.ItemsSource = @class.table.DefaultView;
            Article.Columns[0].Visibility = Visibility.Hidden;
            Article.Columns[1].Header = "Название";
            Article.Columns[2].Header = "Номер";
            Article.Columns[3].Header = "Код кодекса";
        }

        private void Button_Click(object sender, RoutedEventArgs e)// Удаление
        {
            try
            {
                DataRowView row = (DataRowView)Article.SelectedItems[0];
                SQLClass @class = new SQLClass();
                @class.SQLExecute($"delete from [dbo].[Article] where [ID_Article] = {row[0]}", SQLClass.act.manipulation);
                ArtFill();
            }
            catch { }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)// Изменение
        {
            try
            {
                DataRowView row = (DataRowView)Article.SelectedItems[0];
                if (Naz.Text != "" && Num.Text != "" && KKod.Text != "")
                {
                    SQLClass @class = new SQLClass();
                    @class.SQLExecute($"update [dbo].[Article] set [Name_Article] = '{Naz.Text}', [Article_Number] = '{Num.Text}', [Code_ID] = '{KKod.Text}'  where [ID_Article] = {row[0]}", SQLClass.act.manipulation);
                    ArtFill();
                }
            }
            catch { }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)// Добавление
        {
            if (Naz.Text != "" && Num.Text != "" && KKod.Text != "")
            {
                SQLClass @class = new SQLClass();
                @class.SQLExecute($"insert into [dbo].[Article] ([Name_Article], [Article_Number], [Code_ID]) values ('{Naz.Text}', '{Num.Text}', '{KKod.Text}')", SQLClass.act.manipulation);
                ArtFill();
            }
        }
    }
}
