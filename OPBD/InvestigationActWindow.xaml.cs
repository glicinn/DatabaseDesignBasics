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
    /// Логика взаимодействия для InvestigationActWindow.xaml
    /// </summary>
    public partial class InvestigationActWindow : Window
    {
        public InvestigationActWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ActFill();
        }

        private void ActFill()
        {
            SQLClass @class = new SQLClass();
            @class.SQLExecute("select [ID_Investigation_Act], [Investigation_Act_Number], [Beginning_Date], [Revealed_Facts], [Completion_Date] from [dbo].[Investigation_Act]", SQLClass.act.select);
            Act.ItemsSource = @class.table.DefaultView;
            Act.Columns[0].Visibility = Visibility.Hidden;
            Act.Columns[1].Header = "Номер акта";
            Act.Columns[2].Header = "Дата начала";
            Act.Columns[3].Header = "Выявленные факты";
            Act.Columns[4].Header = "Дата завершения";
        }

        private void Button_Click(object sender, RoutedEventArgs e)// Удаление
        {
            try
            {
                DataRowView row = (DataRowView)Act.SelectedItems[0];
                SQLClass @class = new SQLClass();
                @class.SQLExecute($"delete from [dbo].[Investigation_Act] where [ID_Investigation_Act] = {row[0]}", SQLClass.act.manipulation);
                ActFill();
            }
            catch { }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)// Изменение
        {
            try
            {
                DataRowView row = (DataRowView)Act.SelectedItems[0];
                if (Num.Text != "" && DateN.Text != "" && Fact.Text != "" && DateZ.Text != "")
                {
                    SQLClass @class = new SQLClass();
                    @class.SQLExecute($"update [dbo].[Investigation_Act] set [Investigation_Act_Number] = '{Num.Text}', [Beginning_Date] = '{DateN.Text}', [Revealed_Facts] = '{Fact.Text}', [Completion_Date] = '{DateZ.Text}'  where [ID_Investigation_Act] = {row[0]}", SQLClass.act.manipulation);
                    ActFill();
                }
            }
            catch { }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)// Добавление
        {
            if (Num.Text != "" && DateN.Text != "" && Fact.Text != "" && DateZ.Text != "")
            {
                SQLClass @class = new SQLClass();
                @class.SQLExecute($"insert into [dbo].[Investigation_Act] ([Investigation_Act_Number], [Beginning_Date], [Revealed_Facts], [Completion_Date]) values ('{Num.Text}', '{DateN.Text}', '{Fact.Text}', '{DateZ.Text}')", SQLClass.act.manipulation);
                ActFill();
            }
        }
    }
}
