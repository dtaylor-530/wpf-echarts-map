using LikeEcharts.DAL;
using LikeEcharts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Shapes;


namespace LikeEcharts.View
{
    public class Class1
    {

        #region 加载菜单RadioButton
        /// <summary>
        /// 加载RadioButton
        /// </summary>
        //private void InitRadioButton()
        //{
        //    Random rd = new Random();
        //    for (int i = 0; i < m_Source.Count; i++)
        //    {
        //        byte r = (byte)rd.Next(0, 256);
        //        byte g = (byte)rd.Next(0, 256);
        //        byte b = (byte)rd.Next(0, 256);
        //        RadioButton rbtn = new RadioButton();
        //        rbtn.Content = m_Source[i].From.ToString();
        //        rbtn.Margin = new Thickness(0, 20, 0, 0);
        //        rbtn.Background = new SolidColorBrush(Color.FromArgb(200, r, g, b));
        //        rbtn.BorderBrush = new SolidColorBrush(Color.FromArgb(255, r, g, b));
        //        if (i == 0)
        //            rbtn.IsChecked = true;
        //        else
        //            rbtn.IsChecked = false;
        //        rbtn.Click += rbtn_Click;
        //        spnl_Radio.Children.Add(rbtn);
        //    }
        //}
        #endregion
        #region 添加控件和动画到容器
        /// <summary>
        /// 添加控件和动画到容器
        /// </summary>
        /// <param name="item">数据项</param>
        public static IEnumerable<DependencyObject[]> AddAnimation(MapItem item,double m_angle,Storyboard m_Sb,double m_Speed,string m_PointData)
        {
            //grid_Animation.Children.Clear();
            //m_Sb.Children.Clear();
            Random rd = new Random();
            foreach (MapToItem toItem in item.To)
            {
                if (item.From == toItem.To)
                    continue;

                //颜色
                byte[] rgb = new byte[] { (byte)rd.Next(0, 255), (byte)rd.Next(0, 255), (byte)rd.Next(0, 255) };

                //运动轨迹
                double l = 0;

                Point startPoint = ProvincialCapitalRepo.GetProvincialCapitalPoint(item.From).ToPoint();
                Point endPoint = ProvincialCapitalRepo.GetProvincialCapitalPoint(toItem.To).ToPoint();

                Path particlePath = Other.GetParticlePath(startPoint, endPoint, rgb,m_angle, out l);
                particlePath.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, rgb[0], rgb[1], rgb[2]));

                // 跑动的点
                System.Windows.Controls.Grid grid = Other.GetRunPoint(rgb,m_PointData);
                //到达城市的圆
                Ellipse ell = Other.GetToEllipse(toItem, rgb,endPoint);
                double pointTime = l / m_Speed;
                StoryBoard.AddPointToStoryboard(grid, ell, m_Sb, particlePath, l, startPoint, endPoint ,pointTime);

                yield return new DependencyObject[]{ particlePath, grid, ell };
                //grid_Animation.Children.Add(particlePath);
                //grid_Animation.Children.Add(grid);
                //grid_Animation.Children.Add(ell);

                //m_Sb.Begin(this);
            }
        }
        #endregion

//#endregion
//        #region 获取运动轨迹
//        /// <summary>
//        /// 获取运动轨迹
//        /// </summary>
//        /// <param name="from">来自</param>
//        /// <param name="toItem">去</param>
//        /// <param name="rgb">颜色:r,g,b</param>
//        /// <param name="l">两点间的直线距离</param>
//        /// <returns>Path</returns>
//        private Path GetParticlePath(ProvincialCapital from, MapToItem toItem, byte[] rgb, out double l)
//        {
//            Point startPoint = GetProvincialCapitalPoint(from);
//            Point endPoint = GetProvincialCapitalPoint(toItem.To);

//            Path path = new Path();
//            Style style = (Style)FindResource("ParticlePathStyle");
//            path.Style = style;
//            PathGeometry pg = new PathGeometry();
//            PathFigure pf = new PathFigure();
//            pf.StartPoint = startPoint;
//            ArcSegment arc = new ArcSegment();
//            arc.SweepDirection = SweepDirection.Clockwise;//顺时针弧
//            arc.Point = endPoint;
//            //半径 正弦定理a/sinA=2r r=a/2sinA 其中a指的是两个城市点之间的距离 角A指a边的对角
//            double sinA = Math.Sin(Math.PI * m_Angle / 180.0);
//            //计算距离 勾股定理
//            double x = startPoint.X - endPoint.X;
//            double y = startPoint.Y - endPoint.Y;
//            double aa = x * x + y * y;
//            l = Math.Sqrt(aa);
//            double r = l / (sinA * 2);
//            arc.Size = new Size(r, r);
//            pf.Segments.Add(arc);
//            pg.Figures.Add(pf);
//            path.Data = pg;
//            //path.Stroke = new SolidColorBrush(Color.FromArgb(255, rgb[0], rgb[1], rgb[2]));
//            //path.Stretch = Stretch.None;
//            path.ToolTip = string.Format("{0}=>{1}", from.ToString(), toItem.To.ToString());

//            return path;
//        }

//        #region 将点加入到动画故事版
//        /// <summary>
//        /// 将点加入到动画故事版
//        /// </summary>
//        /// <param name="runPoint">运动的点</param>
//        /// <param name="toEll">达到城市的圆</param>
//        /// <param name="sb">故事版</param>
//        /// <param name="particlePath">运动轨迹</param>
//        /// <param name="l">运动轨迹的直线距离</param>
//        /// <param name="from">来自</param>
//        /// <param name="toItem">去</param>
//        private void AddPointToStoryboard(Grid runPoint, Ellipse toEll, Storyboard sb, Path particlePath, double l, ProvincialCapital from, MapToItem toItem)
//        {
//            double pointTime = l / m_Speed;//点运动所需的时间
//            double particleTime = pointTime / 2;//轨迹呈现所需时间(跑的比点快两倍)

//            #region 运动的点
//            TransformGroup tfg = new TransformGroup();
//            MatrixTransform mtf = new MatrixTransform();
//            tfg.Children.Add(mtf);
//            TranslateTransform ttf = new TranslateTransform(-runPoint.Width / 2, -runPoint.Height / 2);//纠正最上角沿path运动到中心沿path运动
//            tfg.Children.Add(ttf);
//            runPoint.RenderTransform = tfg;

//            MatrixAnimationUsingPath maup = new MatrixAnimationUsingPath();
//            maup.PathGeometry = particlePath.Data.GetFlattenedPathGeometry();
//            maup.Duration = new Duration(TimeSpan.FromSeconds(pointTime));
//            maup.RepeatBehavior = RepeatBehavior.Forever;
//            maup.AutoReverse = false;
//            maup.IsOffsetCumulative = false;
//            maup.DoesRotateWithTangent = true;
//            Storyboard.SetTarget(maup, runPoint);
//            Storyboard.SetTargetProperty(maup, new PropertyPath("(Grid.RenderTransform).Children[0].(MatrixTransform.Matrix)"));
//            sb.Children.Add(maup);
//            #endregion

//            #region 达到城市的圆
//            //轨迹到达圆时 圆呈现
//            DoubleAnimation ellda = new DoubleAnimation();
//            ellda.From = 0.2;//此处值设置0-1会有不同的呈现效果
//            ellda.To = 1;
//            ellda.Duration = new Duration(TimeSpan.FromSeconds(particleTime));
//            ellda.BeginTime = TimeSpan.FromSeconds(particleTime);//推迟动画开始时间 等轨迹连接到圆时 开始播放圆的呈现动画
//            ellda.FillBehavior = FillBehavior.HoldEnd;
//            Storyboard.SetTarget(ellda, toEll);
//            Storyboard.SetTargetProperty(ellda, new PropertyPath(Ellipse.OpacityProperty));
//            sb.Children.Add(ellda);
//            //圆呈放射状
//            RadialGradientBrush rgBrush = new RadialGradientBrush();
//            GradientStop gStop0 = new GradientStop(Color.FromArgb(255, 0, 0, 0), 0);
//            //此为控制点 color的a值设为0 off值走0-1 透明部分向外放射 初始设为255是为了初始化效果 开始不呈放射状 等跑动的点运动到城市的圆后 color的a值才设为0开始呈现放射动画
//            GradientStop gStopT = new GradientStop(Color.FromArgb(255, 0, 0, 0), 0);
//            GradientStop gStop1 = new GradientStop(Color.FromArgb(255, 0, 0, 0), 1);
//            rgBrush.GradientStops.Add(gStop0);
//            rgBrush.GradientStops.Add(gStopT);
//            rgBrush.GradientStops.Add(gStop1);
//            toEll.OpacityMask = rgBrush;
//            //跑动的点达到城市的圆时 控制点由不透明变为透明 color的a值设为0 动画时间为0
//            ColorAnimation ca = new ColorAnimation();
//            ca.To = Color.FromArgb(0, 0, 0, 0);
//            ca.Duration = new Duration(TimeSpan.FromSeconds(0));
//            ca.BeginTime = TimeSpan.FromSeconds(pointTime);
//            ca.FillBehavior = FillBehavior.HoldEnd;
//            Storyboard.SetTarget(ca, toEll);
//            Storyboard.SetTargetProperty(ca, new PropertyPath("(Ellipse.OpacityMask).(GradientBrush.GradientStops)[1].(GradientStop.Color)"));
//            sb.Children.Add(ca);
//            //点达到城市的圆时 呈现放射状动画 控制点的off值走0-1 透明部分向外放射
//            DoubleAnimation eda = new DoubleAnimation();
//            eda.To = 1;
//            eda.Duration = new Duration(TimeSpan.FromSeconds(2));
//            eda.RepeatBehavior = RepeatBehavior.Forever;
//            eda.BeginTime = TimeSpan.FromSeconds(particleTime);
//            Storyboard.SetTarget(eda, toEll);
//            Storyboard.SetTargetProperty(eda, new PropertyPath("(Ellipse.OpacityMask).(GradientBrush.GradientStops)[1].(GradientStop.Offset)"));
//            sb.Children.Add(eda);
//            #endregion

//            #region 运动轨迹
//            //找到渐变的起点和终点
//            Point startPoint = GetProvincialCapitalPoint(from);
//            Point endPoint = GetProvincialCapitalPoint(toItem.To);
//            Point start = new Point(0, 0);
//            Point end = new Point(1, 1);
//            if (startPoint.X > endPoint.X)
//            {
//                start.X = 1;
//                end.X = 0;
//            }
//            if (startPoint.Y > endPoint.Y)
//            {
//                start.Y = 1;
//                end.Y = 0;
//            }
//            LinearGradientBrush lgBrush = new LinearGradientBrush();
//            lgBrush.StartPoint = start;
//            lgBrush.EndPoint = end;
//            GradientStop lgStop0 = new GradientStop(Color.FromArgb(255, 0, 0, 0), 0);
//            GradientStop lgStop1 = new GradientStop(Color.FromArgb(0, 0, 0, 0), 0);
//            lgBrush.GradientStops.Add(lgStop0);
//            lgBrush.GradientStops.Add(lgStop1);
//            particlePath.OpacityMask = lgBrush;
//            //运动轨迹呈现
//            DoubleAnimation pda0 = new DoubleAnimation();
//            pda0.To = 1;
//            pda0.Duration = new Duration(TimeSpan.FromSeconds(particleTime));
//            pda0.FillBehavior = FillBehavior.HoldEnd;
//            Storyboard.SetTarget(pda0, particlePath);
//            Storyboard.SetTargetProperty(pda0, new PropertyPath("(Path.OpacityMask).(GradientBrush.GradientStops)[0].(GradientStop.Offset)"));
//            sb.Children.Add(pda0);
//            DoubleAnimation pda1 = new DoubleAnimation();
//            //pda1.From = 0.5; //此处解开注释 值设为0-1 会有不同的轨迹呈现效果
//            pda1.To = 1;
//            pda1.Duration = new Duration(TimeSpan.FromSeconds(particleTime));
//            pda1.FillBehavior = FillBehavior.HoldEnd;
//            Storyboard.SetTarget(pda1, particlePath);
//            Storyboard.SetTargetProperty(pda1, new PropertyPath("(Path.OpacityMask).(GradientBrush.GradientStops)[1].(GradientStop.Offset)"));
//            sb.Children.Add(pda1);
//            #endregion
//        }
//        #endregion
//#endregion
    }
}
