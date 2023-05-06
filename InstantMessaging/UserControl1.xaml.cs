using InstantMessaging.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace InstantMessaging
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            DataContext = App.Current.MainWindow.DataContext;
        }

        public delegate void SendMessageHandler(object sender, string senderName, string message);
        public event SendMessageHandler? SendMessageEvent;

        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the message text from the input field
            string message = MessageTextBox.Text;

            // Get the username from the DataContext
            string senderName = "Jasa";

            // Raise the SendMessageEvent with the necessary data
            SendMessageEvent?.Invoke(this, senderName, message);

            // Clear the message input field
            MessageTextBox.Text = "";
        }
    }
}
