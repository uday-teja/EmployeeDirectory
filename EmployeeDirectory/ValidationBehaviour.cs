using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EmployeeDirectory
{
    class ValidationBehaviour
    {

        public static readonly DependencyProperty ValidateProperty =
          DependencyProperty.RegisterAttached("ValidateProperty", typeof(string), typeof(ValidationBehaviour), new PropertyMetadata(string.Empty, TextBoxChanged));


        public static string GetValidate(DependencyObject obj)
        {
            return (string)obj.GetValue(ValidateProperty);
        }

        public static void SetValidate(DependencyObject obj, string value)
        {
            obj.SetValue(ValidateProperty, value);
        }

        public static string EnumType { get; set; }

        public static void TextBoxChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            EnumType = (string)obj.GetValue(ValidateProperty);
            var textbox = obj as TextBox;
            textbox.LostFocus += Textbox_LostFocus;
        }

        private static void Textbox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            ValidationBehaviour behaviours = new ValidationBehaviour();
            behaviours.ValidateEmail(EnumType, textBox);
        }

        public void ValidateEmail(string enumType, TextBox textBox)
        {
            TextBoxType textBoxType = (TextBoxType)Enum.Parse(typeof(TextBoxType), enumType.ToLower());
            switch (textBoxType)
            {
                case TextBoxType.email:
                    if (!textBox.Text.IsValidInput(Constants.Email))
                    {
                        Helper.DisplayMessage("Please Enter a Valid Email");
                    }
                    break;
            }
        }
    }

    public enum TextBoxType
    {
        email
    }
}