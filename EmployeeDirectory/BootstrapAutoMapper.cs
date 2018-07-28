using AutoMapper;
using EmployeeDirectory.Models;
using DataModel = EmployeeDirectory.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDirectory
{
    public class BootstrapAutoMapper
    {
        public static void InitializeAutoMapper()
        {
            Mapper.Initialize(x =>
            {
                x.CreateMap<DataModel.Employee, Employee>();
                x.CreateMap<Employee, DataModel.Employee>();
            }
            );
        }
    }
}