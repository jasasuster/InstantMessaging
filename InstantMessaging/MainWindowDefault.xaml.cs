using InstantMessaging.MVVM.Model;
using InstantMessaging.MVVM.ViewModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace InstantMessaging
{
    /// <summary>
    /// Interaction logic for MainWindowDedfault.xaml
    /// </summary>
    public partial class MainWindowDefault : Window
    {
        public MainWindowDefault()
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
