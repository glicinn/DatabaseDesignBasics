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
            KGrFill();
            KStFill();
            KKatFill();
            KSotFill();
        }

        private void AppFill()
        {
            SQLClass @class = new SQLClass();
            @class.SQLExecute("select [ID_Appeal], [Appeal_Number], [Formation_Date], [Appeal_Description], [First_Name_Citizen] + ' ' + [Name_Citizen] + ' ' + [Middle_Name_Citizen], [Article_Number] + ' ' + [Name_Article], [Name_Category], [First_Name_Employee] + ' ' + [Name_Employee] + ' ' + [Middle_Name_Employee] from [dbo].[Appeal]" +
                "inner join [dbo].[Citizen] on [Citizen_ID] = [ID_Citizen]" +
                "inner join [dbo].[Article] on [Article_ID] = [ID_Article]" +
                "inner join [dbo].[Category] on [Category_ID] = [ID_Category]" +
                "inner join [dbo].[Employee] on [Employee_ID] = [ID_Employee]", SQLClass.act.select);
            Appeal.ItemsSource = @class.table.DefaultView;
            Appeal.Columns[0].Visibility = Visibility.Hidden;
            Appeal.Columns[1].Header = "Номер";
            Appeal.Columns[2].Header = "Дата формирования";
            Appeal.Columns[3].Header = "Описание";
            Appeal.Columns[4].Header = "Гражданин";
            Appeal.Columns[5].Header = "Статья";
            Appeal.Columns[6].Header = "Категория";
            Appeal.Columns[7].Header = "Сотрудник";

        }

        private void Appeal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Appeal.Items.Count != 0 & Appeal.SelectedItems.Count != 0)
            {
                DataRowView dataRow = (DataRowView)Appeal.SelectedItems[0];
                Num.Text = dataRow[1].ToString();
                DateF.Text = dataRow[2].ToString();
                Opis.Text = dataRow[3].ToString();
                KGr.Text = dataRow[4].ToString();
                KSt.Text = dataRow[5].ToString();
                KKat.Text = dataRow[6].ToString();
                KSot.Text = dataRow[7].ToString();
            }
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
                    @class.SQLExecute($"update [dbo].[Appeal] set [Appeal_Number] = '{Num.Text}', [Formation_Date] = '{DateF.Text}', [Appeal_Description] = '{Opis.Text}', [Citizen_ID] = {KGr.SelectedValue}, [Article_ID] = {KSt.SelectedValue}, [Category_ID] = {KKat.SelectedValue}, [Employee_ID] = {KSot.SelectedValue}  where [ID_Appeal] = {row[0]}", SQLClass.act.manipulation);
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
                @class.SQLExecute($"insert into [dbo].[Appeal] ([Appeal_Number], [Formation_Date], [Appeal_Description], [Citizen_ID], [Article_ID], [Category_ID], [Employee_ID]) values ('{Num.Text}', '{DateF.Text}', '{Opis.Text}', {KGr.SelectedValue}, {KSt.SelectedValue}, {KKat.SelectedValue}, {KSot.SelectedValue})", SQLClass.act.manipulation);
                AppFill();
            }
        }

        private void KGrFill()
        {
            Action action = () =>
            {
                DataBaseClass dataBaseClass = new DataBaseClass();
                dataBaseClass.sqlExecute("select [ID_Citizen], [First_Name_Citizen]+' '+[Name_Citizen]+' '+[Middle_Name_Citizen] as 'Гражданин' from [dbo].[Citizen]", DataBaseClass.act.select);
                dataBaseClass.dependency.OnChange += Dependency_OnChange_KGr;
                KGr.ItemsSource = dataBaseClass.resultTable.DefaultView;
                KGr.SelectedValuePath = dataBaseClass.resultTable.Columns[0].ColumnName;
                KGr.DisplayMemberPath = dataBaseClass.resultTable.Columns[1].ColumnName;
            };
            Dispatcher.Invoke(action);
        }

        private void Dependency_OnChange_KGr(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid) KGrFill();
        }

        private void KStFill()
        {
            Action action = () =>
            {
                DataBaseClass dataBaseClass = new DataBaseClass();
                dataBaseClass.sqlExecute("select [ID_Article], [Article_Number] + ' '  + [Name_Article] as 'Статья' from [dbo].[Article]", DataBaseClass.act.select);
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

        private void KKatFill()
        {
            Action action = () =>
            {
                DataBaseClass dataBaseClass = new DataBaseClass();
                dataBaseClass.sqlExecute("select [ID_Category], [Name_Category] from [dbo].[Category]", DataBaseClass.act.select);
                dataBaseClass.dependency.OnChange += Dependency_OnChange_KKat;
                KKat.ItemsSource = dataBaseClass.resultTable.DefaultView;
                KKat.SelectedValuePath = dataBaseClass.resultTable.Columns[0].ColumnName;
                KKat.DisplayMemberPath = dataBaseClass.resultTable.Columns[1].ColumnName;
            };
            Dispatcher.Invoke(action);
        }

        private void Dependency_OnChange_KKat(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid) KKatFill();
        }

        private void KSotFill()
        {
            Action action = () =>
            {
                DataBaseClass dataBaseClass = new DataBaseClass();
                dataBaseClass.sqlExecute("select [ID_Employee], [First_Name_Employee] + ' '  + [Name_Employee] + ' ' + [Middle_Name_Employee] as 'Сотрудник' from [dbo].[Employee]", DataBaseClass.act.select);
                dataBaseClass.dependency.OnChange += Dependency_OnChange_KSot;
                KSot.ItemsSource = dataBaseClass.resultTable.DefaultView;
                KSot.SelectedValuePath = dataBaseClass.resultTable.Columns[0].ColumnName;
                KSot.DisplayMemberPath = dataBaseClass.resultTable.Columns[1].ColumnName;
            };
            Dispatcher.Invoke(action);
        }

        private void Dependency_OnChange_KSot(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid) KSotFill();
        }
    }
}
