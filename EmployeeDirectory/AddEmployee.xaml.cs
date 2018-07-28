using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EmployeeDirectory.Models;
using DataModel = EmployeeDirectory.Data.Model;
using EmployeeDirectory.Data;
using AutoMapper;
using System.Data.Entity.Migrations;
using System.Windows.Input;

namespace EmployeeDirectory
{
    /// <summary>
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : UserControl
    {
        EmployeeService services;
        public Employee selectedEmployee;
        public Employee modelEmployee;
        public MainWindow mainWindow;
        bool canUpdate;
        string defaultImage = "pack://application:,,,/EmployeeDirectory.Data;component/Images/default_picture.png";

        public AddEmployee()
        {
            InitializeComponent();
            services = new EmployeeService();
            designation.ItemsSource = services.GetDesignations();
        }

        public AddEmployee(Employee employee, MainWindow window) : this()
        {
            selectedEmployee = employee;
            modelEmployee = new Employee
            {
                FirstName = selectedEmployee.FirstName,
                LastName = selectedEmployee.LastName,
                MobileNumber = selectedEmployee.MobileNumber,
                ID = selectedEmployee.ID,
                ImagePath = selectedEmployee.ImagePath,
                JoiningData = selectedEmployee.JoiningData,
                DesignationId = selectedEmployee.DesignationId,
                DesignationName = selectedEmployee.DesignationName,
                Salary = selectedEmployee.Salary,
                Email = selectedEmployee.Email,
                Gender = selectedEmployee.Gender
            };

            this.DataContext = modelEmployee;
            mainWindow = window;

            if (employee.ID == 0)
            {
                addEmployee.Content = "Add";
                window.header.Text = "Add Employee";
                modelEmployee.JoiningData = DateTime.Today;
                photo.Source = new BitmapImage(new Uri(defaultImage, UriKind.Absolute));
                photoSubmit.Content = "Choose Image";
            }
            else
            {
                canUpdate = true;
                addEmployee.Content = "Update";
                window.header.Text = "Update Employee";
            }
        }

        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            mainWindow.header.Text = "Employee HUB";
            DisplayMainWindow();
        }

        private void AddEmployee_Clicked(object sender, RoutedEventArgs e)
        {
            if (canUpdate)
            {
                AddImage();
                services.AddOrUpdateEmployee(Convert(modelEmployee));
                photoSubmit.Content = "Change Image";
                var updateEmployee = mainWindow.RawEmployees.First(emp => emp.ID == modelEmployee.ID);
                updateEmployee.ID = modelEmployee.ID;
                updateEmployee.FirstName = modelEmployee.FirstName;
                updateEmployee.LastName = modelEmployee.LastName;
                updateEmployee.Email = modelEmployee.Email;
                updateEmployee.DesignationId = modelEmployee.DesignationId;
                updateEmployee.DesignationName = modelEmployee.DesignationName;
                updateEmployee.Gender = modelEmployee.Gender;
                updateEmployee.MobileNumber = modelEmployee.MobileNumber;
                updateEmployee.ImagePath = imagePath;
                updateEmployee.Salary = modelEmployee.Salary;
                updateEmployee.JoiningData = modelEmployee.JoiningData;
                mainWindow.Navigate(mainWindow.pageCount - 1, mainWindow.RawEmployees.ToList());
                Helper.DisplayMessage("Employee Updated Successfully...!!");
            }
            else
            {
                modelEmployee.DesignationId = designation.SelectedIndex;
                if (modelEmployee.ImagePath == "")
                    modelEmployee.ImagePath = defaultImage;
                else
                    modelEmployee.ImagePath = imagePath;
                AddImage();
                services.AddOrUpdateEmployee(Convert(modelEmployee));
                mainWindow.RawEmployees.Add(modelEmployee);
                mainWindow.Navigate(mainWindow.pageCount - 1, mainWindow.RawEmployees.ToList());
                Helper.DisplayMessage("Employee Added Successfully...!!");
            }
            DisplayMainWindow();
        }

        public DataModel.Employee Convert(Employee selectedEmployee)
        {
            DataModel.Employee employ = Mapper.Map<DataModel.Employee>(selectedEmployee);
            return employ;
        }

        public void DisplayMainWindow()
        {
            mainWindow.addEmployee.Visibility = Visibility.Collapsed;
            mainWindow.pager.Visibility = mainWindow.displayEmployees.Visibility = mainWindow.addEmployeeButton.Visibility = mainWindow.filter.Visibility = Visibility.Visible;
            mainWindow.header.Text = "Employee HUB";
        }

        public string imagePath;
        public string filePath;

        private void PhotoLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog
            {
                Title = "Select a picture",
                Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png"
            };

            if ((bool)open.ShowDialog())
            {
                imagePath = open.FileName;
                ImageSource imgsource = new BitmapImage(new Uri(imagePath));
                photo.Source = imgsource;
            }
        }

        public void AddImage()
        {
            if (imagePath != null)
            {
                string id = GenerateId();
                File.Copy(imagePath, $@"E:\Tasks\EmployeeDirectory\EmployeeDirectory\EmployeeDirectory\EmployeeImages\{id}{System.IO.Path.GetExtension(imagePath)}");
                //File.Copy(imagePath, $"{path}\\{modelEmployee.FirstName}_{id}{System.IO.Path.GetExtension(imagePath)}");
            }
        }

        public string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }

        private void Agreed_Checked(object sender, RoutedEventArgs e)
        {
            addEmployee.IsEnabled = true;
        }

        private void Agreed_Unchecked(object sender, RoutedEventArgs e)
        {
            addEmployee.IsEnabled = false;
        }

        private void NumberValidation(object sender, TextChangedEventArgs e)
        {
            mobile.Text = Regex.Replace(mobile.Text, "[^0-9]+", "");
        }
    }
}