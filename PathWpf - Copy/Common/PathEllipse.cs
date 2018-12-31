using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace PathWpf
{
    public static class PathEllipse
    {


        /// <summary>
        /// 添加控件和动画到容器
        /// </summary>
        /// <param name="item">数据项</param>
        public static DependencyObject[] GetAnimation(Point startPoint, Point endPoint, double diameter, PathGeometry pathGeometry, byte[] rgb, Storyboard m_Sb, double pointTime, string m_PointData)
        {
            //grid_Animation.Children.Clear();
            //m_Sb.Children.Clear();
            // Random rd = new Random();

            // 跑动的点
            System.Windows.Controls.Grid grid = GetRunPoint(rgb, m_PointData);

            //到达城市的圆
            Ellipse ell = PathEllipse.GetToEllipse(diameter, diameter, rgb, endPoint);

            StoryBoard.AddPointToStoryboard(grid, ell, m_Sb, pathGeometry, pointTime);

            return new DependencyObject[] { grid, ell };
        }



        public static Path GetPath(Point start, Point end, Storyboard sb, byte[] rgb, PathGeometry geometry, double pointTime)
        {

            Path path = new Path
            {
                Style = (Style)Application.Current.Resources["ParticlePathStyle"],
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, rgb[0], rgb[1], rgb[2])),
                OpacityMask =
                new LinearGradientBrush
                {
                    StartPoint = new Point(start.X > end.X ? 1 : 0, start.Y > end.Y ? 1 : 0),
                    EndPoint = new Point(start.X > end.X ? 0 : 1, start.Y > end.Y ? 0 : 1),
                    GradientStops = new GradientStopCollection(new[] { new GradientStop(Color.FromArgb(255, 0, 0, 0), 0), new GradientStop(Color.FromArgb(0, 0, 0, 0), 0) })
                },
                Data=geometry
                
            };

          
            DoubleAnimation pda0 = Animation3(pointTime);
            Storyboard.SetTarget(pda0, path);
            Storyboard.SetTargetProperty(pda0, new PropertyPath("(Path.OpacityMask).(GradientBrush.GradientStops)[0].(GradientStop.Offset)"));
            sb.Children.Add(pda0);


            var pda1 = Animation3(pointTime);
            Storyboard.SetTarget(pda1, path);
            Storyboard.SetTargetProperty(pda1, new PropertyPath("(Path.OpacityMask).(GradientBrush.GradientStops)[1].(GradientStop.Offset)"));
            sb.Children.Add(pda1);


            return path;
        }

        /// <summary>
        /// 获取跑动的点
        /// </summary>
        /// <param name="rgb">颜色:r,g,b</param>
        /// <returns>Grid</returns>
        public static Grid GetRunPoint(byte[] rgb, string m_PointData)
        {

            //Grid
            Grid grid = new Grid
            {
                IsHitTestVisible = false,//不参与命中测试
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Width = 40,
                Height = 15,
                RenderTransformOrigin = new Point(0.5, 0.5)
            };

            //Ellipse
            Ellipse ell = new Ellipse
            {
                Width = 40,
                Height = 15,
                Fill = new SolidColorBrush(Color.FromArgb(255, rgb[0], rgb[1], rgb[2])),
                OpacityMask = new RadialGradientBrush
                {
                    GradientOrigin = new Point(0.8, 0.5),
                    GradientStops = new GradientStopCollection(new[]
                {
                    new GradientStop(Color.FromArgb(255, 0, 0, 0), 0),
                    new GradientStop(Color.FromArgb(22, 0, 0, 0), 1)
                })
                }
            };


            Path path = new Path
            {
                Data = Geometry.Parse(m_PointData),
                Width = 30,
                Height = 4,
                Fill = new LinearGradientBrush
                {
                    StartPoint = new Point(0, 0),
                    EndPoint = new Point(1, 0),
                    GradientStops = new GradientStopCollection(
                        new[]{
                            new GradientStop(Color.FromArgb(88, rgb[0], rgb[1], rgb[2]), 0),
                            new GradientStop(Color.FromArgb(255, 255, 255, 255), 1)
                        })
                },
                Stretch = Stretch.Fill
            };

            grid.Children.Add(ell);
            grid.Children.Add(path);

            return grid;
        }




        /// <summary>
        /// 获取到达城市的圆
        /// </summary>
        /// <param name="toItem">数据项</param>
        /// <param name="rgb">颜色</param>
        /// <returns>Ellipse</returns>
        public static Ellipse GetToEllipse(double width, double height, byte[] rgb, Point toPos) => new Ellipse
        {
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Width = width,
            Height = height,
            Fill = new SolidColorBrush(Color.FromArgb(255, rgb[0], rgb[1], rgb[2])),
            RenderTransform = new TranslateTransform(toPos.X - width / 2, toPos.Y - height / 2),
            Opacity = 0,

        };








        private static DoubleAnimation Animation1(double particleTime) => new DoubleAnimation
        {
            From = 0.2,//此处值设置0-1会有不同的呈现效果
            To = 1,
            Duration = new Duration(TimeSpan.FromSeconds(particleTime)),
            BeginTime = TimeSpan.FromSeconds(particleTime),//推迟动画开始时间 等轨迹连接到圆时 开始播放圆的呈现动画
            FillBehavior = FillBehavior.HoldEnd
        };


        private static DoubleAnimation Animation3(double particleTime) => new DoubleAnimation
        {
            //pda1.From = 0.5; //此处解开注释 值设为0-1 会有不同的轨迹呈现效果
            To = 1,
            Duration = new Duration(TimeSpan.FromSeconds(particleTime)),
            FillBehavior = FillBehavior.HoldEnd
        };

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

            PathGeometry pg = new PathGeometry();
            PathFigure pf = new PathFigure();
            pf.StartPoint = start;
            ArcSegment arc = new ArcSegment();
            arc.SweepDirection = SweepDirection.Clockwise;//顺时针弧
            arc.Point = end;

            double sinA = Math.Sin(Math.PI * m_Angle / 180.0);

            double x = start.X - end.X;
            double y = start.Y - end.Y;
            double aa = x * x + y * y;
            l = Math.Sqrt(aa);
            double r = l / (sinA * 2);
            arc.Size = new Size(r, r);
            pf.Segments.Add(arc);
            pg.Figures.Add(pf);


            return pg;
        }


    }
}
