using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using XiDeng.ViewModel;

namespace XiDeng.Common.Converts
{
    public class DayWeekConverter :IValueConverter
    {

        private bool byWeek;

        public bool ByWeek
        {
            get { return byWeek; }
            set { byWeek = value; }
        }


        public string[] weeks = new string[] {"周一","周二", "周三", "周四", "周五", "周六", "周日" };
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int day = 1;
            //if (targetType != typeof(int))
            //{
            //    throw new InvalidOperationException("The target must be a integer");
            //}

            if (!int.TryParse(value.ToString(), out day))
            {
                throw new InvalidOperationException("The target must be a integer");
            }


            return ByWeek ? weeks[((int)value - 1) % 7 ] : $"第{(int)value}天";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
