using InstantMessaging.MVVM.Model;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace InstantMessaging.MVVM.ViewModel
{
    class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        void NotifyPropertyChanged(string Name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Name));
            }
        }

        public ObservableCollection<ContactModel> Contacts { get; set; }

        private ContactModel? selectedContact = null;
        public ContactModel? SelectedContact
        {
            get => selectedContact;
            set
            {
                selectedContact = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedContact)));

                if(isEditWindowOpen)
                {
                    var selectedContact = SelectedContact;
                    if(selectedContact != null)
                    {
                        editContactWindow.OnSelectedContactChange(selectedContact);
                    }
                }
            }
        }
        private EditContactWindow editContactWindow;
        private bool isEditWindowOpen {  get; set; }

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
        public RelayCommand OpenSettingsCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand ImportCommand { get; }
        public RelayCommand SendMessageCommand { get; }

        public ViewModel()
        {
            Contacts = new ObservableCollection<ContactModel>
            {
                new ContactModel("user1", "User", "1", "user1@gmail.com", DateTime.Now, "./Icons/user.png"),
                new ContactModel("user2", "User", "2", "user2@gmail.com", DateTime.Now, "./Icons/user.png"),
                new ContactModel("user3", "User", "3", "user3@gmail.com", DateTime.Now, "./Icons/user.png")
            };

            Message = "";

            ContactDoubleClickoCommand = new RelayCommand(ContactDoubleCLick);
            AddNewContactCommand = new RelayCommand(OpenAddContactWindow);
            RemoveContactCommand = new RelayCommand(RemoveContact);
            EditContactCommand = new RelayCommand(OpenEditContactWindow);
            OpenSettingsCommand = new RelayCommand(OpenSettings);
            ExportCommand = new RelayCommand(Export);
            ImportCommand = new RelayCommand(Import);
            SendMessageCommand = new RelayCommand(SendMessage);

            isEditWindowOpen = false;
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

        private void OpenEditContactWindow(object parameter)
        {
            if (SelectedContact != null)
            {
                if(!isEditWindowOpen)
                {
                    editContactWindow = new()
                    {
                        Owner = App.Current.MainWindow,
                        Username = SelectedContact.Username,
                        Email = SelectedContact.Email,
                        FirstName = SelectedContact.FirstName,
                        LastName = SelectedContact.LastName,
                        Birthdate = SelectedContact.Birthdate,
                        ImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SelectedContact.ImageSource)
                    };

                    editContactWindow.Closed += (sender, e) =>
                    {
                        isEditWindowOpen = false;

                        if (editContactWindow.contactChanged)
                        {
                            SelectedContact.Username = editContactWindow.Username;
                            SelectedContact.Email = editContactWindow.Email;
                            SelectedContact.FirstName = editContactWindow.FirstName;
                            SelectedContact.LastName = editContactWindow.LastName;
                            SelectedContact.Birthdate = editContactWindow.Birthdate;
                            SelectedContact.ImageSource = editContactWindow.ImagePath;

                            NotifyPropertyChanged(nameof(SelectedContact));
                            NotifyPropertyChanged(nameof(Contacts));
                        }
                    };

                    editContactWindow.Show();
                    isEditWindowOpen = true;                    
                }
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


        private void OpenSettings(object parameter)
        {
            SettingsWindow settingsWindow = new();
            if (settingsWindow.ShowDialog() == true)
            {
                Settings.Default.Username = settingsWindow.Username;
                Settings.Default.Email = settingsWindow.Email;
                Settings.Default.FirstName = settingsWindow.FirstName;
                Settings.Default.LastName = settingsWindow.LastName;
                Settings.Default.Birthdate = settingsWindow.Birthdate;
                Settings.Default.Image = settingsWindow.ImagePath;
                Settings.Default.Save();
            }
        }

        private void OpenAddContactWindow(object parameter)
        {
            AddContactWindow addContactWindow = new();
            if (addContactWindow.ShowDialog() == true)
            {
                Contacts.Add(new ContactModel(
                    addContactWindow.Username,
                    addContactWindow.FirstName,
                    addContactWindow.LastName,
                    addContactWindow.Email,
                    addContactWindow.Birthdate,
                    addContactWindow.ImagePath
                ));
            }
        }

        private void Export(object parameter)
        {
            SaveFileDialog dialog = new();
            dialog.Filter = "Image Files (*.xml)|*.xml";

            if (dialog.ShowDialog() == true)
            {
                XmlSerializer serializer = new(typeof(ObservableCollection<ContactModel>));
                using var writer = new StreamWriter(dialog.FileName);
                serializer.Serialize(writer, Contacts);
            }
        }

        private void Import(object parameter)
        {
            OpenFileDialog dialog = new();
            dialog.Filter = "Image Files (*.xml)|*.xml";

            if (dialog.ShowDialog() == true)
            {
                XmlSerializer serializer = new(typeof(ObservableCollection<ContactModel>));
                using var reader = new StreamReader(dialog.FileName);
                Contacts = (ObservableCollection<ContactModel>)serializer.Deserialize(reader);
                NotifyPropertyChanged(nameof(Contacts));
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
