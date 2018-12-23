using System;
using System.Collections.Generic;

using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace PathWpf
{
    public class PathControl:Control
    {

        private Grid contentGrid;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            contentGrid = GetTemplateChild("PATH_ContentGrid") as Grid;
        }

        private static readonly string m_PointData = "M244.5,98.5 L273.25,93.75 C278.03113,96.916667 277.52785,100.08333 273.25,103.25 z";

        public object Range
        {
            get { return (object)GetValue(RangeProperty); }
            set { SetValue(RangeProperty, value); }
        }


        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Angle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof(double), typeof(PathControl), new PropertyMetadata(10d,AngleChanged));

        private static void AngleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as PathControl).AngleChanges.OnNext((double)e.NewValue);
        }
        ISubject<double> AngleChanges = new Subject<double>();


        public double Diameter
        {
            get { return (double)GetValue(DiameterProperty); }
            set { SetValue(DiameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Diameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DiameterProperty =
            DependencyProperty.Register("Diameter", typeof(double), typeof(PathControl), new PropertyMetadata(10d, DiameterChanged));

        private static void DiameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as PathControl).DiameterChanges.OnNext((double)e.NewValue);
        }

        ISubject<double> DiameterChanges = new Subject<double>();

        public double Speed
        {
            get { return (double)GetValue(SpeedProperty); }
            set { SetValue(SpeedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Speed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpeedProperty =
            DependencyProperty.Register("Speed", typeof(double), typeof(PathControl), new PropertyMetadata(10d, SpeedChanged));

        private static void SpeedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as PathControl).SpeedChanges.OnNext((double)e.NewValue);
        }


        ISubject<double> SpeedChanges = new Subject<double>();
        ISubject<Tuple<Point, Point>> PointChanges = new Subject<Tuple<Point, Point>>();

        public static readonly DependencyProperty RangeProperty =            DependencyProperty.Register("Range", typeof(object), typeof(PathControl), new PropertyMetadata(null,RangeChanged));


        private static void RangeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as PathControl).PointChanges.OnNext((Tuple<Point, Point>)e.NewValue); 

        }

        private static readonly Storyboard _storyboard = new Storyboard();


        public PathControl()
        {
            Uri resourceLocater = new Uri("/PathWpf;component/Themes/PathControl.xaml", System.UriKind.Relative);
            ResourceDictionary resourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
            Style = resourceDictionary["PathControlStyle"] as Style;



            PointChanges
                .CombineLatest(AngleChanges, DiameterChanges, SpeedChanges, (a, b, c, d) => PathEllipse.GetAnimation(a.Item1, a.Item2,c, b, _storyboard, d, m_PointData))
                .Subscribe(_ =>
                {
                    foreach (var x in _)
                        try
                        {
                            contentGrid.Children.Add((UIElement)x);
                        }
                        catch (Exception e)
                        {
                        }
                        _storyboard.Begin(this);
                });
        }


    }
}
