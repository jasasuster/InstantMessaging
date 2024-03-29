﻿using System;
using System.ComponentModel;
using System.IO;

namespace InstantMessaging.MVVM.Model
{
    public class MessageModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        void NotifyPropertyChanged(string Name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Name));
            }
        }
        public MessageModel(string username, string imageSource, string message) { 
            Username = username;
            this.imageSource = imageSource;
            Message = message;
            Time = DateTime.Now;
        }

        public MessageModel() { }

        private string username;
        private string imageSource;
        private string message;
        private DateTime time;

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
                if (!string.IsNullOrEmpty(value))
                {
                    imageSource = value;
                }
                else
                {
                    throw new ArgumentException("File does not exist in the system");
                }
            }
        }
        public string Message 
        { 
            get { return message; }
            set 
            {
                if (value == "")
                {
                    throw new ArgumentException("Message value is an empty string");
                }
                message = value; 
            }
        }
        public DateTime Time 
        { 
            get { return time; }
            set
            {
                if (DateTime.Compare(value, DateTime.Now) > 0)
                {
                    throw new ArgumentException("DateTime value cannot be in the future");
                }
                time = value;
            }
        }

        public string TimeToString
        {
            get { return time.ToString("ddd d MMM h:mm tt"); }
        }
    }
}
