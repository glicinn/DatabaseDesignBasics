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
    /// Логика взаимодействия для CodeWindow.xaml
    /// </summary>
    public partial class CodeWindow : Window
    {
        public CodeWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)// Кодекс
        {
            CodFill();
        }

        public void CodFill()
        {
            SQLClass @class = new SQLClass();
            @class.SQLExecute("select [ID_Code], [Name_Code] from [dbo].[Code]", SQLClass.act.select);
            Code.ItemsSource = @class.table.DefaultView;
            Code.Columns[0].Visibility = Visibility.Hidden;
            Code.Columns[1].Header = "Кодекс";
        }

        private void Button_Click(object sender, RoutedEventArgs e)// Удаление
        {
            try
            {
                DataRowView row = (DataRowView)Code.SelectedItems[0];
                SQLClass @class = new SQLClass();
                @class.SQLExecute($"delete from [dbo].[Code] where [ID_Code] = {row[0]}", SQLClass.act.manipulation);
                CodFill();
            }
            catch { }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)// Изменение
        {
            try
            {
                DataRowView row = (DataRowView)Code.SelectedItems[0];
                if (Cod.Text != "")
                {
                    SQLClass @class = new SQLClass();
                    @class.SQLExecute($"update [dbo].[Code] set [Name_Code] = '{Cod.Text}'  where [ID_Code] = {row[0]}", SQLClass.act.manipulation);
                    CodFill();
                }
            }
            catch { }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)// Добавление
        {
            if (Cod.Text != "")
            {
                SQLClass @class = new SQLClass();
                @class.SQLExecute($"insert into [dbo].[Code] ([Name_Code]) values ('{Cod.Text}')", SQLClass.act.manipulation);
                CodFill();
            }
        }
    }
}
