using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoVisualizer.Models
{
    public class NEOModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string NameLimited { get; set; }

        public double Magnitude { get; set; }

        public double DiameterMin { get; set; }
        public double DiameterMax { get; set; }

        public bool Hazardous { get; set; }

        public DateTime FirstObserved { get; set; }
        public DateTime LastObserved { get; set; }

    }
}
