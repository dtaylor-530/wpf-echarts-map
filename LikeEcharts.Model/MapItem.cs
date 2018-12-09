using System;
using System.Collections.Generic;
using System.Text;

namespace LikeEcharts.Model
{
    /// <summary>
    /// 地图数据项
    /// </summary>
    public class MapItem
    {
        /// <summary>
        /// 出发城市
        /// </summary>
        public ProvincialCapital From { get; set; }
        /// <summary>
        /// 到达城市
        /// </summary>
        public IList<MapToItem> To { get; set; }
    }
}
