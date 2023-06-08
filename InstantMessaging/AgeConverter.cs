using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace InstantMessaging
{
    public class AgeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime birthdate)
            {
                DateTime today = DateTime.Today;
                int age = today.Year - birthdate.Year;

                // Check if the birthdate hasn't occurred yet this year
                if (birthdate > today.AddYears(-age))
                {
                    age--;
                }

                return age >= 18;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

}
