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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace OPBD
{
    /// <summary>
    /// Логика взаимодействия для MainMenuEmployee.xaml
    /// </summary>
    public partial class MainMenuEmployee : Window
    {
        public int Current_Employee_ID;
        public MainMenuEmployee()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RankWindow em = new RankWindow();
            em.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CodeWindow em = new CodeWindow();
            em.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            CategoryWindow em = new CategoryWindow();
            em.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            StatusWindow em = new StatusWindow();
            em.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ArticleWindow em = new ArticleWindow();
            em.Show();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            CitizenWindow em = new CitizenWindow();
            em.Show();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            InvestigationActWindow em = new InvestigationActWindow();
            em.Show();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            PositionWindow em = new PositionWindow();
            em.Show();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            DepartamentWindow em = new DepartamentWindow();
            em.Show();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            OfficeWindow em = new OfficeWindow();
            em.Show();
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            CandidateWindow em = new CandidateWindow();
            em.Show();
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            EmployeeWindow em = new EmployeeWindow();
            em.Show();
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            AppealWindow em = new AppealWindow();
            em.Show();
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            DossierWindow em = new DossierWindow();
            em.Show();
        }
    }
}
