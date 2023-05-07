﻿using InstantMessaging.MVVM.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.ExceptionServices;
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

        public RelayCommand ContactDoubleClickoCommand { get; }
        public RelayCommand AddNewContactCommand { get; }
        public RelayCommand RemoveContactCommand { get; }
        public RelayCommand EditContactCommand { get; }
        public RelayCommand OpenSettingsCommand { get; }

        public ViewModel()
        {
            Contacts = new ObservableCollection<ContactModel>
            {
                new ContactModel("user1", "/Icons/user.png"),
                new ContactModel("user2", "/Icons/user.png"),
                new ContactModel("user3", "/Icons/user.png")
            };

            ContactDoubleClickoCommand = new RelayCommand(ContactDoubleCLick);
            AddNewContactCommand = new RelayCommand(OpenAddContactWindow);
            RemoveContactCommand = new RelayCommand(RemoveContact);
            EditContactCommand = new RelayCommand(EditContact);
            OpenSettingsCommand = new RelayCommand(OpenSettings);
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
            Contacts.Add(new ContactModel("new user", "/Icons/user.png"));
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
    }
}
