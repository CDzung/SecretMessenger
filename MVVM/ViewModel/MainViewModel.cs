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
using Firebase.Database.Query;
using System.Windows.Threading;
using System.Windows;

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
            BindingOperations.EnableCollectionSynchronization(Contacts, _syncLock);


            var currentUser = FirebaseUI.Instance.Client.User;
            var currentUserEntity = new UserEntity();

            var firebase = new FirebaseClient("https://secret-message-6a1d7-default-rtdb.firebaseio.com/");

            #region LoadContactRealTime
            //var users = firebase
            //.Child("users")
            //.AsObservable<UserEntity>()
            //.Subscribe(d =>
            //{
            //    var user = d.Object;

            //    if (!user.Uid.Equals(currentUser.Uid))
            //    {
            //        var contact = Contacts.FirstOrDefault(c => c.UID == user.Uid);
            //        if (d.EventType == Firebase.Database.Streaming.FirebaseEventType.InsertOrUpdate)
            //        {
            //            if (contact == null)
            //            {
            //                Contacts.Add(new ContactModel
            //                {
            //                    UID = user.Uid,
            //                    Username = user.DisplayName,
            //                    ImageSource = user.PhotoUrl,
            //                    Messages = new ObservableCollection<MessageModel>()
            //                });
            //            }
            //            else
            //            {
            //                contact.Username = user.DisplayName;
            //                contact.ImageSource = user.PhotoUrl;
            //                contact.UID = user.Uid;
            //                contact.Messages = new ObservableCollection<MessageModel>();
            //            }
            //        }
            //        else if (d.EventType == Firebase.Database.Streaming.FirebaseEventType.Delete)
            //        {
            //            if (contact is not null)
            //            {
            //                Contacts.Remove(contact);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        currentUserEntity = user;
            //    }

            //});
            #endregion

            var users = firebase
                .Child("users")
                .OnceAsync<UserEntity>()
                .Result;

            foreach (var user in users)
            {
                if (!user.Object.Uid.Equals(currentUser.Uid))
                {
                    Contacts.Add(new ContactModel
                    {
                        UID = user.Object.Uid,
                        Username = user.Object.DisplayName,
                        ImageSource = user.Object.PhotoUrl,
                        Messages = new ObservableCollection<MessageModel>()
                    });
                }
                else
                {
                    currentUserEntity = user.Object;
                }
            }

            var messages = firebase
                        .Child("messages")
                        .AsObservable<MessageEntity>()
                        .Subscribe(m =>
                        {
                            if (m.EventType == Firebase.Database.Streaming.FirebaseEventType.InsertOrUpdate)
                            {
                                var message = m.Object;

                                var contact = Contacts.FirstOrDefault(c => c.UID == message.SenderUID && message.ReceiverUID == currentUser.Uid);

                                if(contact != null)
                                {
                                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                                    {
                                        contact.AddMessage(new MessageModel
                                        {
                                            SenderUID = message.SenderUID,
                                            ReceiverUID = message.ReceiverUID,
                                            Message = message.Message,
                                            Time = message.Time,
                                            ImageSource = contact.ImageSource,
                                            Username = contact.Username,
                                            UsernameColor = "#C6C6C6",
                                        });
                                    }));
                                }
                                else
                                {
                                    contact = Contacts.FirstOrDefault(c => c.UID == message.ReceiverUID && message.SenderUID == currentUser.Uid);
                                    if (contact != null)
                                    {
                                        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => 
                                        {
                                            contact.AddMessage(new MessageModel
                                            {
                                                SenderUID = message.SenderUID,
                                                ReceiverUID = message.ReceiverUID,
                                                Message = message.Message,
                                                Time = message.Time,
                                                ImageSource = currentUser.Info.PhotoUrl,
                                                Username = currentUser.Info.DisplayName,
                                                UsernameColor = "#FFA07A",
                                            });
                                        }));
                                    }
                                }

                               

                                
                            }
                        });

            SendCommand = new RelayCommand(o => 
            {
                var msg = firebase
                        .Child("messages")
                        .PostAsync(new MessageEntity
                        {
                            SenderUID = currentUser.Uid,
                            ReceiverUID = SelectedContact.UID,
                            Message = Message,
                            Time = DateTime.Now
                        });
                Message = "";
            });
            
        }
    }
}
