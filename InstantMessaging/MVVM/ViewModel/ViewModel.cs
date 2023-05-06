using InstantMessaging.MVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace InstantMessaging.MVVM.ViewModel
{
    class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<ContactModel> Contacts { get; set; }


        private ContactModel? selectedContact = null;
        public ContactModel? SelectedContact
        {
            get => selectedContact;
            set
            {
                selectedContact = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedContact)));
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Message)));
            }
        }

        string[] Replies =
        {
            "The voices won't stop, they just keep getting louder.",
            "I can't trust anyone, they're all out to get me.",
            "I feel like I'm trapped in my own mind and I can't escape.",
            "I see things that aren't really there, it's like a waking nightmare.",
            "I can't sleep, my thoughts won't let me.",
            "Why bother trying, everything is just going to end in failure anyway.",
            "I feel like I'm stuck in a never-ending cycle of pain and despair."
        };

        public RelayCommand ContactDoubleClickoCommand { get; }
        public RelayCommand AddNewContactCommand { get; }
        public RelayCommand RemoveContactCommand { get; }
        public RelayCommand EditContactCommand { get; }
        public RelayCommand SendMessageCommand { get; }

        public ViewModel()
        {
            Contacts = new ObservableCollection<ContactModel>
            {
                new ContactModel("user1", "/Images/user1.png"),
                new ContactModel("user2", "/Images/user2.png"),
                new ContactModel("user3", "/Images/user3.png")
            };

            Message = "";

            ContactDoubleClickoCommand = new RelayCommand(ContactDoubleCLick);
            AddNewContactCommand = new RelayCommand(AddNewContact);
            RemoveContactCommand = new RelayCommand(RemoveContact);
            EditContactCommand = new RelayCommand(EditContact);
            SendMessageCommand = new RelayCommand(SendMessage);
        }

        void ContactDoubleCLick(object parameter)
        {
            if (SelectedContact != null)
            {
                string messageBoxText = SelectedContact.ToString();
                string caption = "Info";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Information;

                MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            }
        }

        private void AddNewContact(object parameter)
        {
            Contacts.Add(new ContactModel("new user", "/Images/default_user.png"));
        }
        private void RemoveContact(object parameter)
        {
            if (SelectedContact != null)
            {
                Contacts.Remove(SelectedContact);
                SelectedContact = null;
            }
            else
            {
                ShowMessageBoxSelectContact();
            }
        }
        private void EditContact(object parameter)
        {
            if (SelectedContact != null)
            {
                SelectedContact.Username = "edited user";
            }
            else
            {
                ShowMessageBoxSelectContact();
            }
        }

        private void ShowMessageBoxSelectContact()
        {
            string messageBoxText = "Select a contact!";
            string caption = "Warning";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;

            MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
        }

        private void SendMessage(object parameter)
        {
            if (SelectedContact != null)
            {
                // add message
                SelectedContact.AddMessage("Jasa", "/Images/user4.png", Message);

                // random reply
                Random random = new Random();
                int index = random.Next(Replies.Length); // generate a random index within the bounds of the array
                string randomElement = Replies[index];
                SelectedContact.AddMessage(SelectedContact.Username, SelectedContact.ImageSource, randomElement);
            }

            Message = "";
        }

        private void OnSendMessage(object sender, string senderName, string message)
        {
            if (SelectedContact != null)
            {
                // add message
                SelectedContact.AddMessage(senderName, "/Images/user4.png", message);

                // random reply
                Random random = new Random();
                int index = random.Next(Replies.Length); // generate a random index within the bounds of the array
                string randomElement = Replies[index];
                SelectedContact.AddMessage(SelectedContact.Username, SelectedContact.ImageSource, randomElement);
            }

            Message = "";
        }
    }
}
