using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantMessaging.MVVM.Model
{
    class ContactModel : INotifyPropertyChanged
    {
        public ContactModel(string username, string imageSource) { 
            Username = username;
            ImageSource = "/Icons/" + imageSource;
            LastMessage = "test";
            joined = DateTime.Now;
            isActive = false;
            Messages = new ObservableCollection<MessageModel>();
        }

        private string username;
        private string imageSource;
        private string lastMessage;
        private DateTime joined;
        private bool isActive;

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
                imageSource = value;
                NotifyPropertyChanged("ImageSource");
            }
        }
        public string LastMessage 
        {
            get { return lastMessage; }
            set
            {
                lastMessage = value;
                NotifyPropertyChanged("LastMessage");
            }
        }
        public DateTime LastJoined
        {
            get { return joined;  }
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

        private ObservableCollection<MessageModel> Messages { get; set; }

        public override string ToString()
        {
            return String.Format("{0}", Username);
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
