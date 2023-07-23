using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretMessage.MVVM.Model
{
    public class MessageModel : INotifyPropertyChanged
    {
        private string _SenderUID;
        private string _ReceiverUID; 
        private string _Username;
        private string _UsernameColor;
        private string _ImageSource;
        private string _Message;
        private DateTime _Time;
        private bool _IsNativeOrigin;
        private bool? _FirstMessage;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string SenderUID
        {
            get => _SenderUID;
            set
            {
                _SenderUID = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SenderUID)));
            }
        }

        public string ReceiverUID
        {
            get => _ReceiverUID;
            set
            {
                _ReceiverUID = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ReceiverUID)));
            }
        }

        public string Username
        {
            get => _Username;
            set
            {
                _Username = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Username)));
            }
        }

        public string UsernameColor
        {
            get => _UsernameColor;
            set
            {
                _UsernameColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UsernameColor)));
            }
        }

        public string ImageSource
        {
            get => _ImageSource;
            set
            {
                _ImageSource = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImageSource)));
            }
        }

        public string Message
        {
            get => _Message;
            set
            {
                _Message = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Message)));
            }
        }

        public DateTime Time
        {
            get => _Time;
            set
            {
                _Time = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Time)));
            }
        }

        public bool IsNativeOrigin
        {
            get => _IsNativeOrigin;
            set
            {
                _IsNativeOrigin = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsNativeOrigin)));
            }
        }

        public bool? FirstMessage
        {
            get => _FirstMessage;
            set
            {
                _FirstMessage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FirstMessage)));
            }
        }
    }
}
