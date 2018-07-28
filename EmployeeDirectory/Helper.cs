using EmployeeDirectory.Data;
using EmployeeDirectory.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeeDirectory
{
    public class Helper
    {
        public static void DisplayMessage(string message)
        {
            MessageBox.Show(message);
        }

        public static int ConvertToInteger(string text)
        {
            return Convert.ToInt32(text);
        }
    }
}