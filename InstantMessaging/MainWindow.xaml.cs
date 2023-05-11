using InstantMessaging.MVVM.Model;
using InstantMessaging.MVVM.ViewModel;
using System.Windows;

namespace InstantMessaging
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            var vm = new ViewModel();
            DataContext = vm;

            InitializeComponent();
        }

        void LV_Contacts_DoubleClick(object sender, RoutedEventArgs e)
        {
            ContactModel selectedContact = (ContactModel)LV_Contacts.SelectedItem;
            if (selectedContact != null)
            {
                string messageBoxText = selectedContact.ToString();
                string caption = "Info";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBoxResult result;

                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            }
        }

        void Menu_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LV_Contacts_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
