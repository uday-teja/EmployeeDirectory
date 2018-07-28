using EmployeeDirectory.Data.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using AutoMapper;
using UIModel = EmployeeDirectory.Models;

namespace EmployeeDirectory.Data
{
    public class EmployeeService
    {
        EmployeeDirectoryEntities employeeDirectory;

        public EmployeeService()
        {
            employeeDirectory = new EmployeeDirectoryEntities();
        }

        public List<UIModel.Employee> GetEmployees()
        {
            var employees = from e in employeeDirectory.Employees join d in employeeDirectory.Designations on e.DesignationId equals d.DesignationId select new UIModel.Employee { DesignationName = d.DesignationName, FirstName = e.FirstName, LastName = e.LastName, Email = e.Email, MobileNumber = e.MobileNumber, Salary = (decimal)e.Salary, ID = e.ID, Gender = (UIModel.Gender)e.Gender, Address = e.Address, JoiningData = e.JoiningData.Value, DesignationId = (int)e.DesignationId, ImagePath = e.ImagePath };
            return employees.ToList();
        }

        public void AddOrUpdateEmployee(Employee employee)
        {
            if (employee != null)
            {
                employeeDirectory.Employees.Add(employee);
                employeeDirectory.SaveChanges();
            }
        }

        public void DeleteEmployee(int employeeId)
        {
            if (employeeId != 0)
                employeeDirectory.Employees.Remove(employeeDirectory.Employees.First(e => e.ID == employeeId));
            employeeDirectory.SaveChanges();
        }

        public List<Designation> GetDesignations()
        {
            return employeeDirectory.Designations.ToList();
        }
    }
}