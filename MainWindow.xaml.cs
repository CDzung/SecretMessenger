using Firebase.Auth;
using Firebase.Auth.UI;
using Firebase.Database;
using Firebase.Database.Query;
using SecretMessage.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SecretMessage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            FirebaseUI.Instance.Client.AuthStateChanged += AuthStateChanged;
        }
        public MainWindow(string type)
        {
            InitializeComponent();
            if (String.IsNullOrEmpty(type))
            FirebaseUI.Instance.Client.AuthStateChanged += AuthStateChanged;
            else
            {
                this.Frame.Navigate(new ProfilePage());
            }

        }
        private void AuthStateChanged(object sender, UserEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(async () =>
            {
                if (e.User == null)
                {
                    this.Frame.Navigate(new LoginPage());
                }
                else if ((this.Frame.Content == null || this.Frame.Content.GetType() != typeof(ChatPage)))
                {
                    var firebase = new FirebaseClient("https://secret-message-6a1d7-default-rtdb.firebaseio.com/");
                    var users = await firebase
                    .Child("users")
                    .OnceAsync<UserEntity>();

                    var user = users.Where(a => a.Object.Uid == e.User.Uid).FirstOrDefault();
                    if (user == null)
                    {
                        UserEntity newUser = new UserEntity()
                        {
                            Uid = e.User.Uid,
                            DisplayName = e.User.Info.DisplayName,
                            Email = e.User.Info.Email,
                            Provider = e.User.Credential.ProviderType.ToString(),
                            PhotoUrl = e.User.Info.PhotoUrl
                        };

                        await firebase.Child("users").PostAsync(newUser);
                    }

                    ChatPage chatPage = new ChatPage();
                    chatPage.Show();
                    this.Close();
                }
            });
        }
    }
}
