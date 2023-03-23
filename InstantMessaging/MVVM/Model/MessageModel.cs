using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantMessaging.MVVM.Model
{
    class MessageModel
    {
        public MessageModel(string username, string imageSource, string message) { 
            Username = username;
            ImageSource = imageSource;
            Message = message;
            Time = DateTime.Now;
        }

        private string username;
        private string imageSource;
        private string message;
        private DateTime time;

        public string Username { get; private set; }
        public string ImageSource { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }

        public string TimeToString
        {
            get { return time.ToString("ddd d MMM h:mm tt"); }
        }
    }
}
