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
    /// Логика взаимодействия для DossierWindow.xaml
    /// </summary>
    public partial class DossierWindow : Window// Дело
    {
        public DossierWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DosFill();
            KActFill();
            KStFill();
            KObFill();
        }

        private void DosFill()
        {
            SQLClass @class = new SQLClass();
            @class.SQLExecute("select [ID_Dossier], [Dossier_Number],  [Investigation_Act_Number], [Name_Status], [Appeal_Number] from [dbo].[Dossier]" +
                "inner join [dbo].[Investigation_Act] on [Investigation_Act_ID] = [ID_Investigation_Act]" +
                "inner join [dbo].[Status] on [Status_ID] = [ID_Status]" +
                "inner join [dbo].[Appeal] on [Appeal_IDD] = [ID_Appeal]", SQLClass.act.select);
            Dossier.ItemsSource = @class.table.DefaultView;
            Dossier.Columns[0].Visibility = Visibility.Hidden;
            Dossier.Columns[1].Header = "Номер дела";
            Dossier.Columns[2].Header = "Код акта";
            Dossier.Columns[3].Header = "Код статуса";
            Dossier.Columns[4].Header = "Код обращения";
        }

        private void Dossier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Dossier.Items.Count != 0 & Dossier.SelectedItems.Count != 0)
            {
                DataRowView dataRow = (DataRowView)Dossier.SelectedItems[0];
                Num.Text = dataRow[1].ToString();
                KAct.Text = dataRow[2].ToString();
                KSt.Text = dataRow[3].ToString();
                KOb.Text = dataRow[4].ToString();

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)// Удаление
        {
            try
            {
                DataRowView row = (DataRowView)Dossier.SelectedItems[0];
                SQLClass @class = new SQLClass();
                @class.SQLExecute($"delete from [dbo].[Dossier] where [ID_Dossier] = {row[0]}", SQLClass.act.manipulation);
                DosFill();
            }
            catch { }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)// Изменение
        {
            try
            {
                DataRowView row = (DataRowView)Dossier.SelectedItems[0];
                if (Num.Text != "" && KAct.Text != "" && KSt.Text != "" && KOb.Text != "")
                {
                    SQLClass @class = new SQLClass();
                    @class.SQLExecute($"update [dbo].[Dossier] set [Dossier_Number] = '{Num.Text}', [Investigation_Act_ID] = {KAct.SelectedValue}, [Status_ID] = {KSt.SelectedValue}, [Appeal_IDD] = {KOb.SelectedValue}  where [ID_Dossier] = {row[0]}", SQLClass.act.manipulation);
                    DosFill();
                }
            }
            catch { }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)// Добавление
        {
            if (Num.Text != "" && KAct.Text != "" && KSt.Text != "" && KOb.Text != "")
            {
                SQLClass @class = new SQLClass();
                @class.SQLExecute($"insert into [dbo].[Dossier] ([Dossier_Number], [Investigation_Act_ID], [Status_ID], [Appeal_IDD]) values ('{Num.Text}', {KAct.SelectedValue}, {KSt.SelectedValue}, {KOb.SelectedValue})", SQLClass.act.manipulation);
                DosFill();
            }
        }

        private void KActFill()
        {
            Action action = () =>
            {
                DataBaseClass dataBaseClass = new DataBaseClass();
                dataBaseClass.sqlExecute("select [ID_Investigation_Act], [Investigation_Act_Number] from [dbo].[Investigation_Act]", DataBaseClass.act.select);
                dataBaseClass.dependency.OnChange += Dependency_OnChange_KAct;
                KAct.ItemsSource = dataBaseClass.resultTable.DefaultView;
                KAct.SelectedValuePath = dataBaseClass.resultTable.Columns[0].ColumnName;
                KAct.DisplayMemberPath = dataBaseClass.resultTable.Columns[1].ColumnName;
            };
            Dispatcher.Invoke(action);
        }

        private void Dependency_OnChange_KAct(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid) KActFill();
        }

        private void KStFill()
        {
            Action action = () =>
            {
                DataBaseClass dataBaseClass = new DataBaseClass();
                dataBaseClass.sqlExecute("select [ID_Status], [Name_Status] from [dbo].[Status]", DataBaseClass.act.select);
                dataBaseClass.dependency.OnChange += Dependency_OnChange_KSt;
                KSt.ItemsSource = dataBaseClass.resultTable.DefaultView;
                KSt.SelectedValuePath = dataBaseClass.resultTable.Columns[0].ColumnName;
                KSt.DisplayMemberPath = dataBaseClass.resultTable.Columns[1].ColumnName;
            };
            Dispatcher.Invoke(action);
        }

        private void Dependency_OnChange_KSt(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid) KStFill();
        }

        private void KObFill()
        {
            Action action = () =>
            {
                DataBaseClass dataBaseClass = new DataBaseClass();
                dataBaseClass.sqlExecute("select [ID_Appeal], [Appeal_Number] from [dbo].[Appeal]", DataBaseClass.act.select);
                dataBaseClass.dependency.OnChange += Dependency_OnChange_KOb;
                KOb.ItemsSource = dataBaseClass.resultTable.DefaultView;
                KOb.SelectedValuePath = dataBaseClass.resultTable.Columns[0].ColumnName;
                KOb.DisplayMemberPath = dataBaseClass.resultTable.Columns[1].ColumnName;
            };
            Dispatcher.Invoke(action);
        }

        private void Dependency_OnChange_KOb(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid) KObFill();
        }
    }
}
