using LikeEcharts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LikeEcharts.View
{
    public static class Other
    {

        #region 获取跑动的点
        /// <summary>
        /// 获取跑动的点
        /// </summary>
        /// <param name="rgb">颜色:r,g,b</param>
        /// <returns>Grid</returns>
        public static Grid GetRunPoint(byte[] rgb,string m_PointData)
        {
            //一个Grid里包含一个椭圆 一个Path 椭圆做阴影
            //Grid
            Grid grid = new Grid();
            grid.IsHitTestVisible = false;//不参与命中测试
            grid.HorizontalAlignment = HorizontalAlignment.Left;
            grid.VerticalAlignment = VerticalAlignment.Top;
            grid.Width = 40;
            grid.Height = 15;
            grid.RenderTransformOrigin = new Point(0.5, 0.5);
            //Ellipse
            Ellipse ell = new Ellipse();
            ell.Width = 40;
            ell.Height = 15;
            ell.Fill = new SolidColorBrush(Color.FromArgb(255, rgb[0], rgb[1], rgb[2]));
            RadialGradientBrush rgbrush = new RadialGradientBrush();
            rgbrush.GradientOrigin = new Point(0.8, 0.5);
            rgbrush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 0, 0, 0), 0));
            rgbrush.GradientStops.Add(new GradientStop(Color.FromArgb(22, 0, 0, 0), 1));
            ell.OpacityMask = rgbrush;
            grid.Children.Add(ell);
            //Path
            Path path = new Path();
            path.Data = Geometry.Parse(m_PointData);
            path.Width = 30;
            path.Height = 4;
            LinearGradientBrush lgb = new LinearGradientBrush();
            lgb.StartPoint = new Point(0, 0);
            lgb.EndPoint = new Point(1, 0);
            lgb.GradientStops.Add(new GradientStop(Color.FromArgb(88, rgb[0], rgb[1], rgb[2]), 0));
            lgb.GradientStops.Add(new GradientStop(Color.FromArgb(255, 255, 255, 255), 1));
            path.Fill = lgb;
            path.Stretch = Stretch.Fill;
            grid.Children.Add(path);
            return grid;
        }
        #endregion




        /// <summary>
        /// 获取到达城市的圆
        /// </summary>
        /// <param name="toItem">数据项</param>
        /// <param name="rgb">颜色</param>
        /// <returns>Ellipse</returns>
        public static Ellipse GetToEllipse(MapToItem toItem, byte[] rgb,Point toPos)
        {
            Ellipse ell = new Ellipse();
            ell.HorizontalAlignment = HorizontalAlignment.Left;
            ell.VerticalAlignment = VerticalAlignment.Top;
            ell.Width = toItem.Diameter;
            ell.Height = toItem.Diameter;
            ell.Fill = new SolidColorBrush(Color.FromArgb(255, rgb[0], rgb[1], rgb[2]));
            //Point toPos = GetProvincialCapitalPoint(toItem.To);
            TranslateTransform ttf = new TranslateTransform(toPos.X - ell.Width / 2, toPos.Y - ell.Height / 2);//定位到城市所在的点
            ell.RenderTransform = ttf;
            ell.ToolTip = string.Format("{0} {1}", toItem.To.ToString(), toItem.Tip);
            ell.Opacity = 0;
            return ell;
        }



        public static Path GetParticlePath(Point start, Point end, byte[] rgb, double m_Angle,out double l)
        {


            //Path path = new Path();
            //Path path = (Path)Application.Current.Resources["ParticlePath"];
            Path path = new Path();
            Style style = (Style)Application.Current.Resources["ParticlePathStyle"];
            path.Style = style;
            //path.Style = style;
            path.Data = GetParticlePathGeometry(start, end, m_Angle, out l);
            //path.ToolTip = string.Format("{0}=>{1}",mapitem.ToString(), toItem.To.ToString());

            return path;
        }


        /// <summary>
        /// 获取运动轨迹
        /// </summary>
        /// <param name="from">来自</param>
        /// <param name="toItem">去</param>
        /// <param name="rgb">颜色:r,g,b</param>
        /// <param name="l">两点间的直线距离</param>
        /// <returns>Path</returns>
        public static PathGeometry GetParticlePathGeometry(Point start, Point end, double m_Angle, out double l)
        {
            //Point startPoint = GetProvincialCapitalPoint(from);
            //Point endPoint = GetProvincialCapitalPoint(toItem.To);

            //Path path = new Path();
            //Style style = (Style)FindResource("ParticlePathStyle");
            //path.Style = style;
            PathGeometry pg = new PathGeometry();
            PathFigure pf = new PathFigure();
            pf.StartPoint = start;
            ArcSegment arc = new ArcSegment();
            arc.SweepDirection = SweepDirection.Clockwise;//顺时针弧
            arc.Point = end;
            //半径 正弦定理a/sinA=2r r=a/2sinA 其中a指的是两个城市点之间的距离 角A指a边的对角
            double sinA = Math.Sin(Math.PI * m_Angle / 180.0);
            //计算距离 勾股定理
            double x = start.X - end.X;
            double y = start.Y - end.Y;
            double aa = x * x + y * y;
            l = Math.Sqrt(aa);
            double r = l / (sinA * 2);
            arc.Size = new Size(r, r);
            pf.Segments.Add(arc);
            pg.Figures.Add(pf);
            //path.Data = pg;
            //path.Stroke = new SolidColorBrush(Color.FromArgb(255, rgb[0], rgb[1], rgb[2]));
            //path.Stretch = Stretch.None;
            //path.ToolTip = string.Format("{0}=>{1}", from.ToString(), toItem.To.ToString());

            return pg;
        }


    }
}
