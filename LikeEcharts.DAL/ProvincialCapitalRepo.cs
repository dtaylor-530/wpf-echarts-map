using LikeEcharts.Model;
using System;

namespace LikeEcharts.DAL
{
    public static class ProvincialCapitalRepo
    {
        /// <summary>
        /// 获取省会,直辖市,特别行政区的坐标
        /// </summary>
        /// <param name="pc">城市</param>
        /// <returns>Point(Left,Top)</returns>
        public static XY GetProvincialCapitalPoint(ProvincialCapital city)
        {
            //Point point = new Point(0, 0)};
            switch (city)
            {
                case ProvincialCapital.北京:
                    return new XY
                    {
                        X = 625.71145,
                        Y = 265.20515
                    };
                    
                case ProvincialCapital.天津:
                   return new XY{X = 646.648895,
                   Y = 277.719215};
                    
                case ProvincialCapital.上海:
                   return new XY{X = 730.844,
                   Y = 425.208};
                    
                case ProvincialCapital.重庆:
                   return new XY{X = 487.123,
                   Y = 469.796};
                    
                case ProvincialCapital.石家庄:
                   return new XY{X = 605.527,
                   Y = 300.853};
                    
                case ProvincialCapital.太原:
                   return new XY{X = 575.685,
                   Y = 310.961};
                    
                case ProvincialCapital.沈阳:
                   return new XY{X = 725.375,
                   Y = 214.217};
                    
                case ProvincialCapital.长春:
                   return new XY{X = 742.702,
                   Y = 173.786};
                    
                case ProvincialCapital.哈尔滨:
                   return new XY{X = 751.847,
                   Y = 137.687};
                    
                case ProvincialCapital.南京:
                   return new XY{X = 691.682,
                   Y = 418.295};
                    
                case ProvincialCapital.杭州:
                   return new XY{X = 706.603,
                   Y = 446.211};
                    
                case ProvincialCapital.合肥:
                   return new XY{X = 661.841,
                   Y = 418.295};
                    
                case ProvincialCapital.福州:
                   return new XY{X = 706.603,
                   Y = 528.516};
                    
                case ProvincialCapital.南昌:
                   return new XY{X = 646.439,
                   Y = 486.16};
                    
                case ProvincialCapital.济南:
                   return new XY{X = 648.845,
                   Y = 327.807};
                    
                case ProvincialCapital.郑州:
                   return new XY{X = 596.382,
                   Y = 371.126};
                    
                case ProvincialCapital.武汉:
                   return new XY{X = 617.078,
                   Y = 451.506};
                    
                case ProvincialCapital.长沙:
                   return new XY{X = 593.975,
                   Y = 497.231};
                    
                case ProvincialCapital.广州:
                   return new XY{X = 611.303,
                   Y = 592.531};
                    
                case ProvincialCapital.海口:
                   return new XY{X = 553.545,
                   Y = 663.766};
                    
                case ProvincialCapital.成都:
                   return new XY{X = 445.73,
                   Y = 453.912};
                    
                case ProvincialCapital.贵阳:
                   return new XY{X = 492.417,
                   Y = 533.811};
                    
                case ProvincialCapital.昆明:
                   return new XY{X = 420.22,
                   Y = 563.171};
                    
                case ProvincialCapital.西安:
                   return new XY{X = 522.259,
                   Y = 384.121};
                    
                case ProvincialCapital.兰州:
                   return new XY{X = 442.842,
                   Y = 354.28};
                    
                case ProvincialCapital.西宁:
                   return new XY{X = 408.668,
                   Y = 340.321};
                    
                case ProvincialCapital.拉萨:
                   return new XY{X = 235.394,
                   Y = 452.949};
                    
                case ProvincialCapital.南宁:
                   return new XY{X = 520.815,
                   Y = 605.046};
                    
                case ProvincialCapital.呼和浩特:
                   return new XY{X = 557.877,
                   Y = 255.128};
                    
                case ProvincialCapital.银川:
                   return new XY{X = 479.422,
                   Y = 299.891};
                    
                case ProvincialCapital.乌鲁木齐:
                   return new XY{X = 220.474,
                   Y = 179.562};
                    
                case ProvincialCapital.香港:
                   return new XY{X = 623.817,
                   Y = 611.784};
                    
                case ProvincialCapital.澳门:
                   return new XY{X = 600.714,
                   Y = 615.634};
                    
                case ProvincialCapital.台北:
                   return new XY{X = 747.515,
                   Y = 545.844};
                default:
                    return default(XY);
            }

        }
    }
}
