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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LikeEchartsMap
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var m = new MainViewModel();
            InitializeComponent();
            this.DataContext = m;

            m.DependencyObjects.Subscribe(_ =>
            {
                if (_ != null)
                {
                    foreach (var x in _)
                        try
                        {
                            grid_Animation.Children.Add((UIElement)x);

                        }
                        catch (Exception e)
                        {
                        }

                    m.Storyboard.Begin(this);
                }
            });

        }





    }
}