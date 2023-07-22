using Firebase.Database;
using SecretMessage.Entity;
using SecretMessage.Core;
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
using Firebase.Auth.UI;

namespace SecretMessage.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private object _syncLock = new Object();
        public ObservableCollection<MessageModel> Messages { get; set; }
        public ObservableCollection<ContactModel> Contacts { get; set; }
        /* Commands */
        public RelayCommand SendCommand { get; set; }
        private ContactModel _selectedContact;
        public ContactModel SelectedContact 
        {
            get { return _selectedContact; }
            set { _selectedContact = value;
                OnPropertyChanged();
            }
        }
        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value;
                OnPropertyChanged();
            }

        }
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


            var currentUser = FirebaseUI.Instance.Client.User;

            var firebase = new FirebaseClient("https://secret-message-6a1d7-default-rtdb.firebaseio.com/");
            var users = firebase
            .Child("users")
            .AsObservable<UserEntity>();
            
            users.Subscribe(d =>
            {
                var user = d.Object;

                if(!user.Uid.Equals(currentUser.Uid))
                {
                    var contact = Contacts.FirstOrDefault(c => c.UID == user.Uid);
                    if (d.EventType == Firebase.Database.Streaming.FirebaseEventType.InsertOrUpdate)
                    {
                        if (contact == null)
                        {
                            Contacts.Add(new ContactModel
                            {
                                UID = user.Uid,
                                Username = user.DisplayName,
                                ImageSource = user.PhotoUrl,
                                Messages = Messages
                            });
                        }
                        else
                        {
                            contact.Username = user.DisplayName;
                            contact.ImageSource = user.PhotoUrl;
                            contact.UID = user.Uid;
                            contact.Messages = Messages;

                        }
                    }
                    else if (d.EventType == Firebase.Database.Streaming.FirebaseEventType.Delete)
                    {
                        if (contact is not null)
                        {
                            Contacts.Remove(contact);
                        }
                    }
                }
                
            });

            SendCommand = new RelayCommand(o => 
            {
                Messages.Add(new MessageModel
                {
                    Username="HaHa",
                    UsernameColor= "#FFA07A",
                    Message =Message,
                    Time = DateTime.Now,
                    ImageSource = currentUser.Info.PhotoUrl,
                    FirstMessage=false
                });
                Message = "";
            });
            
        }
    }
}
