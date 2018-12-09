using System;

namespace LikeEcharts.Model
{

    /// <summary>
    /// 地图到达城市数据项
    /// </summary>
    public class MapToItem
    {
        /// <summary>
        /// 到达城市
        /// </summary>
        public ProvincialCapital To { get; set; }
        /// <summary>
        /// 到达城市圆点的直径
        /// </summary>
        public double Diameter { get; set; }
        /// <summary>
        /// 提示的值
        /// </summary>
        public string Tip { get; set; }
    }
}
