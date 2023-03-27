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
        public RelayCommand<int> GetPage { get; set; }

        private int currentPage = 0;
        public int CurrentPage
        {
            get
            {
                return currentPage;
            }
            set
            {
                currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
                OnPropertyChanged(nameof(NextPage));
                OnPropertyChanged(nameof(PreviousPage));
            }
        }
        public int NextPage => CurrentPage + 1;
        public int PreviousPage
        {
            get
            {
                if (CurrentPage - 1 < 0)
                    return 0;
                return CurrentPage - 1;
            }
        }
        private bool loading;
        public bool Loading { 
            get => loading;
            private set
            {
                loading = value;
                OnPropertyChanged(nameof(Loading));
            }
        }

        public NEOvm()
        {
            GetPage = new RelayCommand<int>(GetNeos);
        }

        public ObservableCollection<NEOModel> neoList = new ObservableCollection<NEOModel>();

        public ObservableCollection<NEOModel> NeoList
        {
            get { return neoList; }
            set
            {
                neoList = value;
                OnPropertyChanged(nameof(NeoList));
            }
        }

        public async void GetNeos(int page)
        {
            Loading = true;
            NeoList = new ObservableCollection<NEOModel>(await NASA_API.NasaNeoApi.GetNEOsAsync(page));
            Loading = false;
            CurrentPage = page;
        }
    }
}
