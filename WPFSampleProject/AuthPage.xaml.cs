using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFSampleProject
{
    enum AuthResult {
        Succsess,
        BadUser,
        BadPassword,
        BadLogIn,
        BadConnection
    }

    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        static readonly double AuthBtOpacity = 0.2;
        static bool MouseDownFlag = false;

        static readonly string BadUser = "[ Please enter username. ]";
        static readonly string BadPassword = "[ Please enter password. ]";
        static readonly string BadAuth = "[ Incorrect username or password. ]";
        static readonly string BadConnection = "[ Server is not available. Please try again later. ]";

        MainWindow MainWindow = (MainWindow)Application.Current.MainWindow;

        public AuthPage()
        {
            InitializeComponent();

            Password.GotFocus += (sender, e) => {
                Password.Foreground = Brushes.White;
                _Password.Foreground = Brushes.White;
            };
            User.GotFocus += (sender, e) => {
                User.Foreground = Brushes.White;
                _User.Foreground = Brushes.White;
            };

            Password.LostFocus += (sender, e) => {
                Password.Foreground = Brushes.Gray;
                _Password.Foreground = Brushes.Gray;
            };
            User.LostFocus += (sender, e) => {
                User.Foreground = Brushes.Gray;
                _User.Foreground = Brushes.Gray;
            };

            Password.PreviewKeyDown += (sender, e) => { if (e.Key == Key.Enter) Auth(); };
            User.PreviewKeyDown += (sender, e) => { if (e.Key == Key.Enter) Auth(); };

            AuthBt.MouseEnter += (sender, e) => AuthBt.Opacity -= AuthBtOpacity;
            AuthBt.MouseLeave += (sender, e) => {
                AuthBt.Opacity += AuthBtOpacity;
                MouseDownFlag = false;
            };

            _Password.PreviewMouseDown += (sender, e) => Password.Focus();
            _User.PreviewMouseDown += (sender, e) => User.Focus();

            AuthBt.PreviewMouseLeftButtonDown += (sender, e) => {
                MouseDownFlag = true;

                Header.Focusable = true;
                Header.Focus();
                Header.Focusable = false;
            };
            AuthBt.MouseLeftButtonUp += (sender, e) => { if (MouseDownFlag) Auth(); };
        }

        private async void Auth() {
            switch (await _Auth()) {
                case AuthResult.Succsess:
                    MainWindow.Main.ColumnDefinitions.Insert(0, new ColumnDefinition { Width = new GridLength(245) });
                    MainWindow.Menu.Visibility = Visibility.Visible;

                    var animation = new ThicknessAnimation {                       
                        From = new Thickness(0),
                        To = new Thickness(0, -50, 0, 10),
                        Duration = TimeSpan.FromSeconds(0.5),
                        FillBehavior = FillBehavior.HoldEnd
                    };

                    animation.Completed += (_sender, _e) => {
                        this.IsEnabled = true;
                        MainWindow.AuthPage.Visibility = Visibility.Collapsed;
                    };

                    MainWindow.AuthPage.BeginAnimation(MarginProperty, animation);
                    break;

                case AuthResult.BadUser:
                    MainWindow.BadAuthLabel.Text = BadUser;
                    MainWindow.BadAuthLabel.Visibility = Visibility.Visible;
                    User.Focus();
                    break;

                case AuthResult.BadPassword:
                    MainWindow.BadAuthLabel.Text = BadPassword;
                    MainWindow.BadAuthLabel.Visibility = Visibility.Visible;
                    Password.Focus();
                    break;

                case AuthResult.BadLogIn:
                    MainWindow.BadAuthLabel.Text = BadAuth;
                    MainWindow.BadAuthLabel.Visibility = Visibility.Visible;
                    Password.Clear();
                    User.Focus();
                    User.SelectAll();
                    break;

                case AuthResult.BadConnection:
                    MainWindow.BadAuthLabel.Text = BadConnection;
                    MainWindow.BadAuthLabel.Visibility = Visibility.Visible;
                    break;
            }
        }

        private async Task<AuthResult> _Auth() {
            MainWindow.BadAuthLabel.Visibility = Visibility.Hidden;

            if (User.Text.Equals(string.Empty)) return AuthResult.BadUser;
            if (Password.Password.Equals(string.Empty)) return AuthResult.BadPassword;

            Header.Focusable = true;
            Header.Focus();
            Header.Focusable = false;

            this.IsEnabled = false;
            MainWindow.Cursor = Cursors.Wait;

            var result = await Task.Run(() => {
                Thread.Sleep(2000);

                var list = new List<string> {
                    "01",
                    "02",
                    "03",
                    "04",
                    "05S",
                    "06"
                };

                CreateDBList(list);

                return AuthResult.Succsess;
            });

            if (result != AuthResult.Succsess) this.IsEnabled = true;
            MainWindow.Cursor = Cursors.Arrow;

            return result;
        }

        private void CreateDBList(List<string> CodeList) {
            GlobalVars.ViewModel.DBList = new List<Database>();

            foreach (var code in CodeList) {
                switch (code) {
                    case "05S":
                        var database = GlobalVars.ViewModel.DBList.FirstOrDefault(x => x.Code == "05");

                        if (database != null) database.SpecAccess = true;
                        else GlobalVars.ViewModel.DBList.Add(new Database { Code = "05", SpecAccess = true });
                        break;

                    default:
                        if (GlobalVars.ViewModel.DBList.FirstOrDefault(x => x.Code == code) == null)
                            GlobalVars.ViewModel.DBList.Add(new Database { Code = code });
                        break;
                }
            }
        }
    }
}
