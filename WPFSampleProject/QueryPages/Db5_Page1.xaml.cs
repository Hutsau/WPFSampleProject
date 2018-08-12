using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для Db5_Page1.xaml
    /// </summary>
    public partial class Db5_Page1 : Page
    {
        public Db5_Page1()
        {
            InitializeComponent();

            if (GlobalVars.ViewModel.DBList.Single(x => x.Code == "01").SpecAccess)
                SpecAccess.Visibility = Visibility.Visible;
            else
                SpecAccess.Visibility = Visibility.Collapsed;
        }
    }
}
