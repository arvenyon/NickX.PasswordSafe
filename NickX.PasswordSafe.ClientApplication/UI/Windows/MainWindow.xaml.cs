using Microsoft.Win32;
using NickX.InventoryManagement.ClientApplication.UI.Pages;
using NickX.InventoryManagement.Logic.Classes;
using NickX.InventoryManagement.Logic.Interfaces;
using NickX.InventoryManagement.Utils.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace NickX.InventoryManagement.ClientApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members
        private IDbContext dbContext;
        private Whistleblower logger; // TODO: <MainWindow> - Make use of this shit -> implement logging!
        private Dictionary<Button, UserControl> pageMapping;
        private bool isNavMenuExpanded = true;
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            InitializeDataContext();
            InitializeUiElements();
            InitializeLogger();
            FinalizeInitialization();
        }
        #endregion

        #region Initialization
        private void InitializeLogger()
        {
            var logDir = Path.Combine(Path.GetTempPath(), "NickX.PasswordSafe", "logs");
            this.logger = new Whistleblower(logDir);
        }

        private void InitializeDataContext()
        {
            var conString = string.Empty;
#if DEBUG
            // don't mind this shit, firewall @ work blocks all mongodb communication, so using different local @work and public @home
            conString = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\NickX.PasswordSafe", "CS_WORK", string.Empty).ToString();
            //conString = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\NickX.PasswordSafe", "CS_HOME", string.Empty).ToString();
#endif
            this.dbContext = new MongoContext(conString);
        }

        private void InitializeUiElements()
        {
            this.pageMapping = new Dictionary<Button, UserControl>();
            this.pageMapping.Add(navMenuButtonPasswords, new PagePasswords(this.dbContext));
        }

        private void FinalizeInitialization()
        {
            // default to page passwords
            this.navMenuButtonPasswords.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        #endregion

        #region PopupControl Handling
        private void btnPopupInfo_Click(object sender, RoutedEventArgs e)
        {
            // TODO: <MainWindow> - Implement correct info window or something similar
            var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            var dev = "NickX";
            MessageBox.Show("Version: " + version + Environment.NewLine + "Entwickler: " + dev,
                "Info",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void btnPopupLogout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        #endregion

        #region Window Drag & Move
        private void topBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        #endregion

        #region NavMenu Handling
        private void NavMenuButton_Click(object sender, RoutedEventArgs e)
        {
            var btnClicked = (Button)sender;
            btnClicked.Foreground = new SolidColorBrush(Colors.White);
            pageHolderControl.Children.Clear();
            pageHolderControl.Children.Add(pageMapping[btnClicked]);

            foreach (var child in navMenuButtonsContainer.Children)
            {
                if (child is Button)
                {
                    var btn = (Button)child;
                    if (btn != btnClicked)
                        btn.Foreground = new SolidColorBrush(Colors.Silver);
                }
            }
        }

        private void btnExpandCollapseMenu_Click(object sender, RoutedEventArgs e)
        {
            // set animation 
            var anim = (this.isNavMenuExpanded) ?
                new DoubleAnimation(200, 50, new Duration(TimeSpan.FromMilliseconds(250))) :
                new DoubleAnimation(50, 200, new Duration(TimeSpan.FromMilliseconds(250)));

            // set btn image
            var btn = (Button)sender;
            var img = (Image)btn.Content;
            var uri = (this.isNavMenuExpanded) ?
                new Uri(@"/Resources/Images/icons8_expand_arrow_127px.png", UriKind.Relative) :
                new Uri(@"/Resources/Images/icons8_collapse_arrow_127px.png", UriKind.Relative);
            img.Source = new BitmapImage(uri);

            // prepare navmenu button properties
            foreach (var navMenuButton in pageMapping.Keys)
            {
                var btnGrid = (Grid)navMenuButton.Content;
                var btnTextBlock = (TextBlock)btnGrid.Children[1];
                var padding = (this.isNavMenuExpanded) ? new Thickness(0) : new Thickness(0, 0, 50, 0);
                navMenuButton.Padding = padding;
                btnTextBlock.Visibility = (this.isNavMenuExpanded) ? Visibility.Collapsed : Visibility.Visible;
            }

            // execute animation
            navMenu.BeginAnimation(WidthProperty, anim);
            this.isNavMenuExpanded = !this.isNavMenuExpanded;
        }
        #endregion
    }
}
