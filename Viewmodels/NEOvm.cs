using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NeoVisualizer.NASA_API;
using NeoVisualizer.Viewmodels;
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
        public RelayCommand SwitchDataSource { get; set; }

        public int RequestedPage
        {
            get { 
                return CurrentPage; }
            set { 
                GetNeos(value); 
            }
        }

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
                OnPropertyChanged(nameof(RequestedPage));
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

        private Source dataSource;
        public Source DataSource
        {
            get => dataSource;
            private set
            {
                dataSource = value;
                OnPropertyChanged(nameof(DataSource));
            }
        }
        public enum Source
        {
            Offline,
            Online
        }
        public NEOvm()
        {
            GetPage = new RelayCommand<int>(GetNeos);
            GetNeos(0);
            SwitchDataSource = new RelayCommand(CycleSources);
        }

        public ObservableCollection<NEODetail> neoList = new();

        public ObservableCollection<NEODetail> NeoList
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
            NeoList = new ObservableCollection<NEODetail>();
            switch (DataSource)
            {
                case Source.Offline:
                    NeoList = new ObservableCollection<NEODetail>(await OfflineRepository.GetNEOsAsync(page));
                    break;
                case Source.Online:
                    NeoList = new ObservableCollection<NEODetail>(await NASA_API.NasaNeoApi.GetNEOsAsync(page));
                    break;
                default:
                    break;
            }

            Loading = false;
            CurrentPage = page;
        }


        public void CycleSources()
        {
            int currentDataSourceIdx = (int)DataSource;
            ++currentDataSourceIdx;
            if (currentDataSourceIdx >= Enum.GetNames(typeof(Source)).Length)
                DataSource = 0;
            else
                DataSource = (Source)currentDataSourceIdx;

            GetNeos(CurrentPage);
        }
    }
}
