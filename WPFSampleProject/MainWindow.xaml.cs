using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.Specialized;

namespace WPFSampleProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = GlobalVars.ViewModel;
            InitializeComponent();

            BadAuthLabel.Visibility = Visibility.Collapsed;

            Main.ColumnDefinitions.RemoveAt(0);
            Menu.Visibility = Visibility.Collapsed;
            AuthPage.Visibility = Visibility.Collapsed;
            Cursor = Cursors.AppStarting;

            this.Loaded += async (sender, e) => {
                await Task.Run(() => {
                    Thread.Sleep(1000);

                    Dispatcher.Invoke(() => {
                        this.Cursor = Cursors.Arrow;

                        var animation = new ThicknessAnimation {
                            From = new Thickness(0, -50, 0, 10),
                            To = new Thickness(0),
                            Duration = TimeSpan.FromSeconds(0.5),
                            FillBehavior = FillBehavior.HoldEnd
                        };

                        AuthPage.BeginAnimation(MarginProperty, animation);
                        AuthPage.Visibility = Visibility.Visible;
                    });
                });
            };

            GlobalVars.ViewModel.Data.CollectionChanged += (sender, e) => {
                if (e.Action == NotifyCollectionChangedAction.Add &&
                        EmptyJournalLabel.Visibility == Visibility.Visible)
                    EmptyJournalLabel.Visibility = Visibility.Collapsed;

                if (e.Action == NotifyCollectionChangedAction.Remove &&
                        !GlobalVars.ViewModel.Data.Any())
                    EmptyJournalLabel.Visibility = Visibility.Visible;
            };

            NewQueryBt.MouseDown += (sender, e) => 
                GlobalVars.ViewModel.MainPage = new Uri("NewQueryPage.xaml", UriKind.Relative);

            ExitBt.MouseDown += (sender, e) => this.Close();
        }

        #region Выбор запроса из списка

        bool selQueryflag = false;
        private void SelQueryMouseDown(object sender, MouseEventArgs e)
        {
            selQueryflag = true;
        }

        private void SelQueryMouseLeave(object sender, MouseEventArgs e)
        {
            selQueryflag = false;
        }

        private void SelQueryMouseUp(object sender, MouseEventArgs e)
        {
            if (!selQueryflag || delQueryFlag) return;

            var index = Journal.Items.IndexOf(((Grid)sender).DataContext);
            if (GlobalVars.ViewModel.Data.ElementAt(index).IsSelected) return;

            GlobalVars.ViewModel.Data.ToList().ForEach(x => x.IsSelected = false);
            GlobalVars.ViewModel.Data.ElementAt(index).IsSelected = true;
        }

        #endregion

        #region Удаление запроса

        bool delQueryFlag = false;
        private void DelQueryMouseDown(object sender, MouseEventArgs e)
        {
            delQueryFlag = true;
        }

        private void DelQueryMouseLeave(object sender, MouseEventArgs e)
        {
            delQueryFlag = false;
            selQueryflag = false;
        }

        private void DelQueryMouseEnter(object sender, MouseEventArgs e)
        {
            selQueryflag = false;
        }

        private void DelQueryMouseUp(object sender, MouseEventArgs e)
        {
            if (!delQueryFlag) return;

            MessageBox.Show($"{((_Journal)((Grid)sender).DataContext).Query.Replace("\r\n", " ")} will be deleted.");
            GlobalVars.ViewModel.Data.Remove((_Journal)((Grid)sender).DataContext);
        }

        #endregion
    }
}
