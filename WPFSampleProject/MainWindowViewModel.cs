using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WPFSampleProject
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<_Journal> Data { get; set; } = new ObservableCollection<_Journal>();

        public List<Database> DBList { get; set; } = new List<Database>();

        Uri _MainPage;
        public Uri MainPage {
            get { return _MainPage; }
            set { _MainPage = value; OnPropertyChanged(nameof(MainPage)); }
        }

        Uri _DataFields;
        public Uri DataFields {
            get { return _DataFields; }
            set { _DataFields = value; OnPropertyChanged(nameof(DataFields)); }
        }

        bool _IsFocused = false;
        public bool IsFocused {
            get { return _IsFocused; }
            set { _IsFocused = value; OnPropertyChanged(nameof(IsFocused)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class _Journal : INotifyPropertyChanged
    {
        string dateFormat = "dd.MM.yyyy HH:mm";

        public int ID { get; set; }
        int status = 0;

        string _Query;
        public string Query {
            get { return _Query; }
            set { _Query = $"Query to {value}\r\nfrom {DateTime.Now.ToString(dateFormat)}"; mTrhead(); }
        }

        public string Status
        {
            get { return status == 1 ? "PERFOMED" : "EXECUTING"; }
        }

        public Brush Color
        {
            get
            {
                return status == 1 ? (Brush)new BrushConverter().ConvertFromString("#00FF00") :
                  (Brush)new BrushConverter().ConvertFromString("#AAAAAA");
            }
        }

        bool _IsSelected = false;
        public bool IsSelected {
            get { return _IsSelected; }
            set { _IsSelected = value; OnPropertyChanged(nameof(IsSelected)); }
        }

        void mTrhead() {
            Task.Run(() => {
                Thread.Sleep(10000);
                status = 1;
                OnPropertyChanged(nameof(Status));
                OnPropertyChanged(nameof(Color));
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) {
            if (PropertyChanged == null) return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
