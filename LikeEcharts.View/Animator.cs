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
    public class Animator
    {
        public static IEnumerable<DependencyObject[]> GetAnimations(MapItem item, double m_angle, Storyboard m_Sb, double m_Speed, string m_PointData)
        {
            foreach (MapToItem toItem in item.To)
            {
                if (item.From == toItem.To)
                    continue;
                Point startPoint = ProvincialCapitalRepo.GetProvincialCapitalPoint(item.From).ToPoint();
                Point endPoint = ProvincialCapitalRepo.GetProvincialCapitalPoint(toItem.To).ToPoint();

                yield return PathWpf.PathEllipse.GetAnimation(startPoint, endPoint, toItem.Diameter,m_angle, m_Sb, m_Speed, m_PointData);
            }
        }


    }

}

