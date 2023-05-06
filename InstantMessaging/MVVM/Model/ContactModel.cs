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
                new MessageModel(username, imageSource, "test message 1"),
                new MessageModel(username, imageSource, "test message 2"),
                new MessageModel(username, imageSource, "test message 3")
            };
            LastMessage = Messages.Last();
        }

        private string username;
        private string imageSource;
        private MessageModel lastMessage;
        private DateTime joined;
        private bool isActive;

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

        public ObservableCollection<MessageModel> Messages { get; set; }

        public void AddMessage(string username, string imageSource, string message)
        {
            Messages.Add(new MessageModel(username, imageSource, message));
            NotifyPropertyChanged("Messages");
            LastMessage = Messages.Last();
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
