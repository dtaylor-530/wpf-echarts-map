﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace LikeEcharts.View
{
    public class FirstItemConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            ItemsControl itemscontrol = values[0] as ItemsControl;
            int count = itemscontrol.Items.Count;

            if (values != null && values.Length == 2 && count > 0)
            {
                var itemContext = (values[1] as System.Windows.Controls.ContentPresenter).DataContext;
                var lastItem = itemscontrol.Items[0];
                return Equals(lastItem, itemContext);
            }

            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
