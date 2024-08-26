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
    /// Логика взаимодействия для PositionWindow.xaml
    /// </summary>
    public partial class PositionWindow : Window// Должность
    {
        public PositionWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PosFill();
        }

        private void PosFill()
        {
            SQLClass @class = new SQLClass();
            @class.SQLExecute("select [ID_Position], [Name_Position] from [dbo].[Position]", SQLClass.act.select);
            Position.ItemsSource = @class.table.DefaultView;
            Position.Columns[0].Visibility = Visibility.Hidden;
            Position.Columns[1].Header = "Должность";
        }

        private void Button_Click(object sender, RoutedEventArgs e)// Удаление
        {
            try
            {
                DataRowView row = (DataRowView)Position.SelectedItems[0];
                SQLClass @class = new SQLClass();
                @class.SQLExecute($"delete from [dbo].[Position] where [ID_Position] = {row[0]}", SQLClass.act.manipulation);
                PosFill();
            }
            catch { }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)// Изменение
        {
            try
            {
                DataRowView row = (DataRowView)Position.SelectedItems[0];
                if (Dol.Text != "")
                {
                    SQLClass @class = new SQLClass();
                    @class.SQLExecute($"update [dbo].[Position] set [Name_Position] = '{Dol.Text}'  where [ID_Position] = {row[0]}", SQLClass.act.manipulation);
                    PosFill();
                }
            }
            catch { }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)// Добавление
        {
            if (Dol.Text != "")
            {
                SQLClass @class = new SQLClass();
                @class.SQLExecute($"insert into [dbo].[Position] ([Name_Position]) values ('{Dol.Text}')", SQLClass.act.manipulation);
                PosFill();
            }
        }
    }
}
