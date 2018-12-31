using LikeEcharts.DAL;
using LikeEcharts.Model;
using PathWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;


namespace LikeEcharts.View
{
    public static class Animator
    {
       
        //static Storyboard _storyboard = new Storyboard();
        static Random rd = new Random();
        static ProvincialCapitalRepo repo = new ProvincialCapitalRepo();
        public static IEnumerable<DependencyObject[]> GetAnimations(MapItem item, double m_angle, Storyboard m_Sb, double m_Speed, string m_PointData)
        {
            
            foreach (MapToItem toItem in item.To)
            {
                if (item.From == toItem.To)
                    continue;
                Point startPoint = repo.GetProvincialCapitalPoint(item.From.ToString()).ToPoint();
                Point endPoint = repo.GetProvincialCapitalPoint(toItem.To.ToString()).ToPoint();

                var geometry = PathWpf.PathEllipse.GetParticlePathGeometry(startPoint, endPoint, m_angle);

                double l = geometry.GetLength();
                byte[] rgb = new byte[] { (byte)rd.Next(0, 255), (byte)rd.Next(0, 255), (byte)rd.Next(0, 255) };
                var path = PathWpf.PathEllipse.GetParticlePath(startPoint, endPoint, rgb, m_Sb, geometry, l);

                var c = PathWpf.PathEllipse.GetAnimation(startPoint, endPoint, toItem.Diameter, geometry, l, rgb, m_Sb, m_Speed, m_PointData).ToList();
                c.Add(path);

                yield return c.ToArray();
            }
        }

        private static Point ToPoint(this XY pcr)=>    new Point(pcr.X, pcr.Y);


    }


   
}

