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
        }

        private void DosFill()
        {
            SQLClass @class = new SQLClass();
            @class.SQLExecute("select [ID_Dossier], [Dossier_Number], [Investigation_Act_ID], [Status_ID], [Appeal_ID] from [dbo].[Dossier]" +
                "inner join [dbo].[Investigation_Act] on [Investigation_Act_ID] = [ID_Investigation_Act]" +
                "inner join [dbo].[Status] on [Status_ID] = [ID_Status]" +
                "inner join [dbo].[Appeal] on [Appeal_ID] = [ID_Appeal]", SQLClass.act.select);
            Dossier.ItemsSource = @class.table.DefaultView;
            Dossier.Columns[0].Visibility = Visibility.Hidden;
            Dossier.Columns[1].Header = "Номер дела";
            Dossier.Columns[2].Header = "Код акта";
            Dossier.Columns[3].Header = "Код статуса";
            Dossier.Columns[4].Header = "Код обращения";
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
                    @class.SQLExecute($"update [dbo].[Dossier] set [Dossier_Number] = '{Num.Text}', [Investigation_Act_ID] = '{KAct.Text}', [Status_ID] = '{KSt.Text}', [Appeal_ID] = '{KOb.Text}'  where [ID_Dossier] = {row[0]}", SQLClass.act.manipulation);
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
                @class.SQLExecute($"insert into [dbo].[Dossier] ([Dossier_Number], [Investigation_Act_ID], [Status_ID], [Appeal_ID]) values ('{Num.Text}', '{KAct.Text}', '{KSt.Text}', '{KOb.Text}')", SQLClass.act.manipulation);
                DosFill();
            }
        }
    }
}
