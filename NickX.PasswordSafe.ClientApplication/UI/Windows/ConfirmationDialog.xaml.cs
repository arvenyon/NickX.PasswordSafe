using System;
using System.Windows;

namespace NickX.InventoryManagement.ClientApplication.UI.Windows
{
    /// <summary>
    /// Interaktionslogik für ConfirmationDialog.xaml
    /// </summary>
    public partial class ConfirmationDialog : Window
    {
        private ConfirmationDialogAnswers confirmationDialogAnswer;

        public ConfirmationDialog()
        {
            InitializeComponent();
        }

        private void btnDiscard_Click(object sender, RoutedEventArgs e)
        {
            this.confirmationDialogAnswer = ConfirmationDialogAnswers.Discard;
            this.Close();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            this.confirmationDialogAnswer = ConfirmationDialogAnswers.Confirm;
            this.Close();
        }

        public static ConfirmationDialogAnswers GetConfirmation(String title, String message)
        {
            var dialog = new ConfirmationDialog();
            dialog.tbMessage.Text = message;
            dialog.tbTitle.Text = title;
            dialog.ShowDialog();
            return dialog.confirmationDialogAnswer;
        }
    }

    public enum ConfirmationDialogAnswers
    {
        Confirm,
        Discard
    }
}
