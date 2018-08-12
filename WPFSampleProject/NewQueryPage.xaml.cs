using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFSampleProject
{
    /// <summary>
    /// Логика взаимодействия для NewQueryPage.xaml
    /// </summary>
    public partial class NewQueryPage : Page
    {
        public NewQueryPage()
        {
            this.DataContext = GlobalVars.ViewModel;
            InitializeComponent();

            DBList.SelectionChanged += (sender, e) => {
                QueryList.ItemsSource = null;
                QueryList.Items.Clear();
                QueryList.ItemsSource = DBList.SelectedValue as List<Query>;

                QueryList.Visibility = DBList.SelectedIndex == -1 ? Visibility.Collapsed : Visibility.Visible;
            };

            QueryList.SelectionChanged += (sender, e) => {
                QueryFields.Source = QueryList.SelectedValue as Uri;

                if (QueryFields.Source == null) {
                    SpecAccess.Visibility = Visibility.Collapsed;
                    ExecuteQueryBt.Visibility = Visibility.Collapsed;
                } else {
                    ExecuteQueryBt.Visibility = Visibility.Visible;

                    if (((Database)DBList.SelectedItem).SpecAccess)
                        SpecAccess.Visibility = Visibility.Visible;
                }
            };

            ExecuteQueryBt.Click += (sender, e) => {
                GlobalVars.ViewModel.Data.Insert(0, new _Journal {
                    Query = ((Database)DBList.SelectedItem).Name
                });

                GlobalVars.ViewModel.MainPage = null;
            };
        }
    }
}
