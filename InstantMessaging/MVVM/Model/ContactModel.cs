using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace InstantMessaging.MVVM.Model
{
    class ContactModel : INotifyPropertyChanged
    {
        public ContactModel(string username, string imageSource) {
            Username = username;
            ImageSource = imageSource;
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

        private string username;
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
                username = value;
                NotifyPropertyChanged("Username");
            }
        }
        public string ImageSource 
        { 
            get { return imageSource; }
            set
            {
                if(File.Exists(value))
                {
                    imageSource = value;
                    NotifyPropertyChanged("ImageSource");
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
                isActive = value;
                NotifyPropertyChanged("IsActive");
            }
        }

        public ObservableCollection<MessageModel> Messages 
        { 
            get { return messages; }
            set
            {
                messages = value;
                lastMessage = messages[messages.Count - 1];
            }
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
