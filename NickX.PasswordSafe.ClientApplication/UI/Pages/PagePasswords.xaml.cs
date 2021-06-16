using NickX.InventoryManagement.ClientApplication.UI.UserControls;
using NickX.InventoryManagement.ClientApplication.UI.Windows;
using NickX.InventoryManagement.Logic.Interfaces;
using NickX.InventoryManagement.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace NickX.InventoryManagement.ClientApplication.UI.Pages
{
    // TODO: <PagePasswords> - this code is a fucking heavy mess -> code clean up
    public partial class PagePasswords : UserControl
    {
        #region Public Properties
        public ICollection<Password> PasswordList { get { return this.passwordList; } set { SetPasswordList(value); } }
        public DetailPanelStates DetailPanelState { get { return this.detailPanelState; } private set { SetDetailPanelState(value); } }
        #endregion

        #region Private Members
        private IDbContext dataContext;
        private ICollection<Password> passwordList;
        private DetailPanelStates detailPanelState;
        private PasswordDetailsUserControl detailPanel;
        private ListSortingTypes currentListSortingType = ListSortingTypes.DisplayName;
        private readonly Dictionary<ListSortingTypes, string> listSortingTypesDisplayNameMapping = new Dictionary<ListSortingTypes, string>();
        #endregion

        #region Constructor
        public PagePasswords(IDbContext dbContext)
        {
            InitializeComponent();
            InitializeUiElements();
            InitializeListSortingTypes();
            InitializeDataContext(dbContext);

            ResetToStateVisualizing();
            SetListSortingType(ListSortingTypes.DisplayName);
        }
        #endregion

        #region Initialization
        private void InitializeUiElements()
        {
            this.detailPanel = new PasswordDetailsUserControl();
            this.detailPanel.tbDisplayName.TextChanged += DetailPanel_tbDisplayName_TextChanged;
            this.detailPanelHolderControl.Children.Add(detailPanel);
        }

        private void DetailPanel_tbDisplayName_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            btnSave.IsEnabled = !string.IsNullOrWhiteSpace(tb.Text);
        }

        private void InitializeListSortingTypes()
        {
            listSortingTypesDisplayNameMapping.Add(ListSortingTypes.DisplayName, "Sortierung: Bezeichnung");
            listSortingTypesDisplayNameMapping.Add(ListSortingTypes.DisplayColor, "Sortierung: Farbe");
            listSortingTypesDisplayNameMapping.Add(ListSortingTypes.DateCreate, "Sortierung: Erfasst am");
        }

        private void InitializeDataContext(IDbContext dataContext)
        {
            this.dataContext = dataContext;
        }
        #endregion

        #region PasswordList Handling
        void SetPasswordList(ICollection<Password> passwordList)
        {
            if (passwordList == null)
                return;

            this.PasswordListControl.Children.Clear();
            foreach (var password in passwordList)
            {
                var uc = new PasswordListItemUserControl(password);
                uc.MouseLeftButtonDown += PasswordListItemUserControl_Click;
                this.PasswordListControl.Children.Add(uc);
            }
            this.passwordList = passwordList;
        }

        private void PasswordListItemUserControl_Click(object sender, MouseButtonEventArgs e)
        {
            var pwliuc = (PasswordListItemUserControl)sender;
            detailPanel.DataContext = (Password)pwliuc.DataContext;

            pwliuc.IsSelected = true;
            foreach (PasswordListItemUserControl found in this.PasswordListControl.Children)
            {
                if (found != pwliuc)
                {
                    found.IsSelected = false;
                }
            }
        }

        private void SetListSortingType(ListSortingTypes sortingType)
        {
            switch (sortingType)
            {
                case ListSortingTypes.DisplayName:
                    this.PasswordList = this.PasswordList.OrderBy(x => x.DisplayName).ToList();
                    break;
                case ListSortingTypes.DateCreate:
                    this.PasswordList = this.PasswordList.OrderBy(x => x.DateCreate).ToList();
                    break;
                case ListSortingTypes.DisplayColor:
                    this.PasswordList = this.PasswordList.OrderBy(x => x.DisplayColor).ToList();
                    break;
            }
            ttipBtnChangeSorting.Text = listSortingTypesDisplayNameMapping[this.currentListSortingType];
        }
        #endregion

        #region UI Elements Handling
        private void filterbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txt = ((TextBox)sender).Text;

            if (txt.Length > 3)
            {
                foreach (PasswordListItemUserControl pwliuc in PasswordListControl.Children)
                {
                    var pw = (Password)pwliuc.DataContext;
                    if (!pw.DisplayName.ToLower().Contains(txt.ToLower()))
                    {
                        pwliuc.Visibility = Visibility.Collapsed;
                    }
                }
            }
            else
            {
                foreach (PasswordListItemUserControl pwliuc in PasswordListControl.Children)
                {
                    pwliuc.Visibility = Visibility.Visible;
                }
            }
        }


        private void btnDiscard_Click(object sender, RoutedEventArgs e)
        {
            ResetToStateVisualizing();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // get password from detail panel
            var password = (Password)detailPanel.DataContext;

            // manage logic based on panel state
            if (DetailPanelState == DetailPanelStates.Adding)
            {
                password.Guid = Guid.NewGuid();
                password.DateCreate = DateTime.Now;
                dataContext.CreatePassword(password);
            }
            else if (DetailPanelState == DetailPanelStates.Editing)
            {
                dataContext.UpdatePassword(password);
            }

            // reset state & controls to origin values
            ResetToStateVisualizing();
        }

        private void btnChangeSorting_Click(object sender, RoutedEventArgs e)
        {
            var eCount = Enum.GetNames(typeof(ListSortingTypes)).Length;
            if ((int)this.currentListSortingType < eCount - 1)
                this.currentListSortingType++;
            else
                this.currentListSortingType = 0;
            SetListSortingType(this.currentListSortingType);
        }

        private void defaultButton_MouseEnter(object sender, MouseEventArgs e)
        {
            var btn = (Button)sender;
            var sp = (StackPanel)btn.Content;
            var img = (Image)sp.Children[0];
            var tb = (TextBlock)sp.Children[1];

            var anim = new DoubleAnimation(30, 110, new Duration(TimeSpan.FromMilliseconds(250)));
            btn.BeginAnimation(WidthProperty, anim);

            //btn.Width = Double.NaN;
            img.Margin = new Thickness(5, 0, 0, 0);
            tb.Visibility = Visibility.Visible;
        }

        private void defaultButton_MouseLeave(object sender, MouseEventArgs e)
        {
            var btn = (Button)sender;
            var sp = (StackPanel)btn.Content;
            var img = (Image)sp.Children[0];
            var tb = (TextBlock)sp.Children[1];

            var anim = new DoubleAnimation(110, 30, new Duration(TimeSpan.FromMilliseconds(250)));
            btn.BeginAnimation(WidthProperty, anim);

            //btn.Width = 30;
            img.Margin = new Thickness(0, 0, 0, 0);
            tb.Visibility = Visibility.Collapsed;
        }
        #endregion

        #region Detail Panel Handling
        private void SetDetailPanelState(DetailPanelStates detailPanelState)
        {
            if (detailPanelState == DetailPanelStates.Visualizing)
            {
                detailPanel.DisableControls = true;
            }
            else
            {
                detailPanel.DisableControls = false;
            }

            this.detailPanelState = detailPanelState;
        }

        void ResetToStateVisualizing()
        {
            this.DetailPanelState = DetailPanelStates.Visualizing;

            // reload password list and update ui element accordingly
            this.dataContext.ReloadPasswords();
            this.PasswordList = dataContext.Passwords;

            if (PasswordList.Count > 0)
            {
                PasswordListControl.Children[0].RaiseEvent(new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left)
                {
                    RoutedEvent = Mouse.MouseDownEvent,
                    Source = this,
                });
            }
            else
                detailPanel.DataContext = null;

            btnAddPassword.IsEnabled = btnDeletePassword.IsEnabled = btnEditPassword.IsEnabled = true;
            btnSave.Visibility = Visibility.Hidden;
            btnDiscard.Visibility = Visibility.Hidden;
            btnSave.IsEnabled = false;
        }
        #endregion

        #region Add, Edit & Delete Actions
        private void btnAddPassword_Click(object sender, RoutedEventArgs e)
        {
            foreach (var found in PasswordListControl.Children)
            {
                if (found is PasswordListItemUserControl)
                {
                    ((PasswordListItemUserControl)found).IsSelected = false;
                }
            }

            DetailPanelState = DetailPanelStates.Adding;
            detailPanel.DataContext = new Password();
            btnAddPassword.IsEnabled = btnDeletePassword.IsEnabled = btnEditPassword.IsEnabled = false;
            btnSave.Visibility = Visibility.Visible;
            btnDiscard.Visibility = Visibility.Visible;
            btnSave.IsEnabled = false;
        }

        private void btnEditPassword_Click(object sender, RoutedEventArgs e)
        {
            DetailPanelState = DetailPanelStates.Editing;
            btnAddPassword.IsEnabled = btnDeletePassword.IsEnabled = btnEditPassword.IsEnabled = false;
            btnSave.Visibility = Visibility.Visible;
            btnDiscard.Visibility = Visibility.Visible;
            btnSave.IsEnabled = true;
        }

        private void btnDeletePassword_Click(object sender, RoutedEventArgs e)
        {
            if (ConfirmationDialog.GetConfirmation("Eintrag löschen", "Dadurch wird der Eintrag permanent gelöscht. Dieser Vorgang lässt sich nicht rückgängig machen! Fortfahren?")
                == ConfirmationDialogAnswers.Confirm)
            {
                var pwd = (Password)detailPanel.DataContext;
                dataContext.DeletePassword(pwd);
                ResetToStateVisualizing();
            }
        }
        #endregion
    }

    public enum ListSortingTypes
    {
        DisplayName = 0,
        DisplayColor = 1,
        DateCreate = 2
    }

    public enum DetailPanelStates
    {
        Editing,
        Adding,
        Visualizing
    }
}
