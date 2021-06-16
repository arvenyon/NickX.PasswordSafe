using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace NickX.InventoryManagement.ClientApplication.UI.UserControls
{
    /// <summary>
    /// Interaktionslogik für PasswordDetail.xaml
    /// </summary>
    public partial class PasswordDetailsUserControl : UserControl
    {
        public bool DisableControls { get { return this.disableControls; } set { ApplyDisableControls(value); } }
        private readonly bool disableControls = true;

        public PasswordDetailsUserControl()
        {
            InitializeComponent();
        }

        private void ApplyDisableControls(bool value)
        {
            var op = (value) ? 0f : 0.2f;
            foreach (var element in controlHolderControl.Children)
            {
                if (element is Control)
                {
                    var ctrl = (Control)element;
                    HintAssist.SetHintOpacity(ctrl, op);

                    if (element != tbDateCreate && element != btnCopyPasswordToClipboard)
                        ((Control)element).IsEnabled = !value;
                }
            }
            HintAssist.SetHintOpacity(tbKey, op);
            tbKey.IsEnabled = !value;
        }

        private void tbKey_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void btnCopyPasswordToClipboard_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText(tbKey.Text);
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
    }
}
