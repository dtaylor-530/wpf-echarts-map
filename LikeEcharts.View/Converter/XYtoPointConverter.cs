using LikeEcharts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LikeEcharts.View
{
    public static class XYtoPointConverter
    {
        public static Point ToPoint(this XY xy)            => new Point(xy.X, xy.Y);

    }
}
