// Either you can use Debugger.Break() or put a break point
// in your converter, inorder to step into the debugger.
// Personally I like Debugger.Break() as I often use
// the "Delete All Breakpoints" function in Visual Studio.
using System.Globalization;
using System.Windows.Data;
using System;

namespace MvvmSnake.Views
{
    public class MyDebugConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
               object parameter, CultureInfo culture)
        {
            //System.Diagnostics.Debugger.Break();
            return value;
        }

        public object ConvertBack(object value, Type targetType,
               object parameter, CultureInfo culture)
        {
            System.Diagnostics.Debugger.Break();
            return value;
        }
    }
}