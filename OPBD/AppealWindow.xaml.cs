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
    /// Логика взаимодействия для AppealWindow.xaml
    /// </summary>
    public partial class AppealWindow : Window //Обращение
    {
        public AppealWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AppFill();
        }

        private void AppFill()
        {
            SQLClass @class = new SQLClass();
            @class.SQLExecute("select [ID_Appeal], [Appeal_Number], [Formation_Date], [Appeal_Description], [Citizen_ID], [Article_ID], [Category_ID], [Employee_ID] from [dbo].[Appeal]" +
                "inner join [dbo].[Citizen] on [Citizen_ID] = [ID_Citizen]" +
                "inner join [dbo].[Article] on [Article_ID] = [ID_Article]" +
                "inner join [dbo].[Category] on [Category_ID] = [ID_Category]" +
                "inner join [dbo].[Employee] on [Employee_ID] = [ID_Employee]", SQLClass.act.select);
            Appeal.ItemsSource = @class.table.DefaultView;
            Appeal.Columns[0].Visibility = Visibility.Hidden;
            Appeal.Columns[1].Header = "Номер";
            Appeal.Columns[2].Header = "Дата формирования";
            Appeal.Columns[3].Header = "Описание";
            Appeal.Columns[4].Header = "Код гражданина";
            Appeal.Columns[5].Header = "Код статьи";
            Appeal.Columns[6].Header = "Код категории";
            Appeal.Columns[7].Header = "Код сотрудника";

        }

        private void Button_Click(object sender, RoutedEventArgs e) //Удаление
        {
            try
            {
                DataRowView row = (DataRowView)Appeal.SelectedItems[0];
                SQLClass @class = new SQLClass();
                @class.SQLExecute($"delete from [dbo].[Appeal] where [ID_Appeal] = {row[0]}", SQLClass.act.manipulation);
                AppFill();
            }
            catch { }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //Изменение
        {
            try
            {
                DataRowView row = (DataRowView)Appeal.SelectedItems[0];
                if (Num.Text != "" && DateF.Text != "" && Opis.Text != "" && KGr.Text != "" && KSt.Text != "" && KKat.Text != "" && KSot.Text != "")
                {
                    SQLClass @class = new SQLClass();
                    @class.SQLExecute($"update [dbo].[Appeal] set [Appeal_Number] = '{Num.Text}', [Formation_Date] = '{DateF.Text}', [Appeal_Description] = '{Opis.Text}', [Citizen_ID] = '{KGr.Text}', [Article_ID] = '{KSt.Text}', [Category_ID] = '{KKat.Text}', [Employee_ID] = '{KSot.Text}'  where [ID_Appeal] = {row[0]}", SQLClass.act.manipulation);
                    AppFill();
                }
            }
            catch { }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //Добавление
        {
            if (Num.Text != "" && DateF.Text != "" && Opis.Text != "" && KGr.Text != "" && KSt.Text != "" && KKat.Text != "" && KSot.Text != "")
            {
                SQLClass @class = new SQLClass();
                @class.SQLExecute($"insert into [dbo].[Appeal] ([Appeal_Number], [Formation_Date], [Appeal_Description], [Citizen_ID], [Article_ID], [Category_ID], [Employee_ID]) values ('{Num.Text}', '{DateF.Text}', '{Opis.Text}', '{KGr.Text}', '{KSt.Text}', '{KKat.Text}', '{KSot.Text}')", SQLClass.act.manipulation);
                AppFill();
            }
        }
    }
}
