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
    /// Логика взаимодействия для CandidateWindow.xaml
    /// </summary>
    public partial class CandidateWindow : Window
    {
        public CandidateWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SQLClass @class = new SQLClass();
            @class.SQLExecute("select [ID_Candidate], [First_Name_Candidate]+' '+[Name_Candidate]+' '+[Middle_Name_Candidate], [Login_Candidate], [Password_Candidate], [Passport_Series]+' '+[Passport_Number], [SNILS], [TIN], [Policy], [Military_ID_Series]+' '+[Military_ID_Number], [VPO_Ending_Diploma], [Position_ID], [Office_ID], [Rank_ID] from [dbo].[Candidate]" +
                "inner join [dbo].[Position] on [Position_ID] = [ID_Position]" +
                "inner join [dbo].[Office] on [Office_ID] = [ID_Office]" +
                "inner join [dbo].[Rank] on [Rank_ID] = [ID_Rank]", SQLClass.act.select);
            Candidate.ItemsSource = @class.table.DefaultView;
            Candidate.Columns[0].Visibility = Visibility.Hidden;
            Candidate.Columns[1].Header = "ФИО";
            Candidate.Columns[2].Header = "Логин";
            Candidate.Columns[3].Header = "Пароль";
            Candidate.Columns[4].Header = "Данные паспорта";
            Candidate.Columns[5].Header = "СНИЛС";
            Candidate.Columns[6].Header = "ИНН";
            Candidate.Columns[7].Header = "Полис";
            Candidate.Columns[8].Header = "Военный билет";
            Candidate.Columns[9].Header = "Диплом ВПО";
            Candidate.Columns[10].Header = "Код должности";
            Candidate.Columns[11].Header = "Код ведомства";
            Candidate.Columns[12].Header = "Код звания";
        }
    }
}
