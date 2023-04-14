using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoVisualizer.Viewmodels
{
    public class NEODetail : ObservableObject
    {
        public RelayCommand GoToDetail { get; set; }


        public NEODetail()
        {
            GoToDetail = new RelayCommand(NavigateToDetail);
        }

        private string? name;

        public string? Id { get; set; }
        public string? Name
        {
            get => name; set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string? NameLimited { get; set; }

        public string? ImagePath { get; set; }
        public double Magnitude { get; set; }

        public double DiameterMin { get; set; } = 100;
        public double DiameterMax { get; set; } = 200;

        public bool Hazardous { get; set; }

        public DateTime FirstObserved { get; set; }
        public DateTime LastObserved { get; set; }

        public List<CloseApproach> CloseApproaches { get; set; } = new List<CloseApproach>();
        public class CloseApproach
        {
            public DateTime ApproachData { get; set; }
            public float Velocity { get; set; }
            public double Distance { get; set; }
        }

        public void NavigateToDetail()
        {
            Detail detail = new Detail();
            detail.DataContext = this;
            detail.Show();
        }
    }
}
