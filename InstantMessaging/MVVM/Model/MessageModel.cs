using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantMessaging.MVVM.Model
{
    class MessageModel
    {
        private string Username { get; set; }
        private string UsernameColor { get; set; }
        private string ImageSource { get; set; }
        private string Message { get; set; }
        private DateTime Time { get; set; }
        private bool IsMine { get; set; }
        private bool? FirstMessage { get; set; }
    }
}
