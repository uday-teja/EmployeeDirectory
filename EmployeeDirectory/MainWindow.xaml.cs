using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using EmployeeDirectory.Data;
using EmployeeDirectory.Models;
using AutoMapper;
using System.ComponentModel;
using System.Windows.Input;

namespace EmployeeDirectory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EmployeeService employeeService;

        public ObservableCollection<Employee> RawEmployees { get; set; }

        public int pageCount;

        public MainWindow()
        {
            InitializeComponent();
            header.Text = "Employee HUB";
            employeeService = new EmployeeService();
            RawEmployees = Mapper.Map<ObservableCollection<Employee>>(employeeService.GetEmployees());
            NumberOfRecords.SelectedItem = ItemsPerPage.Three;
            Navigate(0, RawEmployees.ToList());
        }

        private void EditEmployee_Clicked(object sender, RoutedEventArgs e)
        {
            Employee employee = (Employee)displayEmployees.SelectedItem;
            addEmployee.Content = new AddEmployee(employee, this);
            DisplayAddEmployeeView();
        }

        private void DeleteEmployee_Clicked(object sender, RoutedEventArgs e)
        {
            Employee employee = (Employee)displayEmployees.SelectedItem;
            MessageBoxResult result = MessageBox.Show("Are you sure to delete the contact?", "Delete Contact", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    employeeService.DeleteEmployee(employee.ID);
                    Helper.DisplayMessage("Employee Deleted..!!");
                    displayEmployees.Visibility = Visibility.Visible;
                    break;
            }
            RawEmployees.Remove(employee);
            Navigate(0, RawEmployees.ToList());
        }

        public void DisplayAddEmployeeView()
        {
            displayEmployees.Visibility = Visibility.Collapsed;
            addEmployeeButton.Visibility = Visibility.Collapsed;
            pager.Visibility = Visibility.Collapsed;
            filter.Visibility = Visibility.Collapsed;
            addEmployee.Visibility = Visibility.Visible;
        }

        public List<Employee> pagerEmployees;

        private void NumberOfRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Navigate(0, RawEmployees.ToList());
            Search_Click(null, e);
        }

        public int GetNoOfRecordsPerPage()
        {
            return Convert.ToInt32(NumberOfRecords.SelectedItem);
        }

        public void Navigate(int pageNo, List<Employee> employees)
        {
            pagerEmployees = employees;
            int noOfRecords = GetNoOfRecordsPerPage();
            totalPages = (int)Math.Ceiling((decimal)pagerEmployees.Count / (decimal)GetNoOfRecordsPerPage());

            if (pagerEmployees.Count == 0)
            {
                displayEmpty.Visibility = Visibility.Visible;
                pager.Visibility = Visibility.Collapsed;
            }

            pager.Visibility = Visibility.Visible;

            if (employees.Count > totalPages)
            {
                pageCount = pageNo + 1;
                displayEmployees.ItemsSource = pagerEmployees.Skip((pageNo) * noOfRecords).Take(noOfRecords);
                displayEmpty.Visibility = Visibility.Hidden;
                SetPageNumber(pageCount, pagerEmployees);
            }
        }
        public int totalPages;

        public int empCountPerPage;

        private void First_Click(object sender, System.EventArgs e)
        {
            if (pageCount > 1)
                Navigate(0, pagerEmployees.ToList());
        }


        private void Last_Click(object sender, System.EventArgs e)
        {
            if (pageCount < totalPages)
                Navigate(totalPages - 1, pagerEmployees);
        }

        private void Next_Click(object sender, System.EventArgs e)
        {
            if (pageCount < (totalPages))
                Navigate(pageCount, pagerEmployees);
        }

        private void Prev_Click(object sender, System.EventArgs e)
        {
            if (pageCount > 1)
                Navigate(pageCount - 2, pagerEmployees);
        }

        public void SetPageNumber(int pageInformation, List<Employee> employees)
        {
            pageInfo.Text = pageCount + " of " + totalPages;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var employees = RawEmployees.Where(emp => (string.IsNullOrEmpty(emp.FirstName) || emp.FirstName.Contains(nameSearch.Text)) && (string.IsNullOrEmpty(emp.MobileNumber) || emp.MobileNumber.Contains(mobileSearch.Text))).ToList();
            displayEmployees.DataContext = employees;
            Navigate(0, employees);
        }

        private void DisplayDetails_Clicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DisplayDetailsView();
            Employee employee = (Employee)displayEmployees.SelectedItem;
            displayDetails.Content = new DisplayDetails(employee, this);
        }

        public void DisplayDetailsView()
        {
            displayEmployees.Visibility = Visibility.Collapsed;
            addEmployeeButton.Visibility = Visibility.Collapsed;
            pager.Visibility = Visibility.Collapsed;
            filter.Visibility = Visibility.Collapsed;
            displayDetails.Visibility = Visibility.Visible;
        }
    }

    public enum ItemsPerPage
    {
        Three = 3,
        Six = 6,
        Nine = 9
    }
}