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
            KOFill();
        }

        private void ActFill()
        {
            SQLClass @class = new SQLClass();
            @class.SQLExecute("select [ID_Investigation_Act], [Investigation_Act_Number], [Beginning_Date], [Revealed_Facts], [Completion_Date], [Appeal_Number] from [dbo].[Investigation_Act]" +
                 "inner join [dbo].[Appeal] on [Appeal_ID] = [ID_Appeal]", SQLClass.act.select);
            Act.ItemsSource = @class.table.DefaultView;
            Act.Columns[0].Visibility = Visibility.Hidden;
            Act.Columns[1].Header = "Номер акта";
            Act.Columns[2].Header = "Дата начала";
            Act.Columns[3].Header = "Выявленные факты";
            Act.Columns[4].Header = "Дата завершения";
            Act.Columns[5].Header = "Код обращения";
        }

        private void Act_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Act.Items.Count != 0 & Act.SelectedItems.Count != 0)
            {
                DataRowView dataRow = (DataRowView)Act.SelectedItems[0];
                Num.Text = dataRow[1].ToString();
                DateN.Text = dataRow[2].ToString();
                Fact.Text = dataRow[3].ToString();
                DateZ.Text = dataRow[4].ToString();
                KO.Text = dataRow[5].ToString();

            }
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
                if (Num.Text != "" && DateN.Text != "" && Fact.Text != "" && DateZ.Text != "" && KO.Text != "")
                {
                    SQLClass @class = new SQLClass();
                    @class.SQLExecute($"update [dbo].[Investigation_Act] set [Investigation_Act_Number] = '{Num.Text}', [Beginning_Date] = '{DateN.Text}', [Revealed_Facts] = '{Fact.Text}', [Completion_Date] = '{DateZ.Text}', [Appeal_ID] = {KO.SelectedValue}  where [ID_Investigation_Act] = {row[0]}", SQLClass.act.manipulation);
                    ActFill();
                }
            }
            catch { }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)// Добавление
        {
            if (Num.Text != "" && DateN.Text != "" && Fact.Text != "" && DateZ.Text != "" && KO.Text != "")
            {
                SQLClass @class = new SQLClass();
                @class.SQLExecute($"insert into [dbo].[Investigation_Act] ([Investigation_Act_Number], [Beginning_Date], [Revealed_Facts], [Completion_Date], [Appeal_ID]) values ('{Num.Text}', '{DateN.Text}', '{Fact.Text}', '{DateZ.Text}', {KO.SelectedValue})", SQLClass.act.manipulation);
                ActFill();
            }
        }

        private void KOFill()
        {
            Action action = () =>
            {
                DataBaseClass dataBaseClass = new DataBaseClass();
                dataBaseClass.sqlExecute("select [ID_Appeal], [Appeal_Number] from [dbo].[Appeal]", DataBaseClass.act.select);
                dataBaseClass.dependency.OnChange += Dependency_OnChange_KO;
                KO.ItemsSource = dataBaseClass.resultTable.DefaultView;
                KO.SelectedValuePath = dataBaseClass.resultTable.Columns[0].ColumnName;
                KO.DisplayMemberPath = dataBaseClass.resultTable.Columns[1].ColumnName;
            };
            Dispatcher.Invoke(action);
        }

        private void Dependency_OnChange_KO(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid) KOFill();
        }
    }
}
