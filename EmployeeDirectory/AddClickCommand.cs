using EmployeeDirectory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EmployeeDirectory
{
    public class AddClickCommand
    {
        public ICommand AddCommand { get; set; }

        public AddClickCommand()
        {
            AddCommand = new Command(Execute, CanExecute);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MainWindow window = (MainWindow)parameter;
            window.DisplayAddEmployeeView();
            window.addEmployee.Content = new AddEmployee(new Employee(), window);
        }
    }
}