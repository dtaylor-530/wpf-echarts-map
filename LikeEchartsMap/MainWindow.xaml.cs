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

        //所有的动画 都在这个方法里 AddPointToStoryboard

        //#region 成员变量
        ///// <summary>
        ///// 跑动的点里的Path的Data
        ///// </summary>
        //private string m_PointData;
        ///// <summary>
        ///// 点的运动速度 单位距离/秒
        ///// </summary>
        //private double m_Speed;
        ///// <summary>
        ///// 运动轨迹弧线的正弦角度
        ///// </summary>
        //private double m_Angle;
        ///// <summary>
        ///// 数据源
        ///// </summary>
        //private IList<MapItem> m_Source;
        /// <summary>
        /// 故事版
        /// </summary>
        //private Storyboard m_Sb = new Storyboard();
        //#endregion


        /// <summary>
        /// loaded
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //参数设置部分


            //m_Source=DAL.MakeData();
            //InitRadioButton();
            //AddAnimation(m_Source[0]);


        }
//#endregion


        ///// <summary>
        ///// RadioButton点击
        ///// </summary>
        //void rbtn_Click(object sender, RoutedEventArgs e)
        //{
        //    RadioButton rbtn = (RadioButton)sender;
        //    if (rbtn.IsChecked == true)
        //    {
        //        foreach (MapItem item in m_Source)
        //        {
        //            if ((string)rbtn.Content == item.From.ToString())
        //            {
        //                AddAnimation(item);
        //                return;
        //            }
        //        }
        //    }
        //}



    }


}
