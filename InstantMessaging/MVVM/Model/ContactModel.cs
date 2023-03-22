using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantMessaging.MVVM.Model
{
    class ContactModel
    {
        public ContactModel(string username, string imageSource) { 
            Username = username;
            ImageSource = imageSource;
            LastMessage = "";
            Messages = new ObservableCollection<MessageModel>();
        }

        private string Username { get; set; }
        private string ImageSource { get; set; }
        public string LastMessage { get; set; }
        private ObservableCollection<MessageModel> Messages { get; set; }



    }
}
