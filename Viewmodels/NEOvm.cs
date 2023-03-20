using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NeoVisualizer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoVisualizer.Viewmodels
{
    public class NEOvm : ObservableObject
    {

        public NEOvm()
        {
            ImagePaths.Add("/Resources/Images/asteroid0.jpg");
            ImagePaths.Add("/Resources/Images/asteroid1.jpg");
            ImagePaths.Add("/Resources/Images/asteroid2.jpg");
            AddNewCommand = new RelayCommand(AddNew);

            NeoList.Add(new NEOModel()
            {
                NameLimited = "test",
                Name = "test 65151",
                Id = "61565165",
                Magnitude = 15,
                Hazardous = true,
                FirstObserved = DateTime.Now,
                LastObserved = DateTime.Now,
                DiameterMin = 15,
                DiameterMax = 50,
                ImagePath = ImagePaths[0]
            });
            NeoList.Add(new NEOModel()
            {
                NameLimited = "test2",
                Name = "test 65151",
                Id = "61565165",
                Magnitude = 15,
                Hazardous = false,
                FirstObserved = DateTime.Now,
                LastObserved = DateTime.Now,
                DiameterMin = 75,
                DiameterMax = 150,
                ImagePath = ImagePaths[1]
            });
        }

        public ObservableCollection<NEOModel> NeoList
        {
            get { return neoList; }
            set
            {
                neoList = value;
                OnPropertyChanged(nameof(NeoList));
            }
        }
        public ObservableCollection<NEOModel> neoList = new ObservableCollection<NEOModel>();


        
        public List<string> ImagePaths { get; set; } = new List<string>();

        public RelayCommand AddNewCommand { get; set; }


        public void AddNew()
        {
            NeoList = new ObservableCollection<NEOModel>(NASA_API.NasaNeoApi.GetNEOsAsync(01).Result);

        }
    }
}
