using LikeEcharts.Model;
using LikeEcharts.View;
using LikeEcharts.ViewModel;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace LikeEchartsMap
{
    public class MainViewModel
    {
        private static readonly double m_Angle = 15;
        private static readonly double m_Speed = 50;
        private static readonly string m_PointData = "M244.5,98.5 L273.25,93.75 C278.03113,96.916667 277.52785,100.08333 273.25,103.25 z";
        private static readonly Storyboard _storyboard = new Storyboard();

        public MapItemViewModel[] MapItems { get; } = LikeEcharts.DAL.DataFactory.MakeData().Select(_ => new MapItemViewModel(_)).ToArray();

        public ReactiveProperty<DependencyObject[]> DependencyObjects { get; } = new ReactiveProperty<DependencyObject[]>();

        public ReactiveProperty<double> Angle { get; set; } = new ReactiveProperty<double>(m_Angle);
        public ReactiveProperty<double> Speed { get; set; } = new ReactiveProperty<double>(m_Speed);
        public string PointData => m_PointData;

        public Storyboard Storyboard => _storyboard;

        public MainViewModel()
        {
            DependencyObjects = Observable.Create<DependencyObject[]>(
                observer => {
                    List<IDisposable> disposables = new List<IDisposable>();
                    foreach(var mapItem in MapItems)
                             disposables.Add(mapItem
                             .IsSelected
                             .CombineLatest(Speed, Angle, (isselected, speed, angle) => new { isselected, speed, angle })
                             .Where(_ => _.isselected)
                             .Subscribe(_ =>
                             {
                                 foreach (var x in Class1.AddAnimation(mapItem.Model, _.angle, _storyboard, _.speed, m_PointData))
                                     observer.OnNext(x);
                             }));
                    return disposables.AsLazyComposite();
                    }
             ).ToReactiveProperty();

        }


    }


    public static class DisposableHelper
    {
        public static IDisposable AsLazyComposite(this IEnumerable<IDisposable> sequence)
        {
            return System.Reactive.Disposables.Disposable.Create(() =>
            {
                foreach (var disposable in sequence)
                    disposable.Dispose();
            });
        }
    }
}
