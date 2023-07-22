using Firebase.Database;
using SecretMessage.Entity;
using SecretMessage.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SecretMessage.MVVM.ViewModel
{
    public class MainViewModel
    {
        private object _syncLock = new Object();
        public ObservableCollection<MessageModel> Messages { get; set; }
        public ObservableCollection<ContactModel> Contacts { get; set; }
        public ICollectionView ContactCollection { get; set; }
        public ICollectionView MessageCollection { get; set; }
        public MainViewModel()
        {
            Messages = new ObservableCollection<MessageModel>();
            Contacts = new ObservableCollection<ContactModel>();
            ContactCollection = CollectionViewSource.GetDefaultView(Contacts);
            MessageCollection = CollectionViewSource.GetDefaultView(Messages);
            BindingOperations.EnableCollectionSynchronization(Contacts, _syncLock);
            BindingOperations.EnableCollectionSynchronization(Messages, _syncLock);

            var firebase = new FirebaseClient("https://secret-message-6a1d7-default-rtdb.firebaseio.com/");
            var users = firebase
            .Child("users")
            .AsObservable<UserEntity>();
            
            users.Subscribe(d =>
            {
                if (d.EventType == Firebase.Database.Streaming.FirebaseEventType.InsertOrUpdate)
                {
                    var user = d.Object;
                    var contact = Contacts.FirstOrDefault(c => c.UID == user.Uid);
                    if (contact == null)
                    {
                        Contacts.Add(new ContactModel
                        {
                            UID = user.Uid,
                            Username = user.DisplayName,
                            ImageSource = user.PhotoUrl,
                            Messages = new ObservableCollection<MessageModel>()
                        });
                    } 
                    else
                    {
                        contact.Username = user.DisplayName;
                        contact.ImageSource = user.PhotoUrl;
                        contact.UID = user.Uid;
                        contact.Messages = new ObservableCollection<MessageModel>();

                    }
                } 
                else if (d.EventType == Firebase.Database.Streaming.FirebaseEventType.Delete)
                {
                    var user = d.Object;
                    var contact = Contacts.FirstOrDefault(c => c.UID == user.Uid);
                    if(contact is not null)
                    {
                        Contacts.Remove(contact);
                    }
                }
            });

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
        }
    }
}
