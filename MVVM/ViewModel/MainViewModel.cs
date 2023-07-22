using SecretMessage.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretMessage.MVVM.ViewModel
{
    public class MainViewModel
    {
        public ObservableCollection<MessageModel> Messages { get; set; }
        public ObservableCollection<ContactModel> Contacts { get; set; }
        public MainViewModel()
        {
            Messages = new ObservableCollection<MessageModel>();
            Contacts = new ObservableCollection<ContactModel>();

            Messages.Add(new MessageModel
            {
                Username = "Khai",
                UsernameColor = "#409aff",
                ImageSource = "https://ps.w.org/user-avatar-reloaded/assets/icon-256x256.png?rev\u003d2540745",
                Message = "Test",
                Time = DateTime.Now,
                IsNativeOrigin = false,
                FirstMessage = true
            });

            Messages.Add(new MessageModel
            {
                Username = "Huu",
                UsernameColor = "#409aff",
                ImageSource = "https://ps.w.org/user-avatar-reloaded/assets/icon-256x256.png?rev\u003d2540745",
                Message = "Test",
                Time = DateTime.Now,
                IsNativeOrigin = false,
                FirstMessage = true
            });

            Messages.Add(new MessageModel
            {
                Username = "Dung",
                UsernameColor = "#409aff",
                ImageSource = "https://ps.w.org/user-avatar-reloaded/assets/icon-256x256.png?rev\u003d2540745",
                Message = "Test",
                Time = DateTime.Now,
                IsNativeOrigin = false,
                FirstMessage = false
            });

            for(int i=0; i < 4; i++)
            {
                Contacts.Add(new ContactModel
                {
                    Username = $"Contact {i}",
                    ImageSource = "https://ps.w.org/user-avatar-reloaded/assets/icon-256x256.png?rev\u003d2540745",
                    Messages = Messages
                });
            }
        }
    }
}
