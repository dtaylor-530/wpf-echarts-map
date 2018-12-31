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
    public static class StoryBoard
    {


        private static DoubleAnimation Animation1(double particleTime) => new DoubleAnimation
        {
            From = 0.2,//此处值设置0-1会有不同的呈现效果
            To = 1,
            Duration = new Duration(TimeSpan.FromSeconds(particleTime)),
            BeginTime = TimeSpan.FromSeconds(particleTime),//推迟动画开始时间 等轨迹连接到圆时 开始播放圆的呈现动画
            FillBehavior = FillBehavior.HoldEnd
        };



        public static void AddPointToStoryboard(Grid runPoint, Ellipse toEll, Storyboard sb, PathGeometry pathGeometry,           double pointTime)
        {
            //double pointTime = l / m_Speed;//点运动所需的时间
            double particleTime = pointTime / 2;//轨迹呈现所需时间(跑的比点快两倍)

            #region 运动的点
            TransformGroup tfg = new TransformGroup();
            MatrixTransform mtf = new MatrixTransform();
            tfg.Children.Add(mtf);
            TranslateTransform ttf = new TranslateTransform(-runPoint.Width / 2, -runPoint.Height / 2);//纠正最上角沿path运动到中心沿path运动
            tfg.Children.Add(ttf);
            runPoint.RenderTransform = tfg;

            MatrixAnimationUsingPath maup = new MatrixAnimationUsingPath
            {
                PathGeometry = pathGeometry,
                Duration = new Duration(TimeSpan.FromSeconds(pointTime)),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = false,
                IsOffsetCumulative = false,
                DoesRotateWithTangent = true
            };
            Storyboard.SetTarget(maup, runPoint);
            Storyboard.SetTargetProperty(maup, new PropertyPath("(Grid.RenderTransform).Children[0].(MatrixTransform.Matrix)"));
            sb.Children.Add(maup);
            #endregion

            #region 达到城市的圆

            var ellda = Animation1(particleTime);
            Storyboard.SetTarget(ellda, toEll);
            Storyboard.SetTargetProperty(ellda, new PropertyPath(Ellipse.OpacityProperty));
            sb.Children.Add(ellda);


            RadialGradientBrush rgBrush = new RadialGradientBrush();
            GradientStop gStop0 = new GradientStop(Color.FromArgb(255, 0, 0, 0), 0);
         
            GradientStop gStopT = new GradientStop(Color.FromArgb(255, 0, 0, 0), 0);
            GradientStop gStop1 = new GradientStop(Color.FromArgb(255, 0, 0, 0), 1);
            rgBrush.GradientStops.Add(gStop0);
            rgBrush.GradientStops.Add(gStopT);
            rgBrush.GradientStops.Add(gStop1);
            toEll.OpacityMask = rgBrush;


            var ca = ColorAnimation(pointTime);
            Storyboard.SetTarget(ca, toEll);
            Storyboard.SetTargetProperty(ca, new PropertyPath("(Ellipse.OpacityMask).(GradientBrush.GradientStops)[1].(GradientStop.Color)"));
            sb.Children.Add(ca);


            var eda = Animation2(particleTime);
            Storyboard.SetTarget(eda, toEll);
            Storyboard.SetTargetProperty(eda, new PropertyPath("(Ellipse.OpacityMask).(GradientBrush.GradientStops)[1].(GradientStop.Offset)"));
            sb.Children.Add(eda);
            #endregion

        }




        private static ColorAnimation ColorAnimation(double pointTime) => new ColorAnimation
        {
            To = Color.FromArgb(0, 0, 0, 0),
            Duration = new Duration(TimeSpan.FromSeconds(0)),
            BeginTime = TimeSpan.FromSeconds(pointTime),
            FillBehavior = FillBehavior.HoldEnd
        };



        private static DoubleAnimation Animation2(double particleTime) => new DoubleAnimation
        {
            To = 1,
            Duration = new Duration(TimeSpan.FromSeconds(2)),
            RepeatBehavior = RepeatBehavior.Forever,
            BeginTime = TimeSpan.FromSeconds(particleTime)
        };

    }
}
