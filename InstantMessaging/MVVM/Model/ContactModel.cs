﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace InstantMessaging.MVVM.Model
{
    public class ContactModel : INotifyPropertyChanged
    {
        public ContactModel(string username, string firstName, string lastName, string email, DateTime birthdate, string imageSource)
        {
            Username = username;
            this.imageSource = imageSource;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Birthdate = birthdate;
            Joined = DateTime.Now;
            Status = GetRandomStatus();
            Messages = new ObservableCollection<MessageModel>
            {
                new MessageModel(username, imageSource, "test message 1"),
                new MessageModel(username, imageSource, "test message 2"),
                new MessageModel(username, imageSource, "test message 3")
            };
            LastMessage = Messages.Last();
        }

        public ContactModel() { }

        private string username;
        private string firstName;
        private string lastName;
        private string email;
        private DateTime birthdate;
        private string imageSource;
        private MessageModel lastMessage;
        private DateTime joined;
        private string status;

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
                if (Messages != null)
                {
                    foreach (MessageModel message in Messages)
                    {
                        message.Username = username;
                    }
                }
            }
        }
        public string ImageSource 
        { 
            get { return imageSource; }
            set
            {
                if (!string.IsNullOrEmpty(value))
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
            set
            {
                joined = value;
            }
        }

        public string Status
        {
            get { return status; }
            set
            {
                if(!string.IsNullOrEmpty(value) && (value == "Online" || value == "Away" || value == "Offline"))
                {
                    status = value;
                    NotifyPropertyChanged("Status");
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

        public void AddMessage(string username, string imageSource, string message)
        {
            Messages.Add(new MessageModel(username, imageSource, message));
            NotifyPropertyChanged("Messages");
            LastMessage = Messages.Last();
        }

        public override string ToString()
        {
            return String.Format("Username: {0}\nJoined: {1}\nStatus: {2}", Username, Joined.ToString("dd.MM.yyyy HH:mm"), Status);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        void NotifyPropertyChanged(string Name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Name));
            }
        }

        public void ExportToXML(string filename)
        {
            XmlSerializer serializer = new(typeof(ContactModel));
            using var writer = new StreamWriter(filename);
            serializer.Serialize(writer, this);
        }

        public static ObservableCollection<ContactModel> ImportFromXML(string filename)
        {
            XmlSerializer serializer = new(typeof(ContactModel));
            using var reader = new StreamReader(filename);
            return (ObservableCollection<ContactModel>)serializer.Deserialize(reader);
        }    

        private string GetRandomStatus()
        {
            string[] statuses =
            {
                "Online",
                "Away",
                "Offline"
            };

            Random random = new Random();
            int index = random.Next(statuses.Length);
            return statuses[index];
        }
    }
}
