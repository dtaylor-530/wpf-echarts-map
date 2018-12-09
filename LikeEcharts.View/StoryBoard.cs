using LikeEcharts.Model;
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

namespace LikeEcharts.View
{
    public static class StoryBoard
    {
        public static void AddPointToStoryboard(Grid runPoint, Ellipse toEll, Storyboard sb, Path particlePath,
            double l, Point startPoint, Point endPoint,double pointTime)
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

            MatrixAnimationUsingPath maup = new MatrixAnimationUsingPath();
            maup.PathGeometry = particlePath.Data.GetFlattenedPathGeometry();
            maup.Duration = new Duration(TimeSpan.FromSeconds(pointTime));
            maup.RepeatBehavior = RepeatBehavior.Forever;
            maup.AutoReverse = false;
            maup.IsOffsetCumulative = false;
            maup.DoesRotateWithTangent = true;
            Storyboard.SetTarget(maup, runPoint);
            Storyboard.SetTargetProperty(maup, new PropertyPath("(Grid.RenderTransform).Children[0].(MatrixTransform.Matrix)"));
            sb.Children.Add(maup);
            #endregion

            #region 达到城市的圆
            //轨迹到达圆时 圆呈现
            DoubleAnimation ellda = new DoubleAnimation();
            ellda.From = 0.2;//此处值设置0-1会有不同的呈现效果
            ellda.To = 1;
            ellda.Duration = new Duration(TimeSpan.FromSeconds(particleTime));
            ellda.BeginTime = TimeSpan.FromSeconds(particleTime);//推迟动画开始时间 等轨迹连接到圆时 开始播放圆的呈现动画
            ellda.FillBehavior = FillBehavior.HoldEnd;
            Storyboard.SetTarget(ellda, toEll);
            Storyboard.SetTargetProperty(ellda, new PropertyPath(Ellipse.OpacityProperty));
            sb.Children.Add(ellda);
            //圆呈放射状
            RadialGradientBrush rgBrush = new RadialGradientBrush();
            GradientStop gStop0 = new GradientStop(Color.FromArgb(255, 0, 0, 0), 0);
            //此为控制点 color的a值设为0 off值走0-1 透明部分向外放射 初始设为255是为了初始化效果 开始不呈放射状 等跑动的点运动到城市的圆后 color的a值才设为0开始呈现放射动画
            GradientStop gStopT = new GradientStop(Color.FromArgb(255, 0, 0, 0), 0);
            GradientStop gStop1 = new GradientStop(Color.FromArgb(255, 0, 0, 0), 1);
            rgBrush.GradientStops.Add(gStop0);
            rgBrush.GradientStops.Add(gStopT);
            rgBrush.GradientStops.Add(gStop1);
            toEll.OpacityMask = rgBrush;
            //跑动的点达到城市的圆时 控制点由不透明变为透明 color的a值设为0 动画时间为0
            ColorAnimation ca = new ColorAnimation();
            ca.To = Color.FromArgb(0, 0, 0, 0);
            ca.Duration = new Duration(TimeSpan.FromSeconds(0));
            ca.BeginTime = TimeSpan.FromSeconds(pointTime);
            ca.FillBehavior = FillBehavior.HoldEnd;
            Storyboard.SetTarget(ca, toEll);
            Storyboard.SetTargetProperty(ca, new PropertyPath("(Ellipse.OpacityMask).(GradientBrush.GradientStops)[1].(GradientStop.Color)"));
            sb.Children.Add(ca);
            //点达到城市的圆时 呈现放射状动画 控制点的off值走0-1 透明部分向外放射
            DoubleAnimation eda = new DoubleAnimation();
            eda.To = 1;
            eda.Duration = new Duration(TimeSpan.FromSeconds(2));
            eda.RepeatBehavior = RepeatBehavior.Forever;
            eda.BeginTime = TimeSpan.FromSeconds(particleTime);
            Storyboard.SetTarget(eda, toEll);
            Storyboard.SetTargetProperty(eda, new PropertyPath("(Ellipse.OpacityMask).(GradientBrush.GradientStops)[1].(GradientStop.Offset)"));
            sb.Children.Add(eda);
            #endregion

            #region 运动轨迹
            //找到渐变的起点和终点
            //Point startPoint = GetProvincialCapitalPoint(from);
            //Point endPoint = GetProvincialCapitalPoint(toItem.To);
            Point start = new Point(0, 0);
            Point end = new Point(1, 1);
            if (startPoint.X > endPoint.X)
            {
                start.X = 1;
                end.X = 0;
            }
            if (startPoint.Y > endPoint.Y)
            {
                start.Y = 1;
                end.Y = 0;
            }
            LinearGradientBrush lgBrush = new LinearGradientBrush();
            lgBrush.StartPoint = start;
            lgBrush.EndPoint = end;
            GradientStop lgStop0 = new GradientStop(Color.FromArgb(255, 0, 0, 0), 0);
            GradientStop lgStop1 = new GradientStop(Color.FromArgb(0, 0, 0, 0), 0);
            lgBrush.GradientStops.Add(lgStop0);
            lgBrush.GradientStops.Add(lgStop1);
            particlePath.OpacityMask = lgBrush;
            //运动轨迹呈现
            DoubleAnimation pda0 = new DoubleAnimation();
            pda0.To = 1;
            pda0.Duration = new Duration(TimeSpan.FromSeconds(particleTime));
            pda0.FillBehavior = FillBehavior.HoldEnd;
            Storyboard.SetTarget(pda0, particlePath);
            Storyboard.SetTargetProperty(pda0, new PropertyPath("(Path.OpacityMask).(GradientBrush.GradientStops)[0].(GradientStop.Offset)"));
            sb.Children.Add(pda0);
            DoubleAnimation pda1 = new DoubleAnimation();
            //pda1.From = 0.5; //此处解开注释 值设为0-1 会有不同的轨迹呈现效果
            pda1.To = 1;
            pda1.Duration = new Duration(TimeSpan.FromSeconds(particleTime));
            pda1.FillBehavior = FillBehavior.HoldEnd;
            Storyboard.SetTarget(pda1, particlePath);
            Storyboard.SetTargetProperty(pda1, new PropertyPath("(Path.OpacityMask).(GradientBrush.GradientStops)[1].(GradientStop.Offset)"));
            sb.Children.Add(pda1);
            #endregion
        }
    }
}
