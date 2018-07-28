using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmployeeDirectory
{
    public static class ExtensionMethods
    {
        public static bool IsValidInput(this string textboxInput, string inputType)
        {
            return Regex.IsMatch(textboxInput, Constants.Email);
        }
    }
}