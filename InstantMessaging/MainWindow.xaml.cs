using InstantMessaging.MVVM.ViewModel;
using System.Windows;

namespace InstantMessaging
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //ObservableCollection<ContactModel> Contacts = new ObservableCollection<ContactModel>();
        //ContactModel? SelectedContact = null;

        public MainWindow()
        {
            var vm = new ViewModel();
            DataContext = vm;

            InitializeComponent();

            //Contacts.Add(new ContactModel("user1", "/Icons/user.png"));
            //Contacts.Add(new ContactModel("user2", "/Icons/user.png"));
            //Contacts.Add(new ContactModel("user3", "/Icons/user.png"));

            //LV_Contacts.ItemsSource = Contacts;
            //CurrentlySelected.DataContext = SelectedContact;
        }

        void LV_Contacts_DoubleClick(object sender, RoutedEventArgs e)
        {
            //if(SelectedContact != null)
            //{
            //    string messageBoxText = SelectedContact.ToString();
            //    string caption = "Info";
            //    MessageBoxButton button = MessageBoxButton.OK;
            //    MessageBoxImage icon = MessageBoxImage.Information;
            //    MessageBoxResult result;

            //    result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            //}
        }

        void Menu_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
