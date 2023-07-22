using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretMessage.MVVM.Model
{
    public class ContactModel : INotifyPropertyChanged
    {
        private string uID;
        private string username;
        private string imageSource;
        private ObservableCollection<MessageModel> messages;
        private string lastMessage;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string UID
        {
            get => uID;
            set
            {
                uID = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UID)));
            }
        }

        public string Username
        {
            get => username;
            set
            {
                username = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Username)));
            }
        }

        public string ImageSource
        {
            get => imageSource;
            set
            {
                imageSource = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImageSource)));
            }
        }

        public ObservableCollection<MessageModel> Messages
        {
            get => messages;
            set
            {
                messages = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Messages)));
            }
        }

        public string LastMessage
        {
            get => lastMessage;
            set
            {
                lastMessage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LastMessage)));
            }
        }
    }
}
