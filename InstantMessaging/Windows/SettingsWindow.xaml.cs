﻿using InstantMessaging.MVVM.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace InstantMessaging
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window, INotifyPropertyChanged
    {
        private ViewModel viewModel;
        public SettingsWindow(ViewModel vm)
        {
            InitializeComponent();

            // Set the data context to this window
            DataContext = this;

            viewModel = vm;

            // Initialize the settings fields
            Username = Settings.Default.Username;
            Email = Settings.Default.Email;
            FirstName = Settings.Default.FirstName;
            LastName = Settings.Default.LastName;
            Birthdate = Settings.Default.Birthdate;
            ImagePath = Settings.Default.Image;
            if(!string.IsNullOrEmpty(Settings.Default.Image))
            {
                Image = new BitmapImage(new Uri(Settings.Default.Image));
            }
            ErrorMessage = string.Empty;
            autoSaveToggleButton.IsChecked = Settings.Default.AutoSaveEnabled;
            intervalComboBox.SelectItemByTag(Settings.Default.AutoSaveInterval);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        void NotifyPropertyChanged(string Name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Name));
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                 _errorMessage = value;
                NotifyPropertyChanged("ErrorMessage");
            }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    NotifyPropertyChanged("Username");
                }
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    NotifyPropertyChanged("Email");
                }
            }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    NotifyPropertyChanged("FirstName");
                }
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    NotifyPropertyChanged("LastName");
                }
            }
        }

        private DateTime _birthdate;
        public DateTime Birthdate
        {
            get { return _birthdate; }
            set
            {
                if (_birthdate != value)
                {
                    _birthdate = value;
                    NotifyPropertyChanged("Birthdate");
                }
            }
        }

        private ImageSource _image;
        public ImageSource Image
        {
            get { return _image; }
            set
            {
                _image = value;
                ImageControl.Source = value;
            }
        }

        private string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                if (_imagePath != value)
                {
                    try
                    {
                        _imagePath = value;
                        if (value != null)
                        {
                            BitmapImage image = new BitmapImage(new Uri(value));
                            ImageControl.Source = image;
                        }
                    }
                    catch (Exception e)
                    {
                        ErrorMessage = "Image error: " + e.Message;
                    }
                }
            }
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png;";

            if (dialog.ShowDialog() == true)
            {
                ImagePath = dialog.FileName;
                BitmapImage image = new BitmapImage(new Uri(ImagePath));
                ImageControl.Source = image;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorMessage = string.Empty;

            if (string.IsNullOrEmpty(Username))
            {
                ErrorMessage += "Username is required\n";
            }
            if (string.IsNullOrEmpty(Email))
            {
                ErrorMessage += "Email is required\n";
            }
            if (string.IsNullOrEmpty(FirstName))
            {
                ErrorMessage += "FirstName is required\n";
            }
            if (string.IsNullOrEmpty(LastName))
            {
                ErrorMessage += "LastName is required\n";
            }
            if (string.IsNullOrEmpty(ImagePath))
            {
                ErrorMessage += "Image is required\n";
            }

            if (string.IsNullOrEmpty(ErrorMessage))
            {
                DialogResult = true;
                Close();
            }
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (autoSaveToggleButton.IsChecked == true && intervalComboBox.SelectedItem != null)
            {
                try
                {
                    int selectedInterval = int.Parse(((ComboBoxItem)intervalComboBox.SelectedItem).Tag.ToString());
                    viewModel.StartAutomaticSaving(selectedInterval);
                    Settings.Default.AutoSaveInterval = selectedInterval;
                } catch (Exception ex) { 
                    Console.Error.WriteLine(ex.ToString());
                }
                
            }
            else
            {
                viewModel.StopAutomaticSaving();
            }
            Settings.Default.AutoSaveEnabled = autoSaveToggleButton.IsChecked == true;
            Settings.Default.Save();
        }

        private void IntervalComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedTag = int.Parse(((ComboBoxItem)intervalComboBox.SelectedItem).Tag.ToString());
            if (Settings.Default.AutoSaveInterval != selectedTag) 
            {
                Settings.Default.AutoSaveInterval = int.Parse(((ComboBoxItem)intervalComboBox.SelectedItem).Tag.ToString());
                Settings.Default.Save();
            }
            ResetAutomaticSaving(selectedTag);
        }

        private void ResetAutomaticSaving(int selectedInterval)
        {
            viewModel.StopAutomaticSaving();
            viewModel.StartAutomaticSaving(selectedInterval);
        }
    }
}
