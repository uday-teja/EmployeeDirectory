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
using EmployeeDirectory.Data;
using EmployeeDirectory.Models;

namespace EmployeeDirectory
{
    /// <summary>
    /// Interaction logic for DisplayDetails.xaml
    /// </summary>
    public partial class DisplayDetails : UserControl
    {
        MainWindow mainWindow;

        public DisplayDetails()
        {
            InitializeComponent();
        }

        public DisplayDetails(Employee employee, MainWindow window) : this()
        {
            mainWindow = window;
            mainWindow.header.Text = "Employee Details";
            this.DataContext = employee;
        }

        private void Back_Clicked(object sender, RoutedEventArgs e)
        {
            DisplayMainWindow();
        }

        public void DisplayMainWindow()
        {
            mainWindow.displayDetails.Visibility = Visibility.Collapsed;
            mainWindow.pager.Visibility = mainWindow.displayEmployees.Visibility = mainWindow.addEmployeeButton.Visibility = mainWindow.filter.Visibility = Visibility.Visible;
            mainWindow.header.Text = "Employee HUB";
        }

        private void PeronalDeatils_Click(object sender, RoutedEventArgs e)
        {
            personalDetails.Visibility = Visibility.Visible;
            jobDetails.Visibility = Visibility.Collapsed;
        }

        private void JobDeatils_Click(object sender, RoutedEventArgs e)
        {
            jobDetails.Visibility = Visibility.Visible;
            personalDetails.Visibility = Visibility.Hidden;
        }
    }
}