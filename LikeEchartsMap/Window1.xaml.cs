using LikeEcharts.DAL;
using LikeEcharts.Model;
using LikeEcharts.View;
using LikeEcharts.ViewModel;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LikeEchartsMap
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {


        public Window1()
        {
            InitializeComponent();
            this.Loaded += Window1_Loaded;
            grid.DataContext = this;



        }

        private void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            var mapitems = LikeEcharts.DAL.DataFactory.MakeData().Select(_ => new MapItemViewModel(_)).ToArray();
            //foreach (var mapitem in mapitems)
            //{
            //    foreach (MapToItem toItem in mapitem.Model.To)
            //    {

            //        if (mapitem.Model.From == toItem.To)
            //            continue;
            //        Point startPoint = ProvincialCapitalRepo.GetProvincialCapitalPoint(mapitem.Model.From).ToPoint();
            //        Point endPoint = ProvincialCapitalRepo.GetProvincialCapitalPoint(toItem.To).ToPoint();
            //        Range.Range = Tuple.Create(startPoint, endPoint);
            //    }
            //}
            Range.Range = Tuple.Create(new Point(10, 10), new Point(100, 100));

        }
       
    }
}
