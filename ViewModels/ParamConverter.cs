using System;
using System.Windows;
using System.Windows.Data;

namespace SonosController.ViewModels
{
    public class ParamConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            object result = null;
            if (values[0] != null)
            {
                string mode = values[0].ToString();
                if (mode == "CreateStereoPair")
                {
                    result = ProcessStereoPair(values);
                }

                //if 
            }
            else
            {
                result = "An error occurred";
            }
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string ProcessStereoPair(object[] values)
        { 
            bool isChecked = false;
            string UUID = string.Empty;
            isChecked = (values[1] != null && values[1] != DependencyProperty.UnsetValue) ? (bool) values[1] : false;
            UUID = (values[2] != null && values[2] != DependencyProperty.UnsetValue) ? values[2].ToString() : "";
                            if (isChecked == true)
                            {
                                return "True" + "|" + UUID;
                            }
                            else
                            {
                                return "False" + "|" + UUID;
                            }
        }
        
       private object ConvertToTuple(object[] values)
       {
            Tuple<object, object> tuple = new Tuple<object, object>(values[1], values[2]);
            return tuple;
       }
    }
}
