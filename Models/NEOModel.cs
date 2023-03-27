using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoVisualizer.Models
{
    public class NEOModel : ObservableObject
    {
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

        public double DiameterMin { get; set; }
        public double DiameterMax { get; set; }

        public bool Hazardous { get; set; }

        public DateTime FirstObserved { get; set; }
        public DateTime LastObserved { get; set; }

    }
}
