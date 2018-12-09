using LikeEcharts.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikeEcharts.DAL
{
    public class DataFactory
    {

        #region 做数据
        /// <summary>
        /// 做数据
        /// </summary>
        public static  MapItem[] MakeData()
        {
            Random rd = new Random();
            MapToItem[] toList = new[] {
           new MapToItem() { To = ProvincialCapital.成都, Diameter = rd.Next(10, 51) },
           new MapToItem() { To = ProvincialCapital.西宁, Diameter = rd.Next(10, 51) },
           new MapToItem() { To = ProvincialCapital.哈尔滨, Diameter = rd.Next(10, 51) },
           new MapToItem() { To = ProvincialCapital.海口, Diameter = rd.Next(10, 51), Tip = "美丽的大海" },
           new MapToItem() { To = ProvincialCapital.呼和浩特, Diameter = rd.Next(10, 51) },
           new MapToItem() { To = ProvincialCapital.重庆, Diameter = 50, Tip = "山鸡大神在这" },
           new MapToItem() { To = ProvincialCapital.台北, Diameter = rd.Next(10, 51) },
           new MapToItem() { To = ProvincialCapital.乌鲁木齐, Diameter = rd.Next(10, 51) },
           new MapToItem() { To = ProvincialCapital.广州, Diameter = rd.Next(10, 51) },
           new MapToItem() { To = ProvincialCapital.上海, Diameter = 50, Tip = "雷叔的地盘!" },
            };

            return new[] {
                new MapItem() { From = ProvincialCapital.北京, To = toList },
                new MapItem() { From = ProvincialCapital.西安, To = toList },
                new MapItem() { From = ProvincialCapital.拉萨, To = toList } };

        }
        #endregion
    }
}
