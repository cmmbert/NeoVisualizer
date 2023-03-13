using CommunityToolkit.Mvvm.ComponentModel;
using NeoVisualizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoVisualizer.Viewmodels
{
    public class NEOvm : ObservableObject
    {
        public List<NEOModel> NeoList { get; set; } = new List<NEOModel>() {
            new NEOModel() {
                NameLimited = "test",
                Name = "test 65151",
                Id = 61565165,
                Magnitude = 15,
                Hazardous = true,
                FirstObserved = DateTime.Now,
                LastObserved = DateTime.Now,
                DiameterMin = 15,
                DiameterMax = 50,
            },
            new NEOModel() {
                NameLimited = "test2",
                Name = "test 65151",
                Id = 61565165,
                Magnitude = 15,
                Hazardous = false,
                FirstObserved = DateTime.Now,
                LastObserved = DateTime.Now,
                DiameterMin = 75,
                DiameterMax = 150,
            }};
    }
}
