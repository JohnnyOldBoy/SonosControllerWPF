using System;
using System.Windows;
using System.Windows.Data;

namespace SonosController.ViewModels
{
    public class ParamConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool isChecked = false;
            string UUID = string.Empty;
            string roomName = string.Empty;
            isChecked = (values[0] != null && values[0] != DependencyProperty.UnsetValue) ? (bool)values[0] : false;
            UUID = (values[1] != null && values[1] != DependencyProperty.UnsetValue) ? values[1].ToString() : "";
            if (isChecked == true)
            {
                return "True" + "|" + UUID + roomName;
            }
            else
            {
                return "False" + "|" + UUID + roomName;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
