using LikeEcharts.Model;
using Reactive.Bindings;
using System;

namespace LikeEcharts.ViewModel
{

    public class MapItemViewModel
    {

        public ReactiveProperty<bool> IsSelected { get; set; } = new ReactiveProperty<bool>();

        public  MapItem Model{ get;}

        public string Content =>             Model.From.ToString();

        public MapItemViewModel(MapItem mapItem)
        {
            Model = mapItem;

            IsSelected.Subscribe(_ =>
            {

            });
        }

    }
    
}
