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
            KodFill();
        }

        private void ArtFill()
        {
            SQLClass @class = new SQLClass();
            @class.SQLExecute("select [ID_Article], [Name_Article], [Article_Number], [Code_ID], [Name_Code], [Name_Code] from [dbo].[Article]" +
                "inner join [dbo].[Code] on [Code_ID] = [ID_Code]", SQLClass.act.select);
            Article.ItemsSource = @class.table.DefaultView;
            Article.Columns[0].Visibility = Visibility.Hidden;
            Article.Columns[1].Header = "Название";
            Article.Columns[2].Header = "Номер";
            Article.Columns[3].Visibility = Visibility.Hidden;
            Article.Columns[4].Visibility = Visibility.Hidden;
            Article.Columns[5].Header = "Кодекс";
        }

        private void Article_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Article.Items.Count != 0 & Article.SelectedItems.Count != 0)
            {
                DataRowView dataRow = (DataRowView)Article.SelectedItems[0];
                Naz.Text = dataRow[1].ToString();
                Num.Text = dataRow[2].ToString();
                Kod.Text = dataRow[5].ToString();
                
            }
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
                if (Naz.Text != "" && Num.Text != "" && Kod.Text != "")
                {
                    SQLClass @class = new SQLClass();
                    @class.SQLExecute($"update [dbo].[Article] set [Name_Article] = '{Naz.Text}', [Article_Number] = '{Num.Text}', [Code_ID] = {Kod.SelectedValue}  where [ID_Article] = {row[0]}", SQLClass.act.manipulation);
                    ArtFill();
                }
            }
            catch { }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)// Добавление
        {
            if (Naz.Text != "" && Num.Text != "" && Kod.Text != "")
            {
                SQLClass @class = new SQLClass();
                @class.SQLExecute($"insert into [dbo].[Article] ([Name_Article], [Article_Number], [Code_ID]) values ('{Naz.Text}', '{Num.Text}', {Kod.SelectedValue})", SQLClass.act.manipulation);
                ArtFill();
            }
        }

        private void KodFill()
        {
            Action action = () =>
            {
                DataBaseClass dataBaseClass = new DataBaseClass();
                dataBaseClass.sqlExecute("select [ID_Code], [Name_Code] from [dbo].[Code]", DataBaseClass.act.select);
                dataBaseClass.dependency.OnChange += Dependency_OnChange_Kod;
                Kod.ItemsSource = dataBaseClass.resultTable.DefaultView;
                Kod.SelectedValuePath = dataBaseClass.resultTable.Columns[0].ColumnName;
                Kod.DisplayMemberPath = dataBaseClass.resultTable.Columns[1].ColumnName;
            };
            Dispatcher.Invoke(action);
        }

        private void Dependency_OnChange_Kod(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid) KodFill();
        }
    }
}
