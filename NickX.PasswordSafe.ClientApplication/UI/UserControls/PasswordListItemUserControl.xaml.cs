using NickX.InventoryManagement.Models.Classes;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace NickX.InventoryManagement.ClientApplication.UI.UserControls
{
    /// <summary>
    /// Interaktionslogik für PasswordListItemUserControl.xaml
    /// </summary>
    public partial class PasswordListItemUserControl : UserControl
    {
        private readonly Dictionary<string, Color> colorMapping = new Dictionary<string, Color>();

        public bool IsSelected { get { return this.isSelected; } set { SetIsSelected(value); } }

        private bool isSelected;

        public PasswordListItemUserControl(Password password)
        {
            InitializeComponent();
            InitColors();
            this.DataContext = password;

            if (!string.IsNullOrWhiteSpace(password.DisplayColor))
                this.colorBorder.Background = new SolidColorBrush(colorMapping[password.DisplayColor]);

        }

        void InitColors()
        {
            colorMapping.Add("Rot", Colors.LightCoral);
            colorMapping.Add("Blau", Colors.SlateBlue);
            colorMapping.Add("Grün", Colors.LimeGreen);
            colorMapping.Add("Orange", Colors.OrangeRed);
            colorMapping.Add("Grau", Colors.LightSlateGray);
            colorMapping.Add("Weiss", Colors.Ivory);
            colorMapping.Add("Lila", Colors.MediumOrchid);
            colorMapping.Add("Gelb", Colors.Gold);
        }

        private void UserControl_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!IsSelected)
                layoutRoot.Background = new SolidColorBrush(Color.FromRgb(62, 60, 61));
        }

        private void UserControl_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!IsSelected)
                layoutRoot.Background = new SolidColorBrush(Color.FromRgb(42, 40, 41));
        }

        private void SetIsSelected(bool value)
        {
            if (value)
                layoutRoot.Background = new SolidColorBrush(Color.FromRgb(62, 60, 61));
            else
                layoutRoot.Background = new SolidColorBrush(Color.FromRgb(42, 40, 41));

            this.isSelected = value;
        }
    }
}
