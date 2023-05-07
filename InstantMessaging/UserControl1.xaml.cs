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

        //public delegate void SendMessageHandler(object sender, string senderName, string message);
        //public event SendMessageHandler? SendMessageEvent;

        //private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        //{
        //    string message = MessageTextBox.Text;
        //    string senderName = "Jasa";

        //    if (message != null )
        //    {
        //        SendMessageEvent?.Invoke(this, senderName, message);
        //    }

        //    MessageTextBox.Text = "";
        //}
    }
}
