using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace InstantMessaging.MVVM.Model
{
    class ContactModel : INotifyPropertyChanged
    {
        public ContactModel(string username, string imageSource) {
            Username = username;
            this.imageSource = imageSource;
            Joined = DateTime.Now;
            IsActive = false;
            Messages = new ObservableCollection<MessageModel>
            {
                new MessageModel(username, "/Icons/user.png", "test message 1"),
                new MessageModel(username, "/Icons/user.png", "test message 2"),
                new MessageModel(username, "/Icons/user.png", "test message 3")
            };
            LastMessage = Messages.Last();
        }

        public ContactModel(string username, string firstName, string lastName, string email, DateTime birthdate, string imageSource)
        {
            Username = username;
            this.imageSource = imageSource;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Birthdate = birthdate;
            Joined = DateTime.Now;
            IsActive = false;
            Messages = new ObservableCollection<MessageModel>
            {
                new MessageModel(username, imageSource, "test message 1"),
                new MessageModel(username, imageSource, "test message 2"),
                new MessageModel(username, imageSource, "test message 3")
            };
            LastMessage = Messages.Last();
        }

        private string username;
        private string firstName;
        private string lastName;
        private string email;
        private DateTime birthdate;
        private string imageSource;
        private MessageModel lastMessage;
        private DateTime joined;
        private bool isActive;
        private ObservableCollection<MessageModel> messages;

        public string Username 
        { 
            get { return username; }
            set
            {
                if (value == "" || value.Trim().Length > 15)
                {
                    throw new ArgumentException("Value is either an empty string or too long");
                }
                username = value.Trim();
                NotifyPropertyChanged("Username");
            }
        }
        public string ImageSource 
        { 
            get { return imageSource; }
            set
            {
                if (File.Exists(value))
                {
                    imageSource = value;
                    NotifyPropertyChanged("ImageSource");
                }
                else
                {
                    throw new ArgumentException("File does not exist in the system");
                }
            }
        }
        public MessageModel LastMessage 
        {
            get { return lastMessage; }
            set
            {
                if (value != null)
                {
                    if (lastMessage != null)
                    {
                        if(lastMessage.Time < value.Time)
                        {
                            lastMessage = value;
                            NotifyPropertyChanged("LastMessage");
                        }
                    }
                    else
                    {
                        lastMessage = value;
                        NotifyPropertyChanged("LastMessage");
                    }
                }
                else
                {
                    throw new ArgumentException("Value was null");
                }
            }
        }
        public DateTime Joined
        {
            get { return joined; }
            private set
            {
                joined = value;
            }
        }

        public bool IsActive
        {
            get { return isActive; }
            set
            {
                if(IsActive != value)
                {
                    isActive = value;
                    NotifyPropertyChanged("IsActive");
                }
            }
        }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                if(FirstName != value && !string.IsNullOrEmpty(value))
                {
                    firstName = value;
                    NotifyPropertyChanged("FirstName");
                }
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                if(LastName != value && !string.IsNullOrEmpty(value))
                {
                    lastName = value;
                    NotifyPropertyChanged("LastName");
                }
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                if(Email != value && !string.IsNullOrEmpty(value))
                {
                    email = value;
                    NotifyPropertyChanged("Email");
                }
            }
        }

        public DateTime Birthdate
        {
            get { return birthdate; }
            set
            {
                if (Birthdate != value)
                {
                    birthdate = value;
                    NotifyPropertyChanged("Birthdate");
                }
            }
        }

        public ObservableCollection<MessageModel> Messages { get; set; }

        public void AddMessage(string message)
        {
            messages.Add(new MessageModel(this.username, this.imageSource, message));
            NotifyPropertyChanged("Messages");
        }

        public override string ToString()
        {
            return String.Format("Username: {0}\nJoined {1}", Username, Joined.ToString("dd.MM.yyyy HH:mm"));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        void NotifyPropertyChanged(string Name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Name));
            }
        }
    }
}
