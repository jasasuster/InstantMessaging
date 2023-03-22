using InstantMessaging.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InstantMessaging
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<ContactModel> Contacts = new ObservableCollection<ContactModel>();
        ContactModel? SelectedContact = null;

        public MainWindow()
        {
            InitializeComponent();

            Contacts.Add(new ContactModel("user1", "user.png"));
            Contacts.Add(new ContactModel("user2", "user.png"));
            Contacts.Add(new ContactModel("user3", "user.png"));

            LV_Contacts.ItemsSource = Contacts;
            CurrentlySelected.DataContext = SelectedContact;
        }
    }
}
