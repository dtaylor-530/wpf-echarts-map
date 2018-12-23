﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PathWpf
{

    public static class RandomBrush
    {

        static RandomBrush()
        {
            enumerator = BrushHelper.GetBrushes().GetEnumerator();
        }
        public static Brush Brush
        {
            get { enumerator.MoveNext(); return enumerator.Current; }
        }
        static IEnumerator<Brush> enumerator;

    }


    public static class BrushHelper
    { 
        public static IEnumerable<Brush> GetBrushes()
        {
            var props = typeof(Brushes).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            foreach (var propInfo in props)
            {
               yield return (Brush)propInfo.GetValue(null, null);
            }
        }
        
    }
}
